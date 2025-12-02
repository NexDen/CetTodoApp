using SQLite;
namespace CetTodoApp.Data;

public class ToDoDB
{
    private SQLiteAsyncConnection? _db;

    private async Task InitAsync()
    {
        if (_db is not null)
        {
            return;
        }

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tododatabase.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        await _db.CreateTableAsync<TodoItem>();
    }

    public async Task Create(TodoItem item)
    {
        await InitAsync();
        await _db!.InsertAsync(item);
    }

    public async Task Update(TodoItem item)
    {
        await InitAsync();
        await _db!.UpdateAsync(item);
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        await InitAsync();
        return await _db!.Table<TodoItem>().OrderByDescending(t => t.DueDate).ToListAsync();
    }

}