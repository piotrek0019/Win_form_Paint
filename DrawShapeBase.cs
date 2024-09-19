using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPaint
{
    /// <summary>
    /// Base class for shapes to draw
    /// </summary>
    public  abstract class DrawShapeBase
    {
        /// <summary>
        /// Cordinates........!!!!!!!!!!!!!
        /// </summary>
        protected int minX, minY, maxX, maxY;

        /// <summary>
        /// Dimensions
        /// </summary>
        protected int absWidth, absHeight, nonAbsWidth, nonAbsHeight;

        /// <summary>
        /// Point where the shape is started being drawn
        /// </summary>
        protected Point startPoint;

        /// <summary>
        /// Point where the shape is ended being drawn
        /// </summary>
        protected Point endPoint;

        //TODO PA that could be in parent/base class for drawing all shapes!
        public void SetStartEndPoints(Point startPoint, Point endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }

        //TODO PA that could be in parent/base class for drawing all shapes!
        public void CalculateCoordinates()
        {

            minX = Math.Min(startPoint.X, endPoint.X);
            minY = Math.Min(startPoint.Y, endPoint.Y);
            maxX = Math.Max(startPoint.X, endPoint.X);
            maxY = Math.Max(startPoint.Y, endPoint.Y);
            absWidth = Math.Abs(endPoint.X - startPoint.X);
            absHeight = Math.Abs(endPoint.Y - startPoint.Y);

            nonAbsWidth = endPoint.X - startPoint.X;
            nonAbsHeight = endPoint.Y - startPoint.Y;
        }
    }
}
