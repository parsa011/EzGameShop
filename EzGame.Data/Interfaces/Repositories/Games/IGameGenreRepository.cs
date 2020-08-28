using EzGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories.Games
{
   public interface IGameGenreRepository:IDisposable
    {
        Task DeleteAllRelations(object id);
        void Insert(GameGenre entity);
        IEnumerable<GameGenre> Take(int count);
        void Update(GameGenre entity);
        void Delete(object id);
        void Delete(GameGenre entity);
        void Delete(Expression<Func<GameGenre, bool>> where);
        Domain.Entities.GameGenre GetById(object id);
        IEnumerable<GameGenre> GetAll();
        IEnumerable<GameGenre> Where(Expression<Func<GameGenre, bool>> where);

        #region Asyncs

        Task InsertAsync(GameGenre entity);
        Task<GameGenre> GetByIdAsync(object id);
        Task<IEnumerable<GameGenre>> GetAllAsync();
        Task<GameGenre> GetAsync(Expression<Func<GameGenre, bool>> where);
        Task<ICollection<GameGenre>> GetAllAsync(Expression<Func<GameGenre, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}
