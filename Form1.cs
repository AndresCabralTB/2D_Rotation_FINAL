using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _2D_Rotation
{
    public partial class Form1 : Form
    {
        static Bitmap bmp;
        float x, y;
        float textBB,angle;
        static Graphics g;
        private PointF a, b, c, d;




        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            PCT_CANVAS.Image = bmp;
            g = Graphics.FromImage(bmp);

            x = PCT_CANVAS.Width / 2;
            y = PCT_CANVAS.Height / 2;

            //g.TranslateTransform(x, y); //Change origin from (0,0) to (x,y) //CHANGE LATER ALL THE COORDINATES

            //Coordinations and code for cross in the middle
             
            a = new PointF(0,y);
            b = new PointF(x * 2, y);
            c = new PointF(x,0);
            d = new PointF(x, y * 2);

            g.DrawLine(Pens.Red, a, b); //Horizontal Line
            g.DrawLine(Pens.Red, c, d); //Vertical Line

            //CODE FOR THE BOX IN THE MIDDLE

            PointF aa = new PointF(x, y);
            PointF bb = new PointF(x, y -100);
            PointF cc = new PointF(x + 100, y-100);
            PointF dd = new PointF(x + 100, y);

            g.DrawLine(Pens.Black, aa, bb);
            g.DrawLine(Pens.Black, bb, cc);
            g.DrawLine(Pens.Black, cc, dd);
            g.DrawLine(Pens.Black, dd, aa);

            PCT_CANVAS.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetPCT(); //Function to restart canvas 
            angleText();

            PointF p1, p2, p3, p4;
            p1 = new PointF(0, 0);
            p2 = new PointF(0, 100);
            p3 = new PointF(100, 100);
            p4 = new PointF(100, 0);

            Render(p1, p2);
            Render(p2, p3);
            Render(p3, p4);
            Render(p4, p1);


            PCT_CANVAS.Invalidate();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           

            ResetPCT();
            angleText();

            a = new PointF(0,0);
            b = new PointF(0, 100);
            c = new PointF(100, 100);
            d = new PointF(100, 0); 

            RenderLine(a, b);
            RenderLine(b, c);
            RenderLine(c, d);
            RenderLine(d, a);
            PCT_CANVAS.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetPCT();
            angleText();

            PointF p1, p2, p3, p4;
            p1 = new PointF(-50,-50);
            p2 = new PointF(50, -50);
            p3 = new PointF(50, 50);
            p4 = new PointF(-50, 50);

            Render(p1, p2);
            Render(p2, p3);
            Render(p3, p4);
            Render(p4, p1);
            PCT_CANVAS.Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {



        }

  

        private void Render(PointF aa, PointF bb)
        {
            PointF a2, b2;
            int Sx = bmp.Width / 2;
            int Sy = bmp.Height / 2;

            a2 = new PointF(Sx + aa.X, Sy - aa.Y);
            b2 = new PointF(Sx + bb.X, Sy - bb.Y);

            a2.X = Sx + (float)((aa.X * Math.Cos(angle)) - (aa.Y * Math.Sin(angle)));
            a2.Y = Sy - (float)((aa.X * Math.Sin(angle)) + (aa.Y * Math.Cos(angle)));
            b2.X = Sx + (float)((bb.X * Math.Cos(angle)) - (bb.Y * Math.Sin(angle)));
            b2.Y = Sy - (float)((bb.X * Math.Sin(angle)) + (bb.Y * Math.Cos(angle)));

            g.DrawLine(Pens.Black, a2, b2);
        }

        private PointF Rotate(PointF a)
        {
            PointF b = new PointF();
            b.X = (float)((a.X * Math.Cos(angle)) - (a.Y * Math.Sin(angle)));
            b.Y = (float)((a.X * Math.Sin(angle)) + (a.Y * Math.Cos(angle))); 
            return b;
        }

        private PointF TranslateToCenter(PointF a)
        {
            int Sx = (bmp.Width / 2);  // origen central en x
            int Sy = (bmp.Height / 2); // origen central en y

            return new PointF(Sx + a.X, Sy - a.Y);
        }

        private PointF Translate(PointF a, PointF b)
        {
            return new PointF(a.X + b.X, a.Y + b.Y);
        }

        private void RenderLine(PointF a, PointF b)
        {
            
            a = Translate(a, new PointF(-50, -50));
            b = Translate(b, new PointF(-50, -50));

            PointF c = Rotate(a);
            PointF d = Rotate(b);

            c = TranslateToCenter(c);
            d = TranslateToCenter(d);

            c = Translate(c, new PointF(50, -50));
            d = Translate(d, new PointF(50, -50));

            g.DrawLine(Pens.Black, c, d);

        }
        private void ResetPCT()
        {
            g.Clear(Color.White);

            g.DrawLine(Pens.Red, 0, y, x *2, y); //Horizontal Line
            g.DrawLine(Pens.Red, x,0,x,y*2); //Vertical Line
        }

        public void angleText()
        {
            textBB = float.Parse(textBox1.Text);
            angle = (float)(Math.PI * textBB) / 180;
        }
        
    }
}
