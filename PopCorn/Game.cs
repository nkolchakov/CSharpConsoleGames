using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGameRazcukvane
{
    struct Position
    {
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    };


    class Game
    {

        static bool[,] BLOCK = new bool[4,2];

        static int START_BOARD_X = 25;
        static int START_BOARD_Y = 25;

        static int DOT_START_Y = 1;
        static int DOT_START_X = 30;

        static int OFFSET_BALL_Y = 1;
        static int OFFSET_BALL_X = 1;

        static bool IS_BALL_ON_BOARD = false;

        static Position ball = new Position(DOT_START_X, DOT_START_Y);

        static int GAME_WIDTH = 70;
        static int GAME_HEIGHT = 30;

        static int BOARD_SIZE = 7;

        static int GAME_SPEED = 65;



        static string IsOutOfGameField(Position ball)
        {
            if (ball.y == 0)
                return "top";
            else if (ball.x == 0)
                return "left";
            else if (ball.x == GAME_WIDTH - 1)
                return "right";

            else return "none";
        }

        static void Print(char symbol, int x, int y, ConsoleColor color = ConsoleColor.White, int times = 0)
        {
            Console.SetCursorPosition(x,y);
            Console.ForegroundColor = color;
            if (times == 0)
            {
                Console.Write(symbol);
            }
            else
                Console.Write(new string(symbol, times));

        }
        static void DrawSingleBlock(int x, int y)
        {
            for (int i = 0; i < BLOCK.GetLength(0); i++)
            {
                for (int j = 0; j <BLOCK.GetLength(1); j++)
                {
                    Print('*', i,j, ConsoleColor.Blue);
                }
            }
        }


        static void Main(string[] args)
        {
            Console.WindowWidth = GAME_WIDTH;
            Console.BufferWidth = GAME_WIDTH;
            Console.WindowHeight = GAME_HEIGHT + 1;
            Console.BufferHeight = GAME_HEIGHT + 1;
            Console.CursorVisible = false;

            int boardSpeed = 0;

            while (true)
            {
                Console.Clear();

                // print board
                Print('_', START_BOARD_X, START_BOARD_Y, ConsoleColor.Red, BOARD_SIZE);
                DrawSingleBlock(1, 1);

                // land on board
                if (!IS_BALL_ON_BOARD && ball.y == START_BOARD_Y && ball.x >= START_BOARD_X &&
                    ball.x <= START_BOARD_X + BOARD_SIZE)
                {
                    OFFSET_BALL_Y = -1;
                    IS_BALL_ON_BOARD = true;
                }
                else
                {
                    IS_BALL_ON_BOARD = false;
                    switch (IsOutOfGameField(ball))
                    {
                        case "none":
                            ball.x += OFFSET_BALL_X;
                            ball.y += OFFSET_BALL_Y;
                            break;
                        case "top":
                            ball.y = 1;
                            OFFSET_BALL_Y *= -1;
                            break;
                        case "right":
                            ball.x = GAME_WIDTH - 2;
                            OFFSET_BALL_X *= -1;
                            break;
                        case "left":
                            ball.x = 1;
                            OFFSET_BALL_X *= -1;
                            break;
                        default:
                            break;
                    }
                }

                // print ball
                Print('o', ball.x, ball.y, ConsoleColor.Green);

                if (Console.KeyAvailable)
                {

                    if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                        boardSpeed = 5;

                    else if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                        boardSpeed = -5;
                    else
                    {
                        break;
                    }

                    if (START_BOARD_X + boardSpeed >= 0 && (
                        (START_BOARD_X + BOARD_SIZE + boardSpeed) <= GAME_WIDTH))
                    {
                        START_BOARD_X += boardSpeed;
                    }
                }

                Thread.Sleep(GAME_SPEED);
            }
        }
    }
}
