using XOGame3D.Models;

namespace XOGame3D.Interfaces
{
    public interface IUserBasic : IUser
    {
        Coordinate ChooseCell();

        Coordinate ChooseArea();
    }
}
