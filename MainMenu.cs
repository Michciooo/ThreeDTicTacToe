using Godot;
using System;

namespace threeDTicTacToe;
public partial class MainMenu : Node3D
{
	public override void _Ready()
	{
		Button friendGameButton = GetNode<Button>("MainContainer/Container/FriendPlay");
		friendGameButton.Pressed += FriendGameButtonOnPressed;
	}

	private void FriendGameButtonOnPressed()
	{
		//var friendPlayScene = GD.Load<PackedScene>("res://Main.tscn");
		GetTree().ChangeSceneToFile("res://Main.tscn");
	}

	public override void _Process(double delta)
	{
		if(Input.IsPhysicalKeyPressed(Key.Q)) GetTree().Quit();
	}
}
