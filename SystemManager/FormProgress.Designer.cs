namespace SM
{
    partial class FormProgress
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
            this.progressBarCurrentOperation = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelSizeItems = new System.Windows.Forms.Label();
            this.labelCountItems = new System.Windows.Forms.Label();
            this.labelSource = new System.Windows.Forms.Label();
            this.labelDestination = new System.Windows.Forms.Label();
            this.progressBarTotalOperation = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarCurrentOperation
            // 
            this.progressBarCurrentOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarCurrentOperation.Location = new System.Drawing.Point(12, 129);
            this.progressBarCurrentOperation.Name = "progressBarCurrentOperation";
            this.progressBarCurrentOperation.Size = new System.Drawing.Size(776, 34);
            this.progressBarCurrentOperation.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.labelSizeItems);
            this.panel1.Controls.Add(this.labelCountItems);
            this.panel1.Controls.Add(this.labelSource);
            this.panel1.Controls.Add(this.labelDestination);
            this.panel1.Controls.Add(this.progressBarTotalOperation);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.progressBarCurrentOperation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 328);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(212, 276);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "Pause";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(330, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelSizeItems
            // 
            this.labelSizeItems.AutoSize = true;
            this.labelSizeItems.Location = new System.Drawing.Point(615, 226);
            this.labelSizeItems.Name = "labelSizeItems";
            this.labelSizeItems.Size = new System.Drawing.Size(173, 25);
            this.labelSizeItems.TabIndex = 3;
            this.labelSizeItems.Text = "3123 Mb / 9500 Mb";
            // 
            // labelCountItems
            // 
            this.labelCountItems.AutoSize = true;
            this.labelCountItems.Location = new System.Drawing.Point(12, 226);
            this.labelCountItems.Name = "labelCountItems";
            this.labelCountItems.Size = new System.Drawing.Size(59, 25);
            this.labelCountItems.TabIndex = 3;
            this.labelCountItems.Text = "1/100";
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(12, 35);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(70, 25);
            this.labelSource.TabIndex = 3;
            this.labelSource.Text = "Source:";
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Location = new System.Drawing.Point(12, 73);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(106, 25);
            this.labelDestination.TabIndex = 2;
            this.labelDestination.Text = "Destination:";
            // 
            // progressBarTotalOperation
            // 
            this.progressBarTotalOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarTotalOperation.Location = new System.Drawing.Point(12, 169);
            this.progressBarTotalOperation.Name = "progressBarTotalOperation";
            this.progressBarTotalOperation.Size = new System.Drawing.Size(776, 34);
            this.progressBarTotalOperation.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(448, 276);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(112, 34);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Backgroud";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(800, 328);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarCurrentOperation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelSizeItems;
        private System.Windows.Forms.Label labelCountItems;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.ProgressBar progressBarTotalOperation;
        private System.Windows.Forms.Button buttonCancel;
    }
}