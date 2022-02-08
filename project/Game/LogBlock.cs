using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace project.Game;

public class LogBlock : GameWindow
{
    private LinkedList<String> stringList;
    private int maxStrings;

    public LogBlock(int maxStrings)
    {
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
        refreashText();
    }

    public void refreashText()
    {
        StringBuilder newText = new StringBuilder();
        foreach (var text in stringList)
        {
            newText.Append(text).Append("\n");
        }

        LogBox.Text = newText.ToString();
    }
}