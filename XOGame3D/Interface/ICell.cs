using XOGame3D.Enum;

namespace XOGame3D.Interface
{
    public interface ICell
    {
        int Oy { get; set; }
        int Ox { get; set; }
        State State { get; set; }
        bool Equals(ICell obj);
    }
}