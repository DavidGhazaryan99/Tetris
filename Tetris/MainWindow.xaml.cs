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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace Tetris
{
    public partial class MainWindow : Window
    {
        private static Random random;
        private static object syncObj = new object();
        DispatcherTimer Timer;

        int rotateCount = 0;
        public int[][] takingASeat;
        public bool firstBoard = false;
        Border[] block = new Border[4];
        Border[][] allBorder;
        int type = 0;
        public MainWindow()
        {
            InitializeComponent();
            InitRandomNumber(40);
            Timer = new DispatcherTimer();
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            Timer.Tick += GameTick;
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            Timer.Start();
            int Cols = MainGrid.ColumnDefinitions.Count;
            int Rows = MainGrid.RowDefinitions.Count;
            takingASeat = new int[Rows][];
            allBorder = new Border[Rows][];
            for (int i = 0; i < takingASeat.Length; i++)
            {
                allBorder[i] = new Border[Cols];
                takingASeat[i] = new int[Cols];
            }
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (firstBoard == false)
            {
                ThemeGame();
                CreatBoard(GenerateRandomNumber(0, 5));
                firstBoard = true;
            }
            Gravity();
            RowFullCheked();
            GameOverCheked();
        }

        private void GameOverCheked()
        {
            for (int i = 0; i < takingASeat[0].Length; i++)
            {
                if (takingASeat[0][i] == 1)
                {
                    Timer.Stop();
                    TextBlock textBlock1 = new TextBlock();
                    textBlock1.Height = 181;
                    textBlock1.Width = 245;
                    textBlock1.Margin = new Thickness(24.2, 0.333, 0, 0);
                    textBlock1.Foreground = Brushes.Red;
                    textBlock1.FontSize = 48;
                    MainGrid.Children.Add(textBlock1);
                    textBlock.Text = "Game Over";
                }
            }
        }

        private void ThemeGame()
        {
            for (int i = 0; i < allBorder.Length; i++)
            {
                for (int j = 0; j < allBorder[i].Length; j++)
                {
                    allBorder[i][j] = new Border();
                    allBorder[i][j].Background = Brushes.Black;
                    allBorder[i][j].BorderThickness = new Thickness(1, 1, 1, 1);
                    MainGrid.Children.Add(allBorder[i][j]);
                    Grid.SetColumn(allBorder[i][j], j);
                    Grid.SetRow(allBorder[i][j], i);
                }
            }
        }

        private void RowFullCheked()
        {
            for (int i = 0; i < takingASeat.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < takingASeat[i].Length; j++)
                {
                    if (takingASeat[i][j] == 1)
                        count++;
                }
                if (count == takingASeat[i].Length)
                    RowRemove(i);
            }
        }

        private void RowRemove(int index)
        {
            for (int j = 0; j < takingASeat[index].Length; j++)
            {
                takingASeat[index][j] = 0;
                ReplaceByDefaultElement(index, j);
            }
            for (int i = index; i >= 0; i--)
            {
                for (int j = 0; j < takingASeat[i].Length; j++)
                {
                    if (takingASeat[i][j] == 1)
                    {
                        takingASeat[i][j] = 0;
                        int nextRow = i + 1;
                        takingASeat[nextRow][j] = 1;
                        allBorder[nextRow][j].Background = allBorder[i][j].Background;
                        Grid.SetRow(allBorder[nextRow][j], nextRow);
                        ReplaceByDefaultElement(i, j);
                    }
                }
            }
        }

        public Border CloneElement(Border orig)
        {
            if (orig == null)
                return (null);
            string s = XamlWriter.Save(orig);
            StringReader stringReader = new StringReader(s);
            XmlReader xmlReader = XmlTextReader.Create(stringReader, new XmlReaderSettings());
            return (Border)XamlReader.Load(xmlReader);
        }

        private void ReplaceByDefaultElement(int i, int j)
        {
            MainGrid.Children.Remove(allBorder[i][j]);
            allBorder[i][j] = new Border();
            allBorder[i][j].Background = Brushes.Black;
            allBorder[i][j].BorderThickness = new Thickness(1, 1, 1, 1);
            MainGrid.Children.Add(allBorder[i][j]);
            Grid.SetColumn(allBorder[i][j], j);
            Grid.SetRow(allBorder[i][j], i);
        }

        private void Gravity()
        {
            bool canMovedDown = false;
            int count = 0;
            for (int i = 0; i < block.Length; i++)
            {
                int lastRow = MainGrid.RowDefinitions.Count - 1;
                int borderRow = Grid.GetRow(block[i]);
                int borderCol = Grid.GetColumn(block[i]);
                if (borderRow < lastRow && takingASeat[borderRow + 1][borderCol] != 1)
                {
                    count++;
                }
            }
            if (count == 4)
            {
                canMovedDown = true;
            }
            if (canMovedDown == true)
            {
                for (int i = 0; i < block.Length; i++)
                {
                    int rowToBorder = Grid.GetRow(block[i]);
                    int column = Grid.GetColumn(block[i]);
                    rowToBorder++;
                    var elm = CloneElement(block[i]);
                    MainGrid.Children.Remove(block[i]);
                    block[i] = elm;
                    MainGrid.Children.Add(elm);
                    Grid.SetRow(elm, rowToBorder);
                    Grid.SetColumn(elm, column);
                }
            }
            if (canMovedDown == false)
            {
                for (int j = 0; j < block.Length; j++)
                {
                    int rowBorder = Grid.GetRow(block[j]);
                    int colBorder = Grid.GetColumn(block[j]);
                    takingASeat[rowBorder][colBorder] = 1;
                    allBorder[rowBorder][colBorder] = CloneElement(block[j]);
                    MainGrid.Children.Remove(block[j]);
                    MainGrid.Children.Add(allBorder[rowBorder][colBorder]);
                    Grid.SetColumn(allBorder[rowBorder][colBorder], colBorder);
                    Grid.SetRow(allBorder[rowBorder][colBorder], rowBorder);
                }
                CreatBoard(GenerateRandomNumber(1, 8));
            }
        }

        private void CreatBoard(int type)
        {
            this.type = type;
            rotateCount = 0;
            var blockByType = new Dictionary<TypeBlock, IElement>()
            {
                 {TypeBlock.Alpha, new FallingBlocksAlpha()},
                 {TypeBlock.Gamma, new FallingBlocksGamma()},
                 {TypeBlock.Samech, new FallingBlocksSamech()},
                 {TypeBlock.Saw, new FallingBlocksSaw()},
                 {TypeBlock.Stick, new FallingBlocksStick()},
                 {TypeBlock.WASD, new FallingBlocksWASD()},
                 {TypeBlock.Zaw, new FallingBlocksZaw()}
            };
            block = blockByType[(TypeBlock)type].GetElements().ToArray();
            foreach (var blockItem in block)
            {
                MainGrid.Children.Add(blockItem);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "Left":
                    keyLeft();
                    break;
                case "Right":
                    keyRight();
                    break;
                case "Down":
                    keyDown();
                    break;
                case "Up":
                    keyRotate();
                    break;
                case "F3":
                    keyPause();
                    break;
                case "F2":
                    keyStart();
                    break;
            }
        }

        private void keyStart()
        {
            Timer.Start();
        }

        private void keyPause()
        {
            Timer.Stop();
        }

        public void keyLeft()
        {
            bool leftCheked = false;
            for (int i = 0; i < block.Length; i++)
            {
                int colToBorder = Grid.GetColumn(block[i]);
                int rowToBorder = Grid.GetRow(block[i]);
                if (colToBorder == 0)
                    leftCheked = true;
                if (colToBorder - 1 > 0 && takingASeat[rowToBorder][colToBorder - 1] == 1)
                {
                    leftCheked = true;
                }
            }
            if (leftCheked == false)
            {
                for (int i = 0; i < block.Length; i++)
                {
                    int colToBorder = Grid.GetColumn(block[i]);
                    int rowToBorder = Grid.GetRow(block[i]);
                    if (takingASeat[rowToBorder][colToBorder] != 1)
                    {
                        colToBorder--;
                        Grid.SetColumn(block[i], colToBorder);
                    }
                }
            }
        }
        public void keyRight()
        {
            bool leftCheked = false;
            for (int i = 0; i < block.Length; i++)
            {
                int colToBorder = Grid.GetColumn(block[i]);
                int rowToBorder = Grid.GetRow(block[i]);
                if (colToBorder == MainGrid.ColumnDefinitions.Count - 1)
                    leftCheked = true;
                if (colToBorder < MainGrid.ColumnDefinitions.Count - 1 && takingASeat[rowToBorder][colToBorder + 1] == 1)
                {
                    leftCheked = true;
                }
            }
            if (leftCheked == false)
            {
                for (int i = 0; i < block.Length; i++)
                {
                    int colToBorder = Grid.GetColumn(block[i]);
                    int rowToBorder = Grid.GetRow(block[i]);
                    if (takingASeat[rowToBorder][colToBorder] != 1)
                    {
                        colToBorder++;
                        Grid.SetColumn(block[i], colToBorder);
                    }
                }
            }
        }
        public void keyDown()
        {
            Gravity();
        }
        public void keyRotate()
        {
            if (type == (int)TypeBlock.Alpha)
            {
                int row = Grid.GetRow(block[1]);
                int col = Grid.GetColumn(block[1]);
                FallingBlocksAlpha fallingBlocksAlpha = new FallingBlocksAlpha();
                if (rotateCount == 0 && row - 1 >= 0 && row + 1 <= takingASeat.Length - 1 && col - 1 >= 0)
                {
                    if (takingASeat[row - 1][col - 1] != 1 && takingASeat[row - 1][col] != 1 && takingASeat[row + 1][col] != 1)
                    {
                        fallingBlocksAlpha.AlphaRotate(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 1 && row + 2 <= takingASeat.Length - 1 && col + 1 <= takingASeat[0].Length - 1 && col - 1 >= 0)
                {
                    if (takingASeat[row + 2][col - 1] != 1 && takingASeat[row + 1][col - 1] != 1 && takingASeat[row + 1][col + 1] != 1 && takingASeat[row + 1][col] != 1)
                    {
                        fallingBlocksAlpha.AlphaRotate2(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 2 && row + 1 <= takingASeat.Length - 1 && col + 1 <= takingASeat[0].Length - 1 && row - 1 >= 0)
                {
                    if (takingASeat[row + 1][col + 1] != 1 && takingASeat[row + 1][col] != 1 && takingASeat[row - 1][col] != 1)
                    {
                        fallingBlocksAlpha.AlphaRotate3(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 3 && row - 1 >= 0 && col - 2 >= 0)
                {
                    if (takingASeat[row - 1][col] != 1 && takingASeat[row][col - 1] != 1 && takingASeat[row][col - 2] != 1)
                    {
                        fallingBlocksAlpha.AlphaRotate4(block);
                        rotateCount = 0;
                    }
                }
            }
            if (type == (int)TypeBlock.Stick)
            {
                int row = Grid.GetRow(block[1]);
                int col = Grid.GetColumn(block[1]);
                FallingBlocksStick fallingBlocksStick = new FallingBlocksStick();
                if (rotateCount == 0 && col - 1 >= 0 && col + 1 <= takingASeat[0].Length - 1 && col + 2 <= takingASeat[0].Length - 1)
                {
                    if (takingASeat[row][col - 1] != 1 && takingASeat[row][col + 1] != 1 && takingASeat[row][col + 2] != 1)
                    {
                        fallingBlocksStick.StickRotate(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 1 && row - 1 >= 0 && row + 1 <= takingASeat.Length - 1 && row + 2 <= takingASeat.Length - 1)
                {
                    if (takingASeat[row - 1][col] != 1 && takingASeat[row + 1][col] != 1 && takingASeat[row + 2][col] != 1)
                    {
                        fallingBlocksStick.StickRotate2(block);
                        rotateCount = 0;
                    }
                }
            }
            if (type == (int)TypeBlock.Saw)
            {
                int row1 = Grid.GetRow(block[1]);
                int col1 = Grid.GetColumn(block[1]);

                FallingBlocksSaw fallingBlocksSaw = new FallingBlocksSaw();
                if (rotateCount == 0 && row1 - 1 >= 0 && row1 + 1 <= takingASeat.Length - 1 && col1 + 1 <= takingASeat[0].Length - 1)
                {
                    if (takingASeat[row1 - 1][col1] != 1 && takingASeat[row1][col1 + 1] != 1 && takingASeat[row1 + 1][col1 + 1] != 1)
                    {
                        fallingBlocksSaw.SawRotate(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 1 && col1 - 1 >= 0 && row1 + 1 <= takingASeat.Length - 1 && col1 + 1 <= takingASeat[0].Length - 1)
                {
                    if (takingASeat[row1][col1 + 1] != 1 && takingASeat[row1 + 1][col1] != 1 && takingASeat[row1 + 1][col1 - 1] != 1)
                    {
                        fallingBlocksSaw.SawRotate2(block);
                        rotateCount = 0;
                    }
                }
            }
            if (type == (int)TypeBlock.WASD)
            {
                int row1 = Grid.GetRow(block[1]);
                int col1 = Grid.GetColumn(block[1]);

                int row = Grid.GetRow(block[2]);
                int col = Grid.GetColumn(block[2]);
                FallingBlocksWASD fallingBlocksWASD = new FallingBlocksWASD();
                if (rotateCount == 0 && row1 - 1 >= 0 && row1 + 1 <= takingASeat.Length - 1 && col1 + 1 <= takingASeat[0].Length - 1 && col1 + 2 <= takingASeat[0].Length - 1)
                {
                    if (takingASeat[row1][col1 + 2] != 1 && takingASeat[row1 - 1][col1 + 1] != 1 && takingASeat[row1][col1 + 1] != 1 && takingASeat[row1 + 1][col1 + 1] != 1)
                    {
                        fallingBlocksWASD.WASDRotate(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 1 && col - 1 >= 0 && row + 1 <= takingASeat.Length - 1 && col + 1 <= takingASeat[0].Length - 1)
                {
                    if (takingASeat[row + 1][col] != 1 && takingASeat[row][col + 1] != 1 && takingASeat[row][col - 1] != 1)
                    {
                        fallingBlocksWASD.WASDRotate2(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 2 && col - 1 >= 0 && row + 1 <= takingASeat.Length - 1 && row - 1 >= 0)
                {
                    if (takingASeat[row][col - 1] != 1 && takingASeat[row + 1][col] != 1 && takingASeat[row - 1][col] != 1)
                    {
                        fallingBlocksWASD.WASDRotate3(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 3 && row - 1 >= 0 && col + 1 + 1 <= takingASeat[0].Length - 1 && col - 1 >= 0)
                {
                    if (takingASeat[row - 1][col] != 1 && takingASeat[row][col - 1] != 1 && takingASeat[row][col + 1] != 1)
                    {
                        fallingBlocksWASD.WASDRotate4(block);
                        rotateCount = 0;
                    }
                }
            }
            if (type == (int)TypeBlock.Zaw)
            {
                int row = Grid.GetRow(block[2]);
                int col = Grid.GetColumn(block[2]);
                FallingBlocksZaw fallingBlocksZaw = new FallingBlocksZaw();
                if (rotateCount == 0 && row - 1 >= 0 && row + 1 <= takingASeat.Length - 1 && col - 1 >= 0)
                {
                    if (takingASeat[row - 1][col] != 1 && takingASeat[row][col - 1] != 1 && takingASeat[row + 1][col - 1] != 1)
                    {
                        fallingBlocksZaw.ZawRotate(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 1 && row - 1 >= 0 && col + 1 <= takingASeat[0].Length - 1 && col - 1 >= 0)
                {
                    if (takingASeat[row - 1][col - 1] != 1 && takingASeat[row - 1][col] != 1 && takingASeat[row][col + 1] != 1)
                    {
                        fallingBlocksZaw.ZawRotate2(block);
                        rotateCount = 0;
                    }
                }
            }
            if (type == (int)TypeBlock.Gamma)
            {
                int row = Grid.GetRow(block[2]);
                int col = Grid.GetColumn(block[2]);
                FallingBlocksGamma fallingBlocksGamma = new FallingBlocksGamma();
                if (rotateCount == 0 && row - 1 >= 0 && col + 1 <= takingASeat[0].Length - 1 && col - 1 >= 0)
                {
                    if (takingASeat[row - 1][col - 1] != 1 && takingASeat[row][col - 1] != 1 && takingASeat[row][col + 1] != 1)
                    {
                        fallingBlocksGamma.GammaRotate(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 1 && row + 2 <= takingASeat.Length - 1 && col + 1 <= takingASeat[0].Length - 1)
                {
                    if (takingASeat[row + 2][col] != 1 && takingASeat[row + 1][col] != 1 && takingASeat[row][col + 1] != 1)
                    {
                        fallingBlocksGamma.GammaRotate2(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 2 && row + 1 <= takingASeat.Length - 1 && col + 1 <= takingASeat[0].Length - 1 && col - 1 >= 0)
                {
                    if (takingASeat[row + 1][col + 1] != 1 && takingASeat[row][col - 1] != 1 && takingASeat[row][col + 1] != 1)
                    {
                        fallingBlocksGamma.GammaRotate3(block);
                        rotateCount++;
                    }
                }
                else if (rotateCount == 3 && row - 2 >= 0 && col - 1 >= 0)
                {
                    if (takingASeat[row - 2][col] != 1 && takingASeat[row - 1][col] != 1 && takingASeat[row][col - 1] != 1)
                    {
                        fallingBlocksGamma.GammaRotate4(block);
                        rotateCount = 0;
                    }
                }
            }
        }

        private static void InitRandomNumber(int seed)
        {
            random = new Random(seed);

        }
        private static int GenerateRandomNumber(int min, int max)
        {
            lock (syncObj)
            {
                if (random == null)
                    random = new Random(); // Or exception...
                return random.Next(min, max);
            }
        }

        public enum TypeBlock
        {
            Alpha = 1,
            Stick = 2,
            Samech = 3,
            Saw = 4,
            WASD = 5,
            Zaw = 6,
            Gamma = 7
        }
    }
}
