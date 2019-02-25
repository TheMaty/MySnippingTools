using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySnippingTool
{
    public partial class MainMDI : Form
    {
        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EmptyClipboard();

        [DllImport("user32.dll", SetLastError=true)]
        private static extern bool CloseClipboard();

        private int errorCount = 0;
        private string snippingToolFilePath = "";

        private Recorder rec = null;

        private Point videoCapturePanelLocation;
        private Size videoCapturePanelSize;

        public FormWindowState mainState;

        string fileName = "";

        public MainMDI()
        {
            InitializeComponent();
            if (!Directory.Exists("Temporary Images"))
                Directory.CreateDirectory("Temporary Images");
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo("Temporary Images");
                long size = 0;
                // Add file sizes.
                FileInfo[] fis = dirInfo.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }

                if (size > 100000000) //100 MB
                {
                    foreach (FileInfo fi in fis)
                    {
                        fi.Delete();
                    }
                }
                
            }

            if (File.Exists(@"C:\windows\sysnative\SnippingTool.exe"))
                snippingToolFilePath = @"C:\windows\sysnative\SnippingTool.exe";
            else if (File.Exists(@"C:\windows\system32\SnippingTool.exe"))
                snippingToolFilePath = @"C:\windows\system32\SnippingTool.exe";
            else
            {
                MessageBox.Show("SnippingTool does not exist in the windows, please install it first. Application is closing...");
                this.Close();
            }

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings["Directory"] != null)
                toolStripStatusLabel.Text = settings["Directory"].Value == "" ? "Please select folder for the storing images" : $" Selected Folder : {settings["Directory"].Value}";


        }

        private void GetReadyClipboard()
        {
            //cleaning clipboard
            IntPtr hwnd = new IntPtr();

            PleaseWait waitForm = new PleaseWait();

            while (true)
            {
                if (OpenClipboard(hwnd))
                {
                    EmptyClipboard();
                    CloseClipboard();
                    break;
                }

                errorCount = errorCount + 1;

                if (errorCount == 1)
                {
                    waitForm.MdiParent = this;
                    waitForm.Show();
                    waitForm.Focus();
                    waitForm.BringToFront();
                    waitForm.TopMost = true;
                    this.Refresh();
                }
            }
            waitForm.Close();
            waitForm.Dispose();
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            mainState = this.WindowState;
            this.WindowState = FormWindowState.Minimized;
            GetReadyClipboard();
            this.Refresh();

            try
            {
                Image clipboardImg = Operations.CaptureScreen(snippingToolFilePath);
                fileName = $"Temporary Images\\{Guid.NewGuid().ToString()}.jpg";

                clipboardImg.Save(fileName, ImageFormat.Jpeg);

                // 
                // pictureBox
                // 
                CustomPictureBox pictureBox = new CustomPictureBox();
                pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
                pictureBox.Location = new System.Drawing.Point(0, 0);
                pictureBox.Name = $"pictureBox_{DateTime.Now.ToString("yyyyMMddmmhhfff")}";
                pictureBox.TabStop = false;
                pictureBox.Image = Image.FromFile(fileName);
                pictureBox.ClientSize = pictureBox.Image.Size;
                pictureBox.Paint += PictureBox_Paint;
                pictureBox.isItSaved = false;

                // 
                // Form
                // 
                Form form = new Form();
                form.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                form.ClientSize = pictureBox.ClientSize;
                form.Text = "*";
                form.Name = $"Form_{DateTime.Now.ToString("yyyyMMddmmhhfff")} * ";
                form.Controls.Add(pictureBox);
                form.ResumeLayout(false);
                form.Icon = this.Icon;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.ControlBox = false;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.KeyDown += Form_KeyDown;

                form.MdiParent = this;

                form.Show();

                this.ActiveControl = form;

                errorCount = 0;

                this.WindowState = mainState;

                //weird workaround
                this.TopMost = true;
                //it locks other forms to be front later on so
                this.TopMost = false;
                //because
                //this.TopLevel = true;
                //does not work.

                if (form.ClientSize.Width <= 131)
                    MessageBox.Show("very small object is drawn. Image will be adjusted for the window but original size will be kept in case of recording", "Warning", MessageBoxButtons.OK);

                return;
            }
            catch
            {
                this.WindowState = mainState;
                this.BringToFront();
                this.TopMost = true;

                MessageBox.Show(this, "Error is occured... Please try again !");
            }

        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Clipboard.SetImage(((PictureBox)((Form)sender).Controls[0]).Image);
            }

            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                SaveToolStripMenuItem_Click(sender, e);
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (((CustomPictureBox)sender).isItSaved)
                ControlPaint.DrawBorder(e.Graphics, ((CustomPictureBox)sender).ClientRectangle, Color.Green, ButtonBorderStyle.Solid);
            else
                ControlPaint.DrawBorder(e.Graphics, ((CustomPictureBox)sender).ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
        }

        private void OpenFile(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();

            openFolderDialog.Description = "Select folder path ";
            openFolderDialog.SelectedPath = ConfigurationManager.AppSettings["Directory"].ToString();
            if (openFolderDialog.ShowDialog(this) == DialogResult.OK)
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings["Directory"] == null)
                {
                    settings.Add("Directory", openFolderDialog.SelectedPath);
                }
                else
                {
                    settings["Directory"].Value = openFolderDialog.SelectedPath;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                toolStripStatusLabel.Text = $" Selected Folder : {settings["Directory"].Value}";
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings["Directory"] == null)
            {
                MessageBox.Show("You should select path from folder selection in the toolbar");
                return;
            }
            else
            {
                if (!Directory.Exists(settings["Directory"].Value))
                {
                    MessageBox.Show("You should select path from folder selection in the toolbar");
                    return;
                }
                else
                {
                    // Determine the active child form  
                    Form activeChild = this.ActiveMdiChild;
                    
                    // If there is an active child form, find the active control    
                    if (activeChild != null)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();

                        if (activeChild.GetType() != typeof(DisplayVideoForm))
                        {
                            #region Save Image 

                            CustomPictureBox pictureBox = (CustomPictureBox)activeChild.Controls[0];
                            if (pictureBox.isItSaved)
                            {
                                MessageBox.Show("It is already saved !");
                                return;
                            }
                            saveFileDialog.AddExtension = true;
                            saveFileDialog.InitialDirectory = settings["Directory"].Value;
                            saveFileDialog.AddExtension = true;
                            saveFileDialog.CheckPathExists = true;
                            saveFileDialog.CheckFileExists = false;
                            saveFileDialog.Filter = "JPeg Image|*.jpg";
                            saveFileDialog.DefaultExt = "jpg";
                            saveFileDialog.Title = "Save an Image File...";
                            saveFileDialog.FileName = $"Img_{ DateTime.Now.ToString("yyyyMMddmmhhssfff")}.jpg";
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                pictureBox.Image.Save(saveFileDialog.FileName);
                                pictureBox.isItSaved = true;
                                pictureBox.Refresh();
                                activeChild.Text = activeChild.Name = Path.GetFileName(saveFileDialog.FileName);
                            }
                            #endregion
                        }
                        else
                        {
                            #region Save Movie
                            AxWMPLib.AxWindowsMediaPlayer player = (AxWMPLib.AxWindowsMediaPlayer)activeChild.Controls[0];

                            if (((DisplayVideoForm)activeChild).isItSaved)
                            {
                                MessageBox.Show("It is already saved !");
                                return;
                            }
                            saveFileDialog.AddExtension = true;
                            saveFileDialog.InitialDirectory = settings["Directory"].Value;
                            saveFileDialog.AddExtension = true;
                            saveFileDialog.CheckPathExists = true;
                            saveFileDialog.CheckFileExists = false;
                            saveFileDialog.Filter = "Movie|*.avi";
                            saveFileDialog.DefaultExt = "avi";
                            saveFileDialog.Title = "Save an AVI File...";
                            saveFileDialog.FileName = $"Video_{ DateTime.Now.ToString("yyyyMMddmmhhssfff")}.avi";
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                File.Copy(player.URL, saveFileDialog.FileName);
                                ((DisplayVideoForm)activeChild).isItSaved = true;
                                activeChild.Refresh();
                                activeChild.Text = activeChild.Name = Path.GetFileName(saveFileDialog.FileName);
                            }
                            #endregion
                        }
                    }
                    else
                        MessageBox.Show("Select form to save...");
                }
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void toolStripAboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Muhammet ATALAY - http://www.muhammetatalay.com");
        }

        private void MainMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool hasUnsavedForm = false;

            foreach(Form form in this.MdiChildren)
            {
                hasUnsavedForm = form.Name.Contains("*") ? true : false;
            }

            if (hasUnsavedForm)
            {
                var window = MessageBox.Show(
                    "Close the window?",
                    "Are you sure?",
                    MessageBoxButtons.YesNo);

                e.Cancel = (window == DialogResult.No);
            }
        }

        private void SaveAlltoolStripButton_Click(object sender, EventArgs e)
        {
            foreach(Form form in this.MdiChildren)
            {
                this.ActiveControl = form;
                SaveToolStripMenuItem_Click(form, null);
            }

            MessageBox.Show(this.MdiChildren.Count().ToString() + " images created to the selected path successfully","Notification");
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            if (this.ActiveControl is Form)
            {
                if (this.ActiveControl.Name.Contains("*"))
                {
                    var window = MessageBox.Show(
                       "Close the window?",
                       "Are you sure?",
                       MessageBoxButtons.YesNo);
                    if (window == DialogResult.No)
                        return;
                }

                ((Form)this.ActiveControl).Close();
            }
            else
            {
                MessageBox.Show("Select a form to close.");
            }
            
        }

        private void toolStripButtonCloseAll_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                this.ActiveControl = form;

                toolStripButtonClose_Click(null, null);
            }
        }

        private void NewRecordToolStripButton_Click(object sender, EventArgs e)
        {
            mainState = this.WindowState;
            this.WindowState = FormWindowState.Minimized;
            GetReadyClipboard();
            this.Refresh();

            fileName = $"Temporary Images\\{Guid.NewGuid().ToString()}.avi";
            // 
            // globalEventProvider1 -> add event
            // 
            this.globalEventProvider1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseDown);
            this.globalEventProvider1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseUp);

            Thread t = new Thread(() => Operations.CaptureScreen(snippingToolFilePath)) { IsBackground = true };
            t.Start();

            Thread t2 = new Thread(() => VideoRecording()) { IsBackground = true };
            t2.Start();

            NewRecordToolStripButton.Enabled = false;
            StopToolStripButton.Enabled = true;
        }

        private void VideoRecording()
        {
            while (videoCapturePanelSize.Height <= 0 && videoCapturePanelSize.Width <= 0) { }
            rec = new Recorder(new RecorderParams(fileName, 10, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, 70, videoCapturePanelLocation, videoCapturePanelSize), fileName);

            // 
            // globalEventProvider1 -> remove event
            // 
            this.globalEventProvider1.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseDown);
            this.globalEventProvider1.MouseUp -= new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseUp);
        }

        public void StopRecord()
        {
            rec.Dispose();
        }

        public void StopToolStripButton_Click(object sender, EventArgs e)
        {
            rec.Dispose();

            //display it in the form
            // 
            // Form
            // 
            DisplayVideoForm form = new DisplayVideoForm();
            form.filetobePlayed = fileName;
            form.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            form.ClientSize = new Size(videoCapturePanelSize.Width + 15,videoCapturePanelSize.Height + 110);
            form.Text = "*";
            form.Name = $"Form_{DateTime.Now.ToString("yyyyMMddmmhhfff")} * ";
            form.ResumeLayout(false);
            form.Icon = this.Icon;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ControlBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            //form.KeyDown += Form_KeyDown;

            form.MdiParent = this;

            form.Show();

            NewRecordToolStripButton.Enabled = true;
            StopToolStripButton.Enabled = false;

            this.ActiveControl = form;

            this.WindowState = mainState;

            //weird workaround
            this.TopMost = true;
            //it locks other forms to be front later on so
            this.TopMost = false;
            //because
            //this.TopLevel = true;
            //does not work.


            if (videoCapturePanelSize.Width <= 131)
                MessageBox.Show("very small object is drawn. Video will be adjusted for the window but original size will be kept in case of recording", "Warning", MessageBoxButtons.OK);


        }


        private void globalEventProvider1_MouseUp(object sender, MouseEventArgs e)
        {
            videoCapturePanelSize = new Size(Math.Abs(e.X - videoCapturePanelLocation.X), Math.Abs(e.Y - videoCapturePanelLocation.Y));
        }

        private void globalEventProvider1_MouseDown(object sender, MouseEventArgs e)
        {
            videoCapturePanelLocation = new Point(e.X, e.Y);
        }

        private void MainMDI_Layout(object sender, LayoutEventArgs e)
        {
            if (!NewRecordToolStripButton.Enabled)
                StopToolStripButton_Click(sender, e);
        }
    }
}
