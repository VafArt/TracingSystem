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
    public partial class TraceSettingsForm : Form
    {
        public IProjectDataService _project;

        public TraceSettingsForm(IProjectDataService project)
        {
            InitializeComponent();
            _project = project;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (minimalLayerCountCheckBox.Checked)
                _project.ObjectiveFunction = Application.Common.Algorithms.ObjectiveFunction.MinimalLayerCount;

            if (minimalTraceCountCheckBox.Checked)
                _project.ObjectiveFunction = Application.Common.Algorithms.ObjectiveFunction.MinimalDistance;

            if(verticalPriorityCheckBox.Checked)
                _project.TracePriority = Application.Common.Algorithms.TracePriority.Vertical;

            if (horizontalPriorityCheckBox.Checked)
                _project.TracePriority = Application.Common.Algorithms.TracePriority.Horizontal;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
