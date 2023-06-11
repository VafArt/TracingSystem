using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracingSystem.Application.Services;

namespace TracingSystem
{
    public partial class LayerStackForm : Form
    {

        public LayerStackForm(string[] layerNames)
        {
            InitializeComponent();
            for (int i = 0; i < layerNames.Length; i++)
            {
                var label = new Label();
                label.Text = layerNames[i];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Dock = DockStyle.Top;
                label.Height = (int)Math.Round(DeviceDpi * 0.417);
                label.Font = new Font("Segoe UI", 16);
                label.AutoSize = false;
                label.BorderStyle = BorderStyle.FixedSingle;
                panel1.Controls.Add(label);
            }
        }


    }
}
