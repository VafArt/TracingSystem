using System.Windows.Forms;

namespace TracingSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            toolStrip1.AutoSize = false;
            toolStrip1.ImageScalingSize = new Size((int)CreateGraphics().DpiX, (int)CreateGraphics().DpiY);
            toolStrip1.AutoSize = true;
        }

        private void workSpace_MouseMove(object sender, MouseEventArgs e)
        {
            xStatus.Text = $"X: {e.X}";
            yStatus.Text = $"Y: {e.Y}";
        }
    }
}