using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OriginalCircuit.AltiumSharp;
using OriginalCircuit.AltiumSharp.Drawing;
using OriginalCircuit.AltiumSharp.Records;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using TracingSystem.Application.Common.Abstractions;
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
using static TracingSystem.Domain.Errors.DomainErrors;
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
            _project.NameChanged += () => projectNameStatus.Text = $"�������� �������: {_project.Name}";
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
            }
        }

        public void OnProjectChanged()
        {
            //��������� toolStrip ��� ������ pcb
            toolStripChoosePcb.Items.Clear();
            toolStripChoosePcb.Text = "������� �����";
            //���� ������ ������ � toolStrip ����������� ����� ��� ������
            if (_project.Project is null || _project.Project.Pcbs is null) return;
            foreach (var pcb in _project.Project.Pcbs)
                toolStripChoosePcb.Items.Add(pcb.Name);
        }

        private void StartupConfiguration()
        {
            createProjectMenu.Enabled = true;
            openProjectMenu.Enabled = true;
            closeProgramProjectMenu.Enabled = true;
            closeProjectMenu.Enabled = false;
            saveProjectMenu.Enabled = false;
            saveAsProjectMenu.Enabled = false;
            removeProjectMenu.Enabled = true;
            pcbDetailsMenu.Enabled = false;
            addElementMenu.Enabled = false;
            addTraceMenu.Enabled = false;
            removeElementMenu.Enabled = false;
            removeTraceMenu.Enabled = false;
            addLayerMenu.Enabled = false;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            settingsMenu.Enabled = false;
            changeProjectNameMenu.Enabled = false;
            openPcbLib.Enabled = false;
            addPcbToolStripMenuItem.Enabled = false;
            deletePcbToolStripMenuItem.Enabled = false;
            changePcbNameToolStripMenuItem.Enabled = false;
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
            addTraceMenu.Enabled = false;
            removeElementMenu.Enabled = false;
            removeTraceMenu.Enabled = false;
            addLayerMenu.Enabled = true;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            settingsMenu.Enabled = false;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
            addPcbToolStripMenuItem.Enabled = true;
            deletePcbToolStripMenuItem.Enabled = true;
            changePcbNameToolStripMenuItem.Enabled = true;
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
            addTraceMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            removeTraceMenu.Enabled = true;
            addLayerMenu.Enabled = true;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            settingsMenu.Enabled = true;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
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
            addTraceMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            removeTraceMenu.Enabled = true;
            addLayerMenu.Enabled = true;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = true;
            settingsMenu.Enabled = true;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
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
            addTraceMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            removeTraceMenu.Enabled = true;
            addLayerMenu.Enabled = true;
            runBundleMenu.Enabled = true;
            runTraceMenu.Enabled = true;
            settingsMenu.Enabled = true;
            changeProjectNameMenu.Enabled = true;
            openPcbLib.Enabled = true;
        }

        private void workSpace_MouseMove(object sender, MouseEventArgs e)
        {
            xStatus.Text = $"X: {e.X}";
            yStatus.Text = $"Y: {e.Y}";
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
                var messageBoxResult = MessageBox.Show("��������� ������� ������?", "����������", MessageBoxButtons.YesNoCancel);
                if (messageBoxResult == DialogResult.Cancel) return;
                if (messageBoxResult == DialogResult.Yes)
                {
                    var saveProjectCommand = new SaveProjectCommand(_project.Project);
                    var saveResult = await Mediator.Send(saveProjectCommand);
                    if (saveResult.IsFailure) { MessageBox.Show(saveResult.Error.Message, "������!"); return; }
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

            var newProjectName = Microsoft.VisualBasic.Interaction.InputBox("������� �������� �������:", "�������� �������");

            if (newProjectName == string.Empty) return;

            var createProjectCommand = new CreateProjectCommand(newProjectName);
            var result = await Mediator.Send(createProjectCommand);

            if (result.IsFailure) { MessageBox.Show(result.Error.Message, "������!"); return; }

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
            foreach(var element in elements)
            {
                element.ElementControl.MouseDown += ElementControlMouseDownHandler;
                element.ElementControl.MouseMove += ElementControlMouseMoveHandler;
                element.ElementControl.LocationChanged += ElementControlLocationChangedHandler;
            }
        }

        private async void closeProjectMenu_Click(object sender, EventArgs e)
        {
            await AskToSaveProjectAsync();
            _project.ChangeProject(null, ProjectState.Startup);
        }

        private async void saveProjectMenu_Click(object sender, EventArgs e)
        {
            var saveProjectCommand = new SaveProjectCommand(_project.Project);
            var saveResult = await Mediator.Send(saveProjectCommand);
            if (saveResult.IsFailure) { MessageBox.Show(saveResult.Error.Message, "������!"); return; }
            MessageBox.Show("������ ��������!", "����������");
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
        }

        private void pcbDetailsMenu_Click(object sender, EventArgs e)
        {
            var pcbInfoForm = new PcbInfoForm();
            pcbInfoForm.ShowDialog();
            _project.ChangeProject(_project.Project, ProjectState.ConfiguredData);
        }

        private void settingsMenu_Click(object sender, EventArgs e)
        {
            var algorithmDetailsForm = new AlgorithmDetailsForm();
            algorithmDetailsForm.ShowDialog();
            _project.ChangeProject(_project.Project, ProjectState.ConfiguredAlgorithm);
        }

        private void runTraceMenu_Click(object sender, EventArgs e)
        {
            _project.ChangeProject(_project.Project, ProjectState.Traced);
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
            for (decimal relX = 0; relX < workSpace.Width; relX += (Convert.ToDecimal(0.393701) * workSpace.DeviceDpi))
            {
                for (decimal relY = 0; relY < workSpace.Height; relY += (Convert.ToDecimal(0.393701) * workSpace.DeviceDpi))
                {
                    var dotWidth = (int)decimal.Round(0.019685m * workspace.DeviceDpi);
                    var intRelX = (int)decimal.Round(relX);
                    var intRelY = (int)decimal.Round(relY);
                    g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(intRelX, intRelY, dotWidth, dotWidth));
                }
            }

            var pads = _project?.Project?.Pcbs
                ?.FirstOrDefault(pcb => toolStripChoosePcb.Text == pcb.Name)
                ?.Layers
                ?.SelectMany(layer => layer.Elements
                ?.SelectMany(element => element?.Pads));
            if (pads is null) return;
            foreach (var firstPad in pads)
            {
                if (firstPad.ConnectedPadId != Guid.Empty)
                {
                    var secondPad = pads.First(secondPad => secondPad.Id == firstPad.ConnectedPadId);
                    g.DrawLine(new Pen(Color.Red, 3),
                    new PointF(firstPad.Element.LocationX + firstPad.CenterX, firstPad.Element.LocationY + firstPad.CenterY),
                    new PointF(secondPad.Element.LocationX + secondPad.CenterX, secondPad.Element.LocationY + secondPad.CenterY));
                }
            }
        }

        private async void changeProjectNameMenu_Click(object sender, EventArgs e)
        {
            var newProjectName = Microsoft.VisualBasic.Interaction.InputBox("������� �������� �������:", "��������� �������� �������");
            _project.ChangeProjectName(newProjectName);
        }

        private async void openPcbLib_Click(object sender, EventArgs e)
        {
            if (_project.Project.PcbLib != null)
            {
                var dialogResult = MessageBox.Show("���� �������� ���� .PcbLib, �� ��� �������� � ������ ��������, ����������?", "��������!", MessageBoxButtons.YesNoCancel);
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

                MessageBox.Show("���� ��������!", "�����!");
            }
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
            //������ ���������� ���������� ����������
            _componentX = args.X;
            _componentY = args.Y;

            var elementControl = sender as ElementControl;
            //����������� ����� ��������� �������
            //��� �������� �� �� ElementControl �������������� ���������, ������� �� null
            var newSelectedElement = _project.Project.Pcbs
            .FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
            .Layers
            .SelectMany(layer => layer.Elements)
            .FirstOrDefault(element => element.ElementControl == elementControl);

            //���� �� ����� ��� ������ ��������� �� ����� ������ � ���� ���������
            if (_project.SelectedElement != null && newSelectedElement != _project.SelectedElement)
            {
                _project.SelectedElement.ElementControl.Image = RenderPcbComponent(_project.SelectedElement.ElementControl.PcbComponent);
                _project.SelectedElement.ElementControl.Invalidate();
            }

            _project.SelectedElement = newSelectedElement;

            using (var g = Graphics.FromImage(_project.SelectedElement.ElementControl.Image))
            {
                //���� ������ �� pad �� �������� pad, ���� ��� ��� ������� ������ ���, �� ��������� ��
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
                            if (_project.SelectedPad == pad)
                            {
                                _project.SelectedPad = null;
                                _project.SelectedElement.ElementControl.Image = RenderPcbComponent(_project.SelectedElement.ElementControl.PcbComponent);
                                _project.SelectedElement = null;
                            }
                            else if (_project.SelectedPad.Element == pad.Element) { break; }
                            else if (_project.SelectedPad.ConnectedPadId != Guid.Empty) { _project.SelectedPad = null; break; }
                            else
                            {
                                _project.SelectedPad.ConnectedPadId = pad.Id;
                                pad.ConnectedPadId = _project.SelectedPad.Id;
                                _project.SelectedPad = null;
                            }
                        }
                    }
                }

                //������ ��������� ���������� ��������
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

            //������ ��� ������ ������ �� �����
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
                    Id = Guid.NewGuid(),
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
            var newPcbName = Microsoft.VisualBasic.Interaction.InputBox("������� �������� �����:", "���������� ����� �����");
            if (newPcbName == null) { MessageBox.Show("������������ ����!", "������!"); return; }
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
                }
            });
            _project.PerformProjectChangeAction();
        }

        private void deletePcbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pcbToDeleteName = toolStripChoosePcb.SelectedItem as string;
            if (pcbToDeleteName == null) { MessageBox.Show("�������� ����� ��� ��������", "������!"); return; }
            var pcbToDelete = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == pcbToDeleteName);
            if (pcbToDelete == null) { MessageBox.Show(DomainErrors.Pcb.PcbNotFound.Message, "������!"); return; }
            _project.Project.Pcbs.Remove(pcbToDelete);
            _project.PerformProjectChangeAction();
        }

        private void changePcbNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pcbToUpdateName = toolStripChoosePcb.SelectedItem as string;
            if (pcbToUpdateName == null) { MessageBox.Show("�������� ����� ��� ���������", "������!"); return; }
            var pcbToUpdate = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == pcbToUpdateName);
            if (pcbToUpdate == null) { MessageBox.Show(DomainErrors.Pcb.PcbNotFound.Message, "������!"); return; }

            var newName = Microsoft.VisualBasic.Interaction.InputBox("������� ����� �������� �����:", "��������� �����");
            if (newName is null) { MessageBox.Show("������������ ����!", "������!"); return; }

            pcbToUpdate.Name = newName;
            _project.PerformProjectChangeAction();
        }

        private void toolStripChoosePcb_TextChanged(object sender, EventArgs e)
        {
            if (_project.SelectedElement != null) _project.SelectedElement.ElementControl.Image = RenderPcbComponent(_project.SelectedElement.ElementControl.PcbComponent);
            _project.SelectedElement = null;

            workSpace.Controls.Clear();
            var selectedPcb = (sender as ToolStripComboBox).Text;
            if (selectedPcb is "������� �����")
            {
                addElementMenu.Enabled = false;
                addTraceMenu.Enabled = false;
                removeElementMenu.Enabled = false;
                removeTraceMenu.Enabled = false;
            }
            else
            {
                addElementMenu.Enabled = true;
                addTraceMenu.Enabled = true;
                removeElementMenu.Enabled = true;
                removeTraceMenu.Enabled = true;

                var elements = _project.Project.Pcbs
                    .FirstOrDefault(pcb => pcb.Name == selectedPcb)
                    .Layers.SelectMany(layer => layer.Elements);
                foreach (var element in elements)
                    workSpace.Controls.Add(element.ElementControl);
            }
        }

        private void removeElementMenu_Click(object sender, EventArgs e)
        {
            if(_project.SelectedElement == null) { MessageBox.Show("�������� ������� ��� ��������!", "������!"); return; }

            var layer = _project.Project?.Pcbs
                ?.FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
                ?.Layers
                ?.FirstOrDefault(layer => layer.Elements.Contains(_project.SelectedElement));
            layer?.Elements.Remove(_project.SelectedElement);
            workSpace.Controls.Remove(_project.SelectedElement.ElementControl);
            _project.SelectedElement = null;
        }
    }
}