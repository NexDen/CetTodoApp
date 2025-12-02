namespace CetTodoApp.Data;

public static class FakeDb
{
    public static List<TodoItem> Data = new List<TodoItem>();

    public static void AddToDo(TodoItem item )
    {
        Data.Add(item);
    }
}