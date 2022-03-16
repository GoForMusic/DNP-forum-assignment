using Domain.Contracts;
using Domain.Models;

namespace FileData.DataAccess;

public class FileDataDAO : IForumService
{
    private FileContext _fileContext;
    
    public FileDataDAO(FileContext fileContext)
    {
        _fileContext = fileContext;
    }
    
    public Task<ICollection<Forum>> GetForumAsync()
    {
        throw new NotImplementedException();
    }
}