namespace GameStreamer.Backend.Services
{
    public interface IHashService
    {
        public Guid CalculateHashCodeFrom(string value);
    }
}
