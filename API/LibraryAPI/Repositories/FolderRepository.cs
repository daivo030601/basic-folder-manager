using LibraryAPI.Data;
using LibraryAPI.EfCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly EF_DataContext _context;

        public FolderRepository(EF_DataContext context)
        {
            _context = context;
        }
        public async Task AddFolder(string name, int parentId)
        {
            Folder dbTable = new Folder();
            dbTable.Name = name;
            dbTable.ParentId = parentId;
            _context.Folders.Add(dbTable);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFolder(int id)
        {
            var folder = await _context.Folders.Where(q => q.Id.Equals(id)).FirstOrDefaultAsync();
            if (folder != null)
            {
                _context.Folders.Remove(folder);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<FolderTreeModel>> GetFolderChildren(ICollection<FolderTreeModel> allFolders, FolderTreeModel folder)
        {
            if (allFolders.All(f => f.ParentId != folder.Id)) return new List<FolderTreeModel> ();

            folder.Children = allFolders
                .Where(b => b.ParentId == folder.Id)
                .ToList();
            foreach (var item in folder.Children)
            {
                item.Children = await GetFolderChildren(allFolders, item);
            }

            return folder.Children;
        }

        public async Task<List<FolderModel>> GetFolders(int parentFolderId)
        {
            List<FolderModel> response = new List<FolderModel>();
            var dataList = await _context.Folders
                .Where(f => f.ParentId == parentFolderId)
                .ToListAsync();
            dataList.ForEach(row => response.Add(new FolderModel()
            {
                Id = row.Id,
                Name = row.Name,
                ParentId = row.ParentId,
            }));
            return response;
        }

        public async Task<ICollection<FolderTreeModel>> GetAllFolders()
        {
            ICollection<FolderTreeModel> response = new List<FolderTreeModel>();
            var dataList = await _context.Folders
                .ToListAsync();
            dataList.ForEach(row => response.Add(new FolderTreeModel()
            {
                Id = row.Id,
                Name = row.Name,
                ParentId = row.ParentId,
            }));
            return response;
        }

        public async Task<List<FolderTreeModel>> GetFolderTree()
        {
            ICollection<FolderTreeModel> folders = await GetAllFolders();
            foreach (var folder in folders)
            {
                folder.Children = await GetFolderChildren(folders, folder);
            }
            return folders.Where(f => f.ParentId == 0).ToList();
        }

        public async Task UpdateFolder(FolderModel folderModel)
        {
            var dbTable = await _context.Folders.Where(q => q.Id.Equals(folderModel.Id)).FirstOrDefaultAsync();
            if (dbTable != null)
            {
                dbTable.Name = folderModel.Name;
            }
            await _context.SaveChangesAsync();
        }
    }
}
