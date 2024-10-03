namespace WinFormsPaint
{
    /// <summary>
    /// Not stright line e.g. pencil or erase
    /// </summary>
    internal class LineShape : ShapeBase
    {
        /// <summary>
        /// Own start point
        /// </summary>
        private Point startPoint;

        /// <summary>
        /// Own end point
        /// </summary>
        private Point endPoint;

        /// <summary>
        /// Graphics used to paint
        /// </summary>
        private Graphics ownGraphics;

        /// <summary>
        /// Constructor <see cref="LineShape"/>
        /// </summary>
        /// /// <param name="graphics">Graphics used to paint</param>
        /// <param name="pen">Sets the pen type for this object</param>
        public LineShape(Graphics graphics, Pen pen)
        {
            ownGraphics = graphics;
            Pen = pen;
        }

        /// <summary>
        /// Calculates all, necessary for the shape values
        /// </summary>
        public override void CalculateForShape()
        {
            //Doesn't need separete calculations
        }

        /// <summary>
        /// Draw shape based on calculated values
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        public override void DrawShape(Graphics graphics)
        {
            startPoint = EndPoint;
            ownGraphics.DrawLine(Pen, startPoint, endPoint);
            endPoint = startPoint;
        }
    }
}
