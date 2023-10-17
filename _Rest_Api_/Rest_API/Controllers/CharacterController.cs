using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PagedList;
using Rest_API.Models;
using Rest_API.Services.CharacterService;

namespace Rest_API.Controllers
{
    [ApiController]
    [Route("")]
    [EnableRateLimiting("fixed")]
    public class CharacterController : ControllerBase
    {
        private ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return "Dogshouseservice.Version1.0.1";
        }

        [HttpGet("dogs")]
        public async Task<ActionResult<List<Character>>> GetDogs(string? attribute, string? order, int? page, int? size)
        {
            var characters = await _characterService.GetCharacterList();

            if (!string.IsNullOrEmpty(attribute) && !string.IsNullOrEmpty(order))
            {
                IOrderedEnumerable<Character> orderedCharacters;

                switch (attribute.ToLower())
                {
                    case "name":
                        orderedCharacters = order.ToLower() == "desc" ? characters.OrderByDescending(c => c.Name) : characters.OrderBy(c => c.Name);
                        break;
                    case "color":
                        orderedCharacters = order.ToLower() == "desc" ? characters.OrderByDescending(c => c.Color) : characters.OrderBy(c => c.Color);
                        break;
                    case "weight":
                        orderedCharacters = order.ToLower() == "desc" ? characters.OrderByDescending(c => c.Weight) : characters.OrderBy(c => c.Weight);
                        break;
                    case "length":
                        orderedCharacters = order.ToLower() == "desc" ? characters.OrderByDescending(c => c.Tail_length) : characters.OrderBy(c => c.Tail_length);
                        break;
                    default:
                        return BadRequest("Invalid attribute or order parameter.");
                }

                int pageNumber = page ?? 1;
                int pageSize = size ?? 10;
                IPagedList<Character> pagedCharacters = orderedCharacters.ToPagedList(pageNumber, pageSize);

                return Ok(pagedCharacters);
            }
            int defaultPageNumber = page ?? 1;
            int defaultPageSize = size ?? 10;
            IPagedList<Character> defaultPagedCharacters = characters.ToPagedList(defaultPageNumber, defaultPageSize);

            return Ok(defaultPagedCharacters);
        }

        [HttpPost("dog")]
        public async Task<ActionResult<List<Character>>> AddDogs(Character newcharacter)
        {
            var characters = await _characterService.GetCharacterList();

            if (characters.Any(c => c.Name == newcharacter.Name))
            {
                return BadRequest("A dog with the same name already exists.");
            }
            if (newcharacter.Tail_length < 0 || !int.TryParse(newcharacter.Tail_length.ToString(), out int n))
            {
                return BadRequest("Tail height is a negative number or is not a valid integer.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid JSON is passed in the request body.");
            }
            return Ok(await _characterService.AddDogs(newcharacter));
        }
    }
}
