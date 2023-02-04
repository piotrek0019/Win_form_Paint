using System.Net;
using System.Security.Cryptography.Xml;

namespace WinFormsPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Width = 1600;
            this.Height = 800;

            this.DoubleBuffered = true;

            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;

        }

        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen whitePen = new Pen(Color.White, 2);
        Pen erase = new Pen(Color.White, 10);

        Font arialFont = new Font("Arial", 16);
        string distanceOne = "";

        SolidBrush brush = new SolidBrush(Color.Black);
        int minX, minY, maxX, maxY, absWidth, absHeight, nonAbsWidth, nonAbsHeight, adjustedHeight, adjustetWidth, adjustedXforRectangleAngle, adjustedYforRectangleAngle;

        float startAngle;
        float startAlfaAngle;
        float alfaAngle;

        double alfaInRadiants;
        double test;

        Point startPoint;
        Point endPoint;
        Rectangle myRectangle;
        Rectangle rectangleForRectangleAngle;
        private Rectangle rectangleForAlfaAngle;

        Point halfWayPoint = new Point();



        enum Tool
        {
            Color,
            Fill,
            Pencil,
            Eraser,
            Ellipse,
            Rectangle,
            Line,
            Triangle,
            NoTool
        }


        Tool tool;


        public static int VectorLength(Point a, Point b)
        {
            double result;

            result = Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));

            return Convert.ToInt32(result);
        }
        //distanceOne = ((endPoint.X - selPoint.X) / 2).ToString();

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;

            ////cX = e.X;
            ////cY = e.Y;
            //if (tool == Tool.Rectangle)
            //{
            //    selPoint = e.Location;
            //}

           

            switch (tool)
            {
                case Tool.Rectangle:
                case Tool.Ellipse:
                case Tool.Line:
                case Tool.Triangle:
                    startPoint = e.Location;
                    break;
                
            }
            
        }

       
        private void pic_Click(object sender, EventArgs e)
        {

        }

        

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            textBox_x.Text = e.X.ToString();
            textBox_y.Text = e.Y.ToString();

            if (paint)
            {
                if (e.Button == MouseButtons.Left)
                {
                    endPoint = e.Location;

                    minX = Math.Min(startPoint.X, endPoint.X);
                    minY = Math.Min(startPoint.Y, endPoint.Y);
                    maxX = Math.Max(startPoint.X, endPoint.X);
                    maxY = Math.Max(startPoint.Y, endPoint.Y);
                    absWidth = Math.Abs(endPoint.X - startPoint.X);
                    absHeight = Math.Abs(endPoint.Y - startPoint.Y);

                    nonAbsWidth = endPoint.X - startPoint.X;
                    nonAbsHeight = endPoint.Y - startPoint.Y;

                    textBox_width.Text = (maxX - minX).ToString();
                    textBox_height.Text = (maxY - minY).ToString();


                    if ((maxY - minY) > 0 && (maxY - minY) > 0)
                    {
                        alfaInRadiants = Math.Atan(Convert.ToDouble(maxY - minY) / Convert.ToDouble(maxX - minX));
                        alfaAngle = Convert.ToSingle(alfaInRadiants * 180 / Math.PI);

                        //Debug and temporary usage
                        textBox_debug_1.Text = "alfa in rad: " + alfaInRadiants.ToString();
                        textBox_debug_2.Text = "alfa in degrees: " + alfaAngle.ToString();
                    }



                    switch (tool)
                    {
                        case Tool.Pencil:
                            px = e.Location;
                            g.DrawLine(p, px, py);
                            py = px;
                            break;
                        case Tool.Eraser:
                            px = e.Location;
                            g.DrawLine(erase, px, py);
                            py = px;
                            break;
                        case Tool.Line:
                        case Tool.Triangle:
                            //halfWayPoint.X = ((endPoint.X - selPoint.X) / 2) + selPoint.X - 40;
                            //halfWayPoint.Y = ((endPoint.Y - selPoint.Y) / 2) + selPoint.Y - 20;

                            halfWayPoint.X = ((endPoint.X - startPoint.X) / 2) + startPoint.X;
                            halfWayPoint.Y = ((endPoint.Y - startPoint.Y) / 2) + startPoint.Y;

                            //rectangleForAlfaAngle = new Rectangle(endPoint.X - ((endPoint.X - startPoint.X) / 2), minY + ((endPoint.Y - startPoint.Y) / 2), w, h);
                            rectangleForAlfaAngle = new Rectangle(endPoint.X - (absWidth / 2), minY + (nonAbsHeight / 2), absWidth, absHeight);

                            if (startPoint.X <= endPoint.X && startPoint.Y <= endPoint.Y)
                            {
                                startAngle = 270f;
                                startAlfaAngle = 180f;
                            }
                            else if(startPoint.X > endPoint.X && startPoint.Y <= endPoint.Y )
                            {
                                startAngle = 180f;
                                startAlfaAngle = 360f - alfaAngle;
                            }
                            else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
                            {
                                startAngle = 90f;
                                startAlfaAngle = 0f;
                            }
                            else
                            {
                                startAngle = 0f;
                                startAlfaAngle = 180f - alfaAngle;
                            }
                            

                            adjustedHeight = Convert.ToInt32(absHeight / 1.5);
                            adjustetWidth = Convert.ToInt32(absWidth / 1.5);

                            adjustedXforRectangleAngle = adjustetWidth / 4;
                            adjustedYforRectangleAngle = adjustedHeight / 4;

                            //just example and not calculating real distance
                            distanceOne = VectorLength(startPoint, endPoint).ToString();



                            //rectangleForRectangleAngle = new Rectangle(minX - ((endPoint.X - startPoint.X) / 2) + adjustedXforRectangleAngle,
                            //    minY + ((endPoint.Y - startPoint.Y) / 2) + adjustedYforRectangleAngle,
                            //    adjustetWidth,
                            //    adjustedHeight);

                            rectangleForRectangleAngle = new Rectangle(minX - (nonAbsWidth / 2) + adjustedXforRectangleAngle,
                                minY + (nonAbsHeight / 2) + adjustedYforRectangleAngle,
                                adjustetWidth,
                                adjustedHeight);


                            this.Invalidate();

                            //points for line here
                            // or just draw below??
                            break;
                        case Tool.Rectangle:
                        case Tool.Ellipse:
                            myRectangle = new Rectangle(minX, minY, absWidth, absHeight);
                                this.Invalidate();

                                // sX = e.X - myRectangle.Left;
                                // sY = e.Y - myRectangle.Top;
                                // myRectangle = new Rectangle(myRectangle.Left, myRectangle.Top, sX, sY);
                                // //this.Invalidate();
                                // //pic.Refresh();
                                break;

                    }
                }


                pic.Refresh();

                // x = e.X;
                // y = e.Y;
                //
                // sX = e.X - cX;
                // sY = e.Y - cY;

            }
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            switch (tool)
            {
                case Tool.Ellipse:
                    g.DrawEllipse(p, myRectangle);
                    pic.Refresh();
                    break;
                case Tool.Rectangle:
                    g.DrawRectangle(p, myRectangle);
                    pic.Refresh();
                    break;
                case Tool.Line:
                    g.DrawLine(p, startPoint, endPoint);
                    g.DrawString(distanceOne, arialFont, brush, halfWayPoint);
                    break;
                case Tool.Triangle:
                    g.DrawLine(p, startPoint, endPoint);
                    g.DrawString(distanceOne, arialFont, brush, halfWayPoint);
                    g.DrawLine(p, startPoint.X, startPoint.Y, startPoint.X, endPoint.Y);
                    g.DrawLine(p, startPoint.X, endPoint.Y, endPoint.X, endPoint.Y);

                    g.DrawArc(p, rectangleForRectangleAngle, startAngle, 90.0f);

                    g.DrawArc(p, rectangleForAlfaAngle, startAlfaAngle, alfaAngle);
                    break;
            }
            //if (tool == Tool.Ellipse)
            //{
            //    g.DrawEllipse(p, cX, cY, sX, sY);
            //    pic.Refresh();
            //}
            //if (tool == Tool.Rectangle)
            //{
            //    g.DrawRectangle(p, cX, cY, sX, sY);
            //    pic.Refresh();
            //}
        }
        //draws objects while mouse is moving and left button is down
        protected void OnPaint(object sender, PaintEventArgs e)
        {
            //switch
            //ellipse and so on
            // e.Graphics.DrawRectangle(p, myRectangle);

            switch (tool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(p, startPoint, endPoint);
                    e.Graphics.DrawString(distanceOne, arialFont, brush, halfWayPoint);
                    break;
                case Tool.Rectangle:
                    e.Graphics.DrawRectangle(p, myRectangle);
                    break;
                case Tool.Ellipse:
                    e.Graphics.DrawEllipse(p, myRectangle);
                    break;
                case Tool.Triangle:
                    e.Graphics.DrawLine(p, startPoint, endPoint);
                    e.Graphics.DrawString(distanceOne, arialFont, brush, halfWayPoint);
                    e.Graphics.DrawLine(p, startPoint.X, startPoint.Y, startPoint.X, endPoint.Y);
                    e.Graphics.DrawLine(p, startPoint.X, endPoint.Y, endPoint.X, endPoint.Y);


                   
                    if(rectangleForRectangleAngle.Width > 1 && rectangleForRectangleAngle.Height > 1)
                    {
                        //e.Graphics.DrawRectangle(p, myRectangle);
                        e.Graphics.DrawArc(p, rectangleForRectangleAngle, startAngle, 90.0f);
                        //e.Graphics.DrawRectangle(p, rectangleForRectangleAngle);

                        //e.Graphics.DrawRectangle(p, rectangleForAlfaAngle);
                        e.Graphics.DrawArc(p, rectangleForAlfaAngle, startAlfaAngle, alfaAngle);
                        //thirdLine to implement //trigonometie?//
                    }
                    break;
            }
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pic.Image = bm;
            tool = Tool.NoTool;
        }

        private void btn_triangle_Click(object sender, EventArgs e)
        {
            tool = Tool.Triangle;
        }
        private void btn_line_Click(object sender, EventArgs e)
        {
            tool = Tool.Line;
        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            tool = Tool.Rectangle;
        }
        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            tool = Tool.Ellipse;
        }
        private void btn_eraser_Click(object sender, EventArgs e)
        {
            tool = Tool.Eraser;
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            tool = Tool.Pencil;
        }
    }
}