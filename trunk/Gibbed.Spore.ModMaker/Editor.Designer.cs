namespace Gibbed.Spore.ModMaker
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
			this.detailsSplitter = new System.Windows.Forms.SplitContainer();
			this.modDetailsPanel = new System.Windows.Forms.TableLayoutPanel();
			this.nameLabel = new System.Windows.Forms.Label();
			this.nameText = new System.Windows.Forms.TextBox();
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.authorLabel = new System.Windows.Forms.Label();
			this.authorText = new System.Windows.Forms.TextBox();
			this.emailLabel = new System.Windows.Forms.Label();
			this.emailText = new System.Windows.Forms.TextBox();
			this.websiteLabel = new System.Windows.Forms.Label();
			this.websiteText = new System.Windows.Forms.TextBox();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.descriptionText = new System.Windows.Forms.TextBox();
			this.versionText = new System.Windows.Forms.TextBox();
			this.versionLabel = new System.Windows.Forms.Label();
			this.fileList = new System.Windows.Forms.ListBox();
			this.filesBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.filesToolStrip = new System.Windows.Forms.ToolStrip();
			this.addFileButton = new System.Windows.Forms.ToolStripSplitButton();
			this.addFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFileButton = new System.Windows.Forms.ToolStripButton();
			this.fileDetailsPanel = new System.Windows.Forms.TableLayoutPanel();
			this.typeIdText = new System.Windows.Forms.TextBox();
			this.groupIdText = new System.Windows.Forms.TextBox();
			this.instanceIdText = new System.Windows.Forms.TextBox();
			this.instanceIdLabel = new System.Windows.Forms.Label();
			this.groupIdLabel = new System.Windows.Forms.Label();
			this.typeIdLabel = new System.Windows.Forms.Label();
			this.editorToolStrip = new System.Windows.Forms.ToolStrip();
			this.newModButton = new System.Windows.Forms.ToolStripButton();
			this.openModButton = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveModButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.buildPackageButton = new System.Windows.Forms.ToolStripButton();
			this.openModDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveModDialog = new System.Windows.Forms.SaveFileDialog();
			this.addFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.addDirectoryDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.savePackageDialog = new System.Windows.Forms.SaveFileDialog();
			this.detailsSplitter.Panel1.SuspendLayout();
			this.detailsSplitter.Panel2.SuspendLayout();
			this.detailsSplitter.SuspendLayout();
			this.modDetailsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).BeginInit();
			this.filesToolStrip.SuspendLayout();
			this.fileDetailsPanel.SuspendLayout();
			this.editorToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// detailsSplitter
			// 
			resources.ApplyResources(this.detailsSplitter, "detailsSplitter");
			this.detailsSplitter.Name = "detailsSplitter";
			// 
			// detailsSplitter.Panel1
			// 
			this.detailsSplitter.Panel1.Controls.Add(this.modDetailsPanel);
			// 
			// detailsSplitter.Panel2
			// 
			this.detailsSplitter.Panel2.Controls.Add(this.fileList);
			this.detailsSplitter.Panel2.Controls.Add(this.filesToolStrip);
			this.detailsSplitter.Panel2.Controls.Add(this.fileDetailsPanel);
			// 
			// modDetailsPanel
			// 
			resources.ApplyResources(this.modDetailsPanel, "modDetailsPanel");
			this.modDetailsPanel.Controls.Add(this.nameLabel, 0, 0);
			this.modDetailsPanel.Controls.Add(this.nameText, 1, 0);
			this.modDetailsPanel.Controls.Add(this.authorLabel, 0, 1);
			this.modDetailsPanel.Controls.Add(this.authorText, 1, 1);
			this.modDetailsPanel.Controls.Add(this.emailLabel, 0, 2);
			this.modDetailsPanel.Controls.Add(this.emailText, 1, 2);
			this.modDetailsPanel.Controls.Add(this.websiteLabel, 0, 3);
			this.modDetailsPanel.Controls.Add(this.websiteText, 1, 3);
			this.modDetailsPanel.Controls.Add(this.descriptionLabel, 0, 4);
			this.modDetailsPanel.Controls.Add(this.descriptionText, 1, 4);
			this.modDetailsPanel.Controls.Add(this.versionText, 1, 5);
			this.modDetailsPanel.Controls.Add(this.versionLabel, 0, 5);
			this.modDetailsPanel.Name = "modDetailsPanel";
			// 
			// nameLabel
			// 
			resources.ApplyResources(this.nameLabel, "nameLabel");
			this.nameLabel.Name = "nameLabel";
			// 
			// nameText
			// 
			this.nameText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Name", true));
			resources.ApplyResources(this.nameText, "nameText");
			this.nameText.Name = "nameText";
			// 
			// bindingSource
			// 
			this.bindingSource.DataSource = typeof(Gibbed.Spore.ModMaker.Modification);
			// 
			// authorLabel
			// 
			resources.ApplyResources(this.authorLabel, "authorLabel");
			this.authorLabel.Name = "authorLabel";
			// 
			// authorText
			// 
			this.authorText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Author", true));
			resources.ApplyResources(this.authorText, "authorText");
			this.authorText.Name = "authorText";
			// 
			// emailLabel
			// 
			resources.ApplyResources(this.emailLabel, "emailLabel");
			this.emailLabel.Name = "emailLabel";
			// 
			// emailText
			// 
			this.emailText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Email", true));
			resources.ApplyResources(this.emailText, "emailText");
			this.emailText.Name = "emailText";
			// 
			// websiteLabel
			// 
			resources.ApplyResources(this.websiteLabel, "websiteLabel");
			this.websiteLabel.Name = "websiteLabel";
			// 
			// websiteText
			// 
			this.websiteText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Website", true));
			resources.ApplyResources(this.websiteText, "websiteText");
			this.websiteText.Name = "websiteText";
			// 
			// descriptionLabel
			// 
			resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
			this.descriptionLabel.Name = "descriptionLabel";
			// 
			// descriptionText
			// 
			this.descriptionText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Description", true));
			resources.ApplyResources(this.descriptionText, "descriptionText");
			this.descriptionText.Name = "descriptionText";
			// 
			// versionText
			// 
			this.versionText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Version", true));
			resources.ApplyResources(this.versionText, "versionText");
			this.versionText.Name = "versionText";
			// 
			// versionLabel
			// 
			resources.ApplyResources(this.versionLabel, "versionLabel");
			this.versionLabel.Name = "versionLabel";
			// 
			// fileList
			// 
			this.fileList.DataSource = this.filesBindingSource;
			resources.ApplyResources(this.fileList, "fileList");
			this.fileList.FormattingEnabled = true;
			this.fileList.Name = "fileList";
			// 
			// filesBindingSource
			// 
			this.filesBindingSource.DataMember = "Files";
			this.filesBindingSource.DataSource = this.bindingSource;
			// 
			// filesToolStrip
			// 
			this.filesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileButton,
            this.removeFileButton});
			resources.ApplyResources(this.filesToolStrip, "filesToolStrip");
			this.filesToolStrip.Name = "filesToolStrip";
			// 
			// addFileButton
			// 
			this.addFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addFileButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileMenuItem,
            this.addDirectoryMenuItem});
			this.addFileButton.Image = global::Gibbed.Spore.ModMaker.Properties.Resources.AddFile;
			resources.ApplyResources(this.addFileButton, "addFileButton");
			this.addFileButton.Name = "addFileButton";
			this.addFileButton.ButtonClick += new System.EventHandler(this.OnFileAdd);
			// 
			// addFileMenuItem
			// 
			this.addFileMenuItem.Name = "addFileMenuItem";
			resources.ApplyResources(this.addFileMenuItem, "addFileMenuItem");
			this.addFileMenuItem.Click += new System.EventHandler(this.OnFileAdd);
			// 
			// addDirectoryMenuItem
			// 
			this.addDirectoryMenuItem.Name = "addDirectoryMenuItem";
			resources.ApplyResources(this.addDirectoryMenuItem, "addDirectoryMenuItem");
			this.addDirectoryMenuItem.Click += new System.EventHandler(this.OnFileAddDirectory);
			// 
			// removeFileButton
			// 
			this.removeFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.removeFileButton.Image = global::Gibbed.Spore.ModMaker.Properties.Resources.RemoveFile;
			resources.ApplyResources(this.removeFileButton, "removeFileButton");
			this.removeFileButton.Name = "removeFileButton";
			this.removeFileButton.Click += new System.EventHandler(this.OnFileRemove);
			// 
			// fileDetailsPanel
			// 
			resources.ApplyResources(this.fileDetailsPanel, "fileDetailsPanel");
			this.fileDetailsPanel.Controls.Add(this.typeIdText, 0, 1);
			this.fileDetailsPanel.Controls.Add(this.groupIdText, 0, 1);
			this.fileDetailsPanel.Controls.Add(this.instanceIdText, 0, 1);
			this.fileDetailsPanel.Controls.Add(this.instanceIdLabel, 0, 0);
			this.fileDetailsPanel.Controls.Add(this.groupIdLabel, 1, 0);
			this.fileDetailsPanel.Controls.Add(this.typeIdLabel, 2, 0);
			this.fileDetailsPanel.Name = "fileDetailsPanel";
			// 
			// typeIdText
			// 
			this.typeIdText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "InstanceId", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "X8"));
			resources.ApplyResources(this.typeIdText, "typeIdText");
			this.typeIdText.Name = "typeIdText";
			// 
			// groupIdText
			// 
			this.groupIdText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "GroupId", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "X8"));
			resources.ApplyResources(this.groupIdText, "groupIdText");
			this.groupIdText.Name = "groupIdText";
			// 
			// instanceIdText
			// 
			this.instanceIdText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.filesBindingSource, "TypeId", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "X8"));
			resources.ApplyResources(this.instanceIdText, "instanceIdText");
			this.instanceIdText.Name = "instanceIdText";
			// 
			// instanceIdLabel
			// 
			resources.ApplyResources(this.instanceIdLabel, "instanceIdLabel");
			this.instanceIdLabel.Name = "instanceIdLabel";
			// 
			// groupIdLabel
			// 
			resources.ApplyResources(this.groupIdLabel, "groupIdLabel");
			this.groupIdLabel.Name = "groupIdLabel";
			// 
			// typeIdLabel
			// 
			resources.ApplyResources(this.typeIdLabel, "typeIdLabel");
			this.typeIdLabel.Name = "typeIdLabel";
			// 
			// editorToolStrip
			// 
			this.editorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newModButton,
            this.openModButton,
            this.toolStripSeparator1,
            this.saveModButton,
            this.toolStripSeparator2,
            this.buildPackageButton});
			resources.ApplyResources(this.editorToolStrip, "editorToolStrip");
			this.editorToolStrip.Name = "editorToolStrip";
			// 
			// newModButton
			// 
			this.newModButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newModButton.Image = global::Gibbed.Spore.ModMaker.Properties.Resources.NewModification;
			resources.ApplyResources(this.newModButton, "newModButton");
			this.newModButton.Name = "newModButton";
			this.newModButton.Click += new System.EventHandler(this.OnModNew);
			// 
			// openModButton
			// 
			this.openModButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openModButton.Image = global::Gibbed.Spore.ModMaker.Properties.Resources.OpenModification;
			resources.ApplyResources(this.openModButton, "openModButton");
			this.openModButton.Name = "openModButton";
			this.openModButton.ButtonClick += new System.EventHandler(this.OnModOpen);
			this.openModButton.DropDownOpening += new System.EventHandler(this.OnRecentMods);
			this.openModButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnModOpenRecent);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// saveModButton
			// 
			this.saveModButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveModButton.Image = global::Gibbed.Spore.ModMaker.Properties.Resources.SaveModification;
			resources.ApplyResources(this.saveModButton, "saveModButton");
			this.saveModButton.Name = "saveModButton";
			this.saveModButton.Click += new System.EventHandler(this.OnModSave);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// buildPackageButton
			// 
			this.buildPackageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buildPackageButton.Image = global::Gibbed.Spore.ModMaker.Properties.Resources.BuildPackage;
			resources.ApplyResources(this.buildPackageButton, "buildPackageButton");
			this.buildPackageButton.Name = "buildPackageButton";
			this.buildPackageButton.Click += new System.EventHandler(this.OnBuildPackage);
			// 
			// openModDialog
			// 
			resources.ApplyResources(this.openModDialog, "openModDialog");
			// 
			// saveModDialog
			// 
			this.saveModDialog.DefaultExt = "sporemod";
			resources.ApplyResources(this.saveModDialog, "saveModDialog");
			// 
			// addFileDialog
			// 
			resources.ApplyResources(this.addFileDialog, "addFileDialog");
			// 
			// savePackageDialog
			// 
			this.savePackageDialog.DefaultExt = "package";
			resources.ApplyResources(this.savePackageDialog, "savePackageDialog");
			// 
			// Editor
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.detailsSplitter);
			this.Controls.Add(this.editorToolStrip);
			this.Name = "Editor";
			this.Load += new System.EventHandler(this.OnLoad);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClose);
			this.detailsSplitter.Panel1.ResumeLayout(false);
			this.detailsSplitter.Panel2.ResumeLayout(false);
			this.detailsSplitter.Panel2.PerformLayout();
			this.detailsSplitter.ResumeLayout(false);
			this.modDetailsPanel.ResumeLayout(false);
			this.modDetailsPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).EndInit();
			this.filesToolStrip.ResumeLayout(false);
			this.filesToolStrip.PerformLayout();
			this.fileDetailsPanel.ResumeLayout(false);
			this.fileDetailsPanel.PerformLayout();
			this.editorToolStrip.ResumeLayout(false);
			this.editorToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer detailsSplitter;
		private System.Windows.Forms.ToolStrip editorToolStrip;
		private System.Windows.Forms.ToolStripButton newModButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton saveModButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton buildPackageButton;
		private System.Windows.Forms.TableLayoutPanel modDetailsPanel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.Label authorLabel;
		private System.Windows.Forms.TextBox authorText;
		private System.Windows.Forms.Label emailLabel;
		private System.Windows.Forms.TextBox emailText;
		private System.Windows.Forms.Label websiteLabel;
		private System.Windows.Forms.TextBox websiteText;
		private System.Windows.Forms.Label descriptionLabel;
		private System.Windows.Forms.TextBox descriptionText;
		private System.Windows.Forms.TextBox versionText;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.BindingSource bindingSource;
		private System.Windows.Forms.ToolStrip filesToolStrip;
		private System.Windows.Forms.ToolStripButton removeFileButton;
		private System.Windows.Forms.ListBox fileList;
		private System.Windows.Forms.BindingSource filesBindingSource;
		private System.Windows.Forms.TableLayoutPanel fileDetailsPanel;
		private System.Windows.Forms.TextBox typeIdText;
		private System.Windows.Forms.TextBox groupIdText;
		private System.Windows.Forms.TextBox instanceIdText;
		private System.Windows.Forms.Label instanceIdLabel;
		private System.Windows.Forms.Label groupIdLabel;
		private System.Windows.Forms.Label typeIdLabel;
		private System.Windows.Forms.OpenFileDialog openModDialog;
		private System.Windows.Forms.SaveFileDialog saveModDialog;
		private System.Windows.Forms.OpenFileDialog addFileDialog;
		private System.Windows.Forms.ToolStripSplitButton openModButton;
		private System.Windows.Forms.ToolStripSplitButton addFileButton;
		private System.Windows.Forms.ToolStripMenuItem addFileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addDirectoryMenuItem;
		private System.Windows.Forms.FolderBrowserDialog addDirectoryDialog;
		private System.Windows.Forms.SaveFileDialog savePackageDialog;

	}
}

