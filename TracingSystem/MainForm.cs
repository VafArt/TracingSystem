namespace TracingSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void workSpace_MouseMove(object sender, MouseEventArgs e)
        {
            xStatus.Text = $"X: {e.X}";
            yStatus.Text = $"Y: {e.Y}";
        }
    }
}