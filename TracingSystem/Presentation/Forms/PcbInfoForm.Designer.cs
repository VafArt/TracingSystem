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
            label2 = new Label();
            textBoxHeight = new TextBox();
            textBoxWidth = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(38, 34);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(87, 19);
            label1.TabIndex = 0;
            label1.Text = "Длина в мм.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(38, 61);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(99, 19);
            label2.TabIndex = 1;
            label2.Text = "Ширина в мм.";
            // 
            // textBoxHeight
            // 
            textBoxHeight.Location = new Point(156, 35);
            textBoxHeight.Margin = new Padding(2, 1, 2, 1);
            textBoxHeight.Name = "textBoxHeight";
            textBoxHeight.Size = new Size(110, 23);
            textBoxHeight.TabIndex = 2;
            // 
            // textBoxWidth
            // 
            textBoxWidth.Location = new Point(156, 61);
            textBoxWidth.Margin = new Padding(2, 1, 2, 1);
            textBoxWidth.Name = "textBoxWidth";
            textBoxWidth.Size = new Size(110, 23);
            textBoxWidth.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Control;
            button1.Location = new Point(103, 97);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(81, 22);
            button1.TabIndex = 4;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // PcbInfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 143);
            Controls.Add(button1);
            Controls.Add(textBoxWidth);
            Controls.Add(textBoxHeight);
            Controls.Add(label2);
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
        private Label label2;
        private TextBox textBoxHeight;
        private TextBox textBoxWidth;
        private Button button1;
    }
}