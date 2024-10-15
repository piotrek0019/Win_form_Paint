using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPaint
{
    internal class EllipseShape : RectangleShape
    {
        /// <summary>
        /// Draw shape based on calculated values
        /// </summary>
        /// <param name="graphics">Graphics used to paint</param>
        public override void DrawShape(Graphics graphics)
        {
            graphics.DrawEllipse(Pen, Rectangle);
        }
    }
}
