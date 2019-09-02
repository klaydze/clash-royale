using ClashRoyaleApi.Core.Contracts;
using ClashRoyaleApi.Core.Contracts.Repository;
using ClashRoyaleApi.Core.Entities;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Infrastructure.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ClashRoyaleContext _context;
        // private ICardRepository cardRepository;

        private IRepositoryBase<CardEntity> _cardRepository;
        private IRepositoryBase<ArenaEntity> _arenaRepository;
        private IRepositoryBase<ChestEntity> _chestRepository;
        private IRepositoryBase<LeagueEntity> _leagueRepository;
        private IRepositoryBase<CardsUnlockPerArenaEntity> _cardsUnlockPerArenaRepository;
        private IRepositoryBase<ChestsUnlockPerArenaEntity> _chestsUnlockPerArenaRepository;

        public RepositoryWrapper(ClashRoyaleContext context)
        {
            _context = context;
        }

        public IRepositoryBase<CardEntity> Card
            => _cardRepository ?? new RepositoryBase<CardEntity>(_context);

        public IRepositoryBase<ArenaEntity> Arena 
            => _arenaRepository ?? new RepositoryBase<ArenaEntity>(_context);

        public IRepositoryBase<ChestEntity> Chest
            => _chestRepository ?? new RepositoryBase<ChestEntity>(_context);

        public IRepositoryBase<LeagueEntity> League
            => _leagueRepository ?? new RepositoryBase<LeagueEntity>(_context);

        public IRepositoryBase<CardsUnlockPerArenaEntity> CardsUnlockPerArena
            => _cardsUnlockPerArenaRepository ?? new RepositoryBase<CardsUnlockPerArenaEntity>(_context);

        public IRepositoryBase<ChestsUnlockPerArenaEntity> ChestsUnlockPerArena
            => _chestsUnlockPerArenaRepository ?? new RepositoryBase<ChestsUnlockPerArenaEntity>(_context);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
