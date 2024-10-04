using Godot;

namespace threeDTicTacToe;

public partial class WinPopUp : Control
{
    public override void _Ready()
    {
        Button closeBtn = GetNode<Button>("closeBtn");
        closeBtn.Pressed += ClosePopUp;
    }
    private void ClosePopUp()
    {
        Button closeBtn = GetNode<Button>("closeBtn");
        Label winLabel = GetNode<Label>("winLabel");
        
        this.Visible = false;
        closeBtn.Visible = false;
        winLabel.Visible = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}