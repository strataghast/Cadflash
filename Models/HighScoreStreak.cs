using SQLite;

namespace cadflash.Models
{
    public class HighScoreStreak
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int HighestStreak { get; set; }
    }
}
