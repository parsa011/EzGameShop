using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories.Games
{
   public interface IGameEditionRepository:IDisposable
    {
        void Insert(GameEdition entity);
        IEnumerable<GameEdition> Take(int count);
        void Update(GameEdition entity);
        void Delete(object id);
        void Delete(GameEdition entity);
        void Delete(Expression<Func<GameEdition, bool>> where);
        Domain.Entities.GameEdition GetById(object id);
        IEnumerable<GameEdition> GetAll();
        IEnumerable<GameEdition> Where(Expression<Func<GameEdition, bool>> where);

        #region Asyncs

        Task InsertAsync(GameEdition entity);
        Task<GameEdition> GetByIdAsync(object id);
        Task<IEnumerable<GameEdition>> GetAllAsync();
        Task<GameEdition> GetAsync(Expression<Func<GameEdition, bool>> where);
        Task<ICollection<GameEdition>> GetAllAsync(Expression<Func<GameEdition, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
