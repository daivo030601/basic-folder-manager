namespace LibraryAPI.Model
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Answer { get; set; } = string.Empty;

        public int? FolderId { get; set; }
    }
}
