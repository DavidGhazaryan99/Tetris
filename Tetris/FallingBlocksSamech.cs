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
    class FallingBlocksSamech : IElement
    {
        public List<Border> elements;

        public FallingBlocksSamech()
        {
            elements = new List<Border>();
            for (int i = 0; i < 4; i++)
            {
                elements.Add(new Border());
                elements[i].Background = Brushes.Yellow;
                elements[i].BorderThickness = new Thickness(1, 1, 1, 1);
                if (i == 0)
                {
                    Grid.SetRow(elements[i], 0);
                    Grid.SetColumn(elements[i], 4);
                }
                if (i == 1)
                {
                    Grid.SetRow(elements[i], 0);
                    Grid.SetColumn(elements[i], 5);
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
    }
}
