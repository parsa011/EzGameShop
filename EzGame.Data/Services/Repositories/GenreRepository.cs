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
   public class GenreRepository:IGenreRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public GenreRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<int> CountAsync()
        {
            return await _db.Genres.CountAsync();
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

        public void Delete(Genre entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.Genres.Remove(entity);
        }

        public void Delete(Expression<Func<Genre, bool>> where)
        {
            IEnumerable<Genre> entities = _db.Genres.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<Genre> GetAll()
        {
            return _db.Genres.AsEnumerable();
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _db.Genres.ToListAsync();
        }

        public async Task<ICollection<Genre>> GetAllAsync(Expression<Func<Genre, bool>> match)
        {
            return await _db.Genres.Where(match).ToListAsync();
        }

        public async Task<Genre> GetAsync(Expression<Func<Genre, bool>> where)
        {
            return await _db.Genres.Where(where).FirstOrDefaultAsync();
        }

        public Genre GetById(object id)
        {
            return _db.Genres.Find(id);
        }

        public async Task<Genre> GetByIdAsync(object id)
        {
            return await _db.Genres.FindAsync(id);
        }

        public void Insert(Genre entity)
        {
            _db.Genres.Add(entity);
        }

        public async Task InsertAsync(Genre entity)
        {
            await _db.Genres.AddAsync(entity);
        }

        public IEnumerable<Genre> Take(int count)
        {
            return _db.Genres.Take(count);
        }

        public void Update(Genre entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Genres.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Genre> Where(Expression<Func<Genre, bool>> where)
        {
            IQueryable<Genre> entities = _db.Genres;
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
        ~GenreRepository()
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
