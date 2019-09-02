using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Models;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Infrastructure.Services.Contracts
{
    public interface ICardService
    {
        /// <summary>
        /// Create new card information.
        /// </summary>
        /// <param name="entity">Card information to add</param>
        /// <returns></returns>
        Task<int> CreateCardAsync(CardEntity entity);

        /// <summary>
        /// Update card information.
        /// </summary>
        /// <param name="entity">New card information to save</param>
        /// <returns></returns>
        Task UpdateCardAsync(CardEntity entity);

        /// <summary>
        /// Delete card information from the collection.
        /// </summary>
        /// <param name="entity">Card information to delete from the collection</param>
        /// <returns></returns>
        Task DeleteCardAsync(int id);

        /// <summary>
        /// Retrieve card informataion based on its id.
        /// </summary>
        /// <param name="id">Card id to retrieve</param>
        /// <returns></returns>
        Task<Card> GetCardByIdAsync(int id);

        /// <summary>
        /// Retrieve all card information from the collection.
        /// </summary>
        /// <param name="pagingOptions">Pagination options.</param>
        /// <param name="sortOptions">Sort options.</param>
        /// <returns></returns>
        Task<PagedResults<Card>> GetCardsAsync(PagingOptions pagingOptions, 
            SortOptions<Card, CardEntity> sortOptions,
            SearchOptions<Card, CardEntity> searchOptions);

        /// <summary>
        /// Retrieve all card information from the collection.
        /// </summary>
        /// <param name="sortOptions"></param>
        /// <returns></returns>
        Task<PagedResults<Card>> GetCardsAsync(SortOptions<Card, CardEntity> sortOptions,
            SearchOptions<Card, CardEntity> searchOptions);
    }
}
