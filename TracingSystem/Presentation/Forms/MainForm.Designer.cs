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
            menuStrip1 = new MenuStrip();
            projectMenu = new ToolStripMenuItem();
            createProjectMenu = new ToolStripMenuItem();
            openProjectMenu = new ToolStripMenuItem();
            closeProjectMenu = new ToolStripMenuItem();
            saveProjectMenu = new ToolStripMenuItem();
            saveAsProjectMenu = new ToolStripMenuItem();
            removeProjectMenu = new ToolStripMenuItem();
            closeProgramProjectMenu = new ToolStripMenuItem();
            projectDetailsMenu = new ToolStripMenuItem();
            pcbDetailsMenu = new ToolStripMenuItem();
            editMenu = new ToolStripMenuItem();
            addElementMenu = new ToolStripMenuItem();
            addTraceMenu = new ToolStripMenuItem();
            removeElementMenu = new ToolStripMenuItem();
            removeTraceMenu = new ToolStripMenuItem();
            addLayerMenu = new ToolStripMenuItem();
            openPcbLib = new ToolStripMenuItem();
            changeProjectNameMenu = new ToolStripMenuItem();
            addPcbToolStripMenuItem = new ToolStripMenuItem();
            deletePcbToolStripMenuItem = new ToolStripMenuItem();
            changePcbNameToolStripMenuItem = new ToolStripMenuItem();
            runMenu = new ToolStripMenuItem();
            runTraceMenu = new ToolStripMenuItem();
            runBundleMenu = new ToolStripMenuItem();
            settingsMenu = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            saveTool = new ToolStripButton();
            addElementTool = new ToolStripButton();
            removeElementTool = new ToolStripButton();
            addTraceTool = new ToolStripButton();
            removeTraceTool = new ToolStripButton();
            runTool = new ToolStripButton();
            toolStripChoosePcb = new ToolStripComboBox();
            status = new StatusStrip();
            xStatus = new ToolStripStatusLabel();
            yStatus = new ToolStripStatusLabel();
            statusLabel = new ToolStripStatusLabel();
            progressBarStatus = new ToolStripProgressBar();
            projectNameStatus = new ToolStripStatusLabel();
            workSpace = new PictureBox();
            menuStrip1.SuspendLayout();
            toolStrip.SuspendLayout();
            status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)workSpace).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { projectMenu, projectDetailsMenu, editMenu, runMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(3, 1, 0, 1);
            menuStrip1.Size = new Size(862, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // projectMenu
            // 
            projectMenu.DropDownItems.AddRange(new ToolStripItem[] { createProjectMenu, openProjectMenu, closeProjectMenu, saveProjectMenu, saveAsProjectMenu, removeProjectMenu, closeProgramProjectMenu });
            projectMenu.Name = "projectMenu";
            projectMenu.Size = new Size(59, 22);
            projectMenu.Text = "Проект";
            // 
            // createProjectMenu
            // 
            createProjectMenu.Name = "createProjectMenu";
            createProjectMenu.Size = new Size(163, 22);
            createProjectMenu.Text = "Создать...";
            createProjectMenu.Click += createProjectMenu_Click;
            // 
            // openProjectMenu
            // 
            openProjectMenu.Name = "openProjectMenu";
            openProjectMenu.Size = new Size(163, 22);
            openProjectMenu.Text = "Открыть...";
            openProjectMenu.Click += openProjectMenu_Click;
            // 
            // closeProjectMenu
            // 
            closeProjectMenu.Name = "closeProjectMenu";
            closeProjectMenu.Size = new Size(163, 22);
            closeProjectMenu.Text = "Закрыть";
            closeProjectMenu.Click += closeProjectMenu_Click;
            // 
            // saveProjectMenu
            // 
            saveProjectMenu.Name = "saveProjectMenu";
            saveProjectMenu.Size = new Size(163, 22);
            saveProjectMenu.Text = "Сохранить";
            saveProjectMenu.Click += saveProjectMenu_Click;
            // 
            // saveAsProjectMenu
            // 
            saveAsProjectMenu.Name = "saveAsProjectMenu";
            saveAsProjectMenu.Size = new Size(163, 22);
            saveAsProjectMenu.Text = "Сохранить как...";
            saveAsProjectMenu.Click += saveAsProjectMenu_Click;
            // 
            // removeProjectMenu
            // 
            removeProjectMenu.Name = "removeProjectMenu";
            removeProjectMenu.Size = new Size(163, 22);
            removeProjectMenu.Text = "Удалить";
            removeProjectMenu.Click += removeProjectMenu_Click;
            // 
            // closeProgramProjectMenu
            // 
            closeProgramProjectMenu.Name = "closeProgramProjectMenu";
            closeProgramProjectMenu.Size = new Size(163, 22);
            closeProgramProjectMenu.Text = "Выход";
            closeProgramProjectMenu.Click += closeProgramProjectMenu_Click;
            // 
            // projectDetailsMenu
            // 
            projectDetailsMenu.DropDownItems.AddRange(new ToolStripItem[] { pcbDetailsMenu });
            projectDetailsMenu.Name = "projectDetailsMenu";
            projectDetailsMenu.Size = new Size(125, 22);
            projectDetailsMenu.Text = "Проектные данные";
            // 
            // pcbDetailsMenu
            // 
            pcbDetailsMenu.Name = "pcbDetailsMenu";
            pcbDetailsMenu.Size = new Size(225, 22);
            pcbDetailsMenu.Text = "Данные о печатной плате...";
            pcbDetailsMenu.Click += pcbDetailsMenu_Click;
            // 
            // editMenu
            // 
            editMenu.DropDownItems.AddRange(new ToolStripItem[] { addElementMenu, addTraceMenu, removeElementMenu, removeTraceMenu, addLayerMenu, openPcbLib, changeProjectNameMenu, addPcbToolStripMenuItem, deletePcbToolStripMenuItem, changePcbNameToolStripMenuItem });
            editMenu.Name = "editMenu";
            editMenu.Size = new Size(99, 22);
            editMenu.Text = "Редактировать";
            // 
            // addElementMenu
            // 
            addElementMenu.Name = "addElementMenu";
            addElementMenu.Size = new Size(228, 22);
            addElementMenu.Text = "Добавить элемент";
            addElementMenu.Click += addElementMenu_Click;
            // 
            // addTraceMenu
            // 
            addTraceMenu.Name = "addTraceMenu";
            addTraceMenu.Size = new Size(228, 22);
            addTraceMenu.Text = "Добавить трассу";
            // 
            // removeElementMenu
            // 
            removeElementMenu.Name = "removeElementMenu";
            removeElementMenu.Size = new Size(228, 22);
            removeElementMenu.Text = "Удалить элемент";
            removeElementMenu.Click += removeElementMenu_Click;
            // 
            // removeTraceMenu
            // 
            removeTraceMenu.Name = "removeTraceMenu";
            removeTraceMenu.Size = new Size(228, 22);
            removeTraceMenu.Text = "Удалить трассу";
            // 
            // addLayerMenu
            // 
            addLayerMenu.Name = "addLayerMenu";
            addLayerMenu.Size = new Size(228, 22);
            addLayerMenu.Text = "Добавить слой";
            // 
            // openPcbLib
            // 
            openPcbLib.Name = "openPcbLib";
            openPcbLib.Size = new Size(228, 22);
            openPcbLib.Text = "Открыть файл PCBLIB...";
            openPcbLib.Click += openPcbLib_Click;
            // 
            // changeProjectNameMenu
            // 
            changeProjectNameMenu.Name = "changeProjectNameMenu";
            changeProjectNameMenu.Size = new Size(228, 22);
            changeProjectNameMenu.Text = "Изменить название проекта";
            changeProjectNameMenu.Click += changeProjectNameMenu_Click;
            // 
            // addPcbToolStripMenuItem
            // 
            addPcbToolStripMenuItem.Name = "addPcbToolStripMenuItem";
            addPcbToolStripMenuItem.Size = new Size(228, 22);
            addPcbToolStripMenuItem.Text = "Добавить плату";
            addPcbToolStripMenuItem.Click += addPcbToolStripMenuItem_Click;
            // 
            // deletePcbToolStripMenuItem
            // 
            deletePcbToolStripMenuItem.Name = "deletePcbToolStripMenuItem";
            deletePcbToolStripMenuItem.Size = new Size(228, 22);
            deletePcbToolStripMenuItem.Text = "Удалить плату";
            deletePcbToolStripMenuItem.Click += deletePcbToolStripMenuItem_Click;
            // 
            // changePcbNameToolStripMenuItem
            // 
            changePcbNameToolStripMenuItem.Name = "changePcbNameToolStripMenuItem";
            changePcbNameToolStripMenuItem.Size = new Size(228, 22);
            changePcbNameToolStripMenuItem.Text = "Изменить название платы";
            changePcbNameToolStripMenuItem.Click += changePcbNameToolStripMenuItem_Click;
            // 
            // runMenu
            // 
            runMenu.DropDownItems.AddRange(new ToolStripItem[] { runTraceMenu, runBundleMenu, settingsMenu });
            runMenu.Name = "runMenu";
            runMenu.Size = new Size(81, 22);
            runMenu.Text = "Выполнить";
            // 
            // runTraceMenu
            // 
            runTraceMenu.Name = "runTraceMenu";
            runTraceMenu.Size = new Size(208, 22);
            runTraceMenu.Text = "Выполнить трассировку";
            runTraceMenu.Click += runTraceMenu_Click;
            // 
            // runBundleMenu
            // 
            runBundleMenu.Name = "runBundleMenu";
            runBundleMenu.Size = new Size(208, 22);
            runBundleMenu.Text = "Выполнить расслоение";
            // 
            // settingsMenu
            // 
            settingsMenu.Name = "settingsMenu";
            settingsMenu.Size = new Size(208, 22);
            settingsMenu.Text = "Настройка алгоритма...";
            settingsMenu.Click += settingsMenu_Click;
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(32, 32);
            toolStrip.Items.AddRange(new ToolStripItem[] { saveTool, addElementTool, removeElementTool, addTraceTool, removeTraceTool, runTool, toolStripChoosePcb });
            toolStrip.Location = new Point(0, 24);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(862, 39);
            toolStrip.TabIndex = 1;
            toolStrip.Text = "toolStrip1";
            // 
            // saveTool
            // 
            saveTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveTool.Image = (Image)resources.GetObject("saveTool.Image");
            saveTool.ImageTransparentColor = Color.Magenta;
            saveTool.Name = "saveTool";
            saveTool.Size = new Size(36, 36);
            saveTool.Text = "toolStripButton1";
            // 
            // addElementTool
            // 
            addElementTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            addElementTool.Image = Properties.Resources.microchip;
            addElementTool.ImageTransparentColor = Color.Magenta;
            addElementTool.Name = "addElementTool";
            addElementTool.Size = new Size(36, 36);
            addElementTool.Text = "toolStripButton2";
            // 
            // removeElementTool
            // 
            removeElementTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            removeElementTool.Image = Properties.Resources.deletemicrochip;
            removeElementTool.ImageTransparentColor = Color.Magenta;
            removeElementTool.Name = "removeElementTool";
            removeElementTool.Size = new Size(36, 36);
            removeElementTool.Text = "toolStripButton3";
            // 
            // addTraceTool
            // 
            addTraceTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            addTraceTool.Image = Properties.Resources.trace;
            addTraceTool.ImageTransparentColor = Color.Magenta;
            addTraceTool.Name = "addTraceTool";
            addTraceTool.Size = new Size(36, 36);
            addTraceTool.Text = "toolStripButton1";
            // 
            // removeTraceTool
            // 
            removeTraceTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            removeTraceTool.Image = Properties.Resources.deletetrace;
            removeTraceTool.ImageTransparentColor = Color.Magenta;
            removeTraceTool.Name = "removeTraceTool";
            removeTraceTool.Size = new Size(36, 36);
            removeTraceTool.Text = "toolStripButton1";
            // 
            // runTool
            // 
            runTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            runTool.Image = Properties.Resources.run;
            runTool.ImageTransparentColor = Color.Magenta;
            runTool.Name = "runTool";
            runTool.Size = new Size(36, 36);
            runTool.Text = "toolStripButton1";
            // 
            // toolStripChoosePcb
            // 
            toolStripChoosePcb.Name = "toolStripChoosePcb";
            toolStripChoosePcb.Size = new Size(121, 39);
            toolStripChoosePcb.Text = "Выбрать плату";
            toolStripChoosePcb.TextChanged += toolStripChoosePcb_TextChanged;
            // 
            // status
            // 
            status.ImageScalingSize = new Size(32, 32);
            status.Items.AddRange(new ToolStripItem[] { xStatus, yStatus, statusLabel, progressBarStatus, projectNameStatus });
            status.Location = new Point(0, 460);
            status.Name = "status";
            status.Padding = new Padding(1, 0, 8, 0);
            status.Size = new Size(862, 25);
            status.TabIndex = 2;
            status.Text = "statusStrip1";
            // 
            // xStatus
            // 
            xStatus.Margin = new Padding(100, 6, 0, 4);
            xStatus.Name = "xStatus";
            xStatus.Size = new Size(20, 15);
            xStatus.Text = "X: ";
            // 
            // yStatus
            // 
            yStatus.Name = "yStatus";
            yStatus.Size = new Size(20, 20);
            yStatus.Text = "Y: ";
            // 
            // statusLabel
            // 
            statusLabel.Margin = new Padding(300, 6, 0, 4);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(83, 15);
            statusLabel.Text = "Выполнение: ";
            // 
            // progressBarStatus
            // 
            progressBarStatus.Name = "progressBarStatus";
            progressBarStatus.Size = new Size(162, 19);
            // 
            // projectNameStatus
            // 
            projectNameStatus.Margin = new Padding(50, 6, 0, 4);
            projectNameStatus.Name = "projectNameStatus";
            projectNameStatus.Size = new Size(112, 15);
            projectNameStatus.Text = "Название проекта: ";
            // 
            // workSpace
            // 
            workSpace.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            workSpace.BackColor = Color.Gainsboro;
            workSpace.Location = new Point(13, 50);
            workSpace.Margin = new Padding(0);
            workSpace.Name = "workSpace";
            workSpace.Size = new Size(835, 403);
            workSpace.TabIndex = 3;
            workSpace.TabStop = false;
            workSpace.Paint += workSpace_Paint;
            workSpace.MouseMove += workSpace_MouseMove;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(862, 485);
            Controls.Add(workSpace);
            Controls.Add(status);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Трассировка и расслоение";
            SizeChanged += MainForm_SizeChanged;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            status.ResumeLayout(false);
            status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)workSpace).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem addLayerMenu;
        private ToolStripMenuItem settingsMenu;
        private ToolStripStatusLabel projectNameStatus;
        private ToolStripMenuItem openPcbLib;
        private ToolStripMenuItem changeProjectNameMenu;
        private ToolStripComboBox toolStripChoosePcb;
        private ToolStripMenuItem addPcbToolStripMenuItem;
        private ToolStripMenuItem deletePcbToolStripMenuItem;
        private ToolStripMenuItem changePcbNameToolStripMenuItem;
    }
}