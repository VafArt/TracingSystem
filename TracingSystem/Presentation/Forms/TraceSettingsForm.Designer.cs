namespace TracingSystem
{
    partial class TraceSettingsForm
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
            this.label1 = new Label();
            minimalLayerCountCheckBox = new CheckBox();
            minimalTraceCountCheckBox = new CheckBox();
            submitButton = new Button();
            horizontalPriorityCheckBox = new CheckBox();
            verticalPriorityCheckBox = new CheckBox();
            this.label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Segoe UI", 13.875F, FontStyle.Regular, GraphicsUnit.Point);
            this.label1.Location = new Point(90, 9);
            this.label1.Margin = new Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(270, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбор критерия трассировки";
            // 
            // minimalLayerCountCheckBox
            // 
            minimalLayerCountCheckBox.AutoSize = true;
            minimalLayerCountCheckBox.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            minimalLayerCountCheckBox.Location = new Point(25, 39);
            minimalLayerCountCheckBox.Margin = new Padding(2, 1, 2, 1);
            minimalLayerCountCheckBox.Name = "minimalLayerCountCheckBox";
            minimalLayerCountCheckBox.Size = new Size(234, 23);
            minimalLayerCountCheckBox.TabIndex = 1;
            minimalLayerCountCheckBox.Text = "Минимальное количество слоев";
            minimalLayerCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // minimalTraceCountCheckBox
            // 
            minimalTraceCountCheckBox.AutoSize = true;
            minimalTraceCountCheckBox.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            minimalTraceCountCheckBox.Location = new Point(25, 68);
            minimalTraceCountCheckBox.Margin = new Padding(2, 1, 2, 1);
            minimalTraceCountCheckBox.Name = "minimalTraceCountCheckBox";
            minimalTraceCountCheckBox.Size = new Size(197, 23);
            minimalTraceCountCheckBox.TabIndex = 2;
            minimalTraceCountCheckBox.Text = "Минимальная длина трасс";
            minimalTraceCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // submitButton
            // 
            submitButton.Location = new Point(191, 221);
            submitButton.Margin = new Padding(2, 1, 2, 1);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(81, 22);
            submitButton.TabIndex = 9;
            submitButton.Text = "OK";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
            // 
            // horizontalPriorityCheckBox
            // 
            horizontalPriorityCheckBox.AutoSize = true;
            horizontalPriorityCheckBox.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            horizontalPriorityCheckBox.Location = new Point(25, 191);
            horizontalPriorityCheckBox.Margin = new Padding(2, 1, 2, 1);
            horizontalPriorityCheckBox.Name = "horizontalPriorityCheckBox";
            horizontalPriorityCheckBox.Size = new Size(131, 23);
            horizontalPriorityCheckBox.TabIndex = 11;
            horizontalPriorityCheckBox.Text = "Горизонтальное";
            horizontalPriorityCheckBox.UseVisualStyleBackColor = true;
            // 
            // verticalPriorityCheckBox
            // 
            verticalPriorityCheckBox.AutoSize = true;
            verticalPriorityCheckBox.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point);
            verticalPriorityCheckBox.Location = new Point(25, 162);
            verticalPriorityCheckBox.Margin = new Padding(2, 1, 2, 1);
            verticalPriorityCheckBox.Name = "verticalPriorityCheckBox";
            verticalPriorityCheckBox.Size = new Size(116, 23);
            verticalPriorityCheckBox.TabIndex = 10;
            verticalPriorityCheckBox.Text = "Вертикальное";
            verticalPriorityCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new Font("Segoe UI", 13.875F, FontStyle.Regular, GraphicsUnit.Point);
            this.label2.Location = new Point(42, 92);
            this.label2.Margin = new Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(357, 69);
            this.label2.TabIndex = 12;
            this.label2.Text = "Выбор приоритетного направления прокладки трасс";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TraceSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(467, 267);
            Controls.Add(this.label2);
            Controls.Add(horizontalPriorityCheckBox);
            Controls.Add(verticalPriorityCheckBox);
            Controls.Add(submitButton);
            Controls.Add(minimalTraceCountCheckBox);
            Controls.Add(minimalLayerCountCheckBox);
            Controls.Add(this.label1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "TraceSettingsForm";
            Text = "AlgorithmDetailsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox minimalLayerCountCheckBox;
        private CheckBox minimalTraceCountCheckBox;
        private Button submitButton;
        private CheckBox horizontalPriorityCheckBox;
        private CheckBox verticalPriorityCheckBox;
        private Label label2;
    }
}