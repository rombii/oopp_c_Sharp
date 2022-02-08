using System;
using System.Windows;
using project.Game;

namespace project.Editor;

public partial class AddElementWindow : Window
{
    public Element Element;
    public AddElementWindow(Element? element = null)
    {
        InitializeComponent();
        if (element == null) Element = new Element();
        else
        {
            Element = element;
            TextName.Text = element.Name;
            TextSprite.Text = element.SpriteUrl;
            TextWeakToID.Text = element.WeakToId.ToString();
            TextStrongToID.Text = element.StrongToId.ToString();
        }
    }

    private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
    {
        int weakToId, strongToId;
        try
        {
            if (TextWeakToID.Text.Length > 0)
                weakToId = Convert.ToInt32(TextWeakToID.Text);
            else weakToId = -1;
            if (TextStrongToID.Text.Length > 0)
                strongToId = Convert.ToInt32(TextStrongToID.Text);
            else strongToId = -1;
        }
        catch (FormatException ex)
        {
            MessageBox.Show("Podano błędne dane!");
            return;
        }

        Element.WeakToId = weakToId;
        Element.StrongToId = strongToId;
        Element.Name = TextName.Text;
        Element.SpriteUrl = TextSprite.Text;
        DialogResult = true;
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}