using Godot;

[GlobalClass]
public partial class Intent : Resource
{
    [Export]
    public string Number { get; set; } = "";

    [Export]
    public Texture Icon { get; set; } = null;
}
