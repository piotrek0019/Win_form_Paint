using System.Drawing;

namespace WinFormsPaint
{
    /// <summary>
    /// Not stright line e.g. pencil, erase or strightLine
    /// </summary>
    internal class LineShape : ShapeBase
    {
        /// <summary>
        /// Is this object in non-stright line mode?
        /// </summary>
        bool isNonStrightLine = false;

        /// <summary>
        /// Own start point
        /// </summary>
        private Point ownStartPoint;

        /// <summary>
        /// Own end point
        /// </summary>
        private Point ownEndPoint;

        /// <summary>
        /// Midddle point
        /// </summary>
        private Point midPoint;

        /// <summary>
        /// Graphics used to paint
        /// </summary>
        private readonly Graphics? ownGraphics = null;

        /// <summary>
        /// Constructor <see cref="LineShape"/> intialises object for drawing strigh line
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        /// <param name="pen">Sets the pen type for this object</param>
        public LineShape(Pen pen)
        {
            Pen = pen;
        }

        /// <summary>
        /// Constructor <see cref="LineShape"/> intialises object for drawing non-strigh line
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        /// <param name="pen">Sets the pen type for this object</param>
        public LineShape(Graphics graphics, Pen pen)
        {
            ownGraphics = graphics;
            Pen = pen;
            isNonStrightLine = true;
        }

        /// <summary>
        /// Calculates all, necessary for the shape values
        /// </summary>
        public override void CalculateForShape()
        {
            if(isNonStrightLine)
            {
                if(ownEndPoint == default)
                {
                    ownEndPoint = StartPoint;
                }
                
                return;
            }
            midPoint = CalculateMidPoint(StartPoint, EndPoint);
            base.CalculateForShape();
        }

        /// <summary>
        /// Draw shape based on calculated values
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        public override void DrawShape(Graphics graphics)
        {
            if(isNonStrightLine)
            {
                DrawNonStrightLine();
                return;
            }

            DrawStrightLine(graphics);

        }

        /// <summary>
        /// Draws non strigh line e.g. pencil or erase
        /// </summary>
        private void DrawNonStrightLine()
        {
            ownStartPoint = EndPoint;
            ownGraphics?.DrawLine(Pen, ownStartPoint, ownEndPoint);
            ownEndPoint = ownStartPoint;
        }

        /// <summary>
        /// Draws strigh line
        /// </summary>
        private void DrawStrightLine(Graphics graphics)
        {
            graphics.DrawLine(Pen, StartPoint, EndPoint);
            graphics.DrawString(DistanceOne, Font, Brush, midPoint);
        }

        private Point CalculateMidPoint(Point startPoint, Point endPoint)
        {
            var startX = startPoint.X;
            var startY = startPoint.Y;

            var endX = endPoint.X;
            var endY = endPoint.Y;

            var midX = (startX + endX) / 2;
            var midY = (startY + endY) / 2;

            return new Point(midX, midY);
        }
    }
}
