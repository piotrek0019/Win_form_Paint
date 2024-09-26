﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPaint
{
    internal class RectangleShape : ShapeBase
    {

        /// <summary>
        /// Rectanlge shape to be drawn
        /// </summary>
        private Rectangle rectangle;

        /// <summary>
        /// Calculates all, necessary for the shape values
        /// </summary>ry>
        public override void CalculateForShape()
        {
            rectangle = new Rectangle(MinX, MinY, AbsWidth, AbsHeight);
        }

        /// <summary>
        /// Draw rectangle based on calculated values
        /// </summary>
        /// <param name="graphics"></param>
        public override void DrawShape(Graphics graphics)
        {
            graphics.DrawRectangle(Pen, rectangle);
        }
    }
}
