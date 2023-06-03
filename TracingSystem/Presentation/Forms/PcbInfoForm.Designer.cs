namespace TracingSystem
{
    partial class PcbInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBoxPcbHeight = new TextBox();
            textBoxPcbWidth = new TextBox();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            textBoxTracePadding = new TextBox();
            textBoxTraceWidth = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(38, 17);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(146, 52);
            label1.TabIndex = 0;
            label1.Text = "Длина печатной платы в мм.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxPcbHeight
            // 
            textBoxPcbHeight.Location = new Point(219, 34);
            textBoxPcbHeight.Margin = new Padding(2, 1, 2, 1);
            textBoxPcbHeight.Name = "textBoxPcbHeight";
            textBoxPcbHeight.Size = new Size(110, 23);
            textBoxPcbHeight.TabIndex = 2;
            // 
            // textBoxPcbWidth
            // 
            textBoxPcbWidth.Location = new Point(219, 82);
            textBoxPcbWidth.Margin = new Padding(2, 1, 2, 1);
            textBoxPcbWidth.Name = "textBoxPcbWidth";
            textBoxPcbWidth.Size = new Size(110, 23);
            textBoxPcbWidth.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Control;
            button1.Location = new Point(146, 249);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(81, 22);
            button1.TabIndex = 4;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(38, 65);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(146, 52);
            label2.TabIndex = 5;
            label2.Text = "Ширина печатной платы в мм.";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(38, 182);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(146, 52);
            label3.TabIndex = 9;
            label3.Text = "Расстояние между проводниками в мм.";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxTracePadding
            // 
            textBoxTracePadding.Location = new Point(219, 199);
            textBoxTracePadding.Margin = new Padding(2, 1, 2, 1);
            textBoxTracePadding.Name = "textBoxTracePadding";
            textBoxTracePadding.Size = new Size(110, 23);
            textBoxTracePadding.TabIndex = 8;
            // 
            // textBoxTraceWidth
            // 
            textBoxTraceWidth.Location = new Point(219, 151);
            textBoxTraceWidth.Margin = new Padding(2, 1, 2, 1);
            textBoxTraceWidth.Name = "textBoxTraceWidth";
            textBoxTraceWidth.Size = new Size(110, 23);
            textBoxTraceWidth.TabIndex = 7;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(38, 134);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(146, 52);
            label4.TabIndex = 6;
            label4.Text = "Ширина проводников в мм.";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PcbInfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(387, 296);
            Controls.Add(label3);
            Controls.Add(textBoxTracePadding);
            Controls.Add(textBoxTraceWidth);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(textBoxPcbWidth);
            Controls.Add(textBoxPcbHeight);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2, 1, 2, 1);
            Name = "PcbInfoForm";
            Text = "Данные о печатной плате";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxPcbHeight;
        private TextBox textBoxPcbWidth;
        private Button button1;
        private Label label2;
        private Label label3;
        private TextBox textBoxTracePadding;
        private TextBox textBoxTraceWidth;
        private Label label4;
    }
}