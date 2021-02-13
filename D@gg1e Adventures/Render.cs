using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleOutputModule;

namespace DOgg1eAdventures
{
    partial class Game
    {
        static void DrawChar(char ch, int x, int y, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(ch);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }

        static void DrawLevel(Level level)
        {
            /*Console.WriteLine();
            for (int i = 0; i < level.structure.GetLength(0); i++)
            {
                char[] temp = new char[level.structure.GetLength(1) + 1];
                for (int j = 0; j < temp.Length; j++)
                {
                    if (j == 0)
                    {
                        temp[j] = ' ';
                    }
                    else
                    {
                        temp[j] = level.structure[i, j - 1];
                    }
                }
                Console.WriteLine(temp);
            }
            */
            ConsoleOutput.WriteOutput((short)chrBank.GetLength(0), (short)chrBank.GetLength(1), chrBank);
        }
    }
}
