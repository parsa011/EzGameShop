using System;
using System.Threading.Tasks;
using EzGame.Data.Interfaces.Repositories;
using EzGame.Data.Interfaces.Repositories.Games;

namespace EzGame.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameDiscountRepository DiscountRepository { get; }
        IGameEditionRepository GameEditionRepository { get; }
        IGameGenreRepository GameGenreRepository { get; }
        IGamePlatformRepository GamePlatformRepository { get; }
        IGameRepository GameRepository { get; }
        ICommentRepository CommentRepository { get; }
        IDollarPriceRepository DollarPriceRepository { get; }
        IGenreRepository GenreRepository { get; }
        IGameVisitRepository GameVisitRepository { get; }
        IPlatformRepository PlatformRepository { get; }
        IUserRepository UserRepository { get; }
        void SaveChange();
        Task<int> SaveChangeAsync();
    }
}