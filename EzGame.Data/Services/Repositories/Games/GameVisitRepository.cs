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
    public class GameVisitRepository : IGameVisitRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public GameVisitRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<int> CountAsync()
        {
            return await _db.GameVisits.CountAsync();
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

        public void Delete(GameVisit entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.GameVisits.Remove(entity);
        }

        public void Delete(Expression<Func<GameVisit, bool>> where)
        {
            IEnumerable<GameVisit> entities = _db.GameVisits.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<GameVisit> GetAll()
        {
            return _db.GameVisits.AsEnumerable();
        }

        public async Task<IEnumerable<GameVisit>> GetAllAsync()
        {
            return await _db.GameVisits.ToListAsync();
        }

        public async Task<ICollection<GameVisit>> GetAllAsync(Expression<Func<GameVisit, bool>> match)
        {
            return await _db.GameVisits.Where(match).ToListAsync();
        }

        public async Task<GameVisit> GetAsync(Expression<Func<GameVisit, bool>> where)
        {
            return await _db.GameVisits.Where(where).FirstOrDefaultAsync();
        }

        public GameVisit GetById(object id)
        {
            return _db.GameVisits.Find(id);
        }

        public async Task<GameVisit> GetByIdAsync(object id)
        {
            return await _db.GameVisits.FindAsync(id);
        }

        public void Insert(GameVisit entity)
        {
            _db.GameVisits.Add(entity);
        }

        public async Task InsertAsync(GameVisit entity)
        {
            await _db.GameVisits.AddAsync(entity);
        }

        public IEnumerable<GameVisit> Take(int count)
        {
            return _db.GameVisits.Take(count);
        }

        public void Update(GameVisit entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.GameVisits.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<GameVisit> Where(Expression<Func<GameVisit, bool>> where)
        {
            IQueryable<GameVisit> entities = _db.GameVisits;
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
        ~GameVisitRepository()
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
