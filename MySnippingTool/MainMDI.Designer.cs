namespace MySnippingTool
{
    partial class MainMDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMDI));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.NewRecordToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.StopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveAlltoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCloseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripAboutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.globalEventProvider1 = new Gma.UserActivityMonitor.GlobalEventProvider();
            this.toolStripDropDownButtonDelay = new System.Windows.Forms.ToolStripDropDownButton();
            this.noDelayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneSecondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threeSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eightSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.NewRecordToolStripButton,
            this.StopToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.SaveAlltoolStripButton,
            this.toolStripButtonClose,
            this.toolStripButtonCloseAll,
            this.toolStripDropDownButtonDelay,
            this.toolStripSeparator2,
            this.toolStripAboutButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(780, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.ToolTipText = "New Image";
            this.newToolStripButton.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // NewRecordToolStripButton
            // 
            this.NewRecordToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewRecordToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewRecordToolStripButton.Image")));
            this.NewRecordToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewRecordToolStripButton.Name = "NewRecordToolStripButton";
            this.NewRecordToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.NewRecordToolStripButton.Text = "New";
            this.NewRecordToolStripButton.ToolTipText = "New Record";
            this.NewRecordToolStripButton.Click += new System.EventHandler(this.NewRecordToolStripButton_Click);
            // 
            // StopToolStripButton
            // 
            this.StopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopToolStripButton.Enabled = false;
            this.StopToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("StopToolStripButton.Image")));
            this.StopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopToolStripButton.Name = "StopToolStripButton";
            this.StopToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.StopToolStripButton.Text = "Stop";
            this.StopToolStripButton.Click += new System.EventHandler(this.StopToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.OpenFile);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(24, 24);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // SaveAlltoolStripButton
            // 
            this.SaveAlltoolStripButton.AutoToolTip = false;
            this.SaveAlltoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveAlltoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveAlltoolStripButton.Image")));
            this.SaveAlltoolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.SaveAlltoolStripButton.Name = "SaveAlltoolStripButton";
            this.SaveAlltoolStripButton.Size = new System.Drawing.Size(24, 24);
            this.SaveAlltoolStripButton.Text = "Save All";
            this.SaveAlltoolStripButton.ToolTipText = "Save All";
            this.SaveAlltoolStripButton.Click += new System.EventHandler(this.SaveAlltoolStripButton_Click);
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClose.Image")));
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonClose.Text = "Close";
            this.toolStripButtonClose.ToolTipText = "Close";
            this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
            // 
            // toolStripButtonCloseAll
            // 
            this.toolStripButtonCloseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCloseAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCloseAll.Image")));
            this.toolStripButtonCloseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCloseAll.Name = "toolStripButtonCloseAll";
            this.toolStripButtonCloseAll.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonCloseAll.Text = "Close All";
            this.toolStripButtonCloseAll.Click += new System.EventHandler(this.toolStripButtonCloseAll_Click);
            // 
            // toolStripAboutButton
            // 
            this.toolStripAboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAboutButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAboutButton.Image")));
            this.toolStripAboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAboutButton.Name = "toolStripAboutButton";
            this.toolStripAboutButton.Size = new System.Drawing.Size(24, 24);
            this.toolStripAboutButton.Text = "About";
            this.toolStripAboutButton.Click += new System.EventHandler(this.toolStripAboutButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 498);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(780, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // toolStripDropDownButtonDelay
            // 
            this.toolStripDropDownButtonDelay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonDelay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noDelayToolStripMenuItem,
            this.oneSecondToolStripMenuItem,
            this.twoSecondsToolStripMenuItem,
            this.threeSecondsToolStripMenuItem,
            this.fiveSecondsToolStripMenuItem,
            this.eightSecondsToolStripMenuItem});
            this.toolStripDropDownButtonDelay.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonDelay.Image")));
            this.toolStripDropDownButtonDelay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonDelay.Name = "toolStripDropDownButtonDelay";
            this.toolStripDropDownButtonDelay.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButtonDelay.Text = "No delay";
            this.toolStripDropDownButtonDelay.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripDropDownButtonDelay.ToolTipText = "No delay";
            this.toolStripDropDownButtonDelay.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripDropDownButtonDelay_DropDownItemClicked);
            // 
            // noDelayToolStripMenuItem
            // 
            this.noDelayToolStripMenuItem.Name = "noDelayToolStripMenuItem";
            this.noDelayToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.noDelayToolStripMenuItem.Text = "No Delay";
            this.noDelayToolStripMenuItem.ToolTipText = "0";
            // 
            // oneSecondToolStripMenuItem
            // 
            this.oneSecondToolStripMenuItem.Name = "oneSecondToolStripMenuItem";
            this.oneSecondToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oneSecondToolStripMenuItem.Text = "1 Second";
            this.oneSecondToolStripMenuItem.ToolTipText = "1";
            // 
            // twoSecondsToolStripMenuItem
            // 
            this.twoSecondsToolStripMenuItem.Name = "twoSecondsToolStripMenuItem";
            this.twoSecondsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.twoSecondsToolStripMenuItem.Text = "2 Seconds";
            this.twoSecondsToolStripMenuItem.ToolTipText = "2";
            // 
            // threeSecondsToolStripMenuItem
            // 
            this.threeSecondsToolStripMenuItem.Name = "threeSecondsToolStripMenuItem";
            this.threeSecondsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.threeSecondsToolStripMenuItem.Text = "3 Seconds";
            this.threeSecondsToolStripMenuItem.ToolTipText = "3";
            // 
            // fiveSecondsToolStripMenuItem
            // 
            this.fiveSecondsToolStripMenuItem.Name = "fiveSecondsToolStripMenuItem";
            this.fiveSecondsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fiveSecondsToolStripMenuItem.Text = "5 Seconds";
            this.fiveSecondsToolStripMenuItem.ToolTipText = "5";
            // 
            // eightSecondsToolStripMenuItem
            // 
            this.eightSecondsToolStripMenuItem.Name = "eightSecondsToolStripMenuItem";
            this.eightSecondsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eightSecondsToolStripMenuItem.Text = "8 Seconds";
            this.eightSecondsToolStripMenuItem.ToolTipText = "8";
            // 
            // MainMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(780, 520);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainMDI";
            this.Text = "My Snipping Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMDI_FormClosing);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.MainMDI_Layout);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripButton toolStripAboutButton;
        private System.Windows.Forms.ToolStripButton SaveAlltoolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButtonCloseAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonClose;
        private System.Windows.Forms.ToolStripButton NewRecordToolStripButton;
        private System.Windows.Forms.ToolStripButton StopToolStripButton;
        private Gma.UserActivityMonitor.GlobalEventProvider globalEventProvider1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonDelay;
        private System.Windows.Forms.ToolStripMenuItem noDelayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneSecondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threeSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fiveSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eightSecondsToolStripMenuItem;
    }
}



