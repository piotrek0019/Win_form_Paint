namespace WinFormsPaint
{
    public class TriangleShape : ShapeBase
    {

        /// <summary>
        /// Adjusted dimensions
        /// </summary>
        private int adjustedHeight, adjustetWidth;

        /// <summary>
        /// Angles in degrees
        /// </summary>
        float alfaAngle, betaAngle, startAngle, startAlfaAngle, startBetaAngle, adjustedXforRectangleAngle, adjustedYforRectangleAngle;

        /// <summary>
        /// Angles in radiants
        /// </summary>
        private double alfaInRadiants, betaInRadiants;

        /// <summary>
        /// The rectangle used for the rectangle angle
        /// </summary>
        private Rectangle rectangleForRectangleAngle;

        /// <summary>
        /// The rectangle used for the alpha angle
        /// </summary>
        private Rectangle rectangleForAlfaAngle;

        /// <summary>
        /// The rectangle used for the beta angle
        /// </summary>
        private Rectangle rectangleForBetaAngle;

        /// <summary>
        /// The midpoint used in calculations
        /// </summary>
        private Point halfWayPoint = new Point();
        
        /// <summary>
        /// Gets calculated alpha angle in radiants
        /// </summary>
        /// <returns></returns>
        private double GetCaculatedAlphaInRadiants()
        {
            return Math.Atan(Convert.ToDouble(MaxY - MinY) / Convert.ToDouble(MaxX - MinX));
        }

        /// <summary>
        /// Gets calculated beta angle in radiants
        /// </summary>
        /// <returns></returns>
        private double GetCalculatedAlphaInRadiants()
        {
            return Math.Atan(Convert.ToDouble(MaxX - MinX) / Convert.ToDouble(MaxY - MinY));
        }

        /// <summary>
        /// Calculates all, necessary for the shape values
        /// </summary>
        public override void CalculateForShape()
        {
            base.CalculateForShape();

            if ((MaxY - MinY) > 0 && (MaxX - MinX) > 0)
            {
                alfaInRadiants = GetCaculatedAlphaInRadiants();
                alfaAngle = Convert.ToSingle(alfaInRadiants * 180 / Math.PI);


                //beta angle
                betaInRadiants = GetCalculatedAlphaInRadiants();
                betaAngle = Convert.ToSingle(betaInRadiants * 180 / Math.PI);

                //Debug and temporary usage
                //textBox_debug_1.Text = "alfa in rad: " + alfaInRadiants.ToString();
                //textBox_debug_2.Text = "alfa in degrees: " + alfaAngle.ToString();

                //textBox_debug_3.Text = "beta in degrees: " + betaAngle.ToString();
            }



            halfWayPoint.X = ((EndPoint.X - StartPoint.X) / 2) + StartPoint.X;
            halfWayPoint.Y = ((EndPoint.Y - StartPoint.Y) / 2) + StartPoint.Y;

            rectangleForAlfaAngle = new Rectangle(EndPoint.X - (AbsWidth / 2), MinY + (NonAbsHeight / 2), AbsWidth, AbsHeight);

            rectangleForBetaAngle = new Rectangle(MinX - (NonAbsWidth / 2), MinY - (NonAbsHeight / 2), AbsWidth, AbsHeight);

            if (StartPoint.X <= EndPoint.X && StartPoint.Y <= EndPoint.Y)
            {
                startAngle = 270f;
                startAlfaAngle = 180f;
                startBetaAngle = 90f - betaAngle;
            }
            else if (StartPoint.X > EndPoint.X && StartPoint.Y <= EndPoint.Y)
            {
                startAngle = 180f;
                startAlfaAngle = 360f - alfaAngle;
                startBetaAngle = 90f;
            }
            else if (StartPoint.X > EndPoint.X && StartPoint.Y > EndPoint.Y)
            {
                startAngle = 90f;
                startAlfaAngle = 0f;
                startBetaAngle = 270f - betaAngle;
            }
            else
            {
                startAngle = 0f;
                startAlfaAngle = 180f - alfaAngle;
                startBetaAngle = 270f;
            }


            adjustedHeight = Convert.ToInt32(AbsHeight / 1.5);
            adjustetWidth = Convert.ToInt32(AbsWidth / 1.5);

            adjustedXforRectangleAngle = adjustetWidth / 4;
            adjustedYforRectangleAngle = adjustedHeight / 4;

            var rectangleX = Convert.ToInt32(MinX - (NonAbsWidth / 2) + adjustedXforRectangleAngle);
            var rectangeY = Convert.ToInt32(MinY + (NonAbsHeight / 2) + adjustedYforRectangleAngle);
            rectangleForRectangleAngle = new Rectangle(
                rectangleX,
                rectangeY,
                adjustetWidth,
                adjustedHeight);
        }

        /// <summary>
        /// Draw triangle based on calculated values
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        public override void DrawShape(Graphics graphics)
        {
            graphics.DrawLine(Pen, StartPoint, EndPoint);
            graphics.DrawString(DistanceOne, Font, Brush, halfWayPoint);
            graphics.DrawLine(Pen, StartPoint.X, StartPoint.Y, StartPoint.X, EndPoint.Y);
            graphics.DrawLine(Pen, StartPoint.X, EndPoint.Y, EndPoint.X, EndPoint.Y);

            if (rectangleForRectangleAngle.Width > 1 && rectangleForRectangleAngle.Height > 1)
            {
                graphics.DrawArc(Pen, rectangleForRectangleAngle, startAngle, 90.0f);
                graphics.DrawArc(Pen, rectangleForAlfaAngle, startAlfaAngle, alfaAngle);
                graphics.DrawArc(Pen, rectangleForBetaAngle, startBetaAngle, betaAngle);
            }
        }

    }
}