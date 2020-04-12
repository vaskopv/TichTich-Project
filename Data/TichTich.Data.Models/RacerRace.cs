namespace TichTich.Data.Models
{
    public class RacerRace
    {
        public string RacerId { get; set; }

        public ApplicationUser Racer { get; set; }

        public int RaceId { get; set; }

        public Race Race { get; set; }
    }
}
