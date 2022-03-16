using System.Text.Json;
using Domain.Models;

namespace FileData.DataAccess;

public class FileContext
{
    private string todoFilePath = "forum.json";

    private ICollection<Forum>? _forums;

    public ICollection<Forum> Forums
    {
        get
        {
            if (_forums == null)
            {
                LoadData();
            }

            return _forums!;
        }
    }

    public FileContext()
    {
        if (!File.Exists(todoFilePath))
        {
            Seed();
        }
    }

    private void Seed()
    {
        
        
        SaveChanges();
    }

    public void SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Forums);
        File.WriteAllText(todoFilePath, serialize);
        _forums = null;
    }

    private void LoadData()
    {
        string content = File.ReadAllText(todoFilePath);
        _forums = JsonSerializer.Deserialize<List<Forum>>(content);
    }
}