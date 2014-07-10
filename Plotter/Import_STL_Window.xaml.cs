using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Plotter
{
    /// <summary>
    /// Interaction logic for Import_STL_Window.xaml
    /// </summary>
    public partial class Import_STL_Window : Window
    {
        public Import_STL_Window()
        {
            InitializeComponent();
        }


        private STL stl = null;
        private string output_file = @"C:\Users\Yogesh\Desktop\pointcloud_web_viewer\data\sphere_0.1.txt";
        private string inputsdl = @"C:\Users\Yogesh\Desktop\Sphere.stl";
        private double precision = 0.1;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var sr = new StreamReader(inputsdl))
            {
                stl = new STL();
                String line;
                Triangle triangle = null;
                Queue<Point3D> vertexlist = null;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("solid"))
                    {
                        stl = new STL
                        {
                            Name = line.Split(' ')[1]
                        };
                        stl.Triangles = new List<Triangle>();
                    }
                    else if (line.StartsWith("facet"))
                    {
                        var spstr = line.Split(' ');
                        triangle = new Triangle();
                        triangle.UnitNormalVector = new Vector3D()
                        {
                            X = Double.Parse(spstr[2]),
                            Y = Double.Parse(spstr[3]),
                            Z = Double.Parse(spstr[4])
                        };
                    }
                    else if (line.StartsWith("outer loop"))
                    {
                        vertexlist = new Queue<Point3D>(3);
                    }
                    else if (line.StartsWith("vertex"))
                    {
                        var spstr = line.Split(' ');
                        var point = new Point3D()
                        {
                            X = Double.Parse(spstr[1]),
                            Y = Double.Parse(spstr[2]),
                            Z = Double.Parse(spstr[3])
                        };
                        vertexlist.Enqueue(point);
                    }
                    else if (line.StartsWith("endloop"))
                    {
                        triangle.A = vertexlist.Dequeue();
                        triangle.B = vertexlist.Dequeue();
                        triangle.C = vertexlist.Dequeue();
                        if (vertexlist.Count != 0)
                        {
                            throw new Exception("More number of points in a triangle");
                        }
                        vertexlist = null;
                    }
                    else if (line.StartsWith("endfacet"))
                    {
                        stl.Triangles.Add(triangle);
                        triangle = null;
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (File.Exists(output_file))
            {
                File.Delete(output_file);
            }
            int tri_num = 0;
            for (tri_num = 0; tri_num < stl.Triangles.Count; tri_num++)
            {
                ExecuteForTriangle(tri_num);
            }
            MessageBox.Show("Completed");

        }

        private void ExecuteForTriangle(int tri_num)
        {
            Compute_D(stl.Triangles[tri_num]);

            //hitpoints

            var hitpoints = new Queue<HitPoint>();

            var t1 = stl.Triangles[tri_num];
            var z_min = t1.ZDomain.Min;
            var z_max = t1.ZDomain.Max;
            for (var i = t1.XDomain.Min; i <= t1.XDomain.Max; i += precision)
            {
                for (var j = t1.YDomain.Min; j <= t1.YDomain.Max; j += precision)
                {
                    var z = calculate_z(i, j, t1);
                    //Debug.WriteLine(z.ToString());
                    if ((z >= z_min) && (z <= z_max))
                    {
                        hitpoints.Enqueue(new HitPoint(i, j, z, (ulong)tri_num));
                        //33554432
                        if (hitpoints.Count > 10000000)
                        {
                            clean_data(hitpoints);
                        }
                    }
                }
            }

            if (hitpoints.Count > 0) clean_data(hitpoints);
        }

        private double calculate_z(double x, double y, Triangle t1)
        {
            return ((-1 * t1.D_Cons) + (-1 * t1.UnitNormalVector.X * x) + (-1 * t1.UnitNormalVector.Y * y)) / t1.UnitNormalVector.Z; ;
        }


        private void Compute_D(Triangle triangle)
        {
            //a*x+b*y+c*z+d = 0
            triangle.D_Cons = (-1 * triangle.UnitNormalVector.X * triangle.A.X) + (-1 * triangle.UnitNormalVector.Y * triangle.A.Y) + (-1 * triangle.UnitNormalVector.Z * triangle.A.Z);
        }

        private void clean_data(Queue<HitPoint> points)
        {
            using (StreamWriter w = File.AppendText(output_file))
            {
                while (points.Count != 0)
                {
                    w.Write(points.Dequeue().ToString());
                }
                // Update the underlying file.
                w.Flush();
                // Close the writer and underlying file.
                w.Close();
            }
        }
    }
}

