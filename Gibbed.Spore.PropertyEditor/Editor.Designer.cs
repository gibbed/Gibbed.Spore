namespace Gibbed.Spore.PropertyEditor
{
	partial class Editor
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
			this.components = new System.ComponentModel.Container();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertyFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.propertyView = new System.Windows.Forms.TreeView();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.textTab = new System.Windows.Forms.TabPage();
			this.textTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textTableIdTextBox = new System.Windows.Forms.TextBox();
			this.textInstanceIdTextBox = new System.Windows.Forms.TextBox();
			this.textPlaceholderTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.activeFileLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.propertyFileBindingSource)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.textTab.SuspendLayout();
			this.textTableLayoutPanel.SuspendLayout();
			this.statusStrip1.SuspendLayout();
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
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.toolStripMenuItem1,
            this.exitMenuItem});
			this.fileMenuItem.Name = "fileMenuItem";
			this.fileMenuItem.Size = new System.Drawing.Size(40, 20);
			this.fileMenuItem.Text = "&File";
			// 
			// openMenuItem
			// 
			this.openMenuItem.Name = "openMenuItem";
			this.openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openMenuItem.Size = new System.Drawing.Size(151, 22);
			this.openMenuItem.Text = "&Open";
			this.openMenuItem.Click += new System.EventHandler(this.OnOpen);
			// 
			// saveMenuItem
			// 
			this.saveMenuItem.Name = "saveMenuItem";
			this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveMenuItem.Size = new System.Drawing.Size(151, 22);
			this.saveMenuItem.Text = "&Save";
			this.saveMenuItem.Click += new System.EventHandler(this.OnSave);
			// 
			// saveAsMenuItem
			// 
			this.saveAsMenuItem.Name = "saveAsMenuItem";
			this.saveAsMenuItem.Size = new System.Drawing.Size(151, 22);
			this.saveAsMenuItem.Text = "Save &As";
			this.saveAsMenuItem.Click += new System.EventHandler(this.OnSaveAs);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exitMenuItem.Text = "E&xit";
			// 
			// propertyFileBindingSource
			// 
			this.propertyFileBindingSource.DataSource = typeof(Gibbed.Spore.Properties.PropertyFile);
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 24);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.propertyView);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.tabControl);
			this.splitContainer.Size = new System.Drawing.Size(792, 406);
			this.splitContainer.SplitterDistance = 240;
			this.splitContainer.TabIndex = 1;
			// 
			// propertyView
			// 
			this.propertyView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyView.Location = new System.Drawing.Point(0, 0);
			this.propertyView.Name = "propertyView";
			this.propertyView.Size = new System.Drawing.Size(240, 406);
			this.propertyView.TabIndex = 0;
			this.propertyView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterNodeSelect);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.textTab);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(548, 406);
			this.tabControl.TabIndex = 0;
			// 
			// textTab
			// 
			this.textTab.Controls.Add(this.textTableLayoutPanel);
			this.textTab.Location = new System.Drawing.Point(4, 22);
			this.textTab.Name = "textTab";
			this.textTab.Padding = new System.Windows.Forms.Padding(3);
			this.textTab.Size = new System.Drawing.Size(540, 380);
			this.textTab.TabIndex = 0;
			this.textTab.Text = "Text";
			this.textTab.UseVisualStyleBackColor = true;
			// 
			// textTableLayoutPanel
			// 
			this.textTableLayoutPanel.ColumnCount = 2;
			this.textTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.textTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.textTableLayoutPanel.Controls.Add(this.label1, 0, 0);
			this.textTableLayoutPanel.Controls.Add(this.label2, 0, 1);
			this.textTableLayoutPanel.Controls.Add(this.textTableIdTextBox, 1, 0);
			this.textTableLayoutPanel.Controls.Add(this.textInstanceIdTextBox, 1, 1);
			this.textTableLayoutPanel.Controls.Add(this.textPlaceholderTextBox, 1, 2);
			this.textTableLayoutPanel.Controls.Add(this.label3, 0, 2);
			this.textTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
			this.textTableLayoutPanel.Name = "textTableLayoutPanel";
			this.textTableLayoutPanel.RowCount = 3;
			this.textTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.textTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.textTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.textTableLayoutPanel.Size = new System.Drawing.Size(534, 374);
			this.textTableLayoutPanel.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Table ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Instance ID";
			// 
			// textTableIdTextBox
			// 
			this.textTableIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textTableIdTextBox.Location = new System.Drawing.Point(163, 3);
			this.textTableIdTextBox.Name = "textTableIdTextBox";
			this.textTableIdTextBox.Size = new System.Drawing.Size(368, 20);
			this.textTableIdTextBox.TabIndex = 2;
			// 
			// textInstanceIdTextBox
			// 
			this.textInstanceIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textInstanceIdTextBox.Location = new System.Drawing.Point(163, 29);
			this.textInstanceIdTextBox.Name = "textInstanceIdTextBox";
			this.textInstanceIdTextBox.Size = new System.Drawing.Size(368, 20);
			this.textInstanceIdTextBox.TabIndex = 3;
			// 
			// textPlaceholderTextBox
			// 
			this.textPlaceholderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textPlaceholderTextBox.Location = new System.Drawing.Point(163, 55);
			this.textPlaceholderTextBox.Multiline = true;
			this.textPlaceholderTextBox.Name = "textPlaceholderTextBox";
			this.textPlaceholderTextBox.Size = new System.Drawing.Size(368, 316);
			this.textPlaceholderTextBox.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 15);
			this.label3.TabIndex = 5;
			this.label3.Text = "Placeholder Text";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Property File (*.prop)|*.prop|All Files (*.*)|*.*";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeFileLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 430);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(792, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip";
			// 
			// activeFileLabel
			// 
			this.activeFileLabel.Name = "activeFileLabel";
			this.activeFileLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// Editor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 452);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.statusStrip1);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "Editor";
			this.Text = "Property Editor";
			this.Load += new System.EventHandler(this.OnLoad);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.propertyFileBindingSource)).EndInit();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.textTab.ResumeLayout(false);
			this.textTableLayoutPanel.ResumeLayout(false);
			this.textTableLayoutPanel.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
		private System.Windows.Forms.BindingSource propertyFileBindingSource;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage textTab;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.TreeView propertyView;
		private System.Windows.Forms.TableLayoutPanel textTableLayoutPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textTableIdTextBox;
		private System.Windows.Forms.TextBox textInstanceIdTextBox;
		private System.Windows.Forms.TextBox textPlaceholderTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel activeFileLabel;
	}
}

