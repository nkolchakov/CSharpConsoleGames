using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FallingRocks
{
    class Game
    {
        public static int GAME_WIDTH = 40;
        public static int GAME_HEIGHT = 30;

        static int PLAYER_Y = GAME_HEIGHT;
        static int PLAYER_X = GAME_WIDTH / 2;

        static string PLAYER = "\\0/";
        static int SCORE = 0;

        static Random colorGen = new Random();
        static void Main()
        {
            Console.Title = "Falling Rocks";
            Console.WindowWidth = GAME_WIDTH;
            Console.BufferWidth = GAME_WIDTH;
            Console.WindowHeight = GAME_HEIGHT + 1;
            Console.BufferHeight = GAME_HEIGHT + 1;
            Console.CursorVisible = false;

            List<Rock> rocks = new List<Rock>();

            while (true)
            {
                Console.SetCursorPosition(1, 1);
                Console.WriteLine("SCORE:{0} ", SCORE);




                 if (rocks.Any(rock => (rock.X >= PLAYER_X && rock.X <= PLAYER_X + PLAYER.Length &&
                       rock.Y == PLAYER_Y)))
                    {
                        Console.SetCursorPosition(GAME_WIDTH / 2 - 20, GAME_HEIGHT / 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("GAME OVER - SOCRE: {0}",SCORE);
                        return;
                    }
                foreach (var rock in rocks)
                {
                    Console.ForegroundColor = rock.elColor;
                    Console.SetCursorPosition(rock.X, rock.Y);
                    Console.WriteLine(rock.symbol);
                }
                Console.SetCursorPosition(PLAYER_X, PLAYER_Y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(PLAYER);
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                    {
                        if (PLAYER_X > 0)
                            PLAYER_X--;
                    }
                    else
                    {
                        if (PLAYER_X < GAME_WIDTH - 4)
                            PLAYER_X++;
                    }
                   
                }
                rocks.Add(Rock.CreateRandomRock(GAME_WIDTH, GAME_HEIGHT));

                for (int i = 0; i < rocks.Count; i++)
                {
                    if (rocks[i].Y == GAME_HEIGHT)
                        rocks.Remove(rocks[i]);
                    rocks[i].FallTheRock();
                }

                Thread.Sleep(150);
                Console.Clear();
                SCORE += 10;
            }
        }
    }
}
