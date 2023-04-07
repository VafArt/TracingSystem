using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Projects.Commands.CreateProject;
using TracingSystem.Application.Projects.Queries.GetAllProjectNames;
using TracingSystem.Application.Services;
using TracingSystem.Domain;
using TracingSystem.Domain.Shared;
using TracingSystem.Persistance;

namespace TracingSystem
{
    public partial class MainForm : Form
    {
        private readonly ITracingSystemDbContext _dbContext;

        private readonly IMediator Mediator;

        private IProjectDataService _project;

        public MainForm()
        {
            _dbContext = Program.ServiceProvider.GetRequiredService<ITracingSystemDbContext>();
            Mediator = Program.ServiceProvider.GetRequiredService<IMediator>();
            _project = Program.ServiceProvider.GetRequiredService<IProjectDataService>();

            InitializeComponent();
            AlignStatusStrip();
            StartupConfiguration();
            _project.NameChanged += () => projectNameStatus.Text = $"Название проекта: {_project.Name}";
            _project.StateChanged += OnStateChanged;
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
            addElementMenu.Enabled = true;
            addTraceMenu.Enabled = true;
            removeElementMenu.Enabled = true;
            removeTraceMenu.Enabled = true;
            addLayerMenu.Enabled = true;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            settingsMenu.Enabled = false;
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
            var newProjectName = Microsoft.VisualBasic.Interaction.InputBox("Введите название проекта:", "Создание проекта");

            var createProjectCommand = new CreateProjectCommand(newProjectName);
            var result = await Mediator.Send(createProjectCommand);

            if (result.IsFailure) { MessageBox.Show(result.Error.Message, "Ошибка!"); return; }

            _project.Project = result.Value;
            _project.Name = result.Value.Name;
            _project.State = ProjectState.OpenedProject;
        }

        private async void openProjectMenu_Click(object sender, EventArgs e)
        {
            var getAllProjectsNames = new GetAllProjectNamesQuery();
            var result = await Mediator.Send(getAllProjectsNames);
            var projectNames = result.IsSuccess ? result.Value : new List<string>();

            var openProjectForm = new OpenProjectForm(projectNames);
            openProjectForm.ShowDialog();
            _project.State = ProjectState.OpenedProject;
        }

        private void closeProjectMenu_Click(object sender, EventArgs e)
        {
            _project.Name=String.Empty;
            _project.State = ProjectState.Startup;
        }

        private void saveProjectMenu_Click(object sender, EventArgs e)
        {
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

        private void removeProjectMenu_Click(object sender, EventArgs e)
        {
            var deleteProjectForm = new DeleteProjectForm();
            deleteProjectForm.ShowDialog();
            _project.State = ProjectState.Startup;
        }

        private void pcbDetailsMenu_Click(object sender, EventArgs e)
        {
            var pcbInfoForm = new PcbInfoForm();
            pcbInfoForm.ShowDialog();
            _project.State = ProjectState.ConfiguredData;
        }

        private void settingsMenu_Click(object sender, EventArgs e)
        {
            var algorithmDetailsForm = new AlgorithmDetailsForm();
            algorithmDetailsForm.ShowDialog();
            _project.State = ProjectState.ConfiguredAlgorithm;
        }

        private void runTraceMenu_Click(object sender, EventArgs e)
        {
            _project.State = ProjectState.Traced;
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
    }
}