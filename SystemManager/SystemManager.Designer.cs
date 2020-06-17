

using SM.Controls;

namespace SM
{
    partial class SystemManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemManager));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.leftPathPanel = new System.Windows.Forms.Panel();
            this.leftPathLabel = new System.Windows.Forms.Label();
            this.leftVolumePanel = new System.Windows.Forms.Panel();
            this.leftFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxStatus1 = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rightPathPanel = new System.Windows.Forms.Panel();
            this.rightPathLabel = new System.Windows.Forms.Label();
            this.rightVolumePanel = new System.Windows.Forms.Panel();
            this.rightFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxStatus2 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonService = new System.Windows.Forms.Button();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonMedia = new System.Windows.Forms.Button();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.cmdRichTextBox = new System.Windows.Forms.RichTextBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.leftListView = new SM.Controls.ListViewEx();
            this.LeftColumnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeftColumnHeaderExt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeftColumnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeftColumnHeaderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeftColumnHeaderAttr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rightListView = new SM.Controls.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.leftPathPanel.SuspendLayout();
            this.leftVolumePanel.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.rightPathPanel.SuspendLayout();
            this.rightVolumePanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-folder-48.png");
            this.imageList1.Images.SetKeyName(1, "icons8-file-48.png");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.logRichTextBox);
            this.splitContainer2.Panel2.Controls.Add(this.cmdRichTextBox);
            this.splitContainer2.Size = new System.Drawing.Size(1732, 1061);
            this.splitContainer2.SplitterDistance = 912;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            this.splitContainer2.Text = "splitContainer2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.leftPathPanel);
            this.splitContainer1.Panel1.Controls.Add(this.leftVolumePanel);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxStatus1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Panel2.Controls.Add(this.rightPathPanel);
            this.splitContainer1.Panel2.Controls.Add(this.rightVolumePanel);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxStatus2);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1732, 912);
            this.splitContainer1.SplitterDistance = 830;
            this.splitContainer1.SplitterIncrement = 40;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(830, 808);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxFilter);
            this.tabPage1.Controls.Add(this.leftListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(822, 775);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(822, 775);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // leftPathPanel
            // 
            this.leftPathPanel.Controls.Add(this.leftPathLabel);
            this.leftPathPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftPathPanel.Location = new System.Drawing.Point(0, 34);
            this.leftPathPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.leftPathPanel.Name = "leftPathPanel";
            this.leftPathPanel.Size = new System.Drawing.Size(830, 36);
            this.leftPathPanel.TabIndex = 0;
            // 
            // leftPathLabel
            // 
            this.leftPathLabel.AutoSize = true;
            this.leftPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPathLabel.Location = new System.Drawing.Point(0, 0);
            this.leftPathLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.leftPathLabel.Name = "leftPathLabel";
            this.leftPathLabel.Size = new System.Drawing.Size(103, 20);
            this.leftPathLabel.TabIndex = 0;
            this.leftPathLabel.Text = "leftPathLabel";
            // 
            // leftVolumePanel
            // 
            this.leftVolumePanel.Controls.Add(this.leftFlowLayoutPanel);
            this.leftVolumePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftVolumePanel.Location = new System.Drawing.Point(0, 0);
            this.leftVolumePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.leftVolumePanel.Name = "leftVolumePanel";
            this.leftVolumePanel.Size = new System.Drawing.Size(830, 34);
            this.leftVolumePanel.TabIndex = 0;
            // 
            // leftFlowLayoutPanel
            // 
            this.leftFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.leftFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.leftFlowLayoutPanel.Name = "leftFlowLayoutPanel";
            this.leftFlowLayoutPanel.Size = new System.Drawing.Size(830, 34);
            this.leftFlowLayoutPanel.TabIndex = 1;
            // 
            // textBoxStatus1
            // 
            this.textBoxStatus1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxStatus1.Location = new System.Drawing.Point(0, 878);
            this.textBoxStatus1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxStatus1.Multiline = true;
            this.textBoxStatus1.Name = "textBoxStatus1";
            this.textBoxStatus1.ReadOnly = true;
            this.textBoxStatus1.Size = new System.Drawing.Size(830, 34);
            this.textBoxStatus1.TabIndex = 0;
            this.textBoxStatus1.TabStop = false;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(60, 70);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(832, 808);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rightListView);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(824, 775);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(824, 775);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rightPathPanel
            // 
            this.rightPathPanel.Controls.Add(this.rightPathLabel);
            this.rightPathPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightPathPanel.Location = new System.Drawing.Point(60, 34);
            this.rightPathPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rightPathPanel.Name = "rightPathPanel";
            this.rightPathPanel.Size = new System.Drawing.Size(832, 36);
            this.rightPathPanel.TabIndex = 0;
            // 
            // rightPathLabel
            // 
            this.rightPathLabel.AutoSize = true;
            this.rightPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPathLabel.Location = new System.Drawing.Point(0, 0);
            this.rightPathLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.rightPathLabel.Name = "rightPathLabel";
            this.rightPathLabel.Size = new System.Drawing.Size(112, 20);
            this.rightPathLabel.TabIndex = 0;
            this.rightPathLabel.Text = "rightPathLabel";
            // 
            // rightVolumePanel
            // 
            this.rightVolumePanel.Controls.Add(this.rightFlowLayoutPanel);
            this.rightVolumePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightVolumePanel.Location = new System.Drawing.Point(60, 0);
            this.rightVolumePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rightVolumePanel.Name = "rightVolumePanel";
            this.rightVolumePanel.Size = new System.Drawing.Size(832, 34);
            this.rightVolumePanel.TabIndex = 0;
            // 
            // rightFlowLayoutPanel
            // 
            this.rightFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.rightFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rightFlowLayoutPanel.Name = "rightFlowLayoutPanel";
            this.rightFlowLayoutPanel.Size = new System.Drawing.Size(832, 34);
            this.rightFlowLayoutPanel.TabIndex = 1;
            // 
            // textBoxStatus2
            // 
            this.textBoxStatus2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxStatus2.Location = new System.Drawing.Point(60, 878);
            this.textBoxStatus2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxStatus2.Multiline = true;
            this.textBoxStatus2.Name = "textBoxStatus2";
            this.textBoxStatus2.ReadOnly = true;
            this.textBoxStatus2.Size = new System.Drawing.Size(832, 34);
            this.textBoxStatus2.TabIndex = 0;
            this.textBoxStatus2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.flowLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(60, 912);
            this.panel3.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonService);
            this.flowLayoutPanel1.Controls.Add(this.buttonProcess);
            this.flowLayoutPanel1.Controls.Add(this.buttonMedia);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(60, 912);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // buttonService
            // 
            this.buttonService.AutoSize = true;
            this.buttonService.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonService.Location = new System.Drawing.Point(3, 3);
            this.buttonService.Name = "buttonService";
            this.buttonService.Size = new System.Drawing.Size(71, 45);
            this.buttonService.TabIndex = 0;
            this.buttonService.Text = "Service";
            this.buttonService.UseVisualStyleBackColor = true;
            this.buttonService.Click += new System.EventHandler(this.buttonService_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.AutoSize = true;
            this.buttonProcess.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonProcess.Location = new System.Drawing.Point(3, 54);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(76, 45);
            this.buttonProcess.TabIndex = 1;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // buttonMedia
            // 
            this.buttonMedia.AutoSize = true;
            this.buttonMedia.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonMedia.Location = new System.Drawing.Point(3, 105);
            this.buttonMedia.Name = "buttonMedia";
            this.buttonMedia.Size = new System.Drawing.Size(76, 45);
            this.buttonMedia.TabIndex = 2;
            this.buttonMedia.Text = "Media";
            this.buttonMedia.UseVisualStyleBackColor = true;
            this.buttonMedia.Click += new System.EventHandler(this.buttonMedia_Click);
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logRichTextBox.Location = new System.Drawing.Point(0, 34);
            this.logRichTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.Size = new System.Drawing.Size(1732, 113);
            this.logRichTextBox.TabIndex = 0;
            this.logRichTextBox.TabStop = false;
            this.logRichTextBox.Text = "";
            // 
            // cmdRichTextBox
            // 
            this.cmdRichTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.cmdRichTextBox.Multiline = false;
            this.cmdRichTextBox.Name = "cmdRichTextBox";
            this.cmdRichTextBox.Size = new System.Drawing.Size(1732, 34);
            this.cmdRichTextBox.TabIndex = 1;
            this.cmdRichTextBox.Text = "";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(25, 698);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(399, 26);
            this.textBoxFilter.TabIndex = 3;
            this.textBoxFilter.Visible = false;
            // 
            // leftListView
            // 
            this.leftListView.CheckBoxes = true;
            this.leftListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LeftColumnHeaderName,
            this.LeftColumnHeaderExt,
            this.LeftColumnHeaderSize,
            this.LeftColumnHeaderDate,
            this.LeftColumnHeaderAttr});
            this.leftListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.leftListView.FullRowSelect = true;
            this.leftListView.GridLines = true;
            this.leftListView.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.leftListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.leftListView.LabelEdit = true;
            this.leftListView.LabelWrap = false;
            this.leftListView.Location = new System.Drawing.Point(3, 3);
            this.leftListView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.leftListView.Name = "leftListView";
            this.leftListView.Size = new System.Drawing.Size(816, 769);
            this.leftListView.SmallImageList = this.imageList1;
            this.leftListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.leftListView.TabIndex = 2;
            this.leftListView.UseCompatibleStateImageBehavior = false;
            this.leftListView.View = System.Windows.Forms.View.Details;
            // 
            // LeftColumnHeaderName
            // 
            this.LeftColumnHeaderName.Name = "LeftColumnHeaderName";
            this.LeftColumnHeaderName.Text = "Name";
            this.LeftColumnHeaderName.Width = 612;
            // 
            // LeftColumnHeaderExt
            // 
            this.LeftColumnHeaderExt.Name = "LeftColumnHeaderExt";
            this.LeftColumnHeaderExt.Text = "Ext";
            this.LeftColumnHeaderExt.Width = 45;
            // 
            // LeftColumnHeaderSize
            // 
            this.LeftColumnHeaderSize.Name = "LeftColumnHeaderSize";
            this.LeftColumnHeaderSize.Text = "Size";
            this.LeftColumnHeaderSize.Width = 90;
            // 
            // LeftColumnHeaderDate
            // 
            this.LeftColumnHeaderDate.Name = "LeftColumnHeaderDate";
            this.LeftColumnHeaderDate.Text = "Date";
            this.LeftColumnHeaderDate.Width = 108;
            // 
            // LeftColumnHeaderAttr
            // 
            this.LeftColumnHeaderAttr.Name = "LeftColumnHeaderAttr";
            this.LeftColumnHeaderAttr.Text = "Attr";
            this.LeftColumnHeaderAttr.Width = 45;
            // 
            // rightListView
            // 
            this.rightListView.CheckBoxes = true;
            this.rightListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.rightListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rightListView.FullRowSelect = true;
            this.rightListView.GridLines = true;
            this.rightListView.HideSelection = false;
            this.rightListView.LabelEdit = true;
            this.rightListView.LabelWrap = false;
            this.rightListView.Location = new System.Drawing.Point(3, 3);
            this.rightListView.Name = "rightListView";
            this.rightListView.Size = new System.Drawing.Size(818, 769);
            this.rightListView.SmallImageList = this.imageList1;
            this.rightListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.rightListView.TabIndex = 3;
            this.rightListView.UseCompatibleStateImageBehavior = false;
            this.rightListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 612;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ext";
            this.columnHeader2.Width = 45;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date";
            this.columnHeader4.Width = 108;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Attr";
            this.columnHeader5.Width = 45;
            // 
            // SystemManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1732, 1061);
            this.Controls.Add(this.splitContainer2);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SystemManager";
            this.Text = "System Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.leftPathPanel.ResumeLayout(false);
            this.leftPathPanel.PerformLayout();
            this.leftVolumePanel.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.rightPathPanel.ResumeLayout(false);
            this.rightPathPanel.PerformLayout();
            this.rightVolumePanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel leftPathPanel;
        private System.Windows.Forms.Label leftPathLabel;
        private System.Windows.Forms.Panel leftVolumePanel;
        private System.Windows.Forms.FlowLayoutPanel leftFlowLayoutPanel;
        private System.Windows.Forms.TextBox textBoxStatus1;
        private System.Windows.Forms.Panel rightPathPanel;
        private System.Windows.Forms.Label rightPathLabel;
        private System.Windows.Forms.Panel rightVolumePanel;
        private System.Windows.Forms.FlowLayoutPanel rightFlowLayoutPanel;
        private System.Windows.Forms.TextBox textBoxStatus2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonService;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ListViewEx leftListView;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderName;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderExt;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderSize;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderDate;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderAttr;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private ListViewEx rightListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button buttonMedia;
        private System.Windows.Forms.RichTextBox cmdRichTextBox;
        private System.Windows.Forms.TextBox textBoxFilter;
    }
}

