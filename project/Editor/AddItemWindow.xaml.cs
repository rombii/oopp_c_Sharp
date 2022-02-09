using System;
using System.Collections.Generic;
using System.Windows;
using project.Game;
using Item = project.Models.Item;

namespace project.Editor;

public partial class AddItemWindow
{
    public readonly Item Item;
    private AddItemWindow(IEnumerable<Element> elements)
    {
        InitializeComponent();
        ComboElement.ItemsSource = elements;
        ComboElement.DisplayMemberPath = "Name";
    }

    public AddItemWindow(IEnumerable<Element> elements, Game.Item.EType type) : this(elements)
    {
        Item = new Item()
        {
            Type = type
        };
        if (type != Game.Item.EType.Heal) return;
        DmgHpLabel.Content = "Leczenie";
        DmgDash.Visibility = Visibility.Collapsed;
        TextDmgMax.Visibility = Visibility.Collapsed;
        ElementPanel.Visibility = Visibility.Collapsed;
    }

    public AddItemWindow(IEnumerable<Element> elements, Item item) : this(elements)
    {
        Item = item;
        if (item.Type == Game.Item.EType.Heal)
        {
            DmgHpLabel.Content = "Leczenie";
            DmgDash.Visibility = Visibility.Collapsed;
            TextDmgMax.Visibility = Visibility.Collapsed;
            ElementPanel.Visibility = Visibility.Collapsed;
        }

        TextName.Text = item.Name;
        if (item.SpriteUrl != null && item.SpriteUrl.IsAbsoluteUri)
            TextSprite.Text = item.SpriteUrl.AbsolutePath;
        else TextSprite.Text = "Uri nie jest Absolute";
        ComboElement.SelectedItem = item.Element;
        TextDmgMin.Text = item.DmgMin.ToString();
        TextDmgMax.Text = item.DmgMax.ToString();
    }

    private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
    {
        Uri? spriteUrl;
        int dmgMin, dmgMax;
        try
        {
            spriteUrl = new Uri(TextSprite.Text, UriKind.RelativeOrAbsolute);
            dmgMin = Convert.ToInt32(TextDmgMin.Text);
            dmgMax = Item.Type == Game.Item.EType.Weapon ? Convert.ToInt32(TextDmgMax.Text) : dmgMin;
        }
        catch (FormatException)
        {
            MessageBox.Show("Podano błędne dane!");
            return;
        }

        Item.SpriteUrl = spriteUrl;
        Item.DmgMin = dmgMin;
        Item.DmgMax = dmgMax;
        Item.Name = TextName.Text;
        if (ComboElement.SelectedItem is Element element)
            Item.Element = element;
        else Item.Element = null;
        DialogResult = true;
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}