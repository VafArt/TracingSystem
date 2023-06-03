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
    public partial class PcbInfoForm : Form
    {
        public int PcbWidth { get; set; }

        public int PcbHeight { get; set; }

        public int TraceWidth { get; set; } = 3;

        public int TracePadding { get; set; } = 11;

        public PcbInfoForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double.TryParse(textBoxPcbWidth.Text, out double pcbWidth);
            double.TryParse(textBoxPcbHeight.Text, out double pcbHeight);
            double.TryParse(textBoxTraceWidth.Text, out double traceWidth);
            double.TryParse(textBoxTracePadding.Text, out double tracePadding);

            if (pcbWidth == 0 || pcbHeight == 0 || traceWidth == 0 || tracePadding == 0) { MessageBox.Show("Невалидные данные!", "Ошибка!"); return; }
            PcbHeight = (int)Math.Round(pcbHeight / 25.4 * DeviceDpi);
            PcbWidth = (int)Math.Round(pcbWidth / 25.4 * DeviceDpi);


            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
