namespace WinFormsPaint
{
    /// <summary>
    /// Base class for shapes to draw
    /// </summary>
    public abstract class ShapeBase
    {
        /// <summary>
        /// Tool types
        /// </summary>
        public enum ToolTypeEnum
        {
            //TODO each element of enum could have own summary
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

        /// <summary>
        /// The minimal X coordinate
        /// </summary>
        protected int MinX { get; set; }

        /// <summary>
        /// The minimal Y coordinate
        /// </summary>
        protected int MinY { get; set; }

        /// <summary>
        /// The maximal X coordinate
        /// </summary>
        protected int MaxX { get; set; }

        /// <summary>
        /// The maximal Y coordinate
        /// </summary>
        protected int MaxY { get; set; }

        /// <summary>
        /// The absolute width
        /// </summary>
        protected int AbsWidth { get; set; }

        /// <summary>
        /// The absolute height
        /// </summary>
        protected int AbsHeight { get; set; }

        /// <summary>
        /// The non-absolute width
        /// </summary>
        protected int NonAbsWidth { get; set; }

        /// <summary>
        /// The non-absolute height
        /// </summary>
        protected int NonAbsHeight { get; set; }

        /// <summary>
        /// The point where the shape starts being drawn
        /// </summary>
        protected Point StartPoint { get; set; }

        /// <summary>
        /// The point where the shape ends being drawn
        /// </summary>
        protected Point EndPoint { get; set; }

        /// <summary>
        /// The calculated distance to show next to/with the shape
        /// </summary>
        protected string DistanceOne { get; set; } = string.Empty;

        /// <summary>
        /// Drawing pen with default black colour and size 1
        /// </summary>
        public Pen Pen { get; set; } = new Pen(Color.Black, 1);

        /// <summary>
        /// Font with default values: arial, size 16
        /// </summary>
        public Font Font { get; set; } = new Font("Arial", 16);

        /// <summary>
        /// Font's colour
        /// </summary>
        public SolidBrush Brush { get; set; } = new SolidBrush(Color.Black);

        /// <summary>
        /// Sets start and end points
        /// </summary>
        /// <param name="startPoint">Start point to set</param>
        /// <param name="endPoint">End point to set</param>
        public void SetStartEndPoints(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        /// <summary>
        /// Calculates minminal, maxmial coordinates and dimensions
        /// </summary>
        public void CalculateBaseValues()
        {
            MinX = Math.Min(StartPoint.X, EndPoint.X);
            MinY = Math.Min(StartPoint.Y, EndPoint.Y);
            MaxX = Math.Max(StartPoint.X, EndPoint.X);
            MaxY = Math.Max(StartPoint.Y, EndPoint.Y);
            AbsWidth = Math.Abs(EndPoint.X - StartPoint.X);
            AbsHeight = Math.Abs(EndPoint.Y - StartPoint.Y);

            NonAbsWidth = EndPoint.X - StartPoint.X;
            NonAbsHeight = EndPoint.Y - StartPoint.Y;
        }


        /// <summary>
        /// Calculates all, necessary for the shape values
        /// </summary>
        public abstract void CalculateForShape();

        /// <summary>
        /// Draw shape based on calculated values
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        public abstract void DrawShape(Graphics graphics);

        /// <summary>
        /// Initialises different shape objects
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        /// <param name="toolType">Tool type</param>
        /// <returns>New shape object</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ShapeBase InitialiseShape(Graphics graphics, ToolTypeEnum toolType)
        {
            switch (toolType)
            {
                case ToolTypeEnum.Rectangle:
                    return new RectangleShape();
                case ToolTypeEnum.Triangle:
                    return new TriangleShape();
                case ToolTypeEnum.Pencil:
                    return new LineShape(graphics, ShapeHelpers.GetPen());
                case ToolTypeEnum.Eraser:
                    return new LineShape(graphics, ShapeHelpers.GetEraser());
                default:
                    return null;
            }
        }

        //public void TestPencil(Graphics grapthics)
        //{
        //    StartPoint = EndPoint;
        //    grapthics.DrawLine(pen, StartPoint)
        //}
    }
}
