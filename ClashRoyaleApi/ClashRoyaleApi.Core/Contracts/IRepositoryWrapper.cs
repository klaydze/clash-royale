using ClashRoyaleApi.Core.Contracts.Repository;
using ClashRoyaleApi.Core.Entities;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Core.Contracts
{
    public interface IRepositoryWrapper
    {
        // ICardRepository Card { get; }
        IRepositoryBase<CardEntity> Card { get; }
        IRepositoryBase<ArenaEntity> Arena { get; }
        IRepositoryBase<ChestEntity> Chest { get; }
        IRepositoryBase<LeagueEntity> League { get; }
        IRepositoryBase<CardsUnlockPerArenaEntity> CardsUnlockPerArena { get; }
        IRepositoryBase<ChestsUnlockPerArenaEntity> ChestsUnlockPerArena { get; }
        IRepositoryBase<CardStatisticsEntity> CardStatistics { get; }

        Task SaveAsync();
    }
}
