using System.Threading.Tasks;
using AutoMapper;
using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Models;
using ClashRoyaleApi.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace ClashRoyaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArenasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IArenaService _arenaService;
        private readonly LinkGenerator _linkGenerator;
        private readonly PagingOptions _defaultPagingOptions;

        public ArenasController(IMapper mapper,
                        IArenaService arenaService,
                        IOptions<PagingOptions> defaultPagingOptions,
                        LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _arenaService = arenaService;
            _defaultPagingOptions = defaultPagingOptions.Value;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = nameof(GetAllArena))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<PagedResults<Arena>> GetAllArena(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<Arena, ArenaEntity> sortOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var arenas = await _arenaService.GetArenas(pagingOptions, sortOptions);

            return arenas;
        }


        [HttpGet("{arenaId}", Name = nameof(GetArenaById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Arena>> GetArenaById(int arenaId)
        {
            Arena arena = await _arenaService.GetArenaByIdAsync(arenaId);

            if (arena == null)
                return NotFound(new ApiError($"Arena not found!"));

            return Ok(arena);
        }

        [HttpGet("{arenaId}/cards", Name = nameof(GetCardsByArena))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCardsByArena(int arenaId,
            [FromQuery] SortOptions<Card, CardEntity> sortOptions)
        {
            Arena arena = await _arenaService.GetArenaByIdAsync(arenaId);

            if (arena == null)
                return NotFound(new ApiError($"Arena not found!"));

            var cards = await _arenaService.GetUnlockCardsByArenaIdAsync(arenaId, sortOptions);

            return Ok(cards);
        }

        [HttpGet("{arenaId}/chests", Name = nameof(GetChestsByArena))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetChestsByArena(int arenaId,
            [FromQuery] SortOptions<Chest, ChestEntity> sortOptions)
        {
            Arena arena = await _arenaService.GetArenaByIdAsync(arenaId);

            if (arena == null)
                return NotFound(new ApiError($"Arena not found!"));

            var chests = await _arenaService.GetUnlockChestsByArenaIdAsync(arenaId, sortOptions);

            return Ok(chests);
        }
    }
}