using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories.Games
{
    public interface IGameVisitRepository:IDisposable
    {
        void Insert(GameVisit entity);
        IEnumerable<GameVisit> Take(int count);
        void Update(GameVisit entity);
        void Delete(object id);
        void Delete(GameVisit entity);
        void Delete(Expression<Func<GameVisit, bool>> where);
        Domain.Entities.GameVisit GetById(object id);
        IEnumerable<GameVisit> GetAll();
        IEnumerable<GameVisit> Where(Expression<Func<GameVisit, bool>> where);

        #region Asyncs

        Task InsertAsync(GameVisit entity);

        Task<GameVisit> GetByIdAsync(object id);
        Task<IEnumerable<GameVisit>> GetAllAsync();
        Task<GameVisit> GetAsync(Expression<Func<GameVisit, bool>> where);
        Task<ICollection<GameVisit>> GetAllAsync(Expression<Func<GameVisit, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
