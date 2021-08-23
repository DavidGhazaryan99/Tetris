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
    class FallingBlocksStick : IElement
    {
        public List<Border> elements;

        public FallingBlocksStick()
        {
            elements = new List<Border>();
            for (int i = 0; i < 4; i++)
            {
                elements.Add(new Border());
                elements[i].Background = Brushes.Blue;
                elements[i].BorderThickness = new Thickness(1, 1, 1, 1);
                Grid.SetRow(elements[i], i);
                Grid.SetColumn(elements[i], 4);
            }
        }

        public List<Border> GetElements()
        {
            return elements;
        }

        public void StickRotate(Border[] elements)
        {
            int row = Grid.GetRow(elements[1]);
            int col = Grid.GetColumn(elements[1]);
            for (int i = 0; i < elements.Length; i++)
            {
                if (i == 0)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col - 1);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col + 1);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], row);
                    Grid.SetColumn(elements[i], col + 2);
                }
            }

        }
        public void StickRotate2(Border[] elements)
        {
            int row = Grid.GetRow(elements[1]);
            int col = Grid.GetColumn(elements[1]);
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
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 2)
                {
                    Grid.SetRow(elements[i], row + 1);
                    Grid.SetColumn(elements[i], col);
                }
                if (i == 3)
                {
                    Grid.SetRow(elements[i], row + 2);
                    Grid.SetColumn(elements[i], col);
                }
            }
        }
    }
}
