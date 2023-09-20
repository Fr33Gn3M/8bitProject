﻿using FC.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Database.Model
{
    public class OrderInfo
    {
        public string Field { get; set; }

        public SqlOrderBy OrderType { get; set; }
    }
}
