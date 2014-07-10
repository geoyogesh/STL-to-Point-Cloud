using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public class STL
    {
        public String Name { get; set; }

        public List<Triangle> Triangles { get; set; }
    }

    public class Triangle
    {
        public Domain XDomain
        {
            get
            {
                return new Domain(Math.Min(this.A.X, Math.Min(this.B.X, this.C.X)), Math.Max(this.A.X, Math.Max(this.B.X, this.C.X)));
            }
        }

        public Domain YDomain
        {
            get
            {
                return new Domain(Math.Min(this.A.Y, Math.Min(this.B.Y, this.C.Y)), Math.Max(this.A.Y, Math.Max(this.B.Y, this.C.Y)));
            }
        }

        public Domain ZDomain
        {
            get
            {
                return new Domain(Math.Min(this.A.Z, Math.Min(this.B.Z, this.C.Z)), Math.Max(this.A.Z, Math.Max(this.B.Z, this.C.Z)));
            }
        }

        public Point3D A { get; set; }
        public Point3D B { get; set; }
        public Point3D C { get; set; }
        public Double D_Cons { get; set; }
        public Vector3D UnitNormalVector { get; set; }
    }

    public class Point3D
    {
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Z { get; set; }
    }


    public class Vector3D
    {
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Z { get; set; }
    }
    public class Domain
    {
        public Domain() { }

        public Domain(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        public Double Min { get; set; }
        public Double Max { get; set; }
    }

}
