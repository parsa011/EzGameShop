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
   public class DoilarPriceRepository:IDollarPriceRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public DoilarPriceRepository(DatabaseContext db)
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

        public void Delete(DollarPrice entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.DollarPrice.Remove(entity);
        }

        public void Delete(Expression<Func<DollarPrice, bool>> where)
        {
            IEnumerable<DollarPrice> entities = _db.DollarPrice.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<DollarPrice> GetAll()
        {
            return _db.DollarPrice.AsEnumerable();
        }

        public async Task<IEnumerable<DollarPrice>> GetAllAsync()
        {
            return await _db.DollarPrice.ToListAsync();
        }

        public async Task<ICollection<DollarPrice>> GetAllAsync(Expression<Func<DollarPrice, bool>> match)
        {
            return await _db.DollarPrice.Where(match).ToListAsync();
        }

        public async Task<DollarPrice> GetAsync(Expression<Func<DollarPrice, bool>> where)
        {
            return await _db.DollarPrice.Where(where).FirstOrDefaultAsync();
        }

        public DollarPrice GetById(object id)
        {
            return _db.DollarPrice.Find(id);
        }

        public async Task<DollarPrice> GetByIdAsync(object id)
        {
            return await _db.DollarPrice.FindAsync(id);
        }

        public void Insert(DollarPrice entity)
        {
            _db.DollarPrice.Add(entity);
        }

        public async Task InsertAsync(DollarPrice entity)
        {
            await _db.DollarPrice.AddAsync(entity);
        }

        public IEnumerable<DollarPrice> Take(int count)
        {
            return _db.DollarPrice.Take(count);
        }

        public void Update(DollarPrice entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.DollarPrice.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<DollarPrice> Where(Expression<Func<DollarPrice, bool>> where)
        {
            IQueryable<DollarPrice> entities = _db.DollarPrice;
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
        ~DoilarPriceRepository()
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
