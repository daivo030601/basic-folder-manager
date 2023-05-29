using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.EfCore
{
    [Table("folder")]
    public class Folder
    {

        public int Id { get; set; }
        public int ParentId { get; set; }
        public virtual List<Question> Questions { get; set; }
        public string Name { get; set; } = string.Empty;

        /*public Folder(int parentId, string name) 
        {
            this.ParentId = parentId;
            this.Name = name;
        }*/
    }
}
