using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Vision.Motion;

namespace YüzAlgılama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FilterInfoCollection fic;
        VideoCaptureDevice device;
        MotionDetector motiondetector;
        float f;

        private void Form1_Load(object sender, EventArgs e)
        {
            motiondetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in fic)
            {
                comboBoxDevice.Items.Add(item.Name);
            }


            comboBoxDevice.SelectedIndex = 0;

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            device = new VideoCaptureDevice(fic[comboBoxDevice.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = device;
            videoSourcePlayer1.Start();

        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();

        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
           f= motiondetector.ProcessFrame(image);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Hareket:" + f.ToString();
        }

            
    }
}
