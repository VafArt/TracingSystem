namespace TracingSystem
{
    partial class BundleSettingsForm
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
            label2 = new Label();
            selectTracesButton1 = new Button();
            label3 = new Label();
            button2 = new Button();
            selectTracesButton2 = new Button();
            cancelTraces1 = new Button();
            cancelTraces2 = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.875F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(20, 19);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(428, 25);
            label2.TabIndex = 3;
            label2.Text = "Выбор трасс, которые находятся на одном слое";
            // 
            // selectTracesButton1
            // 
            selectTracesButton1.Location = new Point(128, 55);
            selectTracesButton1.Margin = new Padding(2, 1, 2, 1);
            selectTracesButton1.Name = "selectTracesButton1";
            selectTracesButton1.Size = new Size(81, 22);
            selectTracesButton1.TabIndex = 4;
            selectTracesButton1.Text = "Выбрать";
            selectTracesButton1.UseVisualStyleBackColor = true;
            selectTracesButton1.Click += selectTracesButton1_Click;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 13.875F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(20, 102);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(424, 52);
            label3.TabIndex = 6;
            label3.Text = "Выбор трасс, которые должны находиться на разных слоях";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // button2
            // 
            button2.Location = new Point(194, 214);
            button2.Margin = new Padding(2, 1, 2, 1);
            button2.Name = "button2";
            button2.Size = new Size(81, 22);
            button2.TabIndex = 9;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // selectTracesButton2
            // 
            selectTracesButton2.Location = new Point(128, 170);
            selectTracesButton2.Margin = new Padding(2, 1, 2, 1);
            selectTracesButton2.Name = "selectTracesButton2";
            selectTracesButton2.Size = new Size(81, 22);
            selectTracesButton2.TabIndex = 10;
            selectTracesButton2.Text = "Выбрать";
            selectTracesButton2.UseVisualStyleBackColor = true;
            selectTracesButton2.Click += selectTracesButton2_Click;
            // 
            // cancelTraces1
            // 
            cancelTraces1.Location = new Point(265, 55);
            cancelTraces1.Margin = new Padding(2, 1, 2, 1);
            cancelTraces1.Name = "cancelTraces1";
            cancelTraces1.Size = new Size(81, 22);
            cancelTraces1.TabIndex = 11;
            cancelTraces1.Text = "Отменить";
            cancelTraces1.UseVisualStyleBackColor = true;
            cancelTraces1.Click += cancelTraces1_Click;
            // 
            // cancelTraces2
            // 
            cancelTraces2.Location = new Point(265, 170);
            cancelTraces2.Margin = new Padding(2, 1, 2, 1);
            cancelTraces2.Name = "cancelTraces2";
            cancelTraces2.Size = new Size(81, 22);
            cancelTraces2.TabIndex = 12;
            cancelTraces2.Text = "Отменить";
            cancelTraces2.UseVisualStyleBackColor = true;
            cancelTraces2.Click += cancelTraces2_Click;
            // 
            // BundleSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(467, 263);
            Controls.Add(cancelTraces2);
            Controls.Add(cancelTraces1);
            Controls.Add(selectTracesButton2);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(selectTracesButton1);
            Controls.Add(label2);
            Margin = new Padding(2, 1, 2, 1);
            Name = "BundleSettingsForm";
            Text = "AlgorithmDetailsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Button selectTracesButton1;
        private Label label3;
        private Button button2;
        private Button selectTracesButton2;
        private Button cancelTraces1;
        private Button cancelTraces2;
    }
}