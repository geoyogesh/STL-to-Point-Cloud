using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public class DataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double Z { get; set; }

        public double Value { get; set; }

        public override string ToString()
        {
           
            return string.Join(",",this.X,this.Y,this.Z,this.Value);
        }
    }
}
