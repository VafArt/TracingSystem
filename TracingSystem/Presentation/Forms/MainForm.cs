using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyInjection;
using OriginalCircuit.AltiumSharp;
using OriginalCircuit.AltiumSharp.Drawing;
using OriginalCircuit.AltiumSharp.Records;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Algorithms;
using TracingSystem.Application.Controls;
using TracingSystem.Application.Projects.Commands.CreateProject;
using TracingSystem.Application.Projects.Commands.SaveProject;
using TracingSystem.Application.Projects.Commands.UpdateProject;
using TracingSystem.Application.Projects.Queries.GetAllProjectNames;
using TracingSystem.Application.Services;
using TracingSystem.Domain;
using TracingSystem.Domain.Errors;
using TracingSystem.Domain.Shared;
using TracingSystem.Persistance;
using TracingSystem.Presentation.Forms;
using Pcb = TracingSystem.Domain.Pcb;

namespace TracingSystem
{
    public partial class MainForm : Form
    {
        private readonly ITracingSystemDbContext _dbContext;

        private readonly IMediator Mediator;

        private IProjectDataService _project;

        private PcbLibRenderer _pcbLibRenderer;

        private PcbLibReader _pcbLibReader;

        private int[,]? tracingResult;

        public MainForm()
        {
            DoubleBuffered = true;

            _dbContext = Program.ServiceProvider.GetRequiredService<ITracingSystemDbContext>();

            Mediator = Program.ServiceProvider.GetRequiredService<IMediator>();
            _project = Program.ServiceProvider.GetRequiredService<IProjectDataService>();
            _pcbLibRenderer = new PcbLibRenderer();
            _pcbLibReader = Program.ServiceProvider.GetRequiredService<PcbLibReader>();

            InitializeComponent();
            AlignStatusStrip();
            StartupConfiguration();
            _project.NameChanged += () => projectNameStatus.Text = $"Название проекта: {_project.Name}";
            _project.StateChanged += OnStateChanged;
            _project.ProjectChanged += OnProjectChanged;

            toolStrip.ImageScalingSize = DeviceDpi > 150 ? new Size(32, 32) : new Size(18, 18);
        }

        public void OnStateChanged()
        {
            switch (_project.State)
            {
                case ProjectState.Startup:
                    {
                        StartupConfiguration();
                        break;
                    }
                case ProjectState.OpenedProject:
                    {
                        OpenedProjectConfiguration();
                        break;
                    }
                //case ProjectState.PcbSelected:
                //    {
                //        PcbSelectedConfiguration();
                //        break;
                //    }
                case ProjectState.ConfiguredData:
                    {
                        ConfiguredDataConfiguration();
                        break;
                    }
                case ProjectState.ConfiguredAlgorithm:
                    {
                        ConfiguredAlgorithmConfiguration();
                        break;
                    }
                case ProjectState.Traced:
                    {
                        TracedConfiguration();
                        break;
                    }
                case ProjectState.Bundled:
                    {
                        BundledConfiguration();
                        break;
                    }
            }
        }

        public void OnProjectChanged()
        {
            if (_project.Project is null)
            {
                toolStripChoosePcb.Items.Clear();
                toolStripChoosePcb.Text = "Выбрать плату";
            }
            //если выбран проект в toolStrip добавляются платы для выбора
            if (_project.Project is null || _project.Project.Pcbs is null) return;

            if (toolStripChoosePcb.Items.Count == 0)
                foreach (var pcb in _project.Project.Pcbs)
                    toolStripChoosePcb.Items.Add(pcb.Name);
            //очищается выбранное расслоение
            _project.SelectedBundle = null;

            //если есть расслоение появляется окно выбора расслоения
            if (_project.Project.BundleResults != null)
            {
                toolStripRecomendationsMenu.Visible = true;
                toolStripChooseBundle.Visible = true;
                toolStripChooseBundle.Text = "Выбор расслоения";
                toolStripChooseBundle.Items.Add("Без расслоения");
                for (int i = 0; i < _project.Project.BundleResults.Count; i++)
                    toolStripChooseBundle.Items.Add($"Расслоение {i + 1}");
            }
            else
            {
                toolStripChooseBundle.Items.Clear();
                toolStripRecomendationsMenu.Visible = false;
                toolStripChooseBundle.Visible = false;
            }
        }

        private void StartupConfiguration()
        {
            toolStripChooseBundle.Visible = false;
            createProjectMenu.Enabled = true;
            openProjectMenu.Enabled = true;
            closeProgramProjectMenu.Enabled = true;
            closeProjectMenu.Enabled = false;
            saveProjectMenu.Enabled = false;
            saveAsProjectMenu.Enabled = false;
            removeProjectMenu.Enabled = true;
            pcbDetailsMenu.Enabled = false;
            addElementMenu.Enabled = false;
            removeElementMenu.Enabled = false;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            traceSettingsMenu.Enabled = false;
            changeProjectNameMenu.Enabled = false;
            openPcbLib.Enabled = false;
            addPcbToolStripMenuItem.Enabled = false;
            deletePcbToolStripMenuItem.Enabled = false;
            changePcbNameToolStripMenuItem.Enabled = false;
            bundleSettingsMenu.Enabled = false;
            toolStripRecomendationsMenu.Visible = false;

        }

        private void OpenedProjectConfiguration()
        {
            createProjectMenu.Enabled = true;
            openProjectMenu.Enabled = true;
            closeProgramProjectMenu.Enabled = true;
            closeProjectMenu.Enabled = true;
            saveProjectMenu.Enabled = true;
            saveAsProjectMenu.Enabled = true;
            removeProjectMenu.Enabled = true;
            pcbDetailsMenu.Enabled = true;
            addElementMenu.Enabled = false;
            removeElementMenu.Enabled = false;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            traceSettingsMenu.Enabled = false;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
            addPcbToolStripMenuItem.Enabled = true;
            deletePcbToolStripMenuItem.Enabled = true;
            changePcbNameToolStripMenuItem.Enabled = true;
            bundleSettingsMenu.Enabled = false;
        }

        private void ConfiguredDataConfiguration()
        {
            createProjectMenu.Enabled = true;
            openProjectMenu.Enabled = true;
            closeProgramProjectMenu.Enabled = true;
            closeProjectMenu.Enabled = true;
            saveProjectMenu.Enabled = true;
            saveAsProjectMenu.Enabled = true;
            removeProjectMenu.Enabled = true;
            pcbDetailsMenu.Enabled = true;
            addElementMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            traceSettingsMenu.Enabled = true;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
            bundleSettingsMenu.Enabled = false;
        }

