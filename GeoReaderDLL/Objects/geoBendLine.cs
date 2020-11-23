using System;
using System.Collections.Generic;
using System.Text;

namespace GeoReaderDLL
{
    public class geoBendLine
    {
        private geoPoint _Point1;
        public geoPoint Point1 { get => _Point1; }

        private geoPoint _Point2;
        public geoPoint Point2 { get => _Point2; }

        private double _Angle;
        public double Angle { get => _Angle; }

        private double _vkWert;
        public double vkWert { get => _vkWert; }

        private string _OwGroup;
        public string OwGroup { get => _OwGroup; }

        private string _OW;
        public string OW { get => _OW; }

        private string _UwGroup;
        public string UwGroup { get => _UwGroup; }

        private string _OWDisplayTxt;
        public string OWDisplayTxt { get => _OWDisplayTxt; }

        private string _UW;
        public string UW { get => _UW; }

        private string _UWDisplayTxt;
        public string UWDisplayTxt { get => _UWDisplayTxt; }

        private double _Length;
        public double Length { get => _Length; }


        public geoBendLine()
        { }

        public geoBendLine(geoPoint P1, geoPoint P2, double angle, string owgroup, string ow, string uwgroup, string uw, double vkwert)
        {
            _Point1 = P1;
            _Point2 = P2;
            _Angle = angle;
            _OwGroup = owgroup;
            _OW = ow;
            _UwGroup = uwgroup;
            _UW = uw;
            _vkWert = vkwert;

            double y = Math.Pow((P2.Y - P1.Y), 2);
            double x = Math.Pow((P2.X - P1.X), 2);
            _Length = Math.Sqrt((y + x));


            if((_OW == "") && (_OwGroup != ""))
            { _OWDisplayTxt = _OwGroup;  }

            if ((_OW != "") && (_OwGroup == ""))
            { _OWDisplayTxt = _OW; }

            if ((_OW != "") && (_OwGroup != ""))
            { _OWDisplayTxt = _OW; }


            if ((_UW == "") && (_UwGroup != ""))
            { _UWDisplayTxt = _UwGroup; }

            if ((_UW != "") && (_UwGroup == ""))
            { _UWDisplayTxt = _UW; }

            if ((_UW != "") && (_UwGroup != ""))
            { _UWDisplayTxt = _UW; }
        }
    }
}
