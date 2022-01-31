using System;
using System.Linq;

namespace HT6
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value.Skip(70).ToArray(); }
        } 
    }
}
