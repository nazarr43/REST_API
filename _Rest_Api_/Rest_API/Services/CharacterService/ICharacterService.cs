using Rest_API.Models;

namespace Rest_API.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Character>> GetCharacterList();
        Task<List<Character>> AddCharacters(Character newCharacter);

    }
}
