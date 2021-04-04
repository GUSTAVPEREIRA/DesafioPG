namespace DesafioPG.DTO.Marvel
{
    public class CharacterDataContainerDTO
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public CharacterDTO[] Results { get; set; }
    }
}