using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySnippingTool
{
    public partial class DisplayVideoForm : Form
    {
        public string filetobePlayed = "";
        public bool isItSaved = false;
        public DisplayVideoForm()
        {
            InitializeComponent();
        }

        private void DisplayVideoForm_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = filetobePlayed;
        }

        private void axWindowsMediaPlayer_MediaError(object sender, AxWMPLib._WMPOCXEvents_MediaErrorEvent e)
        {
            MessageBox.Show("Cannot play media file.");
            this.Close();
        }

        private void DisplayVideoForm_Paint(object sender, PaintEventArgs e)
        {
            if (((DisplayVideoForm)sender).isItSaved)
                ControlPaint.DrawBorder(e.Graphics, ((DisplayVideoForm)sender).ClientRectangle, Color.Green, ButtonBorderStyle.Solid);
            else
                ControlPaint.DrawBorder(e.Graphics, ((DisplayVideoForm)sender).ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
        }
    }
}
