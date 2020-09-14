using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EzGame.Domain.Entities;
using System.Threading.Tasks;

namespace EzGame.Data.Interfaces.Repositories.Games
{
    public interface IGameRepository : IDisposable
    {
        void Insert(Game entity);
        IEnumerable<Game> Take(int count);
        void Update(Game entity);
        void Delete(object id);
        void Delete(Game entity);
        void Delete(Expression<Func<Game, bool>> where);
        Game GetById(object id);
        IEnumerable<Game> Paging(int take, int pageid, IEnumerable<Game> games);
        IEnumerable<Game> Paging(int take, int pageid);
        IEnumerable<Game> GetAll();
        IEnumerable<Game> Where(Expression<Func<Game, bool>> where);

        #region Asyncs

        Task InsertAsync(Game entity);

        Task<Game> GetByIdAsync(object id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetAsync(Expression<Func<Game, bool>> where);
        Task<ICollection<Game>> GetAllAsync(Expression<Func<Game, bool>> match);
        Task<int> CountAsync();

        #endregion
    }
}