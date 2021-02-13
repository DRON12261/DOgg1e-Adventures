using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleOutputModule;

namespace DOgg1eAdventures
{
    partial class Game
    {
        static Object player;
        static bool EXIT;
        static Level currentLevel;
        static SoundPlayer levelSoundtrack;
        static ConsoleOutput.Cell[,] chrBank;

        struct Object
        {
            public int posX, posY;
            public char[] sprites;
            public char curSprite;
            public Directions dir;
        }

        struct Level
        {
            public string name;
            public char[,] structure;
        }

        enum Directions
        {
            NULL,
            LEFT,
            RIGHT,
            UP,
            DOWN
        }

        static void InitializeGame()
        {
            Console.Title = "D@gg1e Adventures";
            Console.CursorVisible = false;
            player = new Object();
            player.posX = 1; player.posY = 1;
            player.sprites = new char[4] { '@', '@', '@', '@' };
            player.curSprite = player.sprites[0];
            player.dir = Directions.RIGHT;
            currentLevel = LoadLevel(@".\Levels\Level1.txt");
            levelSoundtrack = new SoundPlayer(@".\Shiroyama.wav");
            levelSoundtrack.Load();
            EXIT = true;
            chrBank = new ConsoleOutput.Cell[currentLevel.structure.GetLength(1)+2, currentLevel.structure.GetLength(0)+2];
            for (int i = 0; i < chrBank.GetLength(0); i++)
            {
                for (int j = 0; j < chrBank.GetLength(1); j++)
                {
                    chrBank[i, j] = new ConsoleOutput.Cell();
                    chrBank[i, j].Background_Color = ConsoleOutput.GetColor(ConsoleColor.DarkRed);
                    chrBank[i, j].Foreground_Color = ConsoleOutput.GetColor(ConsoleColor.Red);
                    if (i != 0 && j != 0 && i != chrBank.GetLength(0)-1 && j != chrBank.GetLength(1)-1)
                        chrBank[i, j].Symbol = currentLevel.structure[j - 1, i - 1];
                    else 
                        chrBank[i, j].Symbol = ' ';
                }
            }
        }

        static Level LoadLevel(string path)
        {
            StreamReader reader = new StreamReader(path);
            Level level = new Level();
            int levelWidth = 0, levelHeight = 0;
            for (int i = 0; !reader.EndOfStream; i++)
            {
                if (i == 0) level.name = reader.ReadLine();
                else if (i == 1) levelWidth = int.Parse(reader.ReadLine());
                else if (i == 2)
                {
                    levelHeight = int.Parse(reader.ReadLine());
                    level.structure = new char[levelHeight, levelWidth];
                }
                else
                {
                    char[] tempLine = reader.ReadLine().ToCharArray();
                    for (int j = 0; j < tempLine.Length; j++) level.structure[i - 3, j] = tempLine[j];
                }
            }
            Console.WindowWidth = levelWidth + 2; Console.WindowHeight = levelHeight + 2;
            Console.BufferWidth = levelWidth + 2; Console.BufferHeight = levelHeight + 2;
            return level;
        }
    }
}
