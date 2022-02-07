using System.Collections.Generic;
using System.Windows;
using project.Game;
using Item = project.Models.Item;

namespace project.Editor;

public partial class AddItemWindow : Window
{
    public Item Item;
    public AddItemWindow(IEnumerable<Element> elements, Item item = null)
    {
        InitializeComponent();
    }
}