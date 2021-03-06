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
    class FallingBlocksWASD : IElement
    {
        public List<Border> elements;

        public FallingBlocksWASD()
        {
            elements = new List<Border>();
            for (int i = 0; i < 4; i++)
            {
                elements.Add(new Border());
                elements[i].Background = Brushes.Purple;
                elements[i].BorderThickness = new Thickness(1, 1, 1, 1);
                if (i == 0)
                {
                    Grid.SetRow(elements[i], 0);
                    Grid.SetColumn(elements[i], 4);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], 1);
                    Grid.SetColumn(elements[i], 3);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], 1);
                    Grid.SetColumn(elements[i], 4);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], 1);
                    Grid.SetColumn(elements[i], 5);
                }
            }
        }

        public List<Border> GetElements()
        {
            return elements;
        }

        public void WASDRotate(Border[] elements)
        {
            int row = Grid.GetRow(elements[1]);
            int col = Grid.GetColumn(elements[1]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col + 2);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], row - 1);
                    Grid.SetColumn(elements[i], col + 1);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col + 1);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], row + 1);
                    Grid.SetColumn(elements[i], col + 1);
                }
            }

        }
        public void WASDRotate2(Border[] elements)
        {
            int row = Grid.GetRow(elements[2]);
            int col = Grid.GetColumn(elements[2]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row + 1);
                    Grid.SetColumn(elements[i], col);
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
        public void WASDRotate3(Border[] elements)
        {
            int row = Grid.GetRow(elements[2]);
            int col = Grid.GetColumn(elements[2]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col - 1);
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
                    Grid.SetRow(elements[i], row - 1);
                    Grid.SetColumn(elements[i], col);
                }
            }
        }
        public void WASDRotate4(Border[] elements)
        {
            int row = Grid.GetRow(elements[2]);
            int col = Grid.GetColumn(elements[2]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row - 1);
                    Grid.SetColumn(elements[i], col);
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
    }
}
