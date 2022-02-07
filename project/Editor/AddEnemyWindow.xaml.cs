using System;
using System.Collections.Generic;
using System.Windows;
using project.Game;
using Enemy = project.Models.Enemy;
using Item = project.Models.Item;

namespace project.Editor;

public partial class AddEnemyWindow : Window
{
    public Enemy Enemy;
    public AddEnemyWindow(IEnumerable<Element> elements, IEnumerable<Item> items, Enemy? enemy = null)
    {
        InitializeComponent();
        ComboElement.ItemsSource = elements;
        ComboPrzedmiot.ItemsSource = items;
        if (enemy == null)
        {
            Enemy = new Enemy();
        }
        else
        {
            TextName.Text = enemy.Name;
            if (enemy.Sprite.IsAbsoluteUri)
                TextSprite.Text = enemy.Sprite.AbsolutePath;
            else TextSprite.Text = "Uri nie jest Absolute";
            ComboElement.SelectedItem = enemy.Element;
            TextHealth.Text = enemy.Health.ToString();
            TextDmgMin.Text = enemy.DmgMin.ToString();
            TextDmgMax.Text = enemy.DmgMax.ToString();
            ComboPrzedmiot.SelectedItem = enemy.Item;
            Enemy = enemy;
        }
    }

    private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            Enemy.Sprite = new Uri(TextSprite.Text, UriKind.RelativeOrAbsolute);
            Enemy.Health = Convert.ToInt32(TextHealth.Text);
            Enemy.DmgMin = Convert.ToInt32(TextDmgMin.Text);
            Enemy.DmgMax = Convert.ToInt32(TextDmgMax.Text);
        }
        catch (FormatException ex)
        {
            MessageBox.Show("Podano błędne dane!");
            return;
        }
        Enemy.Name = TextName.Text;
        Enemy.Element = (Element) ComboElement.SelectedItem;
        Enemy.Item = (Item) ComboPrzedmiot.SelectedItem;
        DialogResult = true;
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}