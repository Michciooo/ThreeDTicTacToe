using Godot;
using System;
using NAudio.Wave;

namespace threeDTicTacToe;
public partial class MainMenu : Node3D
{
	public override void _Ready()
	{
		var global = (Global)GetNode("/root/Global");
		global.Statistics();
		var loggdAsLabel = GetNode<Label>("MainContainer/loggedAsLabel");
		loggdAsLabel.Text = $"Logged as : {global.accName}";
		
		Texture customCursor = GD.Load<Texture>("res://Custom_mouse/default_mouse.png");
		Input.SetCustomMouseCursor(customCursor);
		
		string virtualPath = "res://sfx/main_menu_theme.mp3";
		string realPath = ProjectSettings.GlobalizePath(virtualPath);
		
		global.PlayLooping(realPath);
	}
	
	private void SettingsPress()
	{
		var global = (Global)GetNode("/root/Global");
		global.ClickSFX("res://sfx/btn_click.wav");
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
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu2D/MainMenu2D.tscn");
			global.Mode = "2D";
		}
		if (threeDBtn.IsPressed())
		{
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu3D/MainMenu3D.tscn");
			global.Mode = "3D";
		}
		if (Input.IsActionPressed("appExit") || exitBtn.IsPressed())
		{
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().Quit();
		}
		if (settings.IsPressed())
		{
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://MainMenus/Settings.tscn");
		}
		if (accBtn.IsPressed())
		{
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://Registration/AccountLogin.tscn");
		}
	}
}
