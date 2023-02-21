namespace TracingSystem
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.removeProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProgramProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.projectDetailsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pcbDetailsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addElementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addTraceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.removeElementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTraceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openPCBMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runTraceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runBundleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.saveTool = new System.Windows.Forms.ToolStripButton();
            this.addElementTool = new System.Windows.Forms.ToolStripButton();
            this.removeElementTool = new System.Windows.Forms.ToolStripButton();
            this.addTraceTool = new System.Windows.Forms.ToolStripButton();
            this.removeTraceTool = new System.Windows.Forms.ToolStripButton();
            this.runTool = new System.Windows.Forms.ToolStripButton();
            this.status = new System.Windows.Forms.StatusStrip();
            this.xStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.yStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.projectNameStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.workSpace = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectMenu,
            this.projectDetailsMenu,
            this.editMenu,
            this.runMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1600, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectMenu
            // 
            this.projectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createProjectMenu,
            this.openProjectMenu,
            this.closeProjectMenu,
            this.saveProjectMenu,
            this.saveAsProjectMenu,
            this.removeProjectMenu,
            this.closeProgramProjectMenu});
            this.projectMenu.Name = "projectMenu";
            this.projectMenu.Size = new System.Drawing.Size(114, 36);
            this.projectMenu.Text = "Проект";
            // 
            // createProjectMenu
            // 
            this.createProjectMenu.Name = "createProjectMenu";
            this.createProjectMenu.Size = new System.Drawing.Size(321, 44);
            this.createProjectMenu.Text = "Создать...";
            this.createProjectMenu.Click += new System.EventHandler(this.createProjectMenu_Click);
            // 
            // openProjectMenu
            // 
            this.openProjectMenu.Name = "openProjectMenu";
            this.openProjectMenu.Size = new System.Drawing.Size(321, 44);
            this.openProjectMenu.Text = "Открыть...";
            this.openProjectMenu.Click += new System.EventHandler(this.openProjectMenu_Click);
            // 
            // closeProjectMenu
            // 
            this.closeProjectMenu.Name = "closeProjectMenu";
            this.closeProjectMenu.Size = new System.Drawing.Size(321, 44);
            this.closeProjectMenu.Text = "Закрыть";
            this.closeProjectMenu.Click += new System.EventHandler(this.closeProjectMenu_Click);
            // 
            // saveProjectMenu
            // 
            this.saveProjectMenu.Name = "saveProjectMenu";
            this.saveProjectMenu.Size = new System.Drawing.Size(321, 44);
            this.saveProjectMenu.Text = "Сохранить";
            this.saveProjectMenu.Click += new System.EventHandler(this.saveProjectMenu_Click);
            // 
            // saveAsProjectMenu
            // 
            this.saveAsProjectMenu.Name = "saveAsProjectMenu";
            this.saveAsProjectMenu.Size = new System.Drawing.Size(321, 44);
            this.saveAsProjectMenu.Text = "Сохранить как...";
            this.saveAsProjectMenu.Click += new System.EventHandler(this.saveAsProjectMenu_Click);
            // 
            // removeProjectMenu
            // 
            this.removeProjectMenu.Name = "removeProjectMenu";
            this.removeProjectMenu.Size = new System.Drawing.Size(321, 44);
            this.removeProjectMenu.Text = "Удалить";
            this.removeProjectMenu.Click += new System.EventHandler(this.removeProjectMenu_Click);
            // 
            // closeProgramProjectMenu
            // 
            this.closeProgramProjectMenu.Name = "closeProgramProjectMenu";
            this.closeProgramProjectMenu.Size = new System.Drawing.Size(321, 44);
            this.closeProgramProjectMenu.Text = "Выход";
            this.closeProgramProjectMenu.Click += new System.EventHandler(this.closeProgramProjectMenu_Click);
            // 
            // projectDetailsMenu
            // 
            this.projectDetailsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pcbDetailsMenu});
            this.projectDetailsMenu.Name = "projectDetailsMenu";
            this.projectDetailsMenu.Size = new System.Drawing.Size(248, 36);
            this.projectDetailsMenu.Text = "Проектные данные";
            // 
            // pcbDetailsMenu
            // 
            this.pcbDetailsMenu.Name = "pcbDetailsMenu";
            this.pcbDetailsMenu.Size = new System.Drawing.Size(451, 44);
            this.pcbDetailsMenu.Text = "Данные о печатной плате...";
            this.pcbDetailsMenu.Click += new System.EventHandler(this.pcbDetailsMenu_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addElementMenu,
            this.addTraceMenu,
            this.removeElementMenu,
            this.removeTraceMenu,
            this.openPCBMenu});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(196, 36);
            this.editMenu.Text = "Редактировать";
            // 
            // addElementMenu
            // 
            this.addElementMenu.Name = "addElementMenu";
            this.addElementMenu.Size = new System.Drawing.Size(397, 44);
            this.addElementMenu.Text = "Добавить элемент";
            // 
            // addTraceMenu
            // 
            this.addTraceMenu.Name = "addTraceMenu";
            this.addTraceMenu.Size = new System.Drawing.Size(397, 44);
            this.addTraceMenu.Text = "Добавить трассу";
            // 
            // removeElementMenu
            // 
            this.removeElementMenu.Name = "removeElementMenu";
            this.removeElementMenu.Size = new System.Drawing.Size(397, 44);
            this.removeElementMenu.Text = "Удалить элемент";
            // 
            // removeTraceMenu
            // 
            this.removeTraceMenu.Name = "removeTraceMenu";
            this.removeTraceMenu.Size = new System.Drawing.Size(397, 44);
            this.removeTraceMenu.Text = "Удалить трассу";
            // 
            // openPCBMenu
            // 
            this.openPCBMenu.Name = "openPCBMenu";
            this.openPCBMenu.Size = new System.Drawing.Size(397, 44);
            this.openPCBMenu.Text = "Открыть файл PCBLIB...";
            this.openPCBMenu.Click += new System.EventHandler(this.openPCBMenu_Click);
            // 
            // runMenu
            // 
            this.runMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runTraceMenu,
            this.runBundleMenu,
            this.settingsMenu});
            this.runMenu.Name = "runMenu";
            this.runMenu.Size = new System.Drawing.Size(156, 36);
            this.runMenu.Text = "Выполнить";
            // 
            // runTraceMenu
            // 
            this.runTraceMenu.Name = "runTraceMenu";
            this.runTraceMenu.Size = new System.Drawing.Size(413, 44);
            this.runTraceMenu.Text = "Выполнить трассировку";
            this.runTraceMenu.Click += new System.EventHandler(this.runTraceMenu_Click);
            // 
            // runBundleMenu
            // 
            this.runBundleMenu.Name = "runBundleMenu";
            this.runBundleMenu.Size = new System.Drawing.Size(413, 44);
            this.runBundleMenu.Text = "Выполнить расслоение";
            // 
            // settingsMenu
            // 
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(413, 44);
            this.settingsMenu.Text = "Настройка алгоритма...";
            this.settingsMenu.Click += new System.EventHandler(this.settingsMenu_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTool,
            this.addElementTool,
            this.removeElementTool,
            this.addTraceTool,
            this.removeTraceTool,
            this.runTool});
            this.toolStrip.Location = new System.Drawing.Point(0, 40);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1600, 42);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.LayoutCompleted += new System.EventHandler(this.toolStrip_LayoutCompleted);
            // 
            // saveTool
            // 
            this.saveTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveTool.Image = ((System.Drawing.Image)(resources.GetObject("saveTool.Image")));
            this.saveTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveTool.Name = "saveTool";
            this.saveTool.Size = new System.Drawing.Size(46, 36);
            this.saveTool.Text = "toolStripButton1";
            // 
            // addElementTool
            // 
            this.addElementTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addElementTool.Image = global::TracingSystem.Properties.Resources.microchip;
            this.addElementTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addElementTool.Name = "addElementTool";
            this.addElementTool.Size = new System.Drawing.Size(46, 36);
            this.addElementTool.Text = "toolStripButton2";
            // 
            // removeElementTool
            // 
            this.removeElementTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeElementTool.Image = global::TracingSystem.Properties.Resources.deletemicrochip;
            this.removeElementTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeElementTool.Name = "removeElementTool";
            this.removeElementTool.Size = new System.Drawing.Size(46, 36);
            this.removeElementTool.Text = "toolStripButton3";
            // 
            // addTraceTool
            // 
            this.addTraceTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTraceTool.Image = global::TracingSystem.Properties.Resources.trace;
            this.addTraceTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTraceTool.Name = "addTraceTool";
            this.addTraceTool.Size = new System.Drawing.Size(46, 36);
            this.addTraceTool.Text = "toolStripButton1";
            // 
            // removeTraceTool
            // 
            this.removeTraceTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeTraceTool.Image = global::TracingSystem.Properties.Resources.deletetrace;
            this.removeTraceTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeTraceTool.Name = "removeTraceTool";
            this.removeTraceTool.Size = new System.Drawing.Size(46, 36);
            this.removeTraceTool.Text = "toolStripButton1";
            // 
            // runTool
            // 
            this.runTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runTool.Image = global::TracingSystem.Properties.Resources.run;
            this.runTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runTool.Name = "runTool";
            this.runTool.Size = new System.Drawing.Size(46, 36);
            this.runTool.Text = "toolStripButton1";
            // 
            // status
            // 
            this.status.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xStatus,
            this.yStatus,
            this.statusLabel,
            this.progressBarStatus,
            this.projectNameStatus});
            this.status.Location = new System.Drawing.Point(0, 992);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1600, 42);
            this.status.TabIndex = 2;
            this.status.Text = "statusStrip1";
            // 
            // xStatus
            // 
            this.xStatus.Margin = new System.Windows.Forms.Padding(100, 6, 0, 4);
            this.xStatus.Name = "xStatus";
            this.xStatus.Size = new System.Drawing.Size(40, 32);
            this.xStatus.Text = "X: ";
            // 
            // yStatus
            // 
            this.yStatus.Name = "yStatus";
            this.yStatus.Size = new System.Drawing.Size(39, 32);
            this.yStatus.Text = "Y: ";
            // 
            // statusLabel
            // 
            this.statusLabel.Margin = new System.Windows.Forms.Padding(300, 6, 0, 4);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(166, 32);
            this.statusLabel.Text = "Выполнение: ";
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(300, 30);
            // 
            // projectNameStatus
            // 
            this.projectNameStatus.Margin = new System.Windows.Forms.Padding(50, 6, 0, 4);
            this.projectNameStatus.Name = "projectNameStatus";
            this.projectNameStatus.Size = new System.Drawing.Size(228, 32);
            this.projectNameStatus.Text = "Название проекта: ";
            // 
            // workSpace
            // 
            this.workSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workSpace.BackColor = System.Drawing.Color.Gainsboro;
            this.workSpace.Location = new System.Drawing.Point(25, 107);
            this.workSpace.Margin = new System.Windows.Forms.Padding(0);
            this.workSpace.Name = "workSpace";
            this.workSpace.Size = new System.Drawing.Size(1550, 860);
            this.workSpace.TabIndex = 3;
            this.workSpace.TabStop = false;
            this.workSpace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.workSpace_MouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 1034);
            this.Controls.Add(this.workSpace);
            this.Controls.Add(this.status);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(1003, 786);
            this.Name = "MainForm";
            this.Text = "Трассировка и расслоение";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workSpace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem projectMenu;
        private ToolStripMenuItem createProjectMenu;
        private ToolStripMenuItem openProjectMenu;
        private ToolStripMenuItem closeProjectMenu;
        private ToolStripMenuItem projectDetailsMenu;
        private ToolStripMenuItem pcbDetailsMenu;
        private ToolStripMenuItem runMenu;
        private ToolStripMenuItem runTraceMenu;
        private ToolStripMenuItem runBundleMenu;
        private ToolStrip toolStrip;
        private ToolStripButton saveTool;
        private ToolStripButton addElementTool;
        private ToolStripButton removeElementTool;
        private StatusStrip status;
        private ToolStripStatusLabel xStatus;
        private ToolStripStatusLabel yStatus;
        private ToolStripButton addTraceTool;
        private ToolStripMenuItem editMenu;
        private ToolStripMenuItem addElementMenu;
        private ToolStripMenuItem addTraceMenu;
        private ToolStripButton removeTraceTool;
        private PictureBox workSpace;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar progressBarStatus;
        private ToolStripMenuItem removeElementMenu;
        private ToolStripButton runTool;
        private ToolStripMenuItem saveProjectMenu;
        private ToolStripMenuItem saveAsProjectMenu;
        private ToolStripMenuItem removeProjectMenu;
        private ToolStripMenuItem closeProgramProjectMenu;
        private ToolStripMenuItem removeTraceMenu;
        private ToolStripMenuItem openPCBMenu;
        private ToolStripMenuItem settingsMenu;
        private ToolStripStatusLabel projectNameStatus;
    }
}