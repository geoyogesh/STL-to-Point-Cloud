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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Plotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ArrayBased : Window
    {
        string[,,] _matrix;
        int num_hits, num_misses = 0;
        public ArrayBased()
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
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < _matrix.GetLength(2); k++)
                    {
                        var s=getval(_matrix[i, j, k]);
                        var x=s[0];
                        var y=s[1];
                        var z=s[2];
                        if ((x * x + y * y + z * z == 81))
                        {
                            _matrix[i, j, k] = settrue(_matrix[i, j, k]); //initialize with 0 every where
                            num_hits++;
                        }
                        else
                        {
                            num_misses--;
                        }
                    }
                }
            }
            MessageBox.Show(String.Format("number of hits {0}  number of miss {1} total {2}", num_hits.ToString(), num_misses.ToString(), _matrix.GetLength(0) * _matrix.GetLength(1) * _matrix.GetLength(2)));
        }

        private void ComputeMatrix(double min, double max, double precision)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("output.txt", true))
            {
                int mat_dim = Convert.ToInt32((max - min) / precision + 1); //plus 1 is to include the first value
                _matrix = new string[mat_dim, mat_dim, mat_dim];
                for (int i = 0; i < _matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < _matrix.GetLength(1); j++)
                    {
                        for (int k = 0; k < _matrix.GetLength(2); k++)
                        {
                            _matrix[i, j, k] = getString(new double[] { (min + (i * precision)), (min + (j * precision)), (min + (k * precision)), 0 }); //initialize with 0 every where
                            file.WriteLine(_matrix[i, j, k]);
                        }
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
            return String.Join(",",split[0], split[1], split[2],"1");
        }

        private double[] getval(string input)
        {
            var split=input.Split(',');
            return new double[] { double.Parse(split[0]), double.Parse(split[1]), double.Parse(split[2]), double.Parse(split[3]) };
        }



    }
}
