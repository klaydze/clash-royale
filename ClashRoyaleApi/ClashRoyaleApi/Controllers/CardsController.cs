using AutoMapper;
using ClashRoyaleApi.Core.Entities;
using ClashRoyaleApi.Infrastructure.Models;
using ClashRoyaleApi.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ClashRoyaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICardService _cardService;
        private readonly LinkGenerator _linkGenerator;
        private readonly PagingOptions _defaultPagingOptions;

        public CardsController(IMapper mapper,
                                ICardService cardService,
                                IOptions<PagingOptions> defaultPagingOptions,
                                LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _cardService = cardService;
            _defaultPagingOptions = defaultPagingOptions.Value;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = nameof(GetAllCards))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<PagedResults<Card>> GetAllCards(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<Card, CardEntity> sortOptions,
            [FromQuery] SearchOptions<Card, CardEntity> searchOptions)
        {
            // pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            // pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            if (pagingOptions.Limit == null &&
                pagingOptions.Offset == null)
            {
                return await _cardService.GetCardsAsync(sortOptions, searchOptions);
            }

            return await _cardService.GetCardsAsync(pagingOptions, sortOptions, searchOptions);
        }

        [HttpGet("{cardId}", Name = nameof(GetCardById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Card>> GetCardById(int cardId)
        {
            var usr = User;

            Card card = await _cardService.GetCardByIdAsync(cardId);

            if (card == null)
                return NotFound(new ApiError($"Card not found!"));

            return Ok(card);
        }

        [HttpPost(Name = nameof(CreateNewCard))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "ReadWritePolicy")]
        public async Task<IActionResult> CreateNewCard([FromBody] CardEntity cardEntity)
        {
            await _cardService.CreateCardAsync(cardEntity);

            //var location = _linkGenerator.GetUriByAction(nameof(GetCardById), "Cards",
            //                                                new { id = cardEntity.Id },
            //                                                HttpContext.Request.Scheme,
            //                                                HttpContext.Request.Host);

            var location = "https://localhost:5001";

            return Created(location, cardEntity);
        }

        [HttpPut("{cardId}", Name = nameof(UpdateCard))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "ReadWritePolicy")]
        public async Task<IActionResult> UpdateCard(int? cardId, [FromBody] CardEntity cardEntity)
        {
            if (cardId != cardEntity.Id)
                return BadRequest(new ApiError("Can't update the record!"));

            await _cardService.UpdateCardAsync(cardEntity);

            // TODO Do we need to check the existence of card before we update?

            return NoContent();
        }

        [HttpDelete("{id}", Name = nameof(DeleteCard))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
                return NotFound();

            await _cardService.DeleteCardAsync(card.Id);

            return NoContent();
        }
    }
}