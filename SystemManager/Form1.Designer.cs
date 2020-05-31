namespace SystemManager
{
    partial class Form1
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.leftListView = new System.Windows.Forms.ListView();
            this.LeftColumnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.LeftColumnHeaderExt = new System.Windows.Forms.ColumnHeader();
            this.LeftColumnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.LeftColumnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.LeftColumnHeaderAttr = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.leftPathPanel = new System.Windows.Forms.Panel();
            this.leftPathLabel = new System.Windows.Forms.Label();
            this.leftVolumePanel = new System.Windows.Forms.Panel();
            this.leftFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.rightListView = new System.Windows.Forms.ListView();
            this.RightColumnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.RightColumnHeaderExt = new System.Windows.Forms.ColumnHeader();
            this.RightColumnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.RightColumnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.RightColumnHeaderAttr = new System.Windows.Forms.ColumnHeader();
            this.rightPathPanel = new System.Windows.Forms.Panel();
            this.rightPathLabel = new System.Windows.Forms.Label();
            this.rightVolumePanel = new System.Windows.Forms.Panel();
            this.rightFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.leftPathPanel.SuspendLayout();
            this.leftVolumePanel.SuspendLayout();
            this.rightPathPanel.SuspendLayout();
            this.rightVolumePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.leftListView);
            this.splitContainer1.Panel1.Controls.Add(this.leftPathPanel);
            this.splitContainer1.Panel1.Controls.Add(this.leftVolumePanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightListView);
            this.splitContainer1.Panel2.Controls.Add(this.rightPathPanel);
            this.splitContainer1.Panel2.Controls.Add(this.rightVolumePanel);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1898, 1252);
            this.splitContainer1.SplitterDistance = 905;
            this.splitContainer1.SplitterIncrement = 10;
            this.splitContainer1.SplitterWidth = 20;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // leftListView
            // 
            this.leftListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.leftListView.CheckBoxes = true;
            this.leftListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LeftColumnHeaderName,
            this.LeftColumnHeaderExt,
            this.LeftColumnHeaderSize,
            this.LeftColumnHeaderDate,
            this.LeftColumnHeaderAttr});
            this.leftListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.leftListView.FullRowSelect = true;
            this.leftListView.GridLines = true;
            this.leftListView.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.leftListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.leftListView.LabelEdit = true;
            this.leftListView.LabelWrap = false;
            this.leftListView.Location = new System.Drawing.Point(0, 87);
            this.leftListView.Name = "leftListView";
            this.leftListView.Size = new System.Drawing.Size(905, 1165);
            this.leftListView.SmallImageList = this.imageList1;
            this.leftListView.TabIndex = 1;
            this.leftListView.UseCompatibleStateImageBehavior = false;
            this.leftListView.View = System.Windows.Forms.View.Details;
            // 
            // LeftColumnHeaderName
            // 
            this.LeftColumnHeaderName.Name = "LeftColumnHeaderName";
            this.LeftColumnHeaderName.Text = "Name";
            this.LeftColumnHeaderName.Width = 500;
            // 
            // LeftColumnHeaderExt
            // 
            this.LeftColumnHeaderExt.Name = "LeftColumnHeaderExt";
            this.LeftColumnHeaderExt.Text = "Ext";
            // 
            // LeftColumnHeaderSize
            // 
            this.LeftColumnHeaderSize.Name = "LeftColumnHeaderSize";
            this.LeftColumnHeaderSize.Text = "Size";
            // 
            // LeftColumnHeaderDate
            // 
            this.LeftColumnHeaderDate.Name = "LeftColumnHeaderDate";
            this.LeftColumnHeaderDate.Text = "Date";
            this.LeftColumnHeaderDate.Width = 160;
            // 
            // LeftColumnHeaderAttr
            // 
            this.LeftColumnHeaderAttr.Name = "LeftColumnHeaderAttr";
            this.LeftColumnHeaderAttr.Text = "Attr";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-folder-48.png");
            this.imageList1.Images.SetKeyName(1, "icons8-file-48.png");
            // 
            // leftPathPanel
            // 
            this.leftPathPanel.Controls.Add(this.leftPathLabel);
            this.leftPathPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftPathPanel.Location = new System.Drawing.Point(0, 42);
            this.leftPathPanel.Name = "leftPathPanel";
            this.leftPathPanel.Size = new System.Drawing.Size(905, 45);
            this.leftPathPanel.TabIndex = 0;
            // 
            // leftPathLabel
            // 
            this.leftPathLabel.AutoSize = true;
            this.leftPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPathLabel.Location = new System.Drawing.Point(0, 0);
            this.leftPathLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.leftPathLabel.Name = "leftPathLabel";
            this.leftPathLabel.Size = new System.Drawing.Size(112, 25);
            this.leftPathLabel.TabIndex = 0;
            this.leftPathLabel.Text = "leftPathLabel";
            // 
            // leftVolumePanel
            // 
            this.leftVolumePanel.Controls.Add(this.leftFlowLayoutPanel);
            this.leftVolumePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftVolumePanel.Location = new System.Drawing.Point(0, 0);
            this.leftVolumePanel.Name = "leftVolumePanel";
            this.leftVolumePanel.Size = new System.Drawing.Size(905, 42);
            this.leftVolumePanel.TabIndex = 0;
            // 
            // leftFlowLayoutPanel
            // 
            this.leftFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.leftFlowLayoutPanel.Name = "leftFlowLayoutPanel";
            this.leftFlowLayoutPanel.Size = new System.Drawing.Size(905, 42);
            this.leftFlowLayoutPanel.TabIndex = 1;
            // 
            // rightListView
            // 
            this.rightListView.CheckBoxes = true;
            this.rightListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RightColumnHeaderName,
            this.RightColumnHeaderExt,
            this.RightColumnHeaderSize,
            this.RightColumnHeaderDate,
            this.RightColumnHeaderAttr});
            this.rightListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rightListView.FullRowSelect = true;
            this.rightListView.GridLines = true;
            this.rightListView.HideSelection = false;
            listViewItem2.StateImageIndex = 0;
            this.rightListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.rightListView.LabelEdit = true;
            this.rightListView.LabelWrap = false;
            this.rightListView.Location = new System.Drawing.Point(60, 87);
            this.rightListView.Name = "rightListView";
            this.rightListView.Size = new System.Drawing.Size(913, 1165);
            this.rightListView.SmallImageList = this.imageList1;
            this.rightListView.TabIndex = 2;
            this.rightListView.UseCompatibleStateImageBehavior = false;
            this.rightListView.View = System.Windows.Forms.View.Details;
            // 
            // RightColumnHeaderName
            // 
            this.RightColumnHeaderName.Name = "RightColumnHeaderName";
            this.RightColumnHeaderName.Text = "Name";
            this.RightColumnHeaderName.Width = 500;
            // 
            // RightColumnHeaderExt
            // 
            this.RightColumnHeaderExt.Name = "RightColumnHeaderExt";
            this.RightColumnHeaderExt.Text = "Ext";
            // 
            // RightColumnHeaderSize
            // 
            this.RightColumnHeaderSize.Name = "RightColumnHeaderSize";
            this.RightColumnHeaderSize.Text = "Size";
            // 
            // RightColumnHeaderDate
            // 
            this.RightColumnHeaderDate.Name = "RightColumnHeaderDate";
            this.RightColumnHeaderDate.Text = "Date";
            this.RightColumnHeaderDate.Width = 160;
            // 
            // RightColumnHeaderAttr
            // 
            this.RightColumnHeaderAttr.Name = "RightColumnHeaderAttr";
            this.RightColumnHeaderAttr.Text = "Attr";
            // 
            // rightPathPanel
            // 
            this.rightPathPanel.Controls.Add(this.rightPathLabel);
            this.rightPathPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightPathPanel.Location = new System.Drawing.Point(60, 42);
            this.rightPathPanel.Name = "rightPathPanel";
            this.rightPathPanel.Size = new System.Drawing.Size(913, 45);
            this.rightPathPanel.TabIndex = 0;
            // 
            // rightPathLabel
            // 
            this.rightPathLabel.AutoSize = true;
            this.rightPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPathLabel.Location = new System.Drawing.Point(0, 0);
            this.rightPathLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rightPathLabel.Name = "rightPathLabel";
            this.rightPathLabel.Size = new System.Drawing.Size(124, 25);
            this.rightPathLabel.TabIndex = 0;
            this.rightPathLabel.Text = "rightPathLabel";
            // 
            // rightVolumePanel
            // 
            this.rightVolumePanel.Controls.Add(this.rightFlowLayoutPanel);
            this.rightVolumePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightVolumePanel.Location = new System.Drawing.Point(60, 0);
            this.rightVolumePanel.Name = "rightVolumePanel";
            this.rightVolumePanel.Size = new System.Drawing.Size(913, 42);
            this.rightVolumePanel.TabIndex = 0;
            // 
            // rightFlowLayoutPanel
            // 
            this.rightFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.rightFlowLayoutPanel.Name = "rightFlowLayoutPanel";
            this.rightFlowLayoutPanel.Size = new System.Drawing.Size(913, 42);
            this.rightFlowLayoutPanel.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(60, 1252);
            this.panel3.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1898, 1252);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.leftPathPanel.ResumeLayout(false);
            this.leftPathPanel.PerformLayout();
            this.leftVolumePanel.ResumeLayout(false);
            this.rightPathPanel.ResumeLayout(false);
            this.rightPathPanel.PerformLayout();
            this.rightVolumePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel leftPathPanel;
        private System.Windows.Forms.Label leftPathLabel;
        private System.Windows.Forms.Panel leftVolumePanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView leftListView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderName;
        private System.Windows.Forms.ListView rightListView;
        private System.Windows.Forms.Panel rightPathPanel;
        private System.Windows.Forms.Panel rightVolumePanel;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderExt;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderSize;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderAttr;
        private System.Windows.Forms.ColumnHeader RightColumnHeaderName;
        private System.Windows.Forms.ColumnHeader RightColumnHeaderExt;
        private System.Windows.Forms.ColumnHeader RightColumnHeaderSize;
        private System.Windows.Forms.ColumnHeader RightColumnHeaderAttr;
        private System.Windows.Forms.Label rightPathLabel;
        private System.Windows.Forms.FlowLayoutPanel leftFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel rightFlowLayoutPanel;
        private System.Windows.Forms.ColumnHeader LeftColumnHeaderDate;
        private System.Windows.Forms.ColumnHeader RightColumnHeaderDate;
    }
}

