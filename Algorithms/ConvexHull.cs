using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class ConvexHull
    {
        // find the convex hull
        /* IConvexHullAlgorithm hullFinder = new GrahamConvexHull();
         List<AForge.IntPoint> hull = hullFinder.FindHull(lstContourPoints);

         BitmapData bmpData = curvesDetected.LockBits(new Rectangle(0, 0, curvesDetected.Width, curvesDetected.Height), ImageLockMode.ReadWrite, curvesDetected.PixelFormat);
         AForge.Imaging.Drawing.Polygon(bmpData, hull, Color.Red);
         curvesDetected.UnlockBits(bmpData);
           
         int iRedPixel = 0;*/

        /* for (int x = 0; x < this.imgHandContour.Width; x++)
         {
             for (int y = 0; y < this.imgHandContour.Height; y++)
             {
                 //the de red ones (and its different shades)
                 if (this.curvesDetected.GetPixel(x, y).Name != "ffffffff" && this.curvesDetected.GetPixel(x, y).R == 255)
                 {
                     iRedPixel++;
                     lstRedPixels.Add(new AForge.IntPoint(x, y));
                 }
             }
         }*/
    }
}
