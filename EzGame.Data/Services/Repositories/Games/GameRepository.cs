using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EzGame.Data.Context;
using EzGame.Data.Interfaces.Repositories.Games;
using EzGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EzGame.Data.Services.Repositories.Games
{
    public class GameRepository : IGameRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public GameRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<int> CountAsync()
        {
            return await _db.Games.CountAsync();
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

        public void Delete(Game entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.Games.Remove(entity);
        }

        public void Delete(Expression<Func<Game, bool>> where)
        {
            IEnumerable<Game> entities = _db.Games.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<Game> GetAll()
        {
            return _db.Games.AsEnumerable();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _db.Games.ToListAsync();
        }

        public async Task<ICollection<Game>> GetAllAsync(Expression<Func<Game, bool>> match)
        {
            return await _db.Games.Where(match).ToListAsync();
        }

        public async Task<Game> GetAsync(Expression<Func<Game, bool>> where)
        {
            return await _db.Games.Where(where).FirstOrDefaultAsync();
        }

        public Game GetById(object id)
        {
            return _db.Games.Find(id);
        }

        public async Task<Game> GetByIdAsync(object id)
        {
            return await _db.Games.FindAsync(id);
        }

        public void Insert(Game entity)
        {
            _db.Games.Add(entity);
        }

        public async Task InsertAsync(Game entity)
        {
            await _db.Games.AddAsync(entity);
        }

        public IEnumerable<Game> Take(int count)
        {
            return _db.Games.Take(count);
        }

        public void Update(Game entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Games.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Game> Where(Expression<Func<Game, bool>> where)
        {
            IQueryable<Game> entities = _db.Games;
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
        ~GameRepository()
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