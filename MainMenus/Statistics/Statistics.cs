using Godot;
using System;

namespace threeDTicTacToe;

public partial class Statistics : Control
{
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		var backBtn = GetNode<Button>("backBtn");
		if (backBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenus/Settings.tscn");
	}
}
