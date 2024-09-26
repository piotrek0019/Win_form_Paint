using System.Net;
using System.Numerics;
using System.Security.Cryptography.Xml;

namespace WinFormsPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Width = 1600;
            Height = 800;

            DoubleBuffered = true;

            bitmap = new Bitmap(pic.Width, pic.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pic.Image = bitmap;

        }

        Bitmap bitmap;
        Graphics graphics;
        bool paint = false;
        Point px, py;
        Pen pen = new Pen(Color.Black, 1);
        Pen whitePen = new Pen(Color.White, 2);
        Pen erase = new Pen(Color.White, 10);

        Font arialFont = new Font("Arial", 16);
        string distanceOne = "";

        SolidBrush brush = new SolidBrush(Color.Black);
        int minX, minY, maxX, maxY, absWidth, absHeight, nonAbsWidth, nonAbsHeight, adjustedHeight, adjustetWidth, adjustedXforRectangleAngle, adjustedYforRectangleAngle;

        float startAngle;
        float startAlfaAngle;
        float alfaAngle;
        float startBetaAngle;
        float betaAngle;

        double alfaInRadiants;
        double betaInRadiants;
        

        Point startPoint;
        Point endPoint;
        Rectangle myRectangle;
        Rectangle rectangleForRectangleAngle;
        Rectangle rectangleForAlfaAngle;
        Rectangle rectangleForBetaAngle;

        Point halfWayPoint = new Point();


        /// <summary>
        /// Shape objectd
        /// </summary>
        ShapeBase shapeObject;


        ///// <summary>
        ///// Tool types
        ///// </summary>
        //enum ToolTypeEnum
        //{
        //    Color,
        //    Fill,
        //    Pencil,
        //    Eraser,
        //    Ellipse,
        //    Rectangle,
        //    Line,
        //    Triangle,
        //    NoTool
        //}


        ShapeBase.ToolTypeEnum toolType;


        //public static int VectorMagnitude(Point a, Point b)
        //{
        //    double result;
            
        //    //result = Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
        //    result = Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));

        //    return Convert.ToInt32(result);
        //}



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



            switch (toolType)
            {
                case ShapeBase.ToolTypeEnum.Rectangle:
                case ShapeBase.ToolTypeEnum.Ellipse:
                case ShapeBase.ToolTypeEnum.Line:
                case ShapeBase.ToolTypeEnum.Triangle:
                    startPoint = e.Location;
                    break;
            }

            shapeObject = ShapeBase.InitialiseShape(toolType);
                
            
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

                    // Set and calculate common for all shapes values
                    shapeObject?.SetStartEndPoints(startPoint, endPoint);
                    shapeObject?.CalculateBaseValues();

                    //TODO PA when all shapes have own classes the swith should be redundant
                    switch (toolType)
                    {
                        case ShapeBase.ToolTypeEnum.Pencil:
                            px = e.Location;
                            graphics.DrawLine(pen, px, py);
                            py = px;
                            break;
                        case ShapeBase.ToolTypeEnum.Eraser:
                            px = e.Location;
                            graphics.DrawLine(erase, px, py);
                            py = px;
                            break;
                        case ShapeBase.ToolTypeEnum.Line:
                            break;
                        case ShapeBase.ToolTypeEnum.Rectangle:
                        case ShapeBase.ToolTypeEnum.Triangle:
                        case ShapeBase.ToolTypeEnum.Ellipse:
                            shapeObject.CalculateForShape();

                            break;
                    }
                    Invalidate();
                }

                pic.Refresh();
            }
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            switch (toolType)
            {
                case ShapeBase.ToolTypeEnum.Ellipse:
                    graphics.DrawEllipse(pen, myRectangle);
                    pic.Refresh();
                    break;
                case ShapeBase.ToolTypeEnum.Rectangle:
                case ShapeBase.ToolTypeEnum.Triangle:
                    shapeObject.DrawShape(graphics);
                    pic.Refresh();
                    break;
                case ShapeBase.ToolTypeEnum.Line:
                    graphics.DrawLine(pen, startPoint, endPoint);
                    graphics.DrawString(distanceOne, arialFont, brush, halfWayPoint);
                    break;
            }
            
        }
        // TODO PA when all shapes have own classes the swith should be redundant
        // Draws objects while mouse is moving and left button is down
        protected void OnPaint(object sender, PaintEventArgs e)
        {
            switch (toolType)
            {
                case ShapeBase.ToolTypeEnum.Line:
                    e.Graphics.DrawLine(pen, startPoint, endPoint);
                    e.Graphics.DrawString(distanceOne, arialFont, brush, halfWayPoint);
                    break;
                case ShapeBase.ToolTypeEnum.Rectangle:
                case ShapeBase.ToolTypeEnum.Triangle:
                    //e.Graphics.DrawRectangle(pen, myRectangle);
                    shapeObject.DrawShape(e.Graphics);
                    break;
                case ShapeBase.ToolTypeEnum.Ellipse:
                    e.Graphics.DrawEllipse(pen, myRectangle);
                    break;
            }
        }

        /// <summary>
        /// Clear button event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            pic.Image = bitmap;
            toolType = ShapeBase.ToolTypeEnum.NoTool;
        }

        private void btn_triangle_Click(object sender, EventArgs e)
        {
            toolType = ShapeBase.ToolTypeEnum.Triangle;
        }
        private void btn_line_Click(object sender, EventArgs e)
        {
            toolType = ShapeBase.ToolTypeEnum.Line;
        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            toolType = ShapeBase.ToolTypeEnum.Rectangle;
        }
        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            toolType = ShapeBase.ToolTypeEnum.Ellipse;
        }
        private void btn_eraser_Click(object sender, EventArgs e)
        {
            toolType = ShapeBase.ToolTypeEnum.Eraser;
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            toolType = ShapeBase.ToolTypeEnum.Pencil;
        }
    }
}