using ClashRoyaleApi.Core.Contracts;
using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Models;
using ClashRoyaleApi.Infrastructure.Services.Contracts;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ClashRoyaleApi.Infrastructure.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IConfigurationProvider _mappingConfiguration;

        public CardService(IRepositoryWrapper repositoryWrapper,
                            IConfigurationProvider mappingConfiguration)
        {
            _repositoryWrapper = repositoryWrapper;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<int> CreateCardAsync(CardEntity entity)
        {
            await _repositoryWrapper.Card.CreateAsync(entity);
            await _repositoryWrapper.Card.SaveAsync();

            return entity.Id;
        }

        public async Task UpdateCardAsync(CardEntity entity)
        {
            _repositoryWrapper.Card.Update(entity);

            await _repositoryWrapper.Card.SaveAsync();
        }

        public async Task DeleteCardAsync(int id)
        {
            var cardEntity = await _repositoryWrapper.Card.GetByIdAsync(id);

            if (cardEntity == null)
                return;

            _repositoryWrapper.Card.Delete(cardEntity);
            await _repositoryWrapper.Card.SaveAsync();
        }

        public async Task<PagedResults<Card>> GetCardsAsync(PagingOptions pagingOptions, 
            SortOptions<Card, CardEntity> sortOptions,
            SearchOptions<Card, CardEntity> searchOptions)
        {
            var query = _repositoryWrapper.Card.GetAll();

            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var totalSize = await query.CountAsync();
            var items = await query
                        .Skip(pagingOptions.Offset.Value)
                        .Take(pagingOptions.Limit.Value)
                        .ProjectTo<Card>(_mappingConfiguration)
                        .ToListAsync();

            return new PagedResults<Card>
            {
                Items = items,
                TotalSize = totalSize
            };
        }

        public async Task<PagedResults<Card>> GetCardsAsync(SortOptions<Card, CardEntity> sortOptions,
            SearchOptions<Card, CardEntity> searchOptions)
        {
            var query = _repositoryWrapper.Card.GetAll(x => x.CardStatistics);

            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var totalSize = await query.CountAsync();
            var items = await query
                        .ProjectTo<Card>(_mappingConfiguration)
                        .ToListAsync();

            return new PagedResults<Card>
            {
                Items = items,
                TotalSize = totalSize
            };
        }

        public async Task<Card> GetCardByIdAsync(int id)
        {
            var cardEntity = await _repositoryWrapper.Card.GetByIdAsync(id, cs => cs.CardStatistics,
                                                                                        a => a.Arena);

            if (cardEntity == null)
                return null;

            var mapper = _mappingConfiguration.CreateMapper();

            return mapper.Map<Card>(cardEntity);
        }
    }
}
