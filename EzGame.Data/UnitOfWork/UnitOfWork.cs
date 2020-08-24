using System;
using System.Threading.Tasks;
using EzGame.Data.Context;
using EzGame.Data.Interfaces;
using EzGame.Data.Interfaces.Repositories;
using EzGame.Data.Interfaces.Repositories.Games;
using EzGame.Data.Services.Repositories;
using EzGame.Data.Services.Repositories.Games;
// ReSharper disable InconsistentNaming

namespace EzGame.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region ctor
        private readonly DatabaseContext _db;
        public UnitOfWork(DatabaseContext db)
        {
            _db = db;
        }
        #endregion
        
        private IGameDiscountRepository _discountRepository { get; set; }
        public IGameDiscountRepository DiscountRepository => _discountRepository ??= new GameDiscountRepository(_db);
        
        private IGameEditionRepository _gameEditionRepository { get; set; }
        public IGameEditionRepository GameEditionRepository => _gameEditionRepository ??= new GameEditionRepository(_db);
        
        private IGameGenreRepository _gameGenreRepository { get; set; }
        public IGameGenreRepository GameGenreRepository => _gameGenreRepository ??= new GameGenreRepository(_db);
        
        private IGamePlatformRepository _gamePlatformRepository { get; set; }
        public IGamePlatformRepository GamePlatformRepository => _gamePlatformRepository ??= new GamePlatformRepository(_db);
        
        private IGameRepository _gameRepository { get; set; }
        public IGameRepository GameRepository => _gameRepository ??= new GameRepository(_db);
        
        private ICommentRepository _commentRepository { get; set; }
        public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(_db);
        
        private IDollarPriceRepository _dollarPriceRepository { get; set; }
        public IDollarPriceRepository DollarPriceRepository => _dollarPriceRepository ??= new DollarPriceRepository(_db);
        
        private IGenreRepository _genreRepository { get; set; }
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_db);
        
        private IPlatformRepository _platformRepository { get; set; }
        public IPlatformRepository PlatformRepository => _platformRepository ??= new PlatformRepository(_db);
        
        private IUserRepository _userRepository { get; set; }
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_db);

        #region actions
        
        public void SaveChange()
        {
            _db.SaveChanges();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _db.SaveChangesAsync();
        }

        #endregion
        
        
        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _db.Dispose();
            }
            
            _disposedValue = true;
        }
        
        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}