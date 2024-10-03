using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPaint
{
    /// <summary>
    /// Provides method helpers for shape classes
    /// </summary>
    internal static class ShapeHelpers
    {
        /// <summary>
        /// Get basic pen
        /// </summary>
        /// <returns>Basic pen</returns>
        public static Pen GetPen()
        {
            return new Pen(Color.Black, 1);
        }

        /// <summary>
        /// Get customised pen
        /// </summary>
        /// <param name="colour">The colour</param>
        /// <param name="width"></param>
        /// <returns>Customised pen</returns>
        public static Pen GetPen(Color colour, float width) 
        {
            return new Pen(colour, width);
        }

        /// <summary>
        /// Get basic eraser
        /// </summary>
        /// <returns>Basic eraser</returns>
        public static Pen GetEraser()
        {
            return GetPen(Color.White, 10);
        }
    }
}
