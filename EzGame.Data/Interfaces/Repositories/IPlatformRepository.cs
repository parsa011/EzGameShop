using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories
{
    public interface IPlatformRepository : IDisposable
    {
        void Insert(Platform entity);
        IEnumerable<Platform> Take(int count);
        void Update(Platform entity);
        void Delete(object id);
        void Delete(Platform entity);
        void Delete(Expression<Func<Platform, bool>> where);
        Domain.Entities.Platform GetById(object id);
        IEnumerable<Platform> GetAll();
        IEnumerable<Platform> Where(Expression<Func<Platform, bool>> where);

        #region Asyncs

        Task InsertAsync(Platform entity);
        Task<Platform> GetByIdAsync(object id);
        Task<IEnumerable<Platform>> GetAllAsync();
        Task<Platform> GetAsync(Expression<Func<Platform, bool>> where);
        Task<ICollection<Platform>> GetAllAsync(Expression<Func<Platform, bool>> match);
        Task<int> CountAsync();

        #endregion  
    }
}
