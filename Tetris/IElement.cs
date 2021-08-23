using System.Collections.Generic;
using System.Windows.Controls;

namespace Tetris
{
    internal interface IElement
    {
        List<Border> GetElements();
    }
}
