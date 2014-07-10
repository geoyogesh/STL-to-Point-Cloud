using System;
using System.Collections.Generic;
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
    /// Interaction logic for ListBased.xaml
    /// </summary>
    public partial class ListBased : Window
    {
        Queue_LinkedList<DataPoint> _matrix;
        int num_hits, num_misses = 0;


        private int mat_dim;

        public ListBased()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComputeMatrix(-10, 10, 0.1);
            ApplytheEquation();
        }


        private void ApplytheEquation()
        {
            num_hits = 0; num_misses = 0;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("output.txt", true))
            {
                var item=_matrix.MoveNext();
                while (item!= null)
                {
                    var x = item.X;
                    var y = item.Y;
                    var z = item.Z;
                    if ((x * x + y * y + z * z == 81))
                    {
                        item.Z = 1; //initialize with 0 every where
                        num_hits++;
                    }
                    else
                    {
                        num_misses--;
                    }
                    file.WriteLine(_matrix);
                    item = _matrix.MoveNext();
                }
            }
            MessageBox.Show(String.Format("number of hits {0}  number of miss {1} total {2}", num_hits.ToString(), num_misses.ToString(), mat_dim * mat_dim * mat_dim));
        }

        private void ComputeMatrix(double min, double max, double precision)
        {
                mat_dim = Convert.ToInt32((max - min) / precision + 1); //plus 1 is to include the first value
                _matrix = new Queue_LinkedList<DataPoint>();
                for (int i = 0; i < mat_dim; i++)
                {
                    for (int j = 0; j < mat_dim; j++)
                    {
                        for (int k = 0; k < mat_dim; k++)
                        {
                            _matrix.enqueue(new DataPoint()
                            {
                                X=(min + (i * precision)),
                                Y=(min + (j * precision)), 
                                Z=(min + (k * precision)), 
                                Value=0
                            });//initialize with 0 every where
                            
                        }
                    }
                }
            
        }
        private string getString(double[] input)
        {
            return input[0].ToString() + "," + input[1].ToString() + "," + input[2] + "," + input[3].ToString();
        }


        private string settrue(string input)
        {
            var split = input.Split(',');
            return String.Join(",", split[0], split[1], split[2], "1");
        }

        private double[] getval(string input)
        {
            var split = input.Split(',');
            return new double[] { double.Parse(split[0]), double.Parse(split[1]), double.Parse(split[2]), double.Parse(split[3]) };
        }


    }
}
