namespace EzGame.Domain.Core.Interfaces
{
    public interface IIdGenerator
    {
        string NewGuid(int letterCount = 0);
    }
}