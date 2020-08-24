using EzGame.Data.Context;
using EzGame.Data.Interfaces.Repositories;
using EzGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Services.Repositories
{
   public class PlatformRepository:IPlatformRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public PlatformRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<int> CountAsync()
        {
            return await _db.Platforms.CountAsync();
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

        public void Delete(Platform entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.Platforms.Remove(entity);
        }

        public void Delete(Expression<Func<Platform, bool>> where)
        {
            IEnumerable<Platform> entities = _db.Platforms.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<Platform> GetAll()
        {
            return _db.Platforms.AsEnumerable();
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            return await _db.Platforms.ToListAsync();
        }

        public async Task<ICollection<Platform>> GetAllAsync(Expression<Func<Platform, bool>> match)
        {
            return await _db.Platforms.Where(match).ToListAsync();
        }

        public async Task<Platform> GetAsync(Expression<Func<Platform, bool>> where)
        {
            return await _db.Platforms.Where(where).FirstOrDefaultAsync();
        }

        public Platform GetById(object id)
        {
            return _db.Platforms.Find(id);
        }

        public async Task<Platform> GetByIdAsync(object id)
        {
            return await _db.Platforms.FindAsync(id);
        }

        public void Insert(Platform entity)
        {
            _db.Platforms.Add(entity);
        }

        public async Task InsertAsync(Platform entity)
        {
            await _db.Platforms.AddAsync(entity);
        }

        public IEnumerable<Platform> Take(int count)
        {
            return _db.Platforms.Take(count);
        }

        public void Update(Platform entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Platforms.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Platform> Where(Expression<Func<Platform, bool>> where)
        {
            IQueryable<Platform> entities = _db.Platforms;
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
        ~PlatformRepository()
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
