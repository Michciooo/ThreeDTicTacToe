using Godot;
using System;
using Godot.Collections;

namespace threeDTicTacToe
{
	public partial class MainMenu3D : Node3D
	{
		public override void _Ready()
		{
			var settings = GetNode<TextureButton>("Main/Settings/SettingsBtn");
			var playerOneBtn = GetNode<OptionButton>("Main/MainContainer/Container/left/playerOneBtn");
			var playerTwoBtn = GetNode<OptionButton>("Main/MainContainer/Container/center/playerTwoBtn");
			var playerThreeBtn = GetNodeOrNull<OptionButton>("Main/MainContainer/Container/right/playerThreeBtn");
			var fourxfourGridmode = GetNode<CheckBox>("Main/MainContainer/ModesContainer/fourxfourGridMode");
			var threePMode = GetNode<CheckBox>("Main/MainContainer/ModesContainer/threePMode");
			var playBtn = GetNode<Button>("Main/MainContainer/playBtn");
			
			var global = GetNode<Global>("/root/Global");

			if (settings != null) settings.Pressed += SettingsPress;

			playerOneBtn.ItemSelected += FirstPlayerSelect;
			playerTwoBtn.ItemSelected += SecondPlayerSelect;
			fourxfourGridmode.Pressed += FourxfourGridmodeOnPressed;
			threePMode.Pressed += ThreePModeOnPressed;
			
			var playerThreeContainer = GetNode<Container>("Main/MainContainer/Container/right");

			threePMode.ButtonPressed = global.isModeChecked;
			playerThreeContainer.Visible = global.isModeChecked;
			
			var firstPIndex = 0;
			var secondPIndex = 0;
			var thirdPIndex = 0;

			if (global.playersTypes3D[1] == "Human") firstPIndex = 0;
			else if (global.playersTypes3D[1] == "Easy Computer") firstPIndex = 1;
			else if(global.playersTypes3D[1] == "AI Computer") firstPIndex = 2;
			
			if(global.playersTypes3D[2] == "Human") secondPIndex = 0;
			else if(global.playersTypes3D[2] == "Easy Computer") secondPIndex = 1;
			else if(global.playersTypes3D[2] == "AI Computer") secondPIndex = 2;
			
			if(global.playersTypes3D[3] == "Human") thirdPIndex = 0;
			else if(global.playersTypes3D[3] == "Easy Computer") thirdPIndex = 1;
			else if(global.playersTypes3D[3] == "AI Computer") thirdPIndex = 2;

			playerOneBtn.Selected = firstPIndex;
			playerTwoBtn.Selected = secondPIndex;
			if (playerThreeBtn != null)
			{
				playerThreeBtn.ItemSelected += ThirdPlayerSelect;
				playerThreeBtn.Selected = thirdPIndex;
			}
			
			playBtn.Pressed+= PlayBtnOnPressed;
		}
		private void PlayBtnOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			var warningLabel = GetNode<Label>("Main/MainContainer/warningLabel");
			var fourxfourGridMode = GetNode<CheckBox>("Main/MainContainer/ModesContainer/fourxfourGridMode");
			var threePMode = GetNode<CheckBox>("Main/MainContainer/ModesContainer/threePMode");

			if (fourxfourGridMode.IsPressed())
			{
				if ((global.playersTypes3D[1] == global.playersTypes3D[2] && global.playersTypes3D[1] == "Easy Computer") ||
				    (global.playersTypes3D[1] == global.playersTypes3D[2] && global.playersTypes3D[1] == "AI Computer") ||
				    (global.playersTypes3D[1] == "Easy Computer" && global.playersTypes3D[2] == "AI Computer") ||
				    (global.playersTypes3D[1] == "AI Computer" && global.playersTypes3D[2] == "Easy Computer"))
				{
					warningLabel.Text = "The battle between 2 computers is not allowed.";
				}
				else if ((global.playersTypes3D[1] == global.playersTypes3D[2] && global.playersTypes3D[1] == "Human") ||
				         (global.playersTypes3D[1] == "Human" && global.playersTypes3D[2] == "Easy Computer") ||
				         (global.playersTypes3D[1] == "Easy Computer" && global.playersTypes3D[2] == "Human") ||
				         (global.playersTypes3D[1] == "Human" && global.playersTypes3D[2] == "AI Computer") ||
				         (global.playersTypes3D[1] == "AI Computer" && global.playersTypes3D[2] == "Human"))
				{
					global.player13D = global.playersTypes3D[1];
					global.player23D = global.playersTypes3D[2];
					GetTree().ChangeSceneToFile("res://TicTacToeModes/TTT3D/TTT3D.tscn");
				}
				global.ClickSFX("res://sfx/btn_click.wav");
			}
			if (threePMode.IsPressed())
			{
				if ((global.playersTypes3D[1]==global.playersTypes3D[2] && global.playersTypes3D[2]==global.playersTypes3D[3] && global.playersTypes3D[1]=="Easy Computer") ||
				    (global.playersTypes3D[1]==global.playersTypes3D[2] && global.playersTypes3D[2]==global.playersTypes3D[3] &&  global.playersTypes3D[1]=="AI Computer") || 
				    (global.playersTypes3D[1]==global.playersTypes3D[2] && global.playersTypes3D[3]=="AI Computer" && global.playersTypes3D[1]=="Easy Computer") ||
				    (global.playersTypes3D[1]==global.playersTypes3D[2] && global.playersTypes3D[3]=="Easy Computer" && global.playersTypes3D[1]=="AI Computer") ||
				    (global.playersTypes3D[1]==global.playersTypes3D[3] && global.playersTypes3D[2]=="AI Computer" && global.playersTypes3D[1]=="Easy Computer") ||
				    (global.playersTypes3D[1]==global.playersTypes3D[3] && global.playersTypes3D[2]=="Easy Computer" && global.playersTypes3D[1]=="AI Computer") ||
				    (global.playersTypes3D[2]==global.playersTypes3D[3] && global.playersTypes3D[1]=="AI Computer" && global.playersTypes3D[2]=="Easy Computer") ||
				    (global.playersTypes3D[2]==global.playersTypes3D[3] && global.playersTypes3D[1]=="Easy Computer" && global.playersTypes3D[2]=="AI Computer"))
				{
					warningLabel.Text = "The battle between 3 computers is not allowed.";
				}
				else
				{
					global.player13D = global.playersTypes3D[1];
					global.player23D = global.playersTypes3D[2];
					global.player33D = global.playersTypes3D[3];
					GetTree().ChangeSceneToFile("res://TicTacToeModes/TTT3D/TTT3D.tscn");
				}
				global.ClickSFX("res://sfx/btn_click.wav");
			}
		}

