namespace LibraryAPI.Data
{
    public class FolderModel
    {
        public int? Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
