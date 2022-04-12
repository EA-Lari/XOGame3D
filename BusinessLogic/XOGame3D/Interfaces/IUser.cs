using XOGame3D.Enum;
using XOGame3D.Models;

namespace XOGame3D.Interfaces
{
    public interface IUser
    {
        string Name { get; set; }

        States Fraction { get; set; }

        Coordinate ChooseCell();

        Coordinate ChooseArea();
    }
}
