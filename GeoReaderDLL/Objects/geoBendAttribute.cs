using System;
using System.Collections.Generic;
using System.Text;

namespace GeoReaderDLL
{
    public class geoBendAttribute
    {
        private int _Id;
        public int Id { get => _Id; }

        private string _Type;
        public string Type { get => _Type; }

        private string _Val;
        public string Val { get => _Val; }




        public geoBendAttribute()
        {

        }

        public geoBendAttribute(int id, string type, string val)
        {
            _Id = id;
            _Type = type;
            _Val = val;

        }
    }
}
