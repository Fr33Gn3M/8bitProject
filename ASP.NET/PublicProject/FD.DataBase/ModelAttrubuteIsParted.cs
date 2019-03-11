using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FD.DataBase
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