		private void FirstPlayerSelect(long index)
		{
			var global = GetNode<Global>("/root/Global");
			var playerOneBtn = GetNode<OptionButton>("Main/MainContainer/Container/left/playerOneBtn");
			string value = "";

			switch (index)
			{
				case 0: value = playerOneBtn.Text;
					break;
				case 1: value = playerOneBtn.Text;
					break;
				case 2: value = playerOneBtn.Text; 
					break;
			}
			global.ClickSFX("res://sfx/btn_click.wav");
			if(global.playersTypes3D.ContainsKey(1)) global.playersTypes3D[1] = playerOneBtn.Text;
			else global.playersTypes3D.Add(1,playerOneBtn.Text);
		}
		private void SecondPlayerSelect(long index)
		{
			var global = GetNode<Global>("/root/Global");
			var playerTwoBtn = GetNode<OptionButton>("Main/MainContainer/Container/center/playerTwoBtn");
			string value = "";

			switch (index)
			{
				case 0: value = playerTwoBtn.Text;
					break;
				case 1: value = playerTwoBtn.Text;
					break;
				case 2: value = playerTwoBtn.Text; 
					break;
			}
			global.ClickSFX("res://sfx/btn_click.wav");
			if(global.playersTypes3D.ContainsKey(2)) global.playersTypes3D[2] = playerTwoBtn.Text;
			else global.playersTypes3D.Add(2,playerTwoBtn.Text);
		}

		private void ThirdPlayerSelect(long index)
		{
			var global = GetNode<Global>("/root/Global");
			var playerThreeBtn = GetNode<OptionButton>("Main/MainContainer/Container/right/playerThreeBtn");
			string value = "";

			switch (index)
			{
				case 0: value = playerThreeBtn.Text;
					break;
				case 1: value = playerThreeBtn.Text;
					break;
				case 2: value = playerThreeBtn.Text; 
					break;
			}
			global.ClickSFX("res://sfx/btn_click.wav");
			if(global.playersTypes3D.ContainsKey(3)) global.playersTypes3D[3] = playerThreeBtn.Text;
			else global.playersTypes3D.Add(3,playerThreeBtn.Text);
		}
		private void FourxfourGridmodeOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			global.ClickSFX("res://sfx/btn_click.wav");
			global.player3DMode = "4x4x4";
			global.isModeChecked = false;
			
			var playerThreeContainer = GetNode<Container>("Main/MainContainer/Container/right");
			playerThreeContainer.Visible = global.isModeChecked;
		}
		private void ThreePModeOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			global.ClickSFX("res://sfx/btn_click.wav");
			global.player3DMode = "3x3x3";
			global.isModeChecked = true;
			
			var playerThreeContainer = GetNode<Container>("Main/MainContainer/Container/right");
			playerThreeContainer.Visible = global.isModeChecked;
		}
		private void SettingsPress()
		{
			var global = GetNode<Global>("/root/Global");
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu3D/Settings3D.tscn");
		}
		public override void _Process(double delta)
		{
			var global = GetNode<Global>("/root/Global");
			if (Input.IsActionPressed("mainMenuKey"))
			{
				global.ClickSFX("res://sfx/btn_click.wav");
				GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
			}
		}
	}
}