        private void ConfiguredAlgorithmConfiguration()
        {
            createProjectMenu.Enabled = true;
            openProjectMenu.Enabled = true;
            closeProgramProjectMenu.Enabled = true;
            closeProjectMenu.Enabled = true;
            saveProjectMenu.Enabled = true;
            saveAsProjectMenu.Enabled = true;
            removeProjectMenu.Enabled = true;
            pcbDetailsMenu.Enabled = true;
            addElementMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = true;
            traceSettingsMenu.Enabled = true;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
            bundleSettingsMenu.Enabled = false;
        }

        private void TracedConfiguration()
        {
            createProjectMenu.Enabled = true;
            openProjectMenu.Enabled = true;
            closeProgramProjectMenu.Enabled = true;
            closeProjectMenu.Enabled = true;
            saveProjectMenu.Enabled = true;
            saveAsProjectMenu.Enabled = true;
            removeProjectMenu.Enabled = true;
            pcbDetailsMenu.Enabled = true;
            addElementMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            runBundleMenu.Enabled = true;
            runTraceMenu.Enabled = true;
            traceSettingsMenu.Enabled = true;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
            bundleSettingsMenu.Enabled = true;
        }

        private void BundledConfiguration()
        {
            createProjectMenu.Enabled = true;
            openProjectMenu.Enabled = true;
            closeProgramProjectMenu.Enabled = true;
            closeProjectMenu.Enabled = true;
            saveProjectMenu.Enabled = true;
            saveAsProjectMenu.Enabled = true;
            removeProjectMenu.Enabled = true;
            pcbDetailsMenu.Enabled = true;
            addElementMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            runBundleMenu.Enabled = true;
            runTraceMenu.Enabled = true;
            traceSettingsMenu.Enabled = true;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
            bundleSettingsMenu.Enabled = true;
        }

        private void workSpace_MouseMove(object sender, MouseEventArgs e)
        {
            xStatus.Text = $"X: {Math.Round(e.X / 12.5, 3)} мм.";
            yStatus.Text = $"Y: {Math.Round(e.Y / 12.5, 3)} мм.";
            AlignStatusStrip();
        }

        private void AlignStatusStrip()
        {
            statusLabel.Margin = new System.Windows.Forms.Padding(Width / 3 - xStatus.Margin.Left - xStatus.Width - yStatus.Width,
                0, 0, 0);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            AlignStatusStrip();
        }

        private async Task AskToSaveProjectAsync()
        {
            if (_project.State != ProjectState.Startup)
            {
                var messageBoxResult = MessageBox.Show("Сохранить текущий проект?", "Сохранение", MessageBoxButtons.YesNoCancel);
                if (messageBoxResult == DialogResult.Cancel) return;
                if (messageBoxResult == DialogResult.Yes)
                {
                    var saveProjectCommand = new SaveProjectCommand(_project.Project);
                    var saveResult = await Mediator.Send(saveProjectCommand);
                    if (saveResult.IsFailure) { MessageBox.Show(saveResult.Error.Message, "Ошибка!"); return; }
                }
                if (messageBoxResult == DialogResult.No)
                {
                    _dbContext.ChangeTracker.Clear();
                }

                _project.ChangeProject(null, ProjectState.Startup);
            }
        }

        private async void createProjectMenu_Click(object sender, EventArgs e)
        {
            await AskToSaveProjectAsync();

            var newProjectName = Microsoft.VisualBasic.Interaction.InputBox("Введите название проекта:", "Создание проекта");

            if (newProjectName == string.Empty) return;

            var createProjectCommand = new CreateProjectCommand(newProjectName);
            var result = await Mediator.Send(createProjectCommand);

            if (result.IsFailure) { MessageBox.Show(result.Error.Message, "Ошибка!"); return; }

            _project.ChangeProject(result.Value, ProjectState.OpenedProject);
        }

        private async void openProjectMenu_Click(object sender, EventArgs e)
        {
            await AskToSaveProjectAsync();

            var getAllProjectsNames = new GetAllProjectNamesQuery();
            var result = await Mediator.Send(getAllProjectsNames);
            var projectNames = result.IsSuccess ? result.Value : new List<string>();

            var openProjectForm = new OpenProjectForm(projectNames);
            openProjectForm.ShowDialog();
            var elements = _project?.Project?.Pcbs?.SelectMany(pcb => pcb?.Layers?.SelectMany(layer => layer?.Elements));
            if (elements == null) return;
            foreach (var element in elements)
            {
                element.ElementControl.MouseDown += ElementControlMouseDownHandler;
                element.ElementControl.MouseMove += ElementControlMouseMoveHandler;
                element.ElementControl.LocationChanged += ElementControlLocationChangedHandler;
            }

            workSpace.Invalidate();
        }

        private async void closeProjectMenu_Click(object sender, EventArgs e)
        {
            await AskToSaveProjectAsync();

            _project.ChangeProject(null, ProjectState.Startup);

            workSpace.Invalidate();
        }

        private async void saveProjectMenu_Click(object sender, EventArgs e)
        {
            var saveProjectCommand = new SaveProjectCommand(_project.Project);
            var saveResult = await Mediator.Send(saveProjectCommand);
            if (saveResult.IsFailure) { MessageBox.Show(saveResult.Error.Message, "Ошибка!"); return; }
            MessageBox.Show("Проект сохранен!", "Сохранение");
        }

        private void saveAsProjectMenu_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "txt(*.txt)|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, "");
            }
        }

        private void closeProgramProjectMenu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void removeProjectMenu_Click(object sender, EventArgs e)
        {
            var getAllProjectNamesQuery = new GetAllProjectNamesQuery();
            var getProjectNamesResult = await Mediator.Send(getAllProjectNamesQuery);
            var projectNames = getProjectNamesResult.IsFailure ? new List<string>() : getProjectNamesResult.Value;
            var deleteProjectForm = new DeleteProjectForm(projectNames);
            deleteProjectForm.ShowDialog();

            workSpace.Invalidate();
        }

        private void pcbDetailsMenu_Click(object sender, EventArgs e)
        {
            var pcbInfoForm = new PcbInfoForm();
            if (pcbInfoForm.ShowDialog() == DialogResult.OK)
            {
                var pcb = _project?.Project?.Pcbs
                ?.FirstOrDefault(pcb => toolStripChoosePcb.Text == pcb.Name);
                if (pcb == null) return;
                pcb.Width = pcbInfoForm.PcbWidth;
                pcb.Height = pcbInfoForm.PcbHeight;
                pcb.TraceWidth = pcbInfoForm.TraceWidth;
                pcb.TracePadding = pcbInfoForm.TracePadding;
                _project.ChangeProject(_project.Project, ProjectState.ConfiguredData);
            }
            workSpace.Invalidate();
        }

        private void traceSettingsMenu_Click(object sender, EventArgs e)
        {
            var algorithmDetailsForm = new TraceSettingsForm(_project);
            algorithmDetailsForm.ShowDialog();
            _project.ChangeProject(_project.Project, ProjectState.ConfiguredAlgorithm);
        }

        private void toolStrip_LayoutCompleted(object sender, EventArgs e)
        {
            var toolStrip = (ToolStrip)sender;
            toolStrip.ImageScalingSize = DeviceDpi > 150 ? new Size(32, 32) : new Size(18, 18);
            toolStrip.Invalidate();
        }

        private void workSpace_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            var workspace = (PictureBox)sender;
            //for (decimal relX = 0; relX < workSpace.Width; relX += (Convert.ToDecimal(0.393701) * workSpace.DeviceDpi))
            //{
            //    for (decimal relY = 0; relY < workSpace.Height; relY += (Convert.ToDecimal(0.393701) * workSpace.DeviceDpi))
            //    {
            //        var dotWidth = (int)decimal.Round(0.019685m * workspace.DeviceDpi);
            //        var intRelX = (int)decimal.Round(relX);
            //        var intRelY = (int)decimal.Round(relY);
            //        g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(intRelX, intRelY, dotWidth, dotWidth));
            //    }
            //}

            for (decimal relX = 0; relX < workSpace.Width; relX += 12.5m)
            {
                for (decimal relY = 0; relY < workSpace.Height; relY += 12.5m)
                {
                    var dotWidth = (int)decimal.Round(0.019685m * workspace.DeviceDpi);
                    var intRelX = (int)decimal.Round(relX);
                    var intRelY = (int)decimal.Round(relY);
                    g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(intRelX, intRelY, dotWidth, dotWidth));
                }
            }

            var currentPcb = _project?.Project?.Pcbs
                ?.FirstOrDefault(pcb => toolStripChoosePcb.Text == pcb.Name);

            var padsConnections = currentPcb
                ?.PadsConnections;

            var traces = currentPcb
                ?.Layers
                ?.SelectMany(layer => layer?.Traces).ToList();

            if (currentPcb != null)
            {
                g.DrawRectangle(new Pen(Color.Red, 3), 1, 1, currentPcb.Width, currentPcb.Height); //каждая точка на экране через 1 см, * 10 чтобы это обозначало как 1 мм
            }

            if (padsConnections != null && traces?.Count == 0)
            {
                foreach (var padConnection in padsConnections)
                {
                    g.DrawLine(new Pen(Color.Red, 3),
                        new PointF(padConnection.PadFrom.Element.LocationX + padConnection.PadFrom.CenterX, padConnection.PadFrom.Element.LocationY + padConnection.PadFrom.CenterY),
                        new PointF(padConnection.PadTo.Element.LocationX + padConnection.PadTo.CenterX, padConnection.PadTo.Element.LocationY + padConnection.PadTo.CenterY)
                        );
                }
            }

            //var traces = _project?.Project?.Pcbs
            //    ?.FirstOrDefault(pcb => toolStripChoosePcb.Text == pcb.Name)
            //    ?.Layers
            //    ?.SelectMany(layer => layer?.Traces).ToList();

            if (workSpace.Controls.Count == 0)
            {
                var elements = _project?.Project?.Pcbs
                ?.FirstOrDefault(pcb => toolStripChoosePcb.Text == pcb.Name)
                ?.Layers
                ?.SelectMany(layer => layer.Elements).ToList();
                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        g.DrawImage(element.ElementControl.Image, new Point(element.LocationX, element.LocationY));
                    }
                }
            }

            if (traces is not null && _project.SelectedBundle is null)
            {
                var pen = new Pen(Color.Red, 3);
                for (int i = 0; i < traces.Count; i++)
                {
                    var coords = traces[i]?.DirectionChangingCoords?.ToList();
                    if (coords is null) continue;
                    for (int j = 0; j < coords.Count - 1; j++)
                    {
                        var fromPoint = coords[j].GetPointF;
                        var toPoint = coords[j + 1].GetPointF;
                        g.DrawLine(pen, fromPoint, toPoint);
                    }
                }
            }

            if (traces is not null && _project.SelectedBundle is not null)
            {
                var pen = new Pen(Color.Red, 3);
                foreach (var colorWithTraces in _project.SelectedBundle)
                {
                    foreach (var trace in colorWithTraces.Value)
                    {
                        var coords = trace?.DirectionChangingCoords?.ToList();
                        if (coords is null) continue;
                        for (int j = 0; j < coords.Count - 1; j++)
                        {
                            var fromPoint = coords[j].GetPointF;
                            var toPoint = coords[j + 1].GetPointF;
                            pen.Color = Color.FromArgb(colorWithTraces.Key);
                            g.DrawLine(pen, fromPoint, toPoint);
                        }
                    }
                }
            }
        }

        private async void changeProjectNameMenu_Click(object sender, EventArgs e)
        {
            var newProjectName = Microsoft.VisualBasic.Interaction.InputBox("Введите название проекта:", "Изменение названия проекта");
            _project.ChangeProjectName(newProjectName);
        }

        private async void openPcbLib_Click(object sender, EventArgs e)
        {
            if (_project.Project.PcbLib != null)
            {
                var dialogResult = MessageBox.Show("Если изменить файл .PcbLib, то все элементы и трассы пропадут, продолжить?", "Внимание!", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Cancel) return;
                if (dialogResult == DialogResult.No) return;
                _project.Project.Pcbs.Clear();
                _project.Project.PcbLib = null;
                _project.PerformProjectChangeAction();
            }

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PCBLIB(*.pcblib)|*.pcblib";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var stream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    var mem = new MemoryStream();
                    stream.CopyTo(mem);
                    _project.Project.PcbLib = mem.ToArray();
                    using (var pcbLibReader = new PcbLibReader())
                    {
                        var pcbLib = pcbLibReader.Read(mem);
                        var componentNames = new List<string>();
                        foreach (var component in pcbLib.Items)
                        {
                            componentNames.Add(component.Pattern);
                        }
                        _project.Project.PossibleElementNamesJson = JsonSerializer.Serialize(componentNames);
                    }
                    await mem.DisposeAsync();
                }

                MessageBox.Show("Файл добавлен!", "Успех!");
            }

            workSpace.Invalidate();
        }

        private async void addElementMenu_Click(object sender, EventArgs e)
        {
            var elementNames = _project.Project.PossibleElementNamesJson == null ?
                new List<string>() :
                JsonSerializer.Deserialize<List<string>>(_project.Project.PossibleElementNamesJson);
            var addElementForm = new AddElementForm(elementNames);
            var dialogResult = addElementForm.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;

            var component = new PcbComponent();
            using (var mem = new MemoryStream(_project.Project.PcbLib))
            {
                var pcbLib = _pcbLibReader.Read(mem);
                component = pcbLib.Items.FirstOrDefault(component => component.Pattern == addElementForm.SelectedComponentName);
            }

            for (int i = 0; i < addElementForm.SelectedComponentCount; i++)
            {
                HandleElementCreation(component);
            }
        }

        private Bitmap RenderPcbComponent(PcbComponent component)
        {
            _pcbLibRenderer.Component = component;
            _pcbLibRenderer.Zoom = 3;
            var rect = _pcbLibRenderer.CalculateComponentBounds();

            _pcbLibRenderer.BackgroundColor = Color.Transparent;
            var width = (int)Math.Round(_pcbLibRenderer.ScaleCoord(rect.Width));
            var height = (int)Math.Round(_pcbLibRenderer.ScaleCoord(rect.Height));

            var image = new Bitmap(width, height);

            using (var g = Graphics.FromImage(image))
            {
                _pcbLibRenderer.Render(g, width, height, true, false);
                //var pads = component.Primitives.Where(primitive => primitive is PcbPad).Select(pad => pad as PcbPad);
                //var padsPoints = pads.Select(pad => Tuple.Create(_pcbLibRenderer.ScreenFromWorld(pad.Location.X, pad.Location.Y), pad.Size)).ToList();

                //foreach (var pad in padsPoints)
                //{
                //    g.FillEllipse(new SolidBrush(Color.Red),
                //        pad.Item1.X - (_pcbLibRenderer.ScaleCoord(pad.Item2.X) / 2),
                //        pad.Item1.Y - (_pcbLibRenderer.ScaleCoord(pad.Item2.Y) / 2),
                //        _pcbLibRenderer.ScaleCoord(pad.Item2.X),
                //        _pcbLibRenderer.ScaleCoord(pad.Item2.Y));

                //}
            }
            return image;
        }

        private int _componentX;
        private int _componentY;
        private void ElementControlMouseDownHandler(object? sender, MouseEventArgs args)
        {
            //меняем координаты выбранного компонента
            _componentX = args.X;
            _componentY = args.Y;

            var elementControl = sender as ElementControl;
            //присваеваем новый выбранный элемент
            //при открытии из бд ElementControl гарантированно создается, поэтому не null
            var newSelectedElement = _project.Project.Pcbs
            .FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
            .Layers
            .SelectMany(layer => layer.Elements)
            .FirstOrDefault(element => element.ElementControl == elementControl);

            //если до этого был выбран компонент то нужно убрать у него выделение
            if (_project.SelectedElement != null && newSelectedElement != _project.SelectedElement)
            {
                _project.SelectedElement.ElementControl.Image = RenderPcbComponent(_project.SelectedElement.ElementControl.PcbComponent);
                _project.SelectedElement.ElementControl.Invalidate();
            }

            _project.SelectedElement = newSelectedElement;
            var currentPcb = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text);

            using (var g = Graphics.FromImage(_project.SelectedElement.ElementControl.Image))
            {
                //если нажали на pad то выделяем pad, если уже был выделен другой пад, то соединяем их
                var pads = _project.SelectedElement.Pads;
                foreach (var pad in pads)
                {
                    if (CheckIfPointInEllipse(new PointF(pad.CenterX, pad.CenterY), pad.SizeX / 2, pad.SizeY / 2, args.Location))
                    {
                        if (_project.SelectedPad == null)
                        {
                            _project.SelectedPad = pad;
                            g.FillEllipse(new SolidBrush(Color.Red),
                                pad.CenterX - pad.SizeX / 2,
                                pad.CenterY - pad.SizeY / 2,
                                pad.SizeX,
                                pad.SizeY);
                        }
                        else
                        {
                            var padsFrom = currentPcb?.PadsConnections?.Select(connection => connection.PadFrom);
                            var padsTo = currentPcb?.PadsConnections?.Select(connection => connection.PadTo);
                            var connectionToDelete = currentPcb?.PadsConnections?.FirstOrDefault(padsConnection => (padsConnection.PadFrom == pad && padsConnection.PadTo == _project.SelectedPad)
                                                                                           || (padsConnection.PadFrom == _project.SelectedPad && padsConnection.PadTo == pad));
                            if (_project.SelectedPad == pad)
                            {
                                _project.SelectedPad = null;
                                _project.SelectedElement.ElementControl.Image = RenderPcbComponent(_project.SelectedElement.ElementControl.PcbComponent);
                                _project.SelectedElement = null;
                            }
                            else if (_project.SelectedPad.Element == pad.Element) { break; }
                            else if (connectionToDelete is not null)
                            {
                                currentPcb.PadsConnections.Remove(connectionToDelete);
                                _project.SelectedPad = null;
                            }
                            else if (padsFrom.Contains(pad) || padsFrom.Contains(_project.SelectedPad) || padsTo.Contains(pad) || padsTo.Contains(_project.SelectedPad))
                            {
                                _project.SelectedPad = null;
                                break;
                            }
                            else
                            {
                                currentPcb.PadsConnections.Add(new PadsConnection
                                {
                                    PadFrom = _project.SelectedPad,
                                    PadTo = pad,
                                    Pcb = currentPcb
                                });
                                _project.SelectedPad = null;
                            }
                        }
                    }
                }

                //рисуем выделение выбранного элемента
                g.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle(0, 0, elementControl.Width - 1, elementControl.Height - 1));

                workSpace.Invalidate();
                elementControl.Invalidate();
            }
        }

        public bool CheckIfPointInEllipse(PointF center, float xRadius, float yRadius, PointF location)
        {
            if (xRadius <= 0.0 || yRadius <= 0.0)
                return false;
            /* This is a more general form of the circle equation
             *
             * X^2/a^2 + Y^2/b^2 <= 1
             */

            PointF normalized = new PointF(location.X - center.X,
                                         location.Y - center.Y);

            return ((double)(normalized.X * normalized.X)
                     / (xRadius * xRadius)) + ((double)(normalized.Y * normalized.Y) / (yRadius * yRadius))
                <= 1.0;
        }

        private void ElementControlMouseMoveHandler(object? sender, MouseEventArgs args)
        {
            if ((MouseButtons & MouseButtons.Left) != 0)
            {
                var deltaX = args.X - _componentX;
                var deltaY = args.Y - _componentY;
                var pictureBox = sender as PictureBox;
                pictureBox.Location = new Point(pictureBox.Location.X + deltaX, pictureBox.Location.Y + deltaY);
            }
        }

        private void ElementControlLocationChangedHandler(object? sender, EventArgs args)
        {
            var elementControl = sender as ElementControl;
            var element = elementControl.Element;
            element.LocationX = elementControl.Location.X;
            element.LocationY = elementControl.Location.Y;

            workSpace.Invalidate();
        }

        private void AddElementToProject(PcbComponent component, ElementControl elementControl)
        {
            var elementToAdd = new Element()
            {
                Name = component.Pattern,
                ElementControl = elementControl,
                LocationX = elementControl.Location.X,
                LocationY = elementControl.Location.Y,
                Image = Domain.Shared.ImageConverter.ImageToByteArray(elementControl.Image),
                Pads = new List<Pad>()
            };
            elementControl.Element = elementToAdd;

            //просто так рисуем кружки по падам
            var pads = component.Primitives.Where(primitive => primitive is PcbPad).Select(pad => pad as PcbPad);
            //var padsPoints = pads.Select(pad => Tuple.Create(_pcbLibRenderer.ScreenFromWorld(pad.Location.X, pad.Location.Y), pad.Size)).ToList();
            foreach (var pad in pads)
            {
                //elementToAdd.Pads.Add(new Pad(
                //    pad.Item1.X - (_pcbLibRenderer.ScaleCoord(pad.Item2.X) / 2),
                //    pad.Item1.Y - (_pcbLibRenderer.ScaleCoord(pad.Item2.Y) / 2)
                //    ));
                var centerPoint = _pcbLibRenderer.ScreenFromWorld(pad.Location);
                var sizeX = _pcbLibRenderer.ScaleCoord(pad.Size.X);
                var sizeY = _pcbLibRenderer.ScaleCoord(pad.Size.Y);
                elementToAdd.Pads.Add(new Pad
                {
                    CenterX = centerPoint.X,
                    CenterY = centerPoint.Y,
                    SizeX = sizeX,
                    SizeY = sizeY,
                    Element = elementToAdd
                });
            }

            _project?.Project?.Pcbs
                ?.FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
                ?.Layers?.FirstOrDefault()
                ?.Elements.Add(elementToAdd);
        }

        private void HandleElementCreation(PcbComponent component)
        {
            var image = RenderPcbComponent(component);

            var elementControl = new ElementControl();
            elementControl.Width = image.Width;
            elementControl.Height = image.Height;
            elementControl.Image = image;
            elementControl.PcbComponent = component;
            workSpace.Controls.Add(elementControl);
            elementControl.Invalidate();


            elementControl.MouseDown += ElementControlMouseDownHandler;
            elementControl.MouseMove += ElementControlMouseMoveHandler;
            elementControl.LocationChanged += ElementControlLocationChangedHandler;

            AddElementToProject(component, elementControl);
        }

        private void addPcbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newPcbName = Microsoft.VisualBasic.Interaction.InputBox("Введите название платы:", "Добавление новой платы");
            if (newPcbName == null) { MessageBox.Show("Некорректный ввод!", "Ошибка!"); return; }
            _project.Project.Pcbs.Add(new Pcb()
            {
                Name = newPcbName,
                Layers = new List<Layer>()
                {
                    new Layer()
                    {
                        Elements = new List<Element>(),
                        Traces = new List<Trace>(),
                    }
                },
                PadsConnections = new List<PadsConnection>()

            });
            toolStripChoosePcb.Items.Add(newPcbName);
            _project.PerformProjectChangeAction();
        }

        private void deletePcbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pcbToDeleteName = toolStripChoosePcb.SelectedItem as string;
            if (pcbToDeleteName == null) { MessageBox.Show("Выберите плату для удаления", "Ошибка!"); return; }
            var pcbToDelete = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == pcbToDeleteName);
            if (pcbToDelete == null) { MessageBox.Show(DomainErrors.Pcb.PcbNotFound.Message, "Ошибка!"); return; }
            _project.Project.Pcbs.Remove(pcbToDelete);

            var index = toolStripChoosePcb.Items.IndexOf(pcbToDeleteName);
            toolStripChoosePcb.Items.RemoveAt(index);
            toolStripChoosePcb.Text = "";

            _project.PerformProjectChangeAction();
        }

        private void changePcbNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pcbToUpdateName = toolStripChoosePcb.SelectedItem as string;
            if (pcbToUpdateName == null) { MessageBox.Show("Выберите плату для изменения", "Ошибка!"); return; }
            var pcbToUpdate = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == pcbToUpdateName);
            if (pcbToUpdate == null) { MessageBox.Show(DomainErrors.Pcb.PcbNotFound.Message, "Ошибка!"); return; }

            var newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое название платы:", "Изменение платы");
            if (newName is null) { MessageBox.Show("Некорректный ввод!", "Ошибка!"); return; }

            pcbToUpdate.Name = newName;

            var index = toolStripChoosePcb.Items.IndexOf(pcbToUpdateName);
            toolStripChoosePcb.Items[index] = newName;

            _project.PerformProjectChangeAction();
        }

        private void toolStripChoosePcb_TextChanged(object sender, EventArgs e)
        {
            if (_project.SelectedElement != null) _project.SelectedElement.ElementControl.Image = RenderPcbComponent(_project.SelectedElement.ElementControl.PcbComponent);
            _project.SelectedElement = null;

            workSpace.Controls.Clear();
            var selectedPcb = (sender as ToolStripComboBox).Text;
            if (selectedPcb is "Выбрать плату")
            {
                addElementMenu.Enabled = false;
                removeElementMenu.Enabled = false;
            }
            else
            {
                addElementMenu.Enabled = true;
                removeElementMenu.Enabled = true;

                //если есть трассы, то есть трассировка уже сделана, не добавляем элементы, которые можно двигать, вместо этого добавится статичная картинка
                var traces = _project?.Project?.Pcbs
                    ?.FirstOrDefault(pcb => pcb.Name == selectedPcb)
                    ?.Layers?.SelectMany(layer => layer?.Traces);
                if (traces == null || traces?.Count() == 0)
                {
                    var elements = _project?.Project?.Pcbs
                        ?.FirstOrDefault(pcb => pcb.Name == selectedPcb)
                        ?.Layers?.SelectMany(layer => layer.Elements);
                    if (elements != null)
                        foreach (var element in elements)
                            workSpace.Controls.Add(element.ElementControl);
                }
            }
            workSpace.Invalidate();
        }

        private void removeElementMenu_Click(object sender, EventArgs e)
        {
            if (_project.SelectedElement == null) { MessageBox.Show("Выберите элемент для удаления!", "Ошибка!"); return; }

            var layer = _project.Project?.Pcbs
                ?.FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
                ?.Layers
                ?.FirstOrDefault(layer => layer.Elements.Contains(_project.SelectedElement));
            layer?.Elements.Remove(_project.SelectedElement);
            workSpace.Controls.Remove(_project.SelectedElement.ElementControl);

            var padConnections = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text).PadsConnections;
            foreach (var connection in padConnections)
            {
                if (_project.SelectedElement.Pads.Contains(connection.PadFrom) || _project.SelectedElement.Pads.Contains(connection.PadTo))
                    padConnections.Remove(connection);
            }

            _project.SelectedElement = null;
            workSpace.Invalidate();
        }

        private async void runTraceMenu_Click(object sender, EventArgs e)
        {
            var currentPcb = _project?.Project?.Pcbs?.FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text);
            if (currentPcb == null) { MessageBox.Show("Выберите печатную плату!", "Ошибка"); return; }

            var pcbMatrix = PreparePcbMatrix(currentPcb.TraceWidth, currentPcb.TracePadding);
            //var pcbMatrix = new int[,]
            //{
            //    { 0, 0, 0, 0, 0, 0, },
            //    { 0, -3, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, },
            //    { 0, -3, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, },
            //    { 0, 0, 0, 0, 0, 0, },
            //};
            var tracingAlgorithm = new Tracing(_project.ObjectiveFunction, _project.TracePriority);
            var traces = await tracingAlgorithm.RunAsync(pcbMatrix);
            ScaleTracesCoords(traces, currentPcb.PadsConnections, currentPcb.TraceWidth, currentPcb.TracePadding);

            currentPcb.Layers.First().Traces = traces.ToList();
            _project.ChangeProject(_project.Project, ProjectState.Traced);

            //удаляем все элементы, больше их нельзя двигать
            workSpace.Controls.Clear();

            workSpace.Invalidate();
        }

        private void ScaleTracesCoords(IEnumerable<Trace> traces, IEnumerable<PadsConnection> connections, int traceWidthInPixels, int padding)
        {
            var traceWidthWithPadding = traceWidthInPixels + padding * 2;

            var tracesList = traces.ToList();
            for (int i = 0; i < traces.Count(); i++)
            {
                var coordsToScale = tracesList[i].DirectionChangingCoords.ToList();
                var clonedCoords = coordsToScale.Select(coord => new TracePoint(coord.X, coord.Y)).ToList();
                var fromPad = coordsToScale.First();
                var toPad = coordsToScale.Last();

                foreach (var connection in connections)
                {
                    if (IsPointInSquare(
                        (int)Math.Round(connection.PadFrom.CenterX) + connection.PadFrom.Element.LocationX,
                        (int)Math.Round(connection.PadFrom.CenterY) + connection.PadFrom.Element.LocationY,
                        fromPad.X * traceWidthWithPadding,
                        fromPad.Y * traceWidthWithPadding,
                        traceWidthWithPadding,
                        traceWidthWithPadding))
                    {
                        fromPad.X = (int)Math.Round(connection.PadFrom.CenterX) + connection.PadFrom.Element.LocationX;
                        fromPad.Y = (int)Math.Round(connection.PadFrom.CenterY) + connection.PadFrom.Element.LocationY;
                        toPad.X = (int)Math.Round(connection.PadTo.CenterX) + connection.PadTo.Element.LocationX;
                        toPad.Y = (int)Math.Round(connection.PadTo.CenterY) + connection.PadTo.Element.LocationY;
                        break;
                    }
                    if (IsPointInSquare(
                        (int)Math.Round(connection.PadFrom.CenterX) + connection.PadFrom.Element.LocationX,
                        (int)Math.Round(connection.PadFrom.CenterY) + connection.PadFrom.Element.LocationY,
                        toPad.X * traceWidthWithPadding,
                        toPad.Y * traceWidthWithPadding,
                        traceWidthWithPadding,
                        traceWidthWithPadding))
                    {
                        fromPad.X = (int)Math.Round(connection.PadTo.CenterX) + connection.PadTo.Element.LocationX;
                        fromPad.Y = (int)Math.Round(connection.PadTo.CenterY) + connection.PadTo.Element.LocationY;
                        toPad.X = (int)Math.Round(connection.PadFrom.CenterX) + connection.PadFrom.Element.LocationX;
                        toPad.Y = (int)Math.Round(connection.PadFrom.CenterY) + connection.PadFrom.Element.LocationY;
                        break;
                    }
                }

                for (int j = 1; j < coordsToScale.Count - 1; j++)
                {
                    //если поменялась координата Y, то X взять из предыдущего scale значения, а Y серединку
                    if (clonedCoords[j].X == clonedCoords[j - 1].X && clonedCoords[j].Y != clonedCoords[j - 1].Y)
                    {
                        coordsToScale[j].X = coordsToScale[j - 1].X;
                        coordsToScale[j].Y = clonedCoords[j].Y * traceWidthWithPadding /*+ traceWidthWithPadding / 2*/;
                    }
                    //если поменялась координата X, то Y взять из предыдущего scale значения, а X серединку
                    if (clonedCoords[j].Y == clonedCoords[j - 1].Y && clonedCoords[j].X != clonedCoords[j - 1].X)
                    {
                        coordsToScale[j].X = clonedCoords[j].X * traceWidthWithPadding /*+ traceWidthWithPadding / 2*/;
                        coordsToScale[j].Y = coordsToScale[j - 1].Y;
                    }

                    // отдельный случай для предпоследней точки, взять не серединку а значение последней точки
                    if (clonedCoords[j].X == clonedCoords[j - 1].X && clonedCoords[j].Y != clonedCoords[j - 1].Y && j == coordsToScale.Count - 2)
                    {
                        coordsToScale[j].X = coordsToScale[j - 1].X;
                        coordsToScale[j].Y = coordsToScale[j + 1].Y;
                    }
                    if (clonedCoords[j].Y == clonedCoords[j - 1].Y && clonedCoords[j].X != clonedCoords[j - 1].X && j == coordsToScale.Count - 2)
                    {
                        coordsToScale[j].X = coordsToScale[j + 1].X;
                        coordsToScale[j].Y = coordsToScale[j - 1].Y;
                    }
                }
            }
        }

        private bool IsPointInSquare(float x, float y, float squareX, float squareY, float squareWidth, float squareHeight)
        {
            if (x >= squareX && x <= squareX + squareWidth && y >= squareY && y <= squareY + squareHeight) return true;
            return false;
        }

        //возвращает матрицу меньшую в (ширина трассы + 2 отступа) раз
        private int[,] PreparePcbMatrix(int traceWidthInPixels, int padding)
        {
            var traceWidthWithPadding = traceWidthInPixels + padding * 2;
            //в этом месте ширина и высота workspace должна вмещать все элементы иначе будет ошибка
            var matrixHeight = (int)Math.Ceiling((decimal)workSpace.Height / traceWidthWithPadding) * traceWidthWithPadding; // чтобы высота и ширина матрицы были кратны traceWidthWithPadding
            var matrixWidth = (int)Math.Ceiling((decimal)workSpace.Width / traceWidthWithPadding) * traceWidthWithPadding;
            var pcbMatrix = new int[matrixHeight, matrixWidth];


            //обозначаем за стены координаты центров всех падов
            var elements = _project.Project.Pcbs
                .FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
                .Layers
                .SelectMany(layer => layer.Elements);
            foreach (var element in elements)
            {
                var pads = element.Pads;
                foreach (var pad in pads)
                {
                    var padX = (element.LocationX + (int)Math.Round(pad.CenterX));
                    var padY = (element.LocationY + (int)Math.Round(pad.CenterY));
                    pcbMatrix[padY, padX] = -2;
                }
            }

            //обозначаем начало и конец трассы
            var connections = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text).PadsConnections.ToList();
            var minus = 3;
            for (int i = 0; i < connections.Count; i++)
            {
                var connection = connections[i];
                var padFromX = (connection.PadFrom.Element.LocationX + (int)Math.Round(connection.PadFrom.CenterX));
                var padFromY = (connection.PadFrom.Element.LocationY + (int)Math.Round(connection.PadFrom.CenterY));
                var padToX = (connection.PadTo.Element.LocationX + (int)Math.Round(connection.PadTo.CenterX));
                var padToY = (connection.PadTo.Element.LocationY + (int)Math.Round(connection.PadTo.CenterY));
                pcbMatrix[padFromY, padFromX] = i - minus;
                pcbMatrix[padToY, padToX] = i - minus;
                minus += 2;
            }
            var result = ScalePcbMatrix(pcbMatrix, traceWidthInPixels, padding);
            return result;
        }

        private int[,] ScalePcbMatrix(int[,] matrix, int traceWidthInPixels, int padding)
        {
            var traceWidthWithPadding = traceWidthInPixels + padding * 2;
            var matrixHeight = (int)Math.Ceiling((decimal)workSpace.Height / traceWidthWithPadding); // чтобы высота и ширина матрицы были кратны traceWidthWithPadding
            var matrixWidth = (int)Math.Ceiling((decimal)workSpace.Width / traceWidthWithPadding);
            var pcbMatrix = new int[matrixHeight, matrixWidth];

            for (int i = 0; i < pcbMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < pcbMatrix.GetLength(1); j++)
                {
                    CheckIfHasPadInSquare(matrix, i * traceWidthWithPadding, j * traceWidthWithPadding, traceWidthWithPadding, out pcbMatrix[i, j]);
                }
            }
            return pcbMatrix;
        }

        private bool CheckIfHasPadInSquare(int[,] matrix, int row, int column, int squareSize, out int value)
        {
            for (int i = row; i < row + squareSize; i++)
            {
                for (int j = column; j < column + squareSize; j++)
                {
                    //если в этом квадрате есть начало или конец трассы которая обозначается цифровой меньшей -1
                    if (matrix[i, j] < -1)
                    {
                        value = matrix[i, j];
                        return true;
                    }
                }
            }
            value = default;
            return false;
        }

        private async void runBundleMenu_Click(object sender, EventArgs e)
        {
            var traces = _project.Project.Pcbs
                .FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
                .Layers
                .SelectMany(layer => layer.Traces)
                .ToList();

            if (_graph == null)
                _graph = new Graph(traces.Count);

            //находим пересекающиеся трассы
            for (int i = 0; i < traces.Count() - 1; i++)
            {
                var currentLine = traces[i].DirectionChangingCoords.Select(tracePoint => tracePoint.GetPointF).ToList();
                for (int j = i + 1; j < traces.Count(); j++)
                {
                    var anotherLine = traces[j].DirectionChangingCoords.Select(tracePoint => tracePoint.GetPointF).ToList();
                    if (AreLinesIntersecting(currentLine, anotherLine))
                        _graph.Connect(i, j);
                }
            }

            var veismanAlgorithm = new VeismanAlgorithm(_graph);
            var result = await veismanAlgorithm.RunAsync();
            var rnd = new Random();

            _project.Project.BundleResults = new List<Dictionary<int, List<Trace>>>();

            //создаем цвета для каждого слоя
            var colors = new List<int>();
            for (int j = 0; j < result[0].Count; j++)
                colors.Add(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)).ToArgb());

            //проходимся по всем результатам трассировки
            for (int i = 0; i < result.Length; i++)
            {
                _project.Project.BundleResults.Add(new Dictionary<int, List<Trace>>());

                //проходимся по конкретному результату
                for (int j = 0; j < result[i].Count; j++)
                {
                    var color = colors[j];
                    var nodes = result[i][j];
                    var sameColorTraces = nodes.Select(node => traces[node.NodeNumber]).ToList();
                    _project.Project.BundleResults[i].Add(color, sameColorTraces);
                }
            }
            _project.Project.BundleResultsJson = JsonSerializer.Serialize(_project.Project.BundleResults);

            _project.ChangeProject(_project.Project, ProjectState.Bundled);

            workSpace.Invalidate();
        }


        public static bool AreLinesIntersecting(List<PointF> line1, List<PointF> line2)
        {
            for (int i = 0; i < line1.Count - 1; i++)
            {
                for (int j = 0; j < line2.Count - 1; j++)
                {
                    if (AreSegmentsIntersecting(line1[i], line1[i + 1], line2[j], line2[j + 1]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool AreSegmentsIntersecting(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            var d1 = Direction(p3, p4, p1);
            var d2 = Direction(p3, p4, p2);
            var d3 = Direction(p1, p2, p3);
            var d4 = Direction(p1, p2, p4);

            if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) && ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
            {
                return true;
            }
            else if (d1 == 0 && IsOnSegment(p3, p4, p1))
            {
                return true;
            }
            else if (d2 == 0 && IsOnSegment(p3, p4, p2))
            {
                return true;
            }
            else if (d3 == 0 && IsOnSegment(p1, p2, p3))
            {
                return true;
            }
            else if (d4 == 0 && IsOnSegment(p1, p2, p4))
            {
                return true;
            }

            return false;
        }

        private static float Direction(PointF p1, PointF p2, PointF p3)
        {
            return (p3.X - p1.X) * (p2.Y - p1.Y) - (p2.X - p1.X) * (p3.Y - p1.Y);
        }

        private static bool IsOnSegment(PointF p1, PointF p2, PointF p3)
        {
            return Math.Min(p1.X, p2.X) <= p3.X && p3.X <= Math.Max(p1.X, p2.X) &&
                   Math.Min(p1.Y, p2.Y) <= p3.Y && p3.Y <= Math.Max(p1.Y, p2.Y);
        }

        private void toolStripChooseBundle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var bundleNumber = toolStripChooseBundle.SelectedIndex - 1;
            if (bundleNumber == -1)
                _project.SelectedBundle = null;
            else
                _project.SelectedBundle = _project.Project.BundleResults[bundleNumber];

            workSpace.Invalidate();
        }

        private Graph? _graph;
        private void bundleSettingsMenu_Click(object sender, EventArgs e)
        {
            var currentPcb = _project.Project.Pcbs
                .FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text);
            if (currentPcb == null) { MessageBox.Show("Выберите печатную плату!", "Ошибка!"); return; }
            var traces = currentPcb
                .Layers
                .SelectMany(layer => layer.Traces)
                .ToList();
            if (traces == null) { MessageBox.Show("Выполните трассировку!", "Ошибка!"); return; }
            var bundleSettingsForm = new BundleSettingsForm(traces, currentPcb.TraceWidth, this);
            bundleSettingsForm.Show(this);
            bundleSettingsForm.FormClosed += (sender, args) =>
            {
                var tracesToBeInOneLayer = bundleSettingsForm.TracesToBeInOneLayer;
                var tracesToBeInDifferentLayers = bundleSettingsForm.TracesToBeInDifferentLayers;
                _graph = new Graph(traces.Count);

                //соединяем вершины трасс которые должны быть на одном слое со всеми остальными 
                for (int i = 0; i < tracesToBeInOneLayer.Count; i++)
                {
                    for (int j = 0; j < tracesToBeInOneLayer[i].Count; j++)
                    {
                        for (int k = 0; k < traces.Count; k++)
                        {
                            if (!tracesToBeInOneLayer[i].Contains(k))
                                _graph.Connect(tracesToBeInOneLayer[i][j], k);
                        }
                    }
                }

                //соединяем вершины трасс которые должны быть на разных слоях между собой
                for (int i = 0; i < tracesToBeInDifferentLayers.Count; i++)
                {
                    for (int j = 0; j < tracesToBeInDifferentLayers[i].Count - 1; j++)
                    {
                        for (int k = j + 1; k < tracesToBeInDifferentLayers[i].Count; k++)
                            _graph.Connect(tracesToBeInDifferentLayers[i][j], tracesToBeInDifferentLayers[i][k]);
                    }
                }
            };
        }


        private string[,] _layerRecomendationTable = new string[,]
        {
            { "CE", "ECE", "", "", "", "", "" },
            { "CCE", "ECCE", "", "", "", "", "" },
            { "", "CCE-EC", "", "CE-ECE-EC", "", "", ""},
            { "", "CCE-ECC", "","CE-ECCE-EC","","CE-ECE-ECE-EC","" },
            { "", "", "", "CCE-ECE-ECC", "", "CE-ECE-ECCE-EC", "" },
            { "", "", "", "CCE-ECCE-ECC", "", "CCE-ECE-ECE-ECC", "" },
            { "", "", "", "", "", "CCE-ECE-ECCE-ECC", "" }
        };
        private void toolStripRecomendationsMenu_Click(object sender, EventArgs e)
        {
            int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите количество потенциальных слоев:", "Рекомендации"), out var potentialLayersCount);
            var signalLayersCount = _project.Project.BundleResults[0].Count - potentialLayersCount;
            var recomendation =_layerRecomendationTable[signalLayersCount - 1, potentialLayersCount - 1];
            var layerNames = recomendation.Split('-').SelectMany(s => Enumerable.Range(0, s.Length).Select(i => s[i] == 'C' ? $"Сигнальный" : $"Потенциальный")).ToArray();
            var layerStackForm = new LayerStackForm(layerNames);
            layerStackForm.ShowDialog();
        }
    }
}