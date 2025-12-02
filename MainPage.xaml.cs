using CetTodoApp.Data;

namespace CetTodoApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        RefreshListView();
    }
    private ToDoDB db = new ToDoDB();



    private async void AddButton_OnClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("HATA", "Başlık boş olamaz!", "OK");
            return;
        }

        if (DueDate.Date < DateTime.Today)
        {
            await DisplayAlert("HATA", "Bitiş tarihi bugüden önce olamaz!", "OK");
            return;
        }

        await db.Create(new TodoItem{
            Title = TitleEntry.Text,
            DueDate = DueDate.Date
        });
        TitleEntry.Text = string.Empty;
        DueDate.Date=DateTime.Now;
        RefreshListView();
    }

    private async void RefreshListView()
    {
        TasksListView.ItemsSource = null;
        TasksListView.ItemsSource = await db.GetAllAsync();
    }

    private async void TasksListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        var item = e.SelectedItem as TodoItem;
        item.IsComplete = !item.IsComplete;
        await db.Update(item);
        RefreshListView();
       
    }
}