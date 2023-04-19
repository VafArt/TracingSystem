using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OriginalCircuit.AltiumSharp;
using OriginalCircuit.AltiumSharp.Drawing;
using OriginalCircuit.AltiumSharp.Records;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using TracingSystem.Application.Common.Abstractions;
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
            _dbContext = Program.ServiceProvider.GetRequiredService<ITracingSystemDbContext>();
            Mediator = Program.ServiceProvider.GetRequiredService<IMediator>();
            _project = Program.ServiceProvider.GetRequiredService<IProjectDataService>();
            _pcbLibRenderer = new PcbLibRenderer();
            _pcbLibReader = new PcbLibReader();

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
            }
        }

        public void OnProjectChanged()
        {
            toolStripChoosePcb.Items.Clear();
            toolStripChoosePcb.Text = "Выбрать плату";
            if (_project.Project is null) return;
            foreach(var pcb in _project.Project.Pcbs)
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

        //private void PcbSelectedConfiguration()
        //{
        //    createProjectMenu.Enabled = true;
        //    openProjectMenu.Enabled = true;
        //    closeProgramProjectMenu.Enabled = true;
        //    closeProjectMenu.Enabled = true;
        //    saveProjectMenu.Enabled = true;
        //    saveAsProjectMenu.Enabled = true;
        //    removeProjectMenu.Enabled = true;
        //    pcbDetailsMenu.Enabled = true;
        //    addElementMenu.Enabled = true;
        //    addTraceMenu.Enabled = true;
        //    removeElementMenu.Enabled = true;
        //    removeTraceMenu.Enabled = true;
        //    addLayerMenu.Enabled = true;
        //    runBundleMenu.Enabled = false;
        //    runTraceMenu.Enabled = false;
        //    settingsMenu.Enabled = false;
        //    changeProjectNameMenu.Enabled = true;
        //    openPcbLib.Enabled = true;
        //    addPcbToolStripMenuItem.Enabled = true;
        //    deletePcbToolStripMenuItem.Enabled = true;
        //    changePcbNameToolStripMenuItem.Enabled = true;
        //}

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

        private async void createProjectMenu_Click(object sender, EventArgs e)
        {
            if(_project.State != ProjectState.Startup) 
            {
                var messageBoxResult = MessageBox.Show("Сохранить текущий проект?", "Созданение", MessageBoxButtons.YesNoCancel);
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

            var newProjectName = Microsoft.VisualBasic.Interaction.InputBox("Введите название проекта:", "Создание проекта");

            if (newProjectName == string.Empty) return;

            var createProjectCommand = new CreateProjectCommand(newProjectName);
            var result = await Mediator.Send(createProjectCommand);

            if (result.IsFailure) { MessageBox.Show(result.Error.Message, "Ошибка!"); return; }

            _project.ChangeProject(result.Value, ProjectState.OpenedProject);
        }

        private async void openProjectMenu_Click(object sender, EventArgs e)
        {
            if (_project.State != ProjectState.Startup)
            {
                var messageBoxResult = MessageBox.Show("Сохранить текущий проект?", "Созранение", MessageBoxButtons.YesNoCancel);
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

            var getAllProjectsNames = new GetAllProjectNamesQuery();
            var result = await Mediator.Send(getAllProjectsNames);
            var projectNames = result.IsSuccess ? result.Value : new List<string>();

            var openProjectForm = new OpenProjectForm(projectNames);
            openProjectForm.ShowDialog();
        }

        private async void closeProjectMenu_Click(object sender, EventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Сохранить текущий проект?", "Созранение", MessageBoxButtons.YesNoCancel);
            if (messageBoxResult == DialogResult.Cancel) return;
            if (messageBoxResult == DialogResult.Yes)
            {
                var saveProjectCommand = new SaveProjectCommand(_project.Project);
                var saveResult = await Mediator.Send(saveProjectCommand);
                if (saveResult.IsFailure) { MessageBox.Show(saveResult.Error.Message, "Ошибка!"); return; }
            }
            if(messageBoxResult == DialogResult.No) 
            {
                _dbContext.ChangeTracker.Clear();
            };
            _project.ChangeProject(null, ProjectState.Startup);
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
            toolStrip.ImageScalingSize = DeviceDpi>150 ? new Size(32,32) : new Size(18,18);
            toolStrip.Invalidate();
        }

        private void workSpace_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
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
        }

        private async void changeProjectNameMenu_Click(object sender, EventArgs e)
        {
            var newProjectName = Microsoft.VisualBasic.Interaction.InputBox("Введите название проекта:", "Изменение названия проекта");
            //var changeProjectNameCommand = new ChangeProjectNameCommand(newProjectName);
            //var changeNameResult = await Mediator.Send(changeProjectNameCommand);
            //if (changeNameResult.IsFailure) { MessageBox.Show(changeNameResult.Error.Message, "Ошибка!"); return; };
            _project.ChangeProjectName(newProjectName);
        }

        private async void openPcbLib_Click(object sender, EventArgs e)
        {
            if (_project.Project.PcbLib != null)
            {
                var dialogResult = MessageBox.Show("Если изменить файл .PcbLib, то все элементы и трассы пропадут, продолжить?", "Внимание!", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Cancel) return;
                if (dialogResult == DialogResult.No) return;
                _project.Project.Pcbs = null;
                _project.Project.PcbLib = null;
            }

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PCBLIB(*.pcblib)|*.pcblib";
            openFileDialog.RestoreDirectory = true;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
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
                        var project = _dbContext.Projects.FirstOrDefault(project=>project.Name == _project.Name);
                        project.PossibleElementNamesJson = JsonSerializer.Serialize(componentNames);
                        _project.Project.PossibleElementNamesJson = project.PossibleElementNamesJson;
                        await _dbContext.SaveChangesAsync(CancellationToken.None);
                        _dbContext.ChangeTracker.Clear();
                    }
                    await mem.DisposeAsync();
                }

                MessageBox.Show("Файл добавлен!", "Успех!");
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
                await RenderPcbComponentAndAddToProject(component);
            }
        }

        private int _componentX;
        private int _componentY;
        private async Task RenderPcbComponentAndAddToProject(PcbComponent component)
        {
            _pcbLibRenderer.Component = component;
            _pcbLibRenderer.Zoom = 3;
            var rect = _pcbLibRenderer.CalculateComponentBounds();

            _pcbLibRenderer.BackgroundColor = Color.Transparent;
            var width = (int)Math.Round(_pcbLibRenderer.ScaleCoord(rect.Width));
            var height = (int)Math.Round(_pcbLibRenderer.ScaleCoord(rect.Height));

            var image = new Bitmap(width, height);
            var pictureBox = new PictureBox();
            pictureBox.Size = new Size(width, height);

            using (var g = Graphics.FromImage(image))
            {
                _pcbLibRenderer.Render(g, pictureBox.Width, pictureBox.Height, true, false);
                var pads = component.Primitives.Where(primitive => primitive is PcbPad).Select(pad => pad as PcbPad);
                var padsPoints = pads.Select(pad => Tuple.Create(_pcbLibRenderer.ScreenFromWorld(pad.Location.X, pad.Location.Y), pad.Size)).ToList();

                foreach (var pad in padsPoints)
                {
                    g.FillEllipse(new SolidBrush(Color.Red),
                        pad.Item1.X - (_pcbLibRenderer.ScaleCoord(pad.Item2.X) / 2),
                        pad.Item1.Y - (_pcbLibRenderer.ScaleCoord(pad.Item2.Y) / 2),
                        _pcbLibRenderer.ScaleCoord(pad.Item2.X),
                        _pcbLibRenderer.ScaleCoord(pad.Item2.Y));

                }

                pictureBox.Image = image;
                workSpace.Controls.Add(pictureBox);
                pictureBox.Invalidate();


                pictureBox.MouseDown += (sender, args) =>
                {
                    _componentX = args.X;
                    _componentY = args.Y;
                };

                pictureBox.MouseMove += (sender, args) =>
                {
                    if ((MouseButtons & MouseButtons.Left) != 0)
                    {
                        var deltaX = args.X - _componentX;
                        var deltaY = args.Y - _componentY;
                        var pictureBox = sender as PictureBox;
                        pictureBox.Location = new Point(pictureBox.Location.X + deltaX, pictureBox.Location.Y + deltaY);
                    }
                };

                pictureBox.LocationChanged += (sender, args) =>
                {
                    var pictureBox = sender as PictureBox;
                    var element = _project.Project.Pcbs
                    .FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
                    .Layers
                    .SelectMany(layer => layer.Elements)
                    .FirstOrDefault(element => element.PictureBox == pictureBox);
                    element.LocationX = pictureBox.Location.X;
                    element.LocationY = pictureBox.Location.Y;
                };

                var elementToAdd = new Element()
                {
                    Name = component.Pattern,
                    PictureBox = pictureBox,
                    //Location = new ElementPoint(pictureBox.Location),
                    LocationX = pictureBox.Location.X,
                    LocationY = pictureBox.Location.Y,
                    Image = Domain.Shared.ImageConverter.ImageToByteArray(image),
                    PadsCoords = new List<ElementPoint>()
                };

                foreach(var pad in padsPoints)
                {
                    elementToAdd.PadsCoords.Add(new ElementPoint(
                        pad.Item1.X - (_pcbLibRenderer.ScaleCoord(pad.Item2.X) / 2),
                        pad.Item1.Y - (_pcbLibRenderer.ScaleCoord(pad.Item2.Y) / 2)
                        ));
                }

                _project.Project.Pcbs
                    .FirstOrDefault(pcb => pcb.Name == toolStripChoosePcb.Text)
                    ?.Layers.FirstOrDefault()
                    ?.Elements.Add(elementToAdd);
            }
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
                }
            });
            _project.PerformProjectChangeAction();
        }

        private void deletePcbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pcbToDeleteName = toolStripChoosePcb.SelectedItem as string;
            if (pcbToDeleteName == null) { MessageBox.Show("Выберите плату для удаления", "Ошибка!"); return; }
            var pcbToDelete = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == pcbToDeleteName);
            if(pcbToDelete == null) { MessageBox.Show(DomainErrors.Pcb.PcbNotFound.Message, "Ошибка!"); return; }
            _project.Project.Pcbs.Remove(pcbToDelete);
            _project.PerformProjectChangeAction();
        }

        private void changePcbNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pcbToUpdateName = toolStripChoosePcb.SelectedItem as string;
            if (pcbToUpdateName == null) { MessageBox.Show("Выберите плату для изменения", "Ошибка!"); return; }
            var pcbToUpdate = _project.Project.Pcbs.FirstOrDefault(pcb => pcb.Name == pcbToUpdateName);
            if (pcbToUpdate == null) { MessageBox.Show(DomainErrors.Pcb.PcbNotFound.Message, "Ошибка!"); return; }

            var newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое название платы:", "Изменение платы");
            if(newName is null) { MessageBox.Show("Некорректный ввод!", "Ошибка!"); return; }

            pcbToUpdate.Name = newName;
            _project.PerformProjectChangeAction();
        }

        private void toolStripChoosePcb_TextChanged(object sender, EventArgs e)
        {
            workSpace.Controls.Clear();
            var selectedPcb = (sender as ToolStripComboBox).Text;
            if (selectedPcb is "Выбрать плату")
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
                    .Layers.FirstOrDefault()
                    .Elements;
                foreach (var element in elements)
                {
                    var pictureBox = new PictureBox();
                    pictureBox.Location = new Point(element.LocationX, element.LocationY);
                    var image = Domain.Shared.ImageConverter.ByteArrayToImage(element.Image);
                    pictureBox.Size = image.Size;
                    pictureBox.Image = image;
                    workSpace.Controls.Add(pictureBox);

                    pictureBox.MouseDown += (sender, args) =>
                    {
                        _componentX = args.X;
                        _componentY = args.Y;
                    };

                    pictureBox.MouseMove += (sender, args) =>
                    {
                        if ((MouseButtons & MouseButtons.Left) != 0)
                        {
                            var deltaX = args.X - _componentX;
                            var deltaY = args.Y - _componentY;
                            var pictureBox = sender as PictureBox;
                            pictureBox.Location = new Point(pictureBox.Location.X + deltaX, pictureBox.Location.Y + deltaY);
                        }
                    };

                    pictureBox.Invalidate();
                    element.PictureBox = pictureBox;
                }

            }
        }
    }
}