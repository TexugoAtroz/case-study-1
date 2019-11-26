using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Logic.Model
{
    class Drone
    {
        // properties
        public Position CurrentPosition { get; set; }

        // constructor
        public Drone(Int32 x = 0, Int32 y = 0)
        {
            CurrentPosition = new Position(x, y);
        }
    }
}
