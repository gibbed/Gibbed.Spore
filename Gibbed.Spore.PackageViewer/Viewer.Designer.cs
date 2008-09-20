namespace Gibbed.Spore.PackageViewer
{
    partial class Viewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.typeList = new System.Windows.Forms.TreeView();
			this.fileList = new System.Windows.Forms.ListView();
			this.columnName = new System.Windows.Forms.ColumnHeader();
			this.columnGroup = new System.Windows.Forms.ColumnHeader();
			this.columnSize = new System.Windows.Forms.ColumnHeader();
			this.columnUnknown = new System.Windows.Forms.ColumnHeader();
			this.saveAllFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.menuStrip.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(792, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.toolStripMenuItem1,
            this.saveAllToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
			this.fileMenuItem.Name = "fileMenuItem";
			this.fileMenuItem.Size = new System.Drawing.Size(40, 20);
			this.fileMenuItem.Text = "&File";
			// 
			// openMenuItem
			// 
			this.openMenuItem.Name = "openMenuItem";
			this.openMenuItem.Size = new System.Drawing.Size(122, 22);
			this.openMenuItem.Text = "&Open";
			this.openMenuItem.Click += new System.EventHandler(this.OnOpen);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(119, 6);
			// 
			// saveAllToolStripMenuItem
			// 
			this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
			this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.saveAllToolStripMenuItem.Text = "Save &All";
			this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAll);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(119, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// openDialog
			// 
			this.openDialog.Filter = "All Spore Packages|*.package;*.spo;*.db;*.dat;*.pld;*.pkp;*.pkt|Spore Packages (*" +
				".package)|*.package|Spore Save Packages|*.spo;*.db;*.dat;*.pld;*.pkp;*.pkt|All F" +
				"iles (*.*)|*.*";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.typeList);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.fileList);
			this.splitContainer1.Size = new System.Drawing.Size(792, 428);
			this.splitContainer1.SplitterDistance = 240;
			this.splitContainer1.TabIndex = 1;
			// 
			// typeList
			// 
			this.typeList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.typeList.Location = new System.Drawing.Point(0, 0);
			this.typeList.Name = "typeList";
			this.typeList.Size = new System.Drawing.Size(240, 428);
			this.typeList.TabIndex = 0;
			this.typeList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnSelectType);
			// 
			// fileList
			// 
			this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnGroup,
            this.columnSize,
            this.columnUnknown});
			this.fileList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileList.Font = new System.Drawing.Font("Courier New", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fileList.FullRowSelect = true;
			this.fileList.Location = new System.Drawing.Point(0, 0);
			this.fileList.Name = "fileList";
			this.fileList.Size = new System.Drawing.Size(548, 428);
			this.fileList.TabIndex = 0;
			this.fileList.UseCompatibleStateImageBehavior = false;
			this.fileList.View = System.Windows.Forms.View.Details;
			// 
			// columnName
			// 
			this.columnName.Text = "Name";
			this.columnName.Width = 260;
			// 
			// columnGroup
			// 
			this.columnGroup.Text = "Group";
			this.columnGroup.Width = 100;
			// 
			// columnSize
			// 
			this.columnSize.Text = "Size";
			this.columnSize.Width = 80;
			// 
			// columnUnknown
			// 
			this.columnUnknown.Text = "Unknown";
			this.columnUnknown.Width = 80;
			// 
			// Viewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 452);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "Viewer";
			this.Text = "Package Viewer";
			this.Load += new System.EventHandler(this.OnLoad);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView typeList;
		private System.Windows.Forms.ListView fileList;
		private System.Windows.Forms.ColumnHeader columnName;
		private System.Windows.Forms.ColumnHeader columnSize;
		private System.Windows.Forms.ColumnHeader columnGroup;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog saveAllFolderDialog;
		private System.Windows.Forms.ColumnHeader columnUnknown;
    }
}

