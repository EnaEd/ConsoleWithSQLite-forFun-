using SQLite;

namespace TimerProject.Models.SQLModels
{
    public class TimeWork
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        [MaxLength(30), NotNull]
        public string DateStart { get; set; }

        [MaxLength(30), NotNull]
        public string DateEnd { get; set; }

        [NotNull]
        public int IdProject { get; set; }
    }
}
