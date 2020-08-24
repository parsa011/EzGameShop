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
   public class GamePlatformRepository:IGamePlatformRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public GamePlatformRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
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

        public void Delete(GamePlatform entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.GamePlatforms.Remove(entity);
        }

        public void Delete(Expression<Func<GamePlatform, bool>> where)
        {
            IEnumerable<GamePlatform> entities = _db.GamePlatforms.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<GamePlatform> GetAll()
        {
            return _db.GamePlatforms.AsEnumerable();
        }

        public async Task<IEnumerable<GamePlatform>> GetAllAsync()
        {
            return await _db.GamePlatforms.ToListAsync();
        }

        public async Task<ICollection<GamePlatform>> GetAllAsync(Expression<Func<GamePlatform, bool>> match)
        {
            return await _db.GamePlatforms.Where(match).ToListAsync();
        }

        public async Task<GamePlatform> GetAsync(Expression<Func<GamePlatform, bool>> where)
        {
            return await _db.GamePlatforms.Where(where).FirstOrDefaultAsync();
        }

        public GamePlatform GetById(object id)
        {
            return _db.GamePlatforms.Find(id);
        }

        public async Task<GamePlatform> GetByIdAsync(object id)
        {
            return await _db.GamePlatforms.FindAsync(id);
        }

        public void Insert(GamePlatform entity)
        {
            _db.GamePlatforms.Add(entity);
        }

        public async Task InsertAsync(GamePlatform entity)
        {
            await _db.GamePlatforms.AddAsync(entity);
        }

        public IEnumerable<GamePlatform> Take(int count)
        {
            return _db.GamePlatforms.Take(count);
        }

        public void Update(GamePlatform entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.GamePlatforms.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<GamePlatform> Where(Expression<Func<GamePlatform, bool>> where)
        {
            IQueryable<GamePlatform> entities = _db.GamePlatforms;
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
        ~GamePlatformRepository()
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
