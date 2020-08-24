using EzGame.Data.Context;
using EzGame.Data.Interfaces.Repositories.Games;
using EzGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Services.Repositories.Games
{
   public class GameGenreRepository:IGameGenreRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public GameGenreRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<int> CountAsync()
        {
            return await _db.GameGenres.CountAsync();
        }

        public void Delete(object id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new ArgumentException("there is no entity with this id");
            }
            Delete(entity);
        }

        public void Delete(GameGenre entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.GameGenres.Remove(entity);
        }

        public void Delete(Expression<Func<GameGenre, bool>> where)
        {
            IEnumerable<GameGenre> entities = _db.GameGenres.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<GameGenre> GetAll()
        {
            return _db.GameGenres.AsEnumerable();
        }

        public async Task<IEnumerable<GameGenre>> GetAllAsync()
        {
            return await _db.GameGenres.ToListAsync();
        }

        public async Task<ICollection<GameGenre>> GetAllAsync(Expression<Func<GameGenre, bool>> match)
        {
            return await _db.GameGenres.Where(match).ToListAsync();
        }

        public async Task<GameGenre> GetAsync(Expression<Func<GameGenre, bool>> where)
        {
            return await _db.GameGenres.Where(where).FirstOrDefaultAsync();
        }

        public GameGenre GetById(object id)
        {
            return _db.GameGenres.Find(id);
        }

        public async Task<GameGenre> GetByIdAsync(object id)
        {
            return await _db.GameGenres.FindAsync(id);
        }

        public void Insert(GameGenre entity)
        {
            _db.GameGenres.Add(entity);
        }

        public async Task InsertAsync(GameGenre entity)
        {
            await _db.GameGenres.AddAsync(entity);
        }

        public IEnumerable<GameGenre> Take(int count)
        {
            return _db.GameGenres.Take(count);
        }

        public void Update(GameGenre entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.GameGenres.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<GameGenre> Where(Expression<Func<GameGenre, bool>> where)
        {
            IQueryable<GameGenre> entities = _db.GameGenres;
            if (where != null)
            {
                entities = entities.Where(where);
            }

            return entities.AsEnumerable();
        }

        #region IDisposable Support

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _db.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposedValue = true;
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~GameGenreRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
