using System.Text.Json;

public class JsonDataAccess<T> where T : class
{
    private readonly string _filePath;

    public JsonDataAccess(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<T>> GetAllAsync()
    {
        using (StreamReader reader = new StreamReader(_filePath))
        {
            string json = await reader.ReadToEndAsync();
            List<T>? items = JsonSerializer.Deserialize<List<T>>(json);
            return items ?? new List<T>();
        }
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        List<T> items = await GetAllAsync();
        T? item = items.Find(x => x.GetType().GetProperty("Id")?.GetValue(x)?.ToString() == id.ToString());
        return item;
    }


    public async Task AddAsync(T item)
    {
        List<T> items = await GetAllAsync();
        int? maxId = items.Max(x => (int?)x.GetType().GetProperty("Id")?.GetValue(x)) ?? 0;
        int newId = (int)(maxId + 1);
        var itemType = item.GetType();
        var itemIdProperty = itemType.GetProperty("Id");
        if (itemIdProperty != null)
        {
            itemIdProperty.SetValue(item, newId);
            items.Add(item);
            await WriteToFileAsync(items);
        }
    }

    public async Task UpdateAsync(T item)
    {
        List<T> items = await GetAllAsync();
        var itemIdProperty = item.GetType().GetProperty("Id");
        if (itemIdProperty != null)
        {
            string itemId = itemIdProperty.GetValue(item)?.ToString() ?? string.Empty;
            T? existingItem = items.Find(x => x.GetType().GetProperty("Id")?.GetValue(x)?.ToString() == itemId);
            if (existingItem != null)
            {
                int index = items.IndexOf(existingItem);
                items[index] = item;
                await WriteToFileAsync(items);
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        List<T> items = await GetAllAsync();
        T? existingItem = items.Find(x => x.GetType().GetProperty("Id")?.GetValue(x)?.ToString() == id.ToString());
        if (existingItem != null)
        {
            items.Remove(existingItem);
            await WriteToFileAsync(items);
        }
    }

    private async Task WriteToFileAsync(List<T> items)
    {
        using (StreamWriter writer = new StreamWriter(_filePath, false))
        {
            string json = JsonSerializer.Serialize<List<T>>(items, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            await writer.WriteAsync(json);
        }
    }
}
