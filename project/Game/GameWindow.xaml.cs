using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;

namespace project.Game;

public partial class GameWindow
{
    public readonly Entity[,] EntityTable = new Entity[9, 9];
    public Player? Player;
    public int MoveCounter;
    private readonly EntityDbContext _context = new EntityDbContext();
    private readonly List<Models.Enemy> _enemies;
    private readonly List<Models.Item> _items;
    private readonly string _path = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 25) + "/";
    public readonly LogBlock LogBlock;
    private void AddEnemy()
    {
        if (Player == null) return;
        var random = new Random();
        int newEnemyX, newEnemyY;
        do
        {
            newEnemyX = random.Next(9);
        } while (newEnemyX == Player.X);
        do
        {
            newEnemyY = random.Next(9);
        } while (newEnemyY == Player.Y);

        var model = _enemies[random.Next(_enemies.Count)];
        new Enemy(model, newEnemyX, newEnemyY, this);
    }

    private void AddItem()
    {
        if (Player == null) return;
        var random = new Random();
        int newItemX, newItemY;
        do
        {
            newItemX = random.Next(9);
        } while (newItemX == Player.X);

        do
        {
            newItemY = random.Next(9);
        } while (newItemY == Player.Y);

        Models.Item model;
        do
        {
            model = _items[random.Next(_items.Count)];
        } while (Player.CheckForWeapon(model));
        EntityTable[newItemX, newItemY] = new Item(model);
    }

    private void RedrawGrid()
    {
        MainGrid.Children.Clear();
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                var box = new Image
                {
                    Source = EntityTable[j, i].Sprite
                };
                MainGrid.Children.Add(box);
            }
        }
    }

    private void RedrawInv()
    {
        if (Player == null) return;
        InvGrid.Children.Clear();
        for (var i = 0; i < 10; i++)
        {
            var box = new Image
            {
                Source = Player.Inventory[i] == null ? new BitmapImage(new Uri(_path + "res/img/emptyO.png", UriKind.RelativeOrAbsolute)) : Player.Inventory[i]?.Sprite
            };
            if (i == Player.EquippedItemId)
            {
                var border = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.White),
                    BorderThickness = new Thickness(2),
                    Child = box
                };
                InvGrid.Children.Add(border);
            }
            else InvGrid.Children.Add(box);
        }
    }

    private void UpdateInfoBox()
    {
        if(Player == null) return;
        HpText.Text = "HP: " + Player.Hp;
        if (Player.EquippedItem != null)
        {
            switch (Player.EquippedItem.Type)
            {
                case Item.EType.Weapon:
                    if (Player.EquippedItem.Element == null)
                    {
                        ItemText.Text = Player.EquippedItem.Name + "\nObrażenia: " + Player.EquippedItem.DmgMin + "-" +
                                        Player.EquippedItem.DmgMax;
                        ElementImg.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        ItemText.Text = Player.EquippedItem.Name + "\nObrażenia: " + Player.EquippedItem.DmgMin + "-" +
                                        Player.EquippedItem.DmgMax + "\nElement:";
                        ElementImg.Source = new BitmapImage(new Uri(Player.EquippedItem.Element.SpriteUrl));
                        ElementImg.Visibility = Visibility.Visible;
                    }
                    break;
                case Item.EType.Heal:
                    ItemText.Text = Player.EquippedItem.Name + "\nLeczy " + Player.EquippedItem.DmgMin +
                                    " punktów zdrowia";
                    ElementImg.Visibility = Visibility.Hidden;
                    break;
            }
        }
        else
        {
            ItemText.Text = "";
            ElementImg.Visibility = Visibility.Hidden;
        }
    }

    private void MoveEnemies()
    {
        if (Player == null) return;
        var moved = new List<int>();
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (EntityTable[i, j] is not Enemy || Player == null) continue;
                var enemy = (Enemy) EntityTable[i, j];
                if (moved.Contains(enemy.Id)) continue;
                if (enemy.X < Player.X)
                {
                    if (enemy.Y < Player.Y) enemy.MoveDownRight();
                    else if (enemy.Y > Player.Y) enemy.MoveUpRight();
                    else enemy.MoveRight();
                }
                else if (enemy.X > Player.X)
                {
                    if (enemy.Y < Player.Y) enemy.MoveDownLeft();
                    else if (enemy.Y > Player.Y) enemy.MoveUpLeft();
                    else enemy.MoveLeft();
                }
                else if (enemy.Y > Player.Y) enemy.MoveUp();
                else enemy.MoveDown();
                moved.Add(enemy.Id);
            }
        }
        MoveCounter++;
        if (MoveCounter % 12 == 0)
            AddEnemy();
        if (MoveCounter % 15 == 0)
            AddItem();
    }
    public GameWindow()
    {
        InitializeComponent();
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
                EntityTable[i, j] = new Entity();
        }
        _context.Enemies.Load();
        _enemies = _context.Enemies.ToList();
        _context.Items.Load();
        _items = _context.Items.ToList();

        LogBlock = new LogBlock(5, this);
        Player = new Player(new Uri(_path + "res/img/player.png", UriKind.RelativeOrAbsolute), 100, this);
        var startWpn = new Item("Miecz", new Uri(_path + "res/img/sword.png", UriKind.RelativeOrAbsolute), 5, 10);
        Player.Pickup(startWpn);
        HpText.Text = "HP: " + Player.Hp;
        ItemText.Text = Player.EquippedItem.Name + "\nObrażenia: " + Player.EquippedItem.DmgMin + "-" +
                        Player.EquippedItem.DmgMax;
        AddEnemy();
        AddItem();
        RedrawGrid();
        RedrawInv();
    }

    public void SetGameOver()
    {
        this.EndScreen.Visibility = Visibility.Visible;
        
    }

    private void GameWindow_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (Player == null)
        {
            if(e.Key == Key.Escape) this.Close();
                return;
        }
        switch (e.Key)
        {
            case Key.Up or Key.NumPad8:
                Player.MoveUp();
                MoveEnemies();
                break;
            case Key.Down or Key.NumPad2:
                Player.MoveDown();
                MoveEnemies();
                break;
            case Key.Left or Key.NumPad4:
                Player.MoveLeft();
                MoveEnemies();
                break;
            case Key.Right or Key.NumPad6:
                Player.MoveRight();
                MoveEnemies();
                break;
            case Key.Home or Key.NumPad7:
                Player.MoveUpLeft();
                MoveEnemies();
                break;
            case Key.PageUp or Key.NumPad9:
                Player.MoveUpRight();
                MoveEnemies();
                break;
            case Key.End or Key.NumPad1:
                Player.MoveDownLeft();
                MoveEnemies();
                break;
            case Key.PageDown or Key.NumPad3:
                Player.MoveDownRight();
                MoveEnemies();
                break;
            case Key.Space:
                MoveEnemies();
                break;
            case Key.D1:
                Player.EquippedItemId = 0;
                break;
            case Key.D2:
                Player.EquippedItemId = 1;
                break;
            case Key.D3:
                Player.EquippedItemId = 2;
                break;
            case Key.D4:
                Player.EquippedItemId = 3;
                break;
            case Key.D5:
                Player.EquippedItemId = 4;
                break;
            case Key.D6:
                Player.EquippedItemId = 5;
                break;
            case Key.D7:
                Player.EquippedItemId = 6;
                break;
            case Key.D8:
                Player.EquippedItemId = 7;
                break;
            case Key.D9:
                Player.EquippedItemId = 8;
                break;
            case Key.D0:
                Player.EquippedItemId = 9;
                break;
            case Key.E:
                Player.UseItem();
                break;
            case Key.Q:
                Player.DropCurrentItem();
                break;
            case Key.Delete:
                Player.DeleteCurrentItem();
                break;
        }
        RedrawGrid();
        RedrawInv();
        UpdateInfoBox();
    }
}