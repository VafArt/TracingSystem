using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TracingSystem
{
    public partial class OpenProjectForm : Form
    {
        private readonly List<string> _projectNames;
        public OpenProjectForm(List<string> projectNames)
        {
            InitializeComponent();
            _projectNames = projectNames;

            foreach(string name in _projectNames)
            {
                var button = new System.Windows.Forms.Button
                {
                    Text = name,
                    Dock = DockStyle.Top,
                    Height = (int)Math.Round(DeviceDpi * 0.417),
                    FlatStyle = FlatStyle.Flat
                };
                button.FlatAppearance.BorderSize = 0;
                button.Font = DeviceDpi > 96 ? new Font("Segoe UI", 24) : new Font("Segoe UI", 16);
                panel.Controls.Add(button);
            }
        }
    }
}
