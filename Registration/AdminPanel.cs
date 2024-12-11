using Godot;
using System;

namespace threeDTicTacToe;

public partial class AdminPanel : Panel
{
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		var backBtn = (Button)GetNode("backBtn");
		if (backBtn.IsPressed()) GetTree().ChangeSceneToFile("res://Registration/AccountLogin.tscn");
	}
}
