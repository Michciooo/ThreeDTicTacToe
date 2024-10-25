using Godot;
using System;

namespace threeDTicTacToe
{
	public partial class MainMenu : Node3D
	{
		private Global global;
		public override void _Ready()
		{
			var settings = GetNode<TextureButton>("Main/Settings/SettingsBtn");
			var offlineBtn = GetNode<Button>("Main/MainContainer/Container/OfflineBtn");
			var computerBtn = GetNode<Button>("Main/MainContainer/Container/ComputerBtn");

			Input.MouseMode = Input.MouseModeEnum.Confined;

			if (settings != null) settings.Pressed += SettingsPress;
			if (offlineBtn != null) offlineBtn.Pressed += OfflineGameBtnPressed;
			if (computerBtn != null) computerBtn.Pressed += ComputerGamePressed;
		}
		private void SettingsPress()
		{
			GetTree().ChangeSceneToFile("res://Settings.tscn");
		}

		private void OfflineGameBtnPressed()
		{
			global = GetNode<Global>("/root/Global");
			var offlineBtn = GetNode<Button>("Main/MainContainer/Container/OfflineBtn");

			global.buttonName = offlineBtn.Name;
			GD.Print(global.buttonName);
			GetTree().ChangeSceneToFile("res://Main.tscn");
		}

		private void ComputerGamePressed()
		{
			global = GetNode<Global>("/root/Global");
			var computerBtn = GetNode<Button>("Main/MainContainer/Container/ComputerBtn");
			
			global.buttonName = computerBtn.Name;
			GetTree().ChangeSceneToFile("res://Main.tscn");
		}
		public override void _Process(double delta)
		{
			if (Input.IsActionPressed("appExit")) GetTree().Quit();
		}
	}
}
