namespace TicTacToeGame.BLL.Structures
{
    public struct Coordinates
    {
        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public Coordinates(int x, int y)
        {
            CoordX = x;
            CoordY = y;
        }
    }
}
