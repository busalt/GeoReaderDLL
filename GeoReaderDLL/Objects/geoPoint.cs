using System;
using System.Collections.Generic;
using System.Text;

namespace GeoReaderDLL
{
  
   public class geoPoint
   {
       private int _No;
       public int No { get => _No; }

       private double _X;
       public double X { get => _X; }

       private double _Y;
       public double Y { get => _Y; }

       private int _Color;
       public int Color { get => _Color; }


       public geoPoint()
       {

       }

       public geoPoint(int no, double x, double y, int color)
       {
           _No = no;
           _X = x;
           _Y = y;
           _Color = color;

       }
   }
  
}
