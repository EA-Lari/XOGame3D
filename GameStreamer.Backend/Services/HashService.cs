using System.Text;
using System.Security.Cryptography;

namespace GameStreamer.Backend.Services
{
    public class HashService : IHashService
    {
        public Guid CalculateHashCodeFrom(string value)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }
    }
}
