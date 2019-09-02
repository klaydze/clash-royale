using System.Collections.Generic;
using System.Threading.Tasks;
using ClashRoyaleApi.Core.Contracts.Repository;
using ClashRoyaleApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClashRoyaleApi.Infrastructure.Repository
{
    public class CardRepository : RepositoryBase<CardEntity>, ICardRepository
    {
        private readonly ClashRoyaleContext _context;

        public CardRepository(ClashRoyaleContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
