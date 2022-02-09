using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace project.Game;

public class LogBlock : TextBlock
{
    private readonly LinkedList<string> _stringList;
    private readonly int _maxStrings;
    private readonly GameWindow _game;

    public LogBlock(int maxStrings, GameWindow game)
    {
        _game = game;
        _maxStrings = maxStrings;
        _stringList = new LinkedList<string>();
    }

    public void AddLine(string newLine)
    {
        while (_stringList.Count >= _maxStrings)
        {
            _stringList.RemoveFirst();
        }

        Console.WriteLine(newLine);
        _stringList.AddLast(newLine);
        RefreshText();
    }

    public void RefreshText()
    {
        var newText = new StringBuilder();
        foreach (string text in _stringList)
        {
            newText.Append(text).Append('\n');
        }

        _game.LogBox.Text = newText.ToString();
    }
}