using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Image_Rotator
{
    public partial class Form1 : Form
    {
        Bitmap b;
        public Form1()
        {
            InitializeComponent();
        }
        

        Bitmap RotateImage(Bitmap original, float angle)
        {
            Bitmap rotated = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(rotated);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.TranslateTransform(original.Width / 2, original.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(original.Width / 2), -(original.Height / 2));
            g.DrawImage(original, new PointF(0, 0));
            g.Save();
            return rotated;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oFn = "";
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "PNG file|*.png";
            if (d.ShowDialog() == DialogResult.OK)
            {
                oFn = d.FileName;
            }

            Image img = Image.FromFile(oFn);
            b = new Bitmap(img);

            pictureBox1.Width = b.Width;
            pictureBox1.Height = b.Height;

            Bitmap temp = new Bitmap(b.Width, b.Height);
            for (int i = 0; i < 36; i++)
            {
                temp = RotateImage(b, i * 10);
                pictureBox1.BackgroundImage = temp;

                int cnt = 0;
                while(cnt < 10000)
                {
                    cnt++;
                }
                string mFn = oFn.Remove(oFn.Length - 4, 4) + "_" + (i * 10).ToString() + ".png";
                temp.Save(mFn);
            }
        }
    }
}
