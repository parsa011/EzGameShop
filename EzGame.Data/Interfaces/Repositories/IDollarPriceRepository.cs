using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories
{
   public interface IDollarPriceRepository:IDisposable
    {
        IEnumerable<DollarPrice> GetOrdered();
        Task<DollarPrice> GetLastPrice();
        void Insert(DollarPrice entity);
        IEnumerable<DollarPrice> Take(int count);
        void Update(DollarPrice entity);
        void Delete(object id);
        void Delete(DollarPrice entity);
        void Delete(Expression<Func<DollarPrice, bool>> where);
        DollarPrice GetById(object id);
        IEnumerable<DollarPrice> GetAll();
        IEnumerable<DollarPrice> Where(Expression<Func<DollarPrice, bool>> where);

        #region Asyncs

        Task InsertAsync(DollarPrice entity);
        Task<DollarPrice> GetByIdAsync(object id);
        Task<IEnumerable<DollarPrice>> GetAllAsync();
        Task<DollarPrice> GetAsync(Expression<Func<DollarPrice, bool>> where);
        Task<ICollection<DollarPrice>> GetAllAsync(Expression<Func<DollarPrice, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
