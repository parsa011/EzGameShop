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
    public class GameEditionRepository:IGameEditionRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public GameEditionRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<int> CountAsync()
        {
            return await _db.GameEditions.CountAsync();
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

        public void Delete(GameEdition entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.GameEditions.Remove(entity);
        }

        public void Delete(Expression<Func<GameEdition, bool>> where)
        {
            IEnumerable<GameEdition> entities = _db.GameEditions.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<GameEdition> GetAll()
        {
            return _db.GameEditions.AsEnumerable();
        }

        public async Task<IEnumerable<GameEdition>> GetAllAsync()
        {
            return await _db.GameEditions.ToListAsync();
        }

        public async Task<ICollection<GameEdition>> GetAllAsync(Expression<Func<GameEdition, bool>> match)
        {
            return await _db.GameEditions.Where(match).ToListAsync();
        }

        public async Task<GameEdition> GetAsync(Expression<Func<GameEdition, bool>> where)
        {
            return await _db.GameEditions.Where(where).FirstOrDefaultAsync();
        }

        public GameEdition GetById(object id)
        {
            return _db.GameEditions.Find(id);
        }

        public async Task<GameEdition> GetByIdAsync(object id)
        {
            return await _db.GameEditions.FindAsync(id);
        }

        public void Insert(GameEdition entity)
        {
            _db.GameEditions.Add(entity);
        }

        public async Task InsertAsync(GameEdition entity)
        {
            await _db.GameEditions.AddAsync(entity);
        }

        public IEnumerable<GameEdition> Take(int count)
        {
            return _db.GameEditions.Take(count);
        }

        public void Update(GameEdition entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.GameEditions.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<GameEdition> Where(Expression<Func<GameEdition, bool>> where)
        {
            IQueryable<GameEdition> entities = _db.GameEditions;
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
        ~GameEditionRepository()
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
