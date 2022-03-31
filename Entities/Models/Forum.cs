namespace Entities.Models;

public class Forum
{
    public ICollection<SubForum> SubForums { get; set; }
    public ICollection<User> Users { get; set; }

    public Forum()
    {
        SubForums = new List<SubForum>();
        Users = new List<User>();
    }
}