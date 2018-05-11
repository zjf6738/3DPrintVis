using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;


namespace RecordWithEmguCV
{
    public partial class Form1 : Form
    {
        Capture cap;
        Image<Bgr, byte> frame;
        VideoWriter vw;

        public Form1()
        {
            InitializeComponent();


            cap = new Emgu.CV.Capture(1);
            vw = new VideoWriter("123.avi", 30, new Size(cap.Width, cap.Height), true);
            cap.Start();
            frame = new Image<Bgr, byte>(cap.Width, cap.Height);
            cap.ImageGrabbed += Cap_ImageGrabbed;
            cap.SetCaptureProperty(CapProp.Fps, 0);
        }

        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            cap.Retrieve(frame, 0);
            vw.Write(frame.Mat);
            pictureBox1.Image = frame;
        }
    }
}
