using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories
{
   public interface IGenreRepository:IDisposable
    {
        void Insert(Genre entity);
        IEnumerable<Genre> Take(int count);
        void Update(Genre entity);
        void Delete(object id);
        void Delete(Genre entity);
        void Delete(Expression<Func<Genre, bool>> where);
        Domain.Entities.Genre GetById(object id);
        IEnumerable<Genre> GetAll();
        IEnumerable<Genre> Where(Expression<Func<Genre, bool>> where);

        #region Asyncs

        Task InsertAsync(Genre entity);
        Task<Genre> GetByIdAsync(object id);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetAsync(Expression<Func<Genre, bool>> where);
        Task<ICollection<Genre>> GetAllAsync(Expression<Func<Genre, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
