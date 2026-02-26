namespace YuGiOhDeckApi.Models
{
    public class Card
    {
        public string? CardId { get; set; }
        public string? CardName { get; set; }
        public string? Type { get; set; }
        public string? FrameType { get; set; }
        public string? Effect { get; set; }
        public int AttackPoints { get; set; }
        public int DefensePoints { get; set; }
        public int Level { get; set; }
        public string? Race { get; set; }
        public string? Attribute { get; set; }
        public int CardCount { get; set; } = 0;
        public string? Image { get; set; }
    }
}
