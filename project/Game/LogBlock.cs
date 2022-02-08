using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace project.Game;

public class LogBlock : TextBlock
{
    private LinkedList<String> stringList;
    private int maxStrings;
    private protected GameWindow _game;

    public LogBlock(int maxStrings, GameWindow game)
    {
        this._game = game;
        this.maxStrings = maxStrings;
        stringList = new LinkedList<string>();
    }

    public void addLine(String newLine)
    {
        while (stringList.Count >= maxStrings)
        {
            stringList.RemoveFirst();
        }

        Console.WriteLine(newLine);
        stringList.AddLast(newLine);
        refreshText();
    }

    public void refreshText()
    {
        StringBuilder newText = new StringBuilder();
        foreach (var text in stringList)
        {
            newText.Append(text).Append("\n");
        }

        _game.LogBox.Text = newText.ToString();
    }
}