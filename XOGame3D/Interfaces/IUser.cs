using XOGame3D.Enum;

namespace XOGame3D.Interfaces
{
    public interface IUser
    {
        string Name { get; set; }

        States Fraction { get; set; }
    }
}
