using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories.Games
{
   public interface IGamePlatformRepository:IDisposable
    {
        Task DeleteAllRelations(object id);
        void Insert(GamePlatform entity);
        IEnumerable<GamePlatform> Take(int count);
        void Update(GamePlatform entity);
        void Delete(object id);
        void Delete(GamePlatform entity);
        void Delete(Expression<Func<GamePlatform, bool>> where);
        Domain.Entities.GamePlatform GetById(object id);
        IEnumerable<GamePlatform> GetAll();
        IEnumerable<GamePlatform> Where(Expression<Func<GamePlatform, bool>> where);

        #region Asyncs

        Task InsertAsync(GamePlatform entity);
        Task<GamePlatform> GetByIdAsync(object id);
        Task<IEnumerable<GamePlatform>> GetAllAsync();
        Task<GamePlatform> GetAsync(Expression<Func<GamePlatform, bool>> where);
        Task<ICollection<GamePlatform>> GetAllAsync(Expression<Func<GamePlatform, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
