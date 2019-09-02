using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Infrastructure.Services.Contracts
{
    public interface IArenaService
    {
        /// <summary>
        /// Create new arena information.
        /// </summary>
        /// <param name="entity">Arena information to add</param>
        /// <returns></returns>
        Task<int> CreateArenaAsync(ArenaEntity entity);

        /// <summary>
        /// Update arena information.
        /// </summary>
        /// <param name="entity">New arena information to save</param>
        /// <returns></returns>
        Task UpdateArenaAsync(ArenaEntity entity);

        /// <summary>
        /// Delete arena information from the collection.
        /// </summary>
        /// <param name="entity">Arena information to delete from the collection</param>
        /// <returns></returns>
        Task DeleteArenaAsync(int id);

        /// <summary>
        /// Retrieve arena informataion based on its id.
        /// </summary>
        /// <param name="id">Arena id to retrieve</param>
        /// <returns></returns>
        Task<Arena> GetArenaByIdAsync(int id);

        /// <summary>
        /// Retrieve all arena information from the collection.
        /// </summary>
        /// <param name="pagingOptions">Pagination options.</param>
        /// <param name="sortOptions">Sort options.</param>
        /// <returns></returns>
        Task<PagedResults<Arena>> GetArenas(PagingOptions pagingOptions, SortOptions<Arena, ArenaEntity> sortOptions);

        /// <summary>
        /// Retrieve all arena information from the collection.
        /// </summary>
        /// <param name="sortOptions"></param>
        /// <returns></returns>
        Task<PagedResults<Arena>> GetArenas(SortOptions<Arena, ArenaEntity> sortOptions);

        /// <summary>
        /// Retrieve all cards that can be unlock in the said arena.
        /// </summary>
        /// <param name="arenaId"></param>
        /// <param name="sortOptions"></param>
        /// <returns></returns>
        Task<IEnumerable<Card>> GetUnlockCardsByArenaIdAsync(int arenaId, SortOptions<Card, CardEntity> sortOptions);

        /// <summary>
        /// Retrieve all chests that can be unlock in the said arena.
        /// </summary>
        /// <param name="arenaId"></param>
        /// <param name="sortOptions"></param>
        /// <returns></returns>
        Task<IEnumerable<Chest>> GetUnlockChestsByArenaIdAsync(int arenaId, SortOptions<Chest, ChestEntity> sortOptions);
    }
}
