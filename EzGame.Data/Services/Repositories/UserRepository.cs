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
   public class UserRepository:IUserRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public UserRepository(DatabaseContext db)
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

        public void Delete(User entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.Users.Remove(entity);
        }

        public void Delete(Expression<Func<User, bool>> where)
        {
            IEnumerable<User> entities = _db.Users.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users.AsEnumerable();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>> match)
        {
            return await _db.Users.Where(match).ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> where)
        {
            return await _db.Users.Where(where).FirstOrDefaultAsync();
        }

        public User GetById(object id)
        {
            return _db.Users.Find(id);
        }

        public async Task<User> GetByIdAsync(object id)
        {
            return await _db.Users.FindAsync(id);
        }

        public void Insert(User entity)
        {
            _db.Users.Add(entity);
        }

        public async Task InsertAsync(User entity)
        {
            await _db.Users.AddAsync(entity);
        }

        public IEnumerable<User> Take(int count)
        {
            return _db.Users.Take(count);
        }

        public void Update(User entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Users.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<User> Where(Expression<Func<User, bool>> where)
        {
            IQueryable<User> entities = _db.Users;
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
        ~UserRepository()
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
