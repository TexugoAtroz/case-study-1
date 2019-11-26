using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Logic.Model
{
    class Position
    {
        // properties
        // int32 ranges from -2.147.483.647 to 2.147.483.647
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        // constructor
        public Position(Int32 x = 0, Int32 y = 0)
        {
            X = x;
            Y = y;
        }
    }
}
