using System.Windows.Forms;

namespace TracingSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ProjectData.NameChanged+=()=> projectNameStatus.Text = $"Название проекта: {ProjectData.Name}";
            ProjectData.StateChanged += OnStateChanged;
            AlignStatusStrip();
            StartupConfiguration();
        }

        public void OnStateChanged()
        {
            switch (ProjectData.State)
            {
                case State.Startup:
                    {
                        StartupConfiguration();
                        break;
                    }
                case State.OpenedProject:
                    {
                        OpenedProjectConfiguration();
                        break;
                    }
                case State.ConfiguredData:
                    {
                        ConfiguredDataConfiguration();
                        break;
                    }
                case State.ConfiguredAlgorithm:
                    {
                        ConfiguredAlgorithmConfiguration();
                        break;
                    }
                case State.Traced:
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
            openPCBMenu.Enabled = false;
            runBundleMenu.Enabled = false;
            runTraceMenu.Enabled = false;
            settingsMenu.Enabled = false;
        }

        private void DpiConfiguration()
        {
            if(DeviceDpi<150)
            {
                toolStrip.ImageScalingSize= new Size(18,18);
            }
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
            openPCBMenu.Enabled = true;
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
            openPCBMenu.Enabled = true;
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
            openPCBMenu.Enabled = true;
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
            openPCBMenu.Enabled = true;
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

        private void createProjectMenu_Click(object sender, EventArgs e)
        {
            ProjectData.Name = Microsoft.VisualBasic.Interaction.InputBox("Введите название проекта:","Создание проекта");
            ProjectData.State = State.OpenedProject;
        }

        private void openProjectMenu_Click(object sender, EventArgs e)
        {
            var openProjectForm = new OpenProjectForm();
            openProjectForm.ShowDialog();
            ProjectData.State = State.Traced;
            //ProjectData.State = State.OpenedProject;
        }

        private void closeProjectMenu_Click(object sender, EventArgs e)
        {
            ProjectData.Name=String.Empty;
            ProjectData.State = State.Startup;
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
            ProjectData.State = State.Startup;
        }

        private void pcbDetailsMenu_Click(object sender, EventArgs e)
        {
            var pcbInfoForm = new PcbInfoForm();
            pcbInfoForm.ShowDialog();
            ProjectData.State = State.ConfiguredData;
        }

        private void settingsMenu_Click(object sender, EventArgs e)
        {
            var algorithmDetailsForm = new AlgorithmDetailsForm();
            algorithmDetailsForm.ShowDialog();
            ProjectData.State = State.ConfiguredAlgorithm;
        }

        private void runTraceMenu_Click(object sender, EventArgs e)
        {
            ProjectData.State = State.Traced;
        }

        private void openPCBMenu_Click(object sender, EventArgs e)
        {

        }
    }
}