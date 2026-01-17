using System;

namespace Sys.DataBase
{
    public class ModelAttrubuteIsParted : Attribute
    {
        public bool IsParted { get; set; }
        public ModelAttrubuteIsParted()
        {
            IsParted = true;
        }
        public ModelAttrubuteIsParted(bool ischanged)
        {
            IsParted = ischanged;
        }
    }
}
