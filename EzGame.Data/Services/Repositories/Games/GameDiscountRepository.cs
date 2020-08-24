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
   public class GameDiscountRepository : IGameDiscountRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public GameDiscountRepository(DatabaseContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<int> CountAsync()
        {
            return await _db.GameDiscounts.CountAsync();
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

        public void Delete(GameDiscount entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.GameDiscounts.Remove(entity);
        }

        public void Delete(Expression<Func<GameDiscount, bool>> where)
        {
            IEnumerable<GameDiscount> entities = _db.GameDiscounts.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<GameDiscount> GetAll()
        {
            return _db.GameDiscounts.AsEnumerable();
        }

        public async Task<IEnumerable<GameDiscount>> GetAllAsync()
        {
            return await _db.GameDiscounts.ToListAsync();
        }

        public async Task<ICollection<GameDiscount>> GetAllAsync(Expression<Func<GameDiscount, bool>> match)
        {
            return await _db.GameDiscounts.Where(match).ToListAsync();
        }

        public async Task<GameDiscount> GetAsync(Expression<Func<GameDiscount, bool>> where)
        {
            return await _db.GameDiscounts.Where(where).FirstOrDefaultAsync();
        }

        public GameDiscount GetById(object id)
        {
            return _db.GameDiscounts.Find(id);
        }

        public async Task<GameDiscount> GetByIdAsync(object id)
        {
            return await _db.GameDiscounts.FindAsync(id);
        }

        public void Insert(GameDiscount entity)
        {
            _db.GameDiscounts.Add(entity);
        }

        public async Task InsertAsync(GameDiscount entity)
        {
            await _db.GameDiscounts.AddAsync(entity);
        }

        public IEnumerable<GameDiscount> Take(int count)
        {
            return _db.GameDiscounts.Take(count);
        }

        public void Update(GameDiscount entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.GameDiscounts.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<GameDiscount> Where(Expression<Func<GameDiscount, bool>> where)
        {
            IQueryable<GameDiscount> entities = _db.GameDiscounts;
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
        ~GameDiscountRepository()
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
