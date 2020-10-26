using System;
using System.Collections.Generic;
using System.Text;

namespace GeoReaderDLL
{
    public class geoCircle
    {

        private int _Color;
        public int Color { get => _Color; }

        private int _LineType;
        public int LineType { get => _LineType; }

        private double _Diameter;
        public double Diameter { get => _Diameter; }

        private geoPoint _Point;
        public geoPoint Point { get => _Point; }


        public geoCircle()
        {

        }

        public geoCircle(double diameter, geoPoint point, int color, int linetype)
        {

            _LineType = linetype;
            _Color = color;
            _Diameter = diameter;
            _Point = point;
        }
    }
}
