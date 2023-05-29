namespace LibraryAPI.Data
{
    public class FolderTreeModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ParentId { get; set; }

        public ICollection<FolderTreeModel>? Children { get; set; } = new List<FolderTreeModel>();
    }
}
