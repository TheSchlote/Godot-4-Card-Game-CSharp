using Godot;
using System;

public partial class IntentUI : HBoxContainer
{
    public TextureRect Icon;
    public Label Number;

    public override void _Ready()
    {
        Icon = GetNode<TextureRect>("Icon");
        Number = GetNode<Label>("Number");
    }

    public void UpdateIntent(Intent intent)
    {
        if (intent == null)
        {
            Hide();
            return;
        }

        Icon.Texture = (Texture2D)intent.Icon;
        Icon.Visible = Icon.Texture != null;
        Number.Text = intent.Number.ToString();
        Number.Visible = !string.IsNullOrEmpty(intent.Number);
        Show();
    }
}
