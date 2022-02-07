using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Editor;

public partial class EditorWindow : Window
{
    private readonly DBContext _context = new();
    public EditorWindow()
    {
        InitializeComponent();
        
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "ID", Binding = new Binding("EnemyId")});
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "Nazwa", Binding = new Binding("Name")});
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "Sprite", Binding = new Binding("Sprite.AbsolutePath")});
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "Element", Binding = new Binding("Element.Name")});
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "Zdrowie", Binding = new Binding("Health")});
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "Obrażenia min", Binding = new Binding("DmgMin")});
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "Obrażenia max", Binding = new Binding("DmgMax")});
        EnemyTable.Columns.Add(new DataGridTextColumn(){Header = "Upuszczany przedmiot", Binding = new Binding("Item.Name")});
        EnemyTable.AutoGenerateColumns = false;
        _context.Enemies.Load();
        EnemyTable.ItemsSource = _context.Enemies.Local.ToObservableCollection();
        
        _context.Items.Load();
        ItemTable.Columns.Add(new DataGridTextColumn(){Header = "ID", Binding = new Binding("ItemId")});
        ItemTable.Columns.Add(new DataGridTextColumn(){Header = "Nazwa", Binding = new Binding("Name")});
        ItemTable.Columns.Add(new DataGridTextColumn(){Header = "Sprite", Binding = new Binding("SpriteUrl.AbsolutePath")});
        ItemTable.Columns.Add(new DataGridTextColumn(){Header = "Element", Binding = new Binding("Element.Name")});
        ItemTable.Columns.Add(new DataGridTextColumn(){Header = "Obrażenia min/Leczenie", Binding = new Binding("DmgMin")});
        ItemTable.Columns.Add(new DataGridTextColumn(){Header = "Obrażenia max/Leczenie", Binding = new Binding("DmgMax")});
        ItemTable.Columns.Add(new DataGridTextColumn(){Header = "Typ", Binding = new Binding("Type")});
        ItemTable.AutoGenerateColumns = false;
        ItemTable.ItemsSource = _context.Items.Local.ToObservableCollection();
        
        _context.Elements.Load();
        ElementTable.Columns.Add(new DataGridTextColumn(){Header = "ID", Binding = new Binding("ElementId")});
        ElementTable.Columns.Add(new DataGridTextColumn(){Header = "Nazwa", Binding = new Binding("Name")});
        ElementTable.Columns.Add(new DataGridTextColumn(){Header = "Sprite", Binding = new Binding("SpriteUrl.AbsolutePath")});
        ElementTable.Columns.Add(new DataGridTextColumn(){Header = "Słabe przeciw", Binding = new Binding("WeakToId")});
        ElementTable.Columns.Add(new DataGridTextColumn(){Header = "Mocne przeciw", Binding = new Binding("StrongToId")});
        ElementTable.AutoGenerateColumns = false;
        ElementTable.ItemsSource = _context.Items.Local.ToObservableCollection();
    }

    private void ButtonAddEnemy_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new AddEnemyWindow(_context.Elements.Local.ToObservableCollection(), _context.Items.Local.ToObservableCollection());
        if (dialog.ShowDialog() != true) return;
        _context.Enemies.Add(dialog.Enemy);
        _context.SaveChanges();
        EnemyTable.Items.Refresh();
    }

    private void ButtonEditEnemy_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new AddEnemyWindow(_context.Elements.Local.ToObservableCollection(),
            _context.Items.Local.ToObservableCollection(), (Enemy) EnemyTable.SelectedItem);
        if (dialog.ShowDialog() != true) return;
        _context.SaveChanges();
        EnemyTable.Items.Refresh();
    }

    private void ButtonDeleteEnemy_OnClick(object sender, RoutedEventArgs e)
    {
        _context.Enemies.Remove((Enemy) EnemyTable.SelectedItem);
        _context.SaveChanges();
        EnemyTable.Items.Refresh();
    }

    private void ButtonAddWpn_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}