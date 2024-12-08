using Godot;
using System;

namespace threeDTicTacToe;
public partial class MainMenu : Node3D
{
	public override void _Ready()
	{
	}
	private void SettingsPress()
	{
		GetTree().ChangeSceneToFile("res://MainMenus/Settings.tscn");
	}
	public override void _Process(double delta)
	{
		var global = GetNode<Global>("/root/Global");
		Button twoDBtn = GetNode<Button>("MainContainer/Modes/leftSide/twoDBtn");
		Button threeDBtn = GetNode<Button>("MainContainer/Modes/rightSide/threeDBtn");
		Button exitBtn = GetNode<Button>("MainContainer/ExitBtn");
		var settings = GetNode<TextureButton>("MainContainer/Settings/SettingsBtn");
		var accBtn = GetNode<Button>("MainContainer/accBtn");

		if (twoDBtn.IsPressed())
		{
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu2D/MainMenu2D.tscn");
			global.Mode = "2D";
		}
		if (threeDBtn.IsPressed())
		{
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu3D/MainMenu3D.tscn");
			global.Mode = "3D";
		}
		if (Input.IsActionPressed("appExit") || exitBtn.IsPressed()) GetTree().Quit();
		if(settings.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenus/Settings.tscn");
		if(accBtn.IsPressed()) GetTree().ChangeSceneToFile("res://Registration/AccountLogin.tscn");
	}
}
