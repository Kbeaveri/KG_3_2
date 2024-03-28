using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG_3_2
{
    public partial class Form1 : Form
    {
        Graphics g;
        float W;
        float H;
        float x_center;
        float y_center;
        float dx;
        float dy;
        int x1 = 0;
        int y1 = 0;
        int R = 8;
        int x_centering;
        int y_centering;
        bool flag = false;
        bool invertingX = false;
        bool invertingY = false;
        bool invertingXY = false;
        bool startBrzd = false;
        bool startPic = false;
        Pen pen = new Pen(Color.Black);
        Pen linePen = new Pen(Color.Red, 2);
        Pen linePen2 = new Pen(Color.Green, 2);
        Pen pointLine = new Pen(Color.Blue, 10);
        Pen Rec = new Pen(Color.Black);
        Brush Br = Brushes.Gray;
        List<Point> points = new List<Point>();
        List<Point> pic = new List<Point>();
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            W = this.pictureBox1.Width;
            H = this.pictureBox1.Height;
            x_center = W / 2;
            y_center = H / 2;
            dx = W / 20;
            dy = H / 20;
        }
        private void DrawAxis()
        {
            g.Clear(Color.White);
            Pen Axis = new Pen(Color.Black, 3);
            g.DrawLine(Axis, x_center, 0, x_center, H);
            g.DrawLine(Axis, 0, y_center, W, y_center);
            Font Fon = new Font("Arial", 9, FontStyle.Regular);
            Brush Br = Brushes.Black;
            g.DrawString("X", Fon, Br, W - 15, y_center + 10);
            g.DrawString("Y", Fon, Br, x_center - 20, 10);
            for (int i = -10; i < 10; i++)
            {
                g.DrawString(i.ToString(), Fon, Br, x_center - 15, y_center + dy * i);
                g.DrawString(i.ToString(), Fon, Br, x_center + dx * i - 10, y_center + 10);
            }
            for (int i = 0; i < 20; i++)
            {
                g.DrawLine(pen, 1, 1 * i * dy, W, 1 * i * dy);
                g.DrawLine(pen, 1 * i * dx, 1, 1 * i * dx, H);
            }
        }
        float X(int x)
        {
            return x_center + x * dx;
        }
        float Y(int y)
        {
            return y_center + y * -dy;
        }
        private void drawPoint(int x, int y)
        {
            g.DrawEllipse(pointLine,X(x), Y(y), 6, 6);
        }
        void DrawLine(float x1, float y1, float x2, float y2, Pen pen)
        {
            g.DrawLine(pen, x_center + x1 * dx, y_center + y1 * -dy, x_center + x2 * dx, y_center + y2 * -dy);
        }
        void centering()
        {
            x_centering = -x1;
            y_centering = -y1;
            x1 = 0;
            y1 = 0;
            DrawAxis();
        }
        void centering2()
        {
            for (int i = 0; i < pic.Count; i++)
            {
                pic[i] = new Point(pic[i].X - x_centering, pic[i].Y - y_centering);
            }
        }

        private void BRZN()
        {
            int x = 0;
            int y = R;
            int del = 2 * (1 - R);
            int lim = 0;
            int lim1 = 0;
            while (y > 0)
            {
                points.Add(new Point(x, y));
                if (del < 0)
                {
                    lim = 2 * del + 2 * y - 1;
                    if (lim <= 0)
                    {
                        x += 1;
                        del = del + 2 * x + 1;
                        continue;
                    }
                    else
                    {
                        x += 1;
                        y -= 1;
                        del = del + 2* x - 2 * y + 2;
                        continue;
                    }
                }
                else
                {
                    if (del > 0) {
                        lim1 = 2 * del - 2 * x - 1;
                        if (lim1<= 0)
                        {
                            x += 1;
                            y -= 1;
                            del = del + 2 * x - 2 * y + 2;
                            continue;
                        }
                        else
                        {
                            y -= 1;
                            del = del - 2 * y + 1;
                        }
                    }
                    else
                    {
                        x += 1;
                        y -= 1;
                        del = del + 2 * x - 2 * y + 2;
                        continue;
                    }
                }
            }
            points.Add(new Point(R,0));
        }

        private void drawPic()
        {
            pic.Add(new Point(points[0].X, points[0].Y + 1));
            for (int i = 1; i < points.Count - 1; i++)
            {
                if (points[i].Y < points[i + 1].Y)
                {
                    pic.Add(new Point(points[i].X, points[i].Y + 1));
                }
                else
                {
                    pic.Add(new Point(points[i].X, points[i].Y));
                }
            }
        }
        private void drawRec()
        {
            for (int i = 0; i < points.Count; i++)
            {
                g.FillRectangle(Br, X(points[i].X), Y(points[i].Y), 27, 27);
            }
        }

        void invertY()
        {
            for (int i = points.Count-1;i >= 0;i--)
            {
                points.Add(new Point(points[i].X, -points[i].Y));
            }
        }
        void invertX()
        {
            for (int i = points.Count - 1; i >= 0; i--)
            {
                points.Add(new Point(-points[i].X, points[i].Y));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                DrawAxis();
                flag = true;
                return;
            }
            BRZN();
            invertY();
            invertX();
            for (int i = 0;i < points.Count;i++)
            {
                drawPoint(points[i].X, points[i].Y);
            }
            drawRec();


        }
    }
}
