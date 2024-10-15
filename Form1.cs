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
#if DEBUG
            lbl_debug_1.Show();
            lbl_debug_2.Show();
            lbl_debug_3.Show();
            lbl_debug_4.Show();
            textBox_debug_1.Show();
            textBox_debug_2.Show();
            textBox_debug_3.Show();
            textBox_debug_4.Show();
#endif
        }

        Bitmap bitmap;
        Graphics graphics;
        bool paint = false;

        Point startPoint;
        Point endPoint;


        /// <summary>
        /// Shape objectd
        /// </summary>
        ShapeBase shapeObject;

        /// <summary>
        /// Current tool in use
        /// </summary>
        ShapeBase.ToolTypeEnum toolType;


        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;

            startPoint = e.Location;
            shapeObject = ShapeBase.InitialiseShape(graphics, toolType);
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            textBox_x.Text = e.X.ToString();
            textBox_y.Text = e.Y.ToString();

            if(shapeObject == null)
            {
                return;
            }

            if (paint)
            {
                if (e.Button == MouseButtons.Left)
                {
                    endPoint = e.Location;

                    // Set and calculate common for all shapes values
                    shapeObject.SetStartEndPoints(startPoint, endPoint);
                    shapeObject.CalculateBaseValues();

                    textBox_width.Text = shapeObject.AbsWidth.ToString();
                    textBox_height.Text = shapeObject.AbsHeight.ToString();

                  
                    shapeObject.CalculateForShape();
                    Invalidate();
                }

                pic.Refresh();
            }
        }

        /// <summary>
        /// A method when the mouse control button is up
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Mouse event args</param>
        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            shapeObject?.DrawShape(graphics);
            pic.Refresh();

        }
        
        /// <summary>
        /// A method for paint event handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Privides data for paint</param>
        protected void OnPaint(object sender, PaintEventArgs e)
        {
            shapeObject?.DrawShape(e.Graphics);
        }

        /// <summary>
        /// Clear button event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void btn_clear_Click(object sender, EventArgs e)
        {
            graphics?.Clear(Color.White);
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