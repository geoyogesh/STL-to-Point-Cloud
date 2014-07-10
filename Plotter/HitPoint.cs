using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public class HitPoint
    {
        public HitPoint() { }

        public HitPoint(Double x, Double y, Double z, UInt64 trinum)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.TriNum = trinum;
        }
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Z { get; set; }
        public UInt64 TriNum { get; set; }

        public override string ToString()
        {
            return String.Join(",", this.X.ToString(), this.Y.ToString(), this.Z.ToString(), "255", "255", "255\n");
        }
    }
}
