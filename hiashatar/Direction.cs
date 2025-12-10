using System;

namespace Hiashatar
{
    public static class Direction
    {
        public static readonly int[] QUEENLIKEROW = [-1, -1, 0, 1, 1, 1, 0, -1];
        public static readonly int[] QUEENLIKECOL = [0, 1, 1, 1, 0, -1, -1, -1];
        public static readonly int[] HORSEMOVEROW = [2, 2, -2, -2, 1, 1, -1, -1];
        public static readonly int[] HORSEMOVECOL = [1, -1, 1, -1, 2, -2, 2, -2];
    }
}
