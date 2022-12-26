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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.projectDetailsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addTraceCriteriaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addRestrictionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addElementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addTraceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.removeElementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTraceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runTraceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runBundleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveTool = new System.Windows.Forms.ToolStripButton();
            this.addElementTool = new System.Windows.Forms.ToolStripButton();
            this.removeElementTool = new System.Windows.Forms.ToolStripButton();
            this.addTraceTool = new System.Windows.Forms.ToolStripButton();
            this.removeTraceTool = new System.Windows.Forms.ToolStripButton();
            this.status = new System.Windows.Forms.StatusStrip();
            this.xStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.yStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.workSpace = new System.Windows.Forms.PictureBox();
            this.открытьФайлPCBLIBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workSpace)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.projectDetailsMenu,
            this.addMenu,
            this.removeMenu,
            this.runMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1427, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectMenu,
            this.createProjectMenu,
            this.saveProjectMenu});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 36);
            this.toolStripMenuItem1.Text = "Проект";
            // 
            // openProjectMenu
            // 
            this.openProjectMenu.Name = "openProjectMenu";
            this.openProjectMenu.Size = new System.Drawing.Size(359, 44);
            this.openProjectMenu.Text = "Открыть";
            // 
            // createProjectMenu
            // 
            this.createProjectMenu.Name = "createProjectMenu";
            this.createProjectMenu.Size = new System.Drawing.Size(359, 44);
            this.createProjectMenu.Text = "Создать";
            // 
            // saveProjectMenu
            // 
            this.saveProjectMenu.Name = "saveProjectMenu";
            this.saveProjectMenu.Size = new System.Drawing.Size(359, 44);
            this.saveProjectMenu.Text = "Сохранить";
            // 
            // projectDetailsMenu
            // 
            this.projectDetailsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTraceCriteriaMenu,
            this.addRestrictionsMenu});
            this.projectDetailsMenu.Name = "projectDetailsMenu";
            this.projectDetailsMenu.Size = new System.Drawing.Size(248, 36);
            this.projectDetailsMenu.Text = "Проектные данные";
            // 
            // addTraceCriteriaMenu
            // 
            this.addTraceCriteriaMenu.Name = "addTraceCriteriaMenu";
            this.addTraceCriteriaMenu.Size = new System.Drawing.Size(498, 44);
            this.addTraceCriteriaMenu.Text = "Выбрать критерии трассировки";
            // 
            // addRestrictionsMenu
            // 
            this.addRestrictionsMenu.Name = "addRestrictionsMenu";
            this.addRestrictionsMenu.Size = new System.Drawing.Size(498, 44);
            this.addRestrictionsMenu.Text = "Выбрать ограничения";
            // 
            // addMenu
            // 
            this.addMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addElementMenu,
            this.addTraceMenu,
            this.открытьФайлPCBLIBToolStripMenuItem});
            this.addMenu.Name = "addMenu";
            this.addMenu.Size = new System.Drawing.Size(140, 36);
            this.addMenu.Text = "Добавить";
            // 
            // addElementMenu
            // 
            this.addElementMenu.Name = "addElementMenu";
            this.addElementMenu.Size = new System.Drawing.Size(382, 44);
            this.addElementMenu.Text = "Добавить элемент";
            // 
            // addTraceMenu
            // 
            this.addTraceMenu.Name = "addTraceMenu";
            this.addTraceMenu.Size = new System.Drawing.Size(382, 44);
            this.addTraceMenu.Text = "Добавить трассу";
            // 
            // removeMenu
            // 
            this.removeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeElementMenu,
            this.removeTraceMenu});
            this.removeMenu.Name = "removeMenu";
            this.removeMenu.Size = new System.Drawing.Size(122, 36);
            this.removeMenu.Text = "Удалить";
            // 
            // removeElementMenu
            // 
            this.removeElementMenu.Name = "removeElementMenu";
            this.removeElementMenu.Size = new System.Drawing.Size(359, 44);
            this.removeElementMenu.Text = "Удалить элемент";
            // 
            // removeTraceMenu
            // 
            this.removeTraceMenu.Name = "removeTraceMenu";
            this.removeTraceMenu.Size = new System.Drawing.Size(359, 44);
            this.removeTraceMenu.Text = "Удалить трассу";
            // 
            // runMenu
            // 
            this.runMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runTraceMenu,
            this.runBundleMenu});
            this.runMenu.Name = "runMenu";
            this.runMenu.Size = new System.Drawing.Size(156, 36);
            this.runMenu.Text = "Выполнить";
            // 
            // runTraceMenu
            // 
            this.runTraceMenu.Name = "runTraceMenu";
            this.runTraceMenu.Size = new System.Drawing.Size(413, 44);
            this.runTraceMenu.Text = "Выполнить трассировку";
            // 
            // runBundleMenu
            // 
            this.runBundleMenu.Name = "runBundleMenu";
            this.runBundleMenu.Size = new System.Drawing.Size(413, 44);
            this.runBundleMenu.Text = "Выполнить расслоение";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTool,
            this.addElementTool,
            this.removeElementTool,
            this.addTraceTool,
            this.removeTraceTool});
            this.toolStrip1.Location = new System.Drawing.Point(0, 40);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1427, 42);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
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
            this.addElementTool.Image = ((System.Drawing.Image)(resources.GetObject("addElementTool.Image")));
            this.addElementTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addElementTool.Name = "addElementTool";
            this.addElementTool.Size = new System.Drawing.Size(46, 36);
            this.addElementTool.Text = "toolStripButton2";
            // 
            // removeElementTool
            // 
            this.removeElementTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeElementTool.Image = ((System.Drawing.Image)(resources.GetObject("removeElementTool.Image")));
            this.removeElementTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeElementTool.Name = "removeElementTool";
            this.removeElementTool.Size = new System.Drawing.Size(46, 36);
            this.removeElementTool.Text = "toolStripButton3";
            // 
            // addTraceTool
            // 
            this.addTraceTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTraceTool.Image = ((System.Drawing.Image)(resources.GetObject("addTraceTool.Image")));
            this.addTraceTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTraceTool.Name = "addTraceTool";
            this.addTraceTool.Size = new System.Drawing.Size(46, 36);
            this.addTraceTool.Text = "toolStripButton1";
            // 
            // removeTraceTool
            // 
            this.removeTraceTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeTraceTool.Image = ((System.Drawing.Image)(resources.GetObject("removeTraceTool.Image")));
            this.removeTraceTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeTraceTool.Name = "removeTraceTool";
            this.removeTraceTool.Size = new System.Drawing.Size(46, 36);
            this.removeTraceTool.Text = "toolStripButton1";
            // 
            // status
            // 
            this.status.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xStatus,
            this.yStatus,
            this.statusLabel,
            this.toolStripProgressBar1});
            this.status.Location = new System.Drawing.Point(0, 986);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1427, 42);
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
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 30);
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
            this.workSpace.Size = new System.Drawing.Size(1377, 854);
            this.workSpace.TabIndex = 3;
            this.workSpace.TabStop = false;
            this.workSpace.MouseMove += new System.Windows.Forms.MouseEventHandler(this.workSpace_MouseMove);
            // 
            // открытьФайлPCBLIBToolStripMenuItem
            // 
            this.открытьФайлPCBLIBToolStripMenuItem.Name = "открытьФайлPCBLIBToolStripMenuItem";
            this.открытьФайлPCBLIBToolStripMenuItem.Size = new System.Drawing.Size(382, 44);
            this.открытьФайлPCBLIBToolStripMenuItem.Text = "Открыть файл PCBLIB";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1427, 1028);
            this.Controls.Add(this.workSpace);
            this.Controls.Add(this.status);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(817, 768);
            this.Name = "MainForm";
            this.Text = "Трассировка и расслоение";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workSpace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem openProjectMenu;
        private ToolStripMenuItem createProjectMenu;
        private ToolStripMenuItem saveProjectMenu;
        private ToolStripMenuItem projectDetailsMenu;
        private ToolStripMenuItem addTraceCriteriaMenu;
        private ToolStripMenuItem runMenu;
        private ToolStripMenuItem runTraceMenu;
        private ToolStripMenuItem runBundleMenu;
        private ToolStrip toolStrip1;
        private ToolStripButton saveTool;
        private ToolStripButton addElementTool;
        private ToolStripButton removeElementTool;
        private StatusStrip status;
        private ToolStripStatusLabel xStatus;
        private ToolStripStatusLabel yStatus;
        private ToolStripMenuItem addRestrictionsMenu;
        private ToolStripButton addTraceTool;
        private ToolStripMenuItem addMenu;
        private ToolStripMenuItem addElementMenu;
        private ToolStripMenuItem addTraceMenu;
        private ToolStripMenuItem removeMenu;
        private ToolStripMenuItem removeElementMenu;
        private ToolStripMenuItem removeTraceMenu;
        private ToolStripButton removeTraceTool;
        private PictureBox workSpace;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripMenuItem открытьФайлPCBLIBToolStripMenuItem;
    }
}