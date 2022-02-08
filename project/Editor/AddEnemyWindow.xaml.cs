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
        ComboElement.DisplayMemberPath = "Name";
        ComboPrzedmiot.ItemsSource = items;
        ComboPrzedmiot.DisplayMemberPath = "Name";
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
        Uri? sprite;
        int health, dmgMin, dmgMax;
        try
        {
            sprite = new Uri(TextSprite.Text, UriKind.RelativeOrAbsolute);
            health = Convert.ToInt32(TextHealth.Text);
            dmgMin = Convert.ToInt32(TextDmgMin.Text);
            dmgMax = Convert.ToInt32(TextDmgMax.Text);
        }
        catch (FormatException ex)
        {
            MessageBox.Show("Podano błędne dane!");
            return;
        }

        Enemy.Sprite = sprite;
        Enemy.Health = health;
        Enemy.DmgMin = dmgMin;
        Enemy.DmgMax = dmgMax;
        Enemy.Name = TextName.Text;
        if (ComboElement.SelectedItem is Element element)
            Enemy.Element = element;
        else Enemy.Element = null;
        Enemy.Element = (Element) ComboElement.SelectedItem;
        if (ComboPrzedmiot.SelectedItem is Item item)
            Enemy.Item = item;
        else Enemy.Item = null;
        DialogResult = true;
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}