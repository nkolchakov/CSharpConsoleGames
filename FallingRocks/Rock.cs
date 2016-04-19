using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingRocks
{
    public class Rock
    {
        private int x;
        private int y;
        public char symbol;
        public ConsoleColor elColor;

        private static Random generator = new Random();
        public static string symbols = "^@*&+%$#!.;-";

        public Rock(char sym, int xDrop, int yDrop)
        {
            x = xDrop;
            y = yDrop;
            symbol = sym;

           elColor = (ConsoleColor)generator.Next(0, 16);
        }

        public static char GenerateSymbol()
        {
            return symbols[generator.Next(0, symbols.Length)];
        }
        
        public void FallTheRock()
        {
            if (y <Game.GAME_HEIGHT )
                y++;
        }

        public int X { get { return this.x; } }

        public int Y { get { return this.y; } }

        public static Rock CreateRandomRock(int maxX, int maxY)
        {
            char symbol = GenerateSymbol();
            int objX = generator.Next(1, maxX - 1);
            int objY = generator.Next(1, (maxY - 1) / 2);

            return new Rock(symbol, objX, objY);
        }
    }
}
