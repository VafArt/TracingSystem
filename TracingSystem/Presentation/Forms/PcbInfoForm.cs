using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TracingSystem
{
    public partial class PcbInfoForm : Form
    {
        public int PcbWidth { get; set; }

        public int PcbHeight { get; set; }

        public PcbInfoForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double.TryParse(textBoxWidth.Text, out double width);
            double.TryParse(textBoxHeight.Text, out double height);
            if (width == 0 || height == 0) { MessageBox.Show("Невалидные данные!", "Ошибка!"); return; }
            PcbHeight = (int)Math.Round(height);
            PcbWidth = (int)Math.Round(width);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
