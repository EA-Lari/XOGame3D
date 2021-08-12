namespace TicTacToeGame.BLL.Structures
{
    public struct Coordinates
    {
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }

        public Coordinates(int x, int y)
        {
            CoordX = x;
            CoordY = y;
        }
    }
}
