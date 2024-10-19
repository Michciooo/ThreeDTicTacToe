using Godot;
using System;

namespace threeDTicTacToe;
public partial class Score : Control
{
	public int x_wins;
	public int o_wins;
	public override void _Ready()
	{
	}

	public void ScoreSystem()
	{
		Label xWinsLabel = GetNode<Label>("CrossWins");
		Label oWinsLabel = GetNode<Label>("CircleWins");
		xWinsLabel.Text = $"X won : {x_wins} times";
		oWinsLabel.Text = $"O won : {o_wins} times";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
