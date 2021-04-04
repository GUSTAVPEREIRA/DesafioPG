namespace DesafioPG.DTO.Marvel
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CharacterListDTO Comics { get; set; }
        public CharacterListDTO Stories { get; set; }
        public CharacterListDTO Events { get; set; }
        public CharacterListDTO Series { get; set; }

        public CharacterDTO()
        {
            Name = "";
            Description = "";
        }
    }
}