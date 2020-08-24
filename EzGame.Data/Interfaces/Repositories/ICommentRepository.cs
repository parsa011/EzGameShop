using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories
{
   public interface ICommentRepository:IDisposable
    {
        void Insert(Comment entity);
        IEnumerable<Comment> Take(int count);
        void Update(Comment entity);
        void Delete(object id);
        void Delete(Comment entity);
        void Delete(Expression<Func<Comment, bool>> where);
        Domain.Entities.Comment GetById(object id);
        IEnumerable<Comment> GetAll();
        IEnumerable<Comment> Where(Expression<Func<Comment, bool>> where);

        #region Asyncs

        Task InsertAsync(Comment entity);
        Task<Comment> GetByIdAsync(object id);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetAsync(Expression<Func<Comment, bool>> where);
        Task<ICollection<Comment>> GetAllAsync(Expression<Func<Comment, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
