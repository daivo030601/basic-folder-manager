using LibraryAPI.Data;

namespace LibraryAPI.Repositories
{
    public interface IFolderRepository
    {
        public Task<List<FolderModel>> GetFolders(int parentFolderId);
        public Task AddFolder(string name, int parentId);
        public Task UpdateFolder(FolderModel folderModel);
        public Task DeleteFolder(int id);
        public Task<ICollection<FolderTreeModel>> GetAllFolders();
        public Task<List<FolderTreeModel>> GetFolderTree();
        public Task<ICollection<FolderTreeModel>> GetFolderChildren(ICollection<FolderTreeModel> allFolders, FolderTreeModel folderTree);
    }
}
