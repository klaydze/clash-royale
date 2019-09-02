using ClashRoyaleApi.Core.Entities;

namespace ClashRoyaleApi.Core.Contracts.Repository
{
    public interface ICardRepository 
        : IRepositoryBase<CardEntity>
    {
        /*Task<CardEntity> GetCardByIdAsync(int id);
        Task CreateCardAsync(CardEntity cardEntity);
        Task UpdateCardAsync(CardEntity cardEntity);
        Task DeleteCardAsync(CardEntity cardEntity);*/
    }
}
