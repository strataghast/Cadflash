using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace cadflash.Models
{
    [Table("Flashcard")]
    public class Flashcard
    {
        [PrimaryKey]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Question")]
        public string Question { get; set; }

        [Column("Answer")]
        public string Answer { get; set; }

        [Column("Difficulty")]
        public string Difficulty { get; set; }
    }
}
