using Godot;
using System;

namespace threeDTicTacToe;
public partial class Score : Control
{
	public int x_wins;
	public int o_wins;
	public int triangle_wins;

	public override void _Ready()
	{
		var global = (Global)GetNode("/root/Global");
		Label triangleWinsLabel = GetNode<Label>("TriangleWins");
		if (global.player3DMode == "3x3x3")
		{
			triangleWinsLabel.Visible = true;
		}

		if (global.player3DMode == "4x4x4" || global.Mode=="2D")
		{
			triangleWinsLabel.Visible = false;
		}
	}

	public void ScoreSystem()
	{
		Label xWinsLabel = GetNode<Label>("CrossWins");
		Label oWinsLabel = GetNode<Label>("CircleWins");
		Label triangleWinsLabel = GetNode<Label>("TriangleWins");
		
		xWinsLabel.Text = $"X won : {x_wins} times";
		oWinsLabel.Text = $"O won : {o_wins} times";
		triangleWinsLabel.Text = $"\u25b3 won : {triangle_wins} times";
	}
	public override void _Process(double delta)
	{
	}
}
