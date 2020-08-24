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
using System.Windows.Input;

namespace EzGame.Data.Services.Repositories
{
   public class CommentRepository:ICommentRepository
    {
        #region ctor
        private readonly DatabaseContext _db;
        public CommentRepository(DatabaseContext db)
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

        public void Delete(Comment entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Attach(entity);
            }
            _db.Comments.Remove(entity);
        }

        public void Delete(Expression<Func<Comment, bool>> where)
        {
            IEnumerable<Comment> entities = _db.Comments.Where(where);
            foreach (var item in entities)
            {
                Delete(item);
            }
        }

        public IEnumerable<Comment> GetAll()
        {
            return _db.Comments.AsEnumerable();
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _db.Comments.ToListAsync();
        }

        public async Task<ICollection<Comment>> GetAllAsync(Expression<Func<Comment, bool>> match)
        {
            return await _db.Comments.Where(match).ToListAsync();
        }

        public async Task<Comment> GetAsync(Expression<Func<Comment, bool>> where)
        {
            return await _db.Comments.Where(where).FirstOrDefaultAsync();
        }

        public Comment GetById(object id)
        {
            return _db.Comments.Find(id);
        }

        public async Task<Comment> GetByIdAsync(object id)
        {
            return await _db.Comments.FindAsync(id);
        }

        public void Insert(Comment entity)
        {
            _db.Comments.Add(entity);
        }

        public async Task InsertAsync(Comment entity)
        {
            await _db.Comments.AddAsync(entity);
        }

        public IEnumerable<Comment> Take(int count)
        {
            return _db.Comments.Take(count);
        }

        public void Update(Comment entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _db.Comments.Attach(entity);
            }
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<Comment> Where(Expression<Func<Comment, bool>> where)
        {
            IQueryable<Comment> entities = _db.Comments;
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
        ~CommentRepository()
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
