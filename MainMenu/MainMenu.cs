using Godot;
using System;

namespace threeDTicTacToe;
public partial class MainMenu : Node3D
{
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		Button twoDBtn = GetNode<Button>("MainContainer/Modes/leftSide/twoDBtn");
		Button threeDBtn = GetNode<Button>("MainContainer/Modes/rightSide/threeDBtn");
		Button exitBtn = GetNode<Button>("MainContainer/ExitBtn");
		
		if(twoDBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenu2D/MainMenu2D.tscn");
		if(threeDBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenu3D/MainMenu3D.tscn");
		
		if (Input.IsActionPressed("appExit") || exitBtn.IsPressed()) GetTree().Quit();
	}
}
