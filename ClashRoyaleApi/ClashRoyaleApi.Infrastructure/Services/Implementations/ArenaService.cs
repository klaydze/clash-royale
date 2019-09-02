using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClashRoyaleApi.Core.Contracts;
using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Models;
using ClashRoyaleApi.Infrastructure.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Infrastructure.Services.Implementations
{
    public class ArenaService : IArenaService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IConfigurationProvider _mappingConfiguration;

        public ArenaService(IRepositoryWrapper repositoryWrapper,
            IConfigurationProvider mappingConfiguration)
        {
            _repositoryWrapper = repositoryWrapper;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<int> CreateArenaAsync(ArenaEntity entity)
        {
            await _repositoryWrapper.Arena.CreateAsync(entity);
            await _repositoryWrapper.Arena.SaveAsync();

            return entity.Id;
        }

        public async Task DeleteArenaAsync(int id)
        {
            var arenaEntity = await _repositoryWrapper.Arena.GetByIdAsync(id);

            if (arenaEntity == null)
                return;

            _repositoryWrapper.Arena.Delete(arenaEntity);
            await _repositoryWrapper.Arena.SaveAsync();
        }

        public async Task<PagedResults<Arena>> GetArenas(PagingOptions pagingOptions, SortOptions<Arena, ArenaEntity> sortOptions)
        {
            var query = _repositoryWrapper.Arena.GetAll();

            query = sortOptions.Apply(query);

            var totalSize = await query.CountAsync();
            var items = await query
                        .Skip(pagingOptions.Offset.Value)
                        .Take(pagingOptions.Limit.Value)
                        .ProjectTo<Arena>(_mappingConfiguration)
                        .ToListAsync();

            return new PagedResults<Arena>
            {
                Items = items,
                TotalSize = totalSize
            };
        }

        public async Task<PagedResults<Arena>> GetArenas(SortOptions<Arena, ArenaEntity> sortOptions)
        {
            var query = _repositoryWrapper.Arena.GetAll();

            query = sortOptions.Apply(query);

            var totalSize = await query.CountAsync();
            var items = await query
                        .ProjectTo<Arena>(_mappingConfiguration)
                        .ToListAsync();

            return new PagedResults<Arena>
            {
                Items = items,
                TotalSize = totalSize
            };
        }

        public async Task<Arena> GetArenaByIdAsync(int id)
        {
            var arenaEntity = await _repositoryWrapper.Arena.GetByIdAsync(id);

            if (arenaEntity == null)
                return null;

            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<Arena>(arenaEntity);
        }

        public async Task<IEnumerable<Card>> GetUnlockCardsByArenaIdAsync(int arenaId,
            SortOptions<Card, CardEntity> sortOptions)
        {
            var cardIds = _repositoryWrapper.CardsUnlockPerArena.GetAll()
                            .Where(arena => arena.ArenaId == arenaId)
                            .Select(c => c.CardId);

            if (cardIds.Any())
            {
                var cards = from card in _repositoryWrapper.Card.GetAll()
                            where cardIds.Contains(card.Id)
                            select card;

                cards = sortOptions.Apply(cards);

                return await cards
                            .ProjectTo<Card>(_mappingConfiguration)
                            .ToListAsync();
            }

            return null;
        }

        public async Task<IEnumerable<Chest>> GetUnlockChestsByArenaIdAsync(int arenaId, 
            SortOptions<Chest, ChestEntity> sortOptions)
        {
            var chestIds = _repositoryWrapper.ChestsUnlockPerArena.GetAll()
                .Where(chest => chest.ArenaId == arenaId)
                .Select(c => c.ChestId);

            if (chestIds.Any())
            {
                var chests = from chest in _repositoryWrapper.Chest.GetAll()
                             where chestIds.Contains(chest.Id)
                             select chest;

                chests = sortOptions.Apply(chests);

                return await chests
                            .ProjectTo<Chest>(_mappingConfiguration)
                            .ToListAsync();
            }

            return null;
        }

        public async Task UpdateArenaAsync(ArenaEntity entity)
        {
            _repositoryWrapper.Arena.Update(entity);

            await _repositoryWrapper.Arena.SaveAsync();
        }
    }
}
