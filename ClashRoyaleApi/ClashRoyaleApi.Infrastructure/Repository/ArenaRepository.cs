using ClashRoyaleApi.Core.Contracts.Repository;
using ClashRoyaleApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Repository
{
    public class ArenaRepository : RepositoryBase<ArenaEntity>
    {
        public ArenaRepository(ClashRoyaleContext context)
            : base(context)
        {

        }
    }
}
