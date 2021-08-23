using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris
{
    class FallingBlocksGamma : IElement
    {

        public List<Border> elements;
        public FallingBlocksGamma()
        {
            elements = new List<Border>();
            for (int i = 0; i < 4; i++)
            {
                elements.Add( new Border());
                elements[i].Background = Brushes.DarkBlue;
                elements[i].BorderThickness = new Thickness(1, 1, 1, 1);
                if (i == 0)
                {
                    Grid.SetRow(elements[i], 0);
                    Grid.SetColumn(elements[i], 4);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], 1);
                    Grid.SetColumn(elements[i], 4);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], 2);
                    Grid.SetColumn(elements[i], 4);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], 2);
                    Grid.SetColumn(elements[i], 3);
                }
            }
        }
        public void GammaRotate(Border[] elements)
        {
            int row = Grid.GetRow(elements[2]);
            int col = Grid.GetColumn(elements[2]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row - 1);
                    Grid.SetColumn(elements[i], col - 1);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col - 1);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col + 1);
                }
            }

        }
        public void GammaRotate2(Border[] elements)
        {
            int row = Grid.GetRow(elements[2]);
            int col = Grid.GetColumn(elements[2]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row + 2);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], row + 1);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col + 1);
                }
            }
        }
        public void GammaRotate3(Border[] elements)
        {
            int row = Grid.GetRow(elements[2]);
            int col = Grid.GetColumn(elements[2]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row + 1);
                    Grid.SetColumn(elements[i], col + 1);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col + 1);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col - 1);
                }
            }
        }
        public void GammaRotate4(Border[] elements)
        {
            int row = Grid.GetRow(elements[2]);
            int col = Grid.GetColumn(elements[2]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row - 2);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], row - 1);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col - 1);
                }
            }
        }

        public List<Border> GetElements()
        {
            return elements;
        }
    }
}
