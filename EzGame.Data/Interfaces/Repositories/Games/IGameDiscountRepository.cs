using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories.Games
{
   public interface IGameDiscountRepository:IDisposable
    {
        void Insert(GameDiscount entity);
        IEnumerable<GameDiscount> Take(int count);
        void Update(GameDiscount entity);
        void Delete(object id);
        void Delete(GameDiscount entity);
        void Delete(Expression<Func<GameDiscount, bool>> where);
        Domain.Entities.GameDiscount GetById(object id);
        IEnumerable<GameDiscount> GetAll();
        IEnumerable<GameDiscount> Where(Expression<Func<GameDiscount, bool>> where);

        #region Asyncs

        Task InsertAsync(GameDiscount entity);
        Task<GameDiscount> GetByIdAsync(object id);
        Task<IEnumerable<GameDiscount>> GetAllAsync();
        Task<GameDiscount> GetAsync(Expression<Func<GameDiscount, bool>> where);
        Task<ICollection<GameDiscount>> GetAllAsync(Expression<Func<GameDiscount, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
