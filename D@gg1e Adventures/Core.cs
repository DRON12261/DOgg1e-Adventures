using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DOgg1eAdventures
{
    partial class Game
    {
        static void Update()
        {
            DrawLevel(currentLevel);
            DrawChar(player.curSprite, player.posX+1, player.posY+1);
            levelSoundtrack.PlayLooping();
            while (EXIT)
            {
                Input(Console.ReadKey(true));
                Console.Clear();
                DrawLevel(currentLevel);
                DrawChar(player.curSprite, player.posX+1, player.posY+1);
            }
        }

        static void Main(string[] args)
        {
            InitializeGame();
            Update();
        }
    }
}
