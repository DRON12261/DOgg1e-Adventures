using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOgg1eAdventures
{
    partial class Game
    {
        static void Input(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    PlayerMove(-1, 0, Directions.LEFT);
                    break;
                case ConsoleKey.RightArrow:
                    PlayerMove(1, 0, Directions.RIGHT);
                    break;
                case ConsoleKey.UpArrow:
                    PlayerMove(0, -1, Directions.UP);
                    break;
                case ConsoleKey.DownArrow:
                    PlayerMove(0, 1, Directions.DOWN);
                    break;
                case ConsoleKey.Escape:
                    EXIT = false;
                    break;
                default:

                    break;
            }
        }

        static void PlayerMove(int dx, int dy, Directions dir = Directions.NULL)
        {
            if (currentLevel.structure[player.posY + dy, player.posX + dx] != '#')
            {
                player.posX += dx;
                player.posY += dy;
                if (dir != Directions.NULL) player.dir = dir;
            }
        }
    }
}
