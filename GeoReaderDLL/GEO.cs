using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace GeoReaderDLL
{
    public class GEO
    {
        private string _FileName;
        public string FileName { get; }

        private ObservableCollection<geoPoint> _Points;
        public ObservableCollection<geoPoint> Points { get => _Points; }

        private ObservableCollection<geoBendLine> _BendLines;
        public ObservableCollection<geoBendLine> BendLines { get => _BendLines; }

        private ObservableCollection<geoBendAttribute> _Batt;
        public ObservableCollection<geoBendAttribute> Batt { get => _Batt; }

        private ObservableCollection<geoCircle> _Circles;
        public ObservableCollection<geoCircle> Circles { get => _Circles; }

        private bool _IsCreatedByBoost;
        public bool IsCreatedByBoost { get => _IsCreatedByBoost; }

        private string _Werkstoff;
        public string Werkstoff { get => _Werkstoff; }

        private string _RawMaterial;
        public string RawMaterial { get => _RawMaterial; }

        private int _BendCount;
        public int BendCount { get => _BendCount; }

        private double _Length;
        public double Length { get => _Length; }

        private double _Width;
        public double Width { get => _Width; }

        private double _Area;
        public double Area { get => _Area; }

        private double _Thickness;
        public double Thickness { get => _Thickness; }

        public GEO()
        {

        }

        public GEO(string filename)
        {
            _FileName = filename;
            _Points = new ObservableCollection<geoPoint>();
            _BendLines = new ObservableCollection<geoBendLine>();
            _Batt = new ObservableCollection<geoBendAttribute>();
            _Circles = new ObservableCollection<geoCircle>();

            int counter = 0;



            //Parameter für Geo Typ => TopsClassic oder Boost
            bool readGeneralInfo = false;


            //Parameter für Punkte
            bool countPoint = false;
            int counterPoint = 0;
            string line;
            int PointNo = 0;
            double x = 0;
            double y = 0;

            //Parameter für Biegelinie
            bool countBendline = false;
            int counterBendline = 0;

            bool countBendline2 = false;
            int counterBendline2 = 0;

            string OW = "";
            string UW = "";
            string OwGroup = "";
            string UwGroup = "";
            double Winkel = 0;
            double vkWert = 0;
            int PointFrom = 0;
            int PointTo = 0;
            int BattIdOwGroup = 0;
            int BattIdUwGroup = 0;
            int BattIdVkWert = 0;

            //Parameter für BendAttribute
            bool countBatt = false;
            int counterBatt = 0;
            int BattId = 0;
            string BattVal = "";
            string BattType = "";

            //Parameter für Kreise
            bool countCir = false;
            int counterCir = 0;
            int CirPointID = 0;
            double CirDiameter = 0;
            int CirLineType = 0;
            int CirLineColor = 0;


            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(_FileName);

            while ((line = file.ReadLine()) != null)
            {

                //Parameter für Länge und Breite
                if (counter == 5)
                {
                    _Length = Convert.ToDouble(line.Split(' ')[0], new CultureInfo("en-US"));
                    _Width = Convert.ToDouble(line.Split(' ')[1], new CultureInfo("en-US"));
                }

                if (counter == 6)
                { _Area = Convert.ToDouble(line, new CultureInfo("en-US")); }


                //S - Allgemeine Informationen  
                if (line == "#~30")
                {
                    readGeneralInfo = true;
                }

                if (readGeneralInfo)
                {
                    if (line.Split('@')[0] == "MAT")
                    { _RawMaterial = line.Split('@')[1]; }

                    if (line.Split('@')[0] == "WERKSTOFF")
                    { _Werkstoff = line.Split('@')[1]; }

                    if (line.Split('@')[0] == "BEND_COUNT")
                    { _BendCount = Convert.ToInt32(line.Split('@')[1]); }

                    if (line.Split('@')[0] == "BOOST_VERSION")
                    { _IsCreatedByBoost = true; }

                    if (line == "#~TTINFO_END")
                    {
                        readGeneralInfo = false;
                    }

                }
                //E - Allgemeine Informationen


                //S - Ponit Information
                if (line == "P")
                {
                    countPoint = true;
                }

                if (countPoint)
                {
                    if (counterPoint == 1)
                    { PointNo = Convert.ToInt32(line); }

                    if (counterPoint == 2)
                    {
                        x = Convert.ToDouble(line.Split(' ')[0], new CultureInfo("en-US"));
                        y = Convert.ToDouble(line.Split(' ')[1], new CultureInfo("en-US"));
                    }

                    counterPoint++;

                    if (countPoint && line == "|~")
                    {
                        geoPoint p = new geoPoint(PointNo, x, y, 1);
                        _Points.Add(p);
                        counterPoint = 0;
                        countPoint = false;
                        PointNo = 0;
                        x = 0;
                        y = 0;
                    }
                }
                //E - Ponit Information

                //S - BendLineInfomation
                if (line == "#~371")
                {
                    countBendline2 = true;
                }

                if (countBendline2)
                {
                    if (counterBendline2 == 3)
                    {
                        PointFrom = Convert.ToInt32(line.Split(' ')[0], new CultureInfo("en-US"));
                        PointTo = Convert.ToInt32(line.Split(' ')[1], new CultureInfo("en-US"));
                    }
                    counterBendline2++;
                }


                if (line == "#~37")
                {
                    countBendline = true;
                }

                if (countBendline)
                {

                    //Wenn Counter =2 => Winkel
                    if (counterBendline == 2)
                    { Winkel = Convert.ToDouble(line.Split(' ')[0], new CultureInfo("en-US")); }

                    //Counter = 4 => Verkürzungswert
                    if (counterBendline == 4)
                    { vkWert = Convert.ToDouble(line, new CultureInfo("en-US")); }

                    //Counter = 5 => Oberwerkzeug
                    if (counterBendline == 5)
                    { OW = line; }

                    //Counter = 6 => Unterwerkzeug
                    if (counterBendline == 6)
                    { UW = line; }

                    //Counter = 10 => Bei Boost geo = Verkürzungswert
                    if ((_IsCreatedByBoost) && (counterBendline == 10))
                    {
                        try
                        { BattIdVkWert = Convert.ToInt32(line); }
                        catch
                        { }
                    }

                    //Counter = 11 => Bei Boost geo = OW-Gruppe
                    if ((_IsCreatedByBoost) && (counterBendline == 11))
                    {
                        try
                        { BattIdOwGroup = Convert.ToInt32(line); }
                        catch
                        { }
                    }


                    //Counter = 12 => Bei Boost geo = UW-Gruppe
                    if ((_IsCreatedByBoost) && (counterBendline == 12))
                    {
                        try
                        { BattIdUwGroup = Convert.ToInt32(line); }
                        catch
                        { }
                    }


                    counterBendline++;

                    if ((countBendline) && (line == "#~BIEG_END"))
                    {
                        geoPoint pf = Points.Single(lin => lin.No == PointFrom) as geoPoint;
                        geoPoint pt = Points.Single(lin => lin.No == PointTo) as geoPoint;

                        if (BattIdOwGroup != 0)
                        {
                            geoBendAttribute battOW = _Batt.Single(xa => xa.Id == BattIdOwGroup) as geoBendAttribute;
                            OwGroup = battOW.Val;
                        }

                        if (BattIdUwGroup != 0)
                        {
                            geoBendAttribute battUW = _Batt.Single(xa => xa.Id == BattIdUwGroup) as geoBendAttribute;
                            UwGroup = battUW.Val;
                        }

                        if (BattIdVkWert != 0)
                        {
                            geoBendAttribute battVK = _Batt.Single(xa => xa.Id == BattIdVkWert) as geoBendAttribute;

                            vkWert = Convert.ToDouble(battVK.Val, new CultureInfo("en-US"));
                        }

                        BendLines.Add(new geoBendLine(pf, pt, Winkel, OwGroup, OW, UwGroup, UW, vkWert));

                        countBendline = false;
                        counterBendline = 0;

                        countBendline2 = false;
                        counterBendline2 = 0;



                        OW = "";
                        UW = "";
                        OwGroup = "";
                        UwGroup = "";
                        Winkel = 0;
                        vkWert = 0;
                        PointFrom = 0;
                        PointTo = 0;
                        BattIdOwGroup = 0;
                        BattIdUwGroup = 0;
                    }
                }
                //E - BendLineInfomation


                //S - BendAttributeInfomation
                if (line == "BATT")
                { countBatt = true; }

                if (countBatt)
                {
                    if (counterBatt == 1)
                    { BattId = Convert.ToInt32(line); }

                    if (counterBatt == 3)
                    { BattVal = line; }

                    if (counterBatt == 4)
                    { BattType = line; }

                    counterBatt++;

                    if ((countBatt) && (line == "|~"))
                    {
                        Batt.Add(new geoBendAttribute(BattId, BattType, BattVal));
                        BattId = 0;
                        BattType = "";
                        BattVal = "";
                        countBatt = false;
                        counterBatt = 0;
                    }
                }
                //E - BendAttributeInfomation


                //S - CircleInfomation
                if (line == "CIR")
                { countCir = true; }

                if (countCir)
                {
                    //Linieninformationen zu Kreis
                    if (counterCir == 1)
                    {
                        CirLineColor = Convert.ToInt32(line.Split(' ')[0]);
                        CirLineType = Convert.ToInt32(line.Split(' ')[1]);
                    }

                    //Punktnummer zu Kreis
                    if (counterCir == 2)
                    { CirPointID = Convert.ToInt32(line); }

                    //Durchmesser zu Kreis
                    if (counterCir == 3)
                    { CirDiameter = Convert.ToDouble(line, new CultureInfo("en-US")); }


                    counterCir++;

                    if ((countCir) && (line == "|~"))
                    {
                        geoPoint p = _Points.Single(xa => xa.No == CirPointID) as geoPoint;
                        Circles.Add(new geoCircle(CirDiameter, p, CirLineColor, CirLineType));


                        countCir = false;
                        counterCir = 0;
                        CirPointID = 0;
                        CirDiameter = 0;
                        CirLineType = 0;
                        CirLineColor = 0;
                    }
                }
                //E - CircleInfomation

                counter++;
            }

            file.Close();
        }
    }
}
