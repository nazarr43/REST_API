using _Rest_Api_.Models;

namespace _Rest_Api_.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Character>> GetCharacterList();
        Task<List<Character>> AddCharacters(Character newCharacter);
    }
}
