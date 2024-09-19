namespace WinFormsPaint
{
    public class TriangleFactory : DrawShapeBase
    {
        /// <summary>
        /// Drawing pen
        /// </summary>
        private Pen pen = new Pen(Color.Black, 1);

        /// <summary>
        /// Font arial, size 16
        /// </summary>
        private Font arialFont = new Font("Arial", 16);

        /// <summary>
        /// Font's colour
        /// </summary>
        private SolidBrush brush = new SolidBrush(Color.Black);

        /// <summary>
        /// Calculated distance to show
        /// </summary>
        string distanceOne = "";

        /// <summary>
        /// Adjusted dimensions
        /// </summary>
        private int adjustedHeight, adjustetWidth;

        /// <summary>
        /// Angles
        /// </summary>
        float startAngle, startAlfaAngle, startBetaAngle, adjustedXforRectangleAngle, adjustedYforRectangleAngle;

        /// <summary>
        /// Backing field for <see cref="AlfaInRadiants"/>!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        private double alfaInRadiants, betaInRadiants;

        // <summary>
        /// Backing field for <see cref="AlfaAngle"/>
        /// </summary>
        private float alfaAngle;

        // <summary>
        /// Backing field for <see cref="BetaAngle"/>
        /// </summary>
        private float betaAngle;

        Rectangle rectangleForRectangleAngle;
        Rectangle rectangleForAlfaAngle;
        Rectangle rectangleForBetaAngle;

        Point halfWayPoint = new Point();

        //or mayby invalidate eg like to null???
        public void Calculate()
        {
            if ((maxY - minY) > 0 && (maxX - minX) > 0)
            {
                alfaInRadiants = Math.Atan(Convert.ToDouble(maxY - minY) / Convert.ToDouble(maxX - minX));
                alfaAngle = Convert.ToSingle(alfaInRadiants * 180 / Math.PI);



                //
                //

                //beta angle
                betaInRadiants = Math.Atan(Convert.ToDouble(maxX - minX) / Convert.ToDouble(maxY - minY));
                betaAngle = Convert.ToSingle(betaInRadiants * 180 / Math.PI);

                //Debug and temporary usage
                //textBox_debug_1.Text = "alfa in rad: " + alfaInRadiants.ToString();
                //textBox_debug_2.Text = "alfa in degrees: " + alfaAngle.ToString();

                //textBox_debug_3.Text = "beta in degrees: " + betaAngle.ToString();
            }



            halfWayPoint.X = ((endPoint.X - startPoint.X) / 2) + startPoint.X;
            halfWayPoint.Y = ((endPoint.Y - startPoint.Y) / 2) + startPoint.Y;

            //rectangleForAlfaAngle = new Rectangle(endPoint.X - ((endPoint.X - startPoint.X) / 2), minY + ((endPoint.Y - startPoint.Y) / 2), w, h);
            rectangleForAlfaAngle = new Rectangle(endPoint.X - (absWidth / 2), minY + (nonAbsHeight / 2), absWidth, absHeight);

            rectangleForBetaAngle = new Rectangle(minX - (nonAbsWidth / 2), minY - (nonAbsHeight / 2), absWidth, absHeight);

            if (startPoint.X <= endPoint.X && startPoint.Y <= endPoint.Y)
            {
                startAngle = 270f;
                startAlfaAngle = 180f;
                startBetaAngle = 90f - betaAngle;
            }
            else if (startPoint.X > endPoint.X && startPoint.Y <= endPoint.Y)
            {
                startAngle = 180f;
                startAlfaAngle = 360f - alfaAngle;
                startBetaAngle = 90f;
            }
            else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
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


            adjustedHeight = Convert.ToInt32(absHeight / 1.5);
            adjustetWidth = Convert.ToInt32(absWidth / 1.5);

            adjustedXforRectangleAngle = adjustetWidth / 4;
            adjustedYforRectangleAngle = adjustedHeight / 4;

            //just example and not calculating real distance
            distanceOne = VectorMagnitude(startPoint, endPoint).ToString();



            //rectangleForRectangleAngle = new Rectangle(minX - ((endPoint.X - startPoint.X) / 2) + adjustedXforRectangleAngle,
            //    minY + ((endPoint.Y - startPoint.Y) / 2) + adjustedYforRectangleAngle,
            //    adjustetWidth,
            //    adjustedHeight);
            var rectangleX = Convert.ToInt32(minX - (nonAbsWidth / 2) + adjustedXforRectangleAngle);
            var rectangeY = Convert.ToInt32(minY + (nonAbsHeight / 2) + adjustedYforRectangleAngle);
            rectangleForRectangleAngle = new Rectangle(
                rectangleX,
                rectangeY,
                adjustetWidth,
                adjustedHeight);
        }

        public double GetCaculatedAlphaInRadiants()
        {
            return Math.Atan(Convert.ToDouble(maxY - minY) / Convert.ToDouble(maxX - minX));
        }

        public static int VectorMagnitude(Point a, Point b)
        {
            double result;
            result = Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));

            return Convert.ToInt32(result);
        }

        public void DrawTriangle(Graphics graphics)
        {
            graphics.DrawLine(pen, startPoint, endPoint);
            graphics.DrawString(distanceOne, arialFont, brush, halfWayPoint);
            graphics.DrawLine(pen, startPoint.X, startPoint.Y, startPoint.X, endPoint.Y);
            graphics.DrawLine(pen, startPoint.X, endPoint.Y, endPoint.X, endPoint.Y);
            graphics.DrawArc(pen, rectangleForRectangleAngle, startAngle, 90.0f);
            graphics.DrawArc(pen, rectangleForAlfaAngle, startAlfaAngle, alfaAngle);
            graphics.DrawArc(pen, rectangleForBetaAngle, startBetaAngle, betaAngle);
        }

        public void DrawTriangle2(Graphics graphics)
        {
            graphics.DrawLine(pen, startPoint, endPoint);
            graphics.DrawString(distanceOne, arialFont, brush, halfWayPoint);
            graphics.DrawLine(pen, startPoint.X, startPoint.Y, startPoint.X, endPoint.Y);
            graphics.DrawLine(pen, startPoint.X, endPoint.Y, endPoint.X, endPoint.Y);

            if (rectangleForRectangleAngle.Width > 1 && rectangleForRectangleAngle.Height > 1)
            {
                //e.Graphics.DrawRectangle(p, myRectangle);
                graphics.DrawArc(pen, rectangleForRectangleAngle, startAngle, 90.0f);
                //e.Graphics.DrawRectangle(p, rectangleForRectangleAngle);

                //e.Graphics.DrawRectangle(p, rectangleForAlfaAngle);
                graphics.DrawArc(pen, rectangleForAlfaAngle, startAlfaAngle, alfaAngle);

                //e.Graphics.DrawRectangle(p, rectangleForBetaAngle);
                graphics.DrawArc(pen, rectangleForBetaAngle, startBetaAngle, betaAngle);
                //thirdLine to implement //trigonometie?//
            }
        }

    }
}