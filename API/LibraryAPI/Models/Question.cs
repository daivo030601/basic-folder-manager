using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.EfCore
{
    [Table("question")]
    public class Question
    {
        public int Id { get; set; }
        public int? FolderId { get; set; }
        public Folder Folder { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
