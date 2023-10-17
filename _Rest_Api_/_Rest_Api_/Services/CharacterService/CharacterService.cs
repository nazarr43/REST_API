using _Rest_Api_.Models;

namespace _Rest_Api_.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character {Name = "henry", Color = "cl", Tail_length = 4, Weight = 66}
        };
        public async Task<List<Character>> AddCharacters(Character newcharacter)
        {
            characters.Add(newcharacter);
            return characters;
        }

        public async Task<List<Character>> GetCharacterList()
        {
            return characters;
        }

    }
}
