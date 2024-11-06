using Godot;
using System;

namespace threeDTicTacToe
{
	public partial class MainMenu2D : Control
	{
		private Global global;
		public override void _Ready()
		{
			var settings = GetNode<TextureButton>("Main/Settings/SettingsBtn");
			var offlineBtn = GetNode<Button>("Main/MainContainer/Container/OfflineBtn");
			var computerBtn = GetNode<Button>("Main/MainContainer/Container/ComputerBtn");
			var easyModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/EasyModeBtn");
			var aiModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/AiModeBtn");

			Input.MouseMode = Input.MouseModeEnum.Confined;

			if (settings != null) settings.Pressed += SettingsPress;
			if (offlineBtn != null) offlineBtn.Pressed += OfflineGameBtnPressed;
			computerBtn.MouseEntered += OnComputerGameHover;
			if(easyModeBtn != null) easyModeBtn.Pressed += ComputerGamePressed;
			if(aiModeBtn != null) aiModeBtn.Pressed += ComputerGamePressed;
			
			easyModeBtn.MouseExited += OnComputerGameUnHover;
			aiModeBtn.MouseExited += OnComputerGameUnHover;
		}
		private void OnComputerGameHover()
		{
			var computerBtn = GetNode<Button>("Main/MainContainer/Container/ComputerBtn");
			var easyModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/EasyModeBtn");
			var aiModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/AiModeBtn");

			computerBtn.Visible = false;
			easyModeBtn.Visible = true;
			aiModeBtn.Visible = true;
		}
		private void OnComputerGameUnHover()
		{
			var computerBtn = GetNode<Button>("Main/MainContainer/Container/ComputerBtn");
			var easyModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/EasyModeBtn");
			var aiModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/AiModeBtn");
			
			computerBtn.Visible = true;
			easyModeBtn.Visible = false;
			aiModeBtn.Visible = false;
		}
		
		private void SettingsPress()
		{
			GetTree().ChangeSceneToFile("res://MainMenu2D/Settings2D.tscn");
		}

		private void OfflineGameBtnPressed()
		{
			global = GetNode<Global>("/root/Global");
			var offlineBtn = GetNode<Button>("Main/MainContainer/Container/OfflineBtn");

			global.buttonName2D = offlineBtn.Name;
			GetTree().ChangeSceneToFile("res://MainMenu2D/TTT2D.tscn");
		}

		private void ComputerGamePressed()
		{
			global = GetNode<Global>("/root/Global");
			
			var easyModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/EasyModeBtn");
			var aiModeBtn = GetNode<Button>("Main/MainContainer/Container/miniBtnsBox/AiModeBtn");
			
			if(easyModeBtn.IsPressed()) global.buttonName2D = easyModeBtn.Name;
			if(aiModeBtn.IsPressed()) global.buttonName2D = aiModeBtn.Name;
			
			GetTree().ChangeSceneToFile("res://MainMenu2D/TTT2D.tscn");
		}
		public override void _Process(double delta)
		{
			if (Input.IsActionPressed("mainMenu")) GetTree().ChangeSceneToFile("res://MainMenu/MainMenu.tscn");
		}
	}
}
