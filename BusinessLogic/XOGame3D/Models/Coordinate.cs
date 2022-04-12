namespace XOGame3D.Models
{
    public class Coordinate 
    {
        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public static bool operator ==(Coordinate a, Coordinate b)
             => a.Column == b.Column && a.Row == b.Row;
  
        public static bool operator !=(Coordinate a, Coordinate b)
            => a.Column != b.Column || a.Row != b.Row;

        public override bool Equals(object obj)
        {
            var c = obj as Coordinate;
            return c.Column == this.Column && c.Row == this.Row;
        }
    }
}
