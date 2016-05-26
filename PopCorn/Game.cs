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
        static int SCORE = 0;

        static char BOARD_SYMBOL = '=';
        static char BALL_SYMBOL = '@';
        static int SPACE_BETWEEN_BLOCKS = 4;

        static int BLOCK_WIDTH = 5;
        static int BLOCK_HEIGHT = 2;

        static int START_BOARD_X = 25;
        static int START_BOARD_Y = 25;

        static int DOT_START_Y = START_BOARD_X;
        static int DOT_START_X = START_BOARD_Y;

        static int OFFSET_BALL_Y = 1;
        static int OFFSET_BALL_X = 1;

        static bool IS_BALL_ON_BOARD = false;

        static Position ball = new Position(DOT_START_X, DOT_START_Y);

        static int GAME_WIDTH = 70;
        static int GAME_HEIGHT = 30;

        static int BOARD_SIZE = 7;

        static int GAME_SPEED = 65;
        static int BLOCK_LINES = 2;



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
        static void WriteOnConsole(string text, int x, int y, ConsoleColor color = ConsoleColor.DarkYellow)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;

            Console.WriteLine(text);
        }
        static void Print(char symbol, int x, int y, ConsoleColor color = ConsoleColor.White, int times = 0)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            if (times == 0)
            {
                Console.Write(symbol);
            }
            else
                Console.Write(new string(symbol, times));

        }
        static void DrawSingleBlock(int[] start)
        {
            int y = start[1];
            for (int i = 0; i < BLOCK_HEIGHT; i++)
            {
                Print('*', start[0], y, ConsoleColor.Yellow, BLOCK_WIDTH);
                y++;
            }
        }

        static bool CheckIfDead()
        {
            return ball.y > START_BOARD_Y;

        }
        static List<Block> GenerateBlocks(int startX, int startY, int lines)
        {
            int stX = startX;
            int stY = startY;
            List<Block> blocks = new List<Block>();
            int blocksNumber = (GAME_WIDTH / 4) - 1;

            for (int i = 0; i < lines; i++)
            {

                for (int j = 0; j < ((GAME_WIDTH / BLOCK_WIDTH) / 2) + 1; j++)
                {
                    blocks.Add(new Block(new int[] { stX, stY },
                        new int[] { stX + BLOCK_WIDTH, stY + BLOCK_HEIGHT }));
                    stX += BLOCK_WIDTH + SPACE_BETWEEN_BLOCKS;
                }
                stX = startX;
                stY += BLOCK_HEIGHT + 2;
            }

            return blocks;
        }

        static void CollideWithBlocks(Position ball, List<Block> allBlocks)
        {
            for (int i = 0; i < allBlocks.Count; i++)
            {
                if (allBlocks[i].IsCoordInBlock(ball.x, ball.y))
                {
                    allBlocks.Remove(allBlocks[i]);
                    OFFSET_BALL_Y *= -1;
                    SCORE += 10;
                    // check if hits the side of the block, then reverse X axis
                }
            }
        }

        static void DrawBlocks(List<Block> blocks)
        {
            foreach (var block in blocks)
            {
                DrawSingleBlock(block.start);
            }
        }
        static void Main()
        {
            Console.Title = "NIKI POPCORN";
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WindowWidth = GAME_WIDTH;
            Console.BufferWidth = GAME_WIDTH;
            Console.WindowHeight = GAME_HEIGHT + 1;
            Console.BufferHeight = GAME_HEIGHT + 1;
            Console.CursorVisible = false;

            int boardSpeed = 0;

            List<Block> blocks = GenerateBlocks(1, 1, BLOCK_LINES);

            while (true)
            {
                DrawBlocks(blocks);
                CollideWithBlocks(ball, blocks);

                // print board
                Print(BOARD_SYMBOL, START_BOARD_X, START_BOARD_Y, ConsoleColor.Green, BOARD_SIZE);

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
                Print(BALL_SYMBOL, ball.x, ball.y, ConsoleColor.White);

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                        boardSpeed = 5;

                    else
                        boardSpeed = -5;

                    if (START_BOARD_X + boardSpeed >= 0 && (
                        (START_BOARD_X + BOARD_SIZE + boardSpeed) <= GAME_WIDTH))
                    {
                        START_BOARD_X += boardSpeed;
                    }
                }
                if (CheckIfDead())
                {
                    Console.WriteLine("GAME OVER, MATE");
                    break;
                }
                WriteOnConsole(string.Format("SCORE: {0}", SCORE), 5, 26) ;
                Thread.Sleep(GAME_SPEED);
                Console.Clear();
            }
        }
    }
}
