namespace TracingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            var containerControl = new ContainerControl();
            containerControl.Controls.Add(button1);
            containerControl.Controls.Add(button2);
            Controls.Add(containerControl);
            InitializeComponent();
        }
    }
}