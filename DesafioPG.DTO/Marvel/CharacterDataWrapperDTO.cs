namespace DesafioPG.DTO.Marvel
{
    public class CharacterDataWrapperDTO
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public string Etag { get; set; }
        public CharacterDataContainerDTO Data { get; set; }
    }
}