using CetTodoApp.Data;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CetTodoApp;

public partial class MainPage : ContentPage
{
   

    public MainPage()
    {
        InitializeComponent();
        FakeDb.AddToDo("dün" ,DateTime.Now.AddDays(-1));
        FakeDb.AddToDo("yarın" ,DateTime.Now.AddDays(1));
        FakeDb.AddToDo("bugün" ,DateTime.Now);
        RefreshListView();
    }


    private void AddButton_OnClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            DisplayAlert("HATA", "Başlık boş olamaz!", "OK");
            return;
        }

        if (DueDate.Date < DateTime.Today)
        {
            DisplayAlert("HATA", "Bitiş tarihi bugüden önce olamaz!", "OK");
            return;
        }

        FakeDb.AddToDo(TitleEntry.Text, DueDate.Date);
        TitleEntry.Text = string.Empty;
        DueDate.Date=DateTime.Now;
        RefreshListView();
    }

    private void RefreshListView()
    {
        TasksListView.ItemsSource = null;
        TasksListView.ItemsSource = FakeDb.Data.Where(x => !x.IsComplete ||
                                                           (x.IsComplete && x.DueDate > DateTime.Now.AddDays(-1)))
            .ToList();

        
    }

    private void TasksListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        var item = e.SelectedItem as TodoItem;
       FakeDb.ChageCompletionStatus(item);
       RefreshListView();
       
    }
}