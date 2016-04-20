using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameRazcukvane
{
    public class Block
    {
        public int[] start;
        public int[] end;

        public Block(int[] start, int[] end)
        {
            this.start = start;
            this.end = end;
        }

        public bool IsCoordInBlock(int x, int y)
        {
            return x >= start[0] && x <= end[0] && 
                y >= start[1] && y <= end[1];
        }

    }
}
