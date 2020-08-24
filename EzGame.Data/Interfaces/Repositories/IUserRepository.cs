using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories
{
   public interface IUserRepository:IDisposable
    {
        void Insert(User entity);
        IEnumerable<User> Take(int count);
        void Update(User entity);
        void Delete(object id);
        void Delete(User entity);
        void Delete(Expression<Func<User, bool>> where);
        Domain.Entities.User GetById(object id);
        IEnumerable<User> GetAll();
        IEnumerable<User> Where(Expression<Func<User, bool>> where);

        #region Asyncs

        Task InsertAsync(User entity);
        Task<User> GetByIdAsync(object id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(Expression<Func<User, bool>> where);
        Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
