using Godot;
using System;
using Godot.Collections;

namespace threeDTicTacToe
{
	public partial class MainMenu3D : Node3D
	{
		public Dictionary<int,string> playersTypes = new Dictionary<int,string>
		{
			{ 1, "Human" },
			{ 2, "Human" },
			{ 3, "Human"}
		};
		
		public override void _Ready()
		{
			Input.MouseMode = Input.MouseModeEnum.Confined;
			var settings = GetNode<TextureButton>("Main/Settings/SettingsBtn");
			var playerOneBtn = GetNode<OptionButton>("Main/MainContainer/Container/left/playerOneBtn");
			var playerTwoBtn = GetNode<OptionButton>("Main/MainContainer/Container/center/playerTwoBtn");
			var playerThreeBtn = GetNodeOrNull<OptionButton>("Main/MainContainer/Container/right/playerThreeBtn");
			var fourxfourGridmode = GetNode<CheckBox>("Main/MainContainer/ModesContainer/fourxfourGridMode");
			var threePMode = GetNode<CheckBox>("Main/MainContainer/ModesContainer/threePMode");
			var playBtn = GetNode<Button>("Main/MainContainer/playBtn");
			
			var global = GetNode<Global>("/root/Global");
			global.player3DMode = "4x4x4";

			if (settings != null) settings.Pressed += SettingsPress;

			playerOneBtn.ItemSelected += FirstPlayerSelect;
			playerTwoBtn.ItemSelected += SecondPlayerSelect;
			if(playerThreeBtn != null) playerThreeBtn.ItemSelected += ThirdPlayerSelect;
			fourxfourGridmode.Pressed += FourxfourGridmodeOnPressed;
			threePMode.Pressed += ThreePModeOnPressed;
			
			playBtn.Pressed+= PlayBtnOnPressed;
		}
		private void PlayBtnOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			var warningLabel = GetNode<Label>("Main/MainContainer/warningLabel");
			var threePMode = GetNode<CheckBox>("Main/MainContainer/ModesContainer/threePMode");
			
			if ((playersTypes[1] == playersTypes[2] && playersTypes[1] == "Easy Computer") || 
			    (playersTypes[1] == playersTypes[2] && playersTypes[1] == "AI Computer") || 
			    (playersTypes[1] == "Easy Computer" && playersTypes[2] == "AI Computer") ||  
			    (playersTypes[1] == "AI Computer" && playersTypes[2] == "Easy Computer"))
			{
				warningLabel.Text = "The battle between 2 computers is not allowed.";
			}
			else if ((playersTypes[1] == playersTypes[2] && playersTypes[1] == "Human") ||
			         (playersTypes[1] == "Human" && playersTypes[2] == "Easy Computer") ||
			         (playersTypes[1] == "Easy Computer" && playersTypes[2] == "Human") ||
			         (playersTypes[1] == "Human" && playersTypes[2] == "AI Computer") ||
			         (playersTypes[1] == "AI Computer" && playersTypes[2] == "Human"))
			{
				global.player13D = playersTypes[1];
				global.player23D = playersTypes[2];
				GetTree().ChangeSceneToFile("res://TicTacToeModes/TTT3D/TTT3D.tscn");
			}
			if (threePMode.IsPressed())
			{
				if ((playersTypes[1]==playersTypes[2] && playersTypes[2]==playersTypes[3] && playersTypes[1]=="Easy Computer") ||
				    (playersTypes[1]==playersTypes[2] && playersTypes[2]==playersTypes[3] &&  playersTypes[1]=="AI Computer") || 
				    (playersTypes[1]==playersTypes[2] && playersTypes[3]=="AI Computer" && playersTypes[1]=="Easy Computer") ||
				    (playersTypes[1]==playersTypes[2] && playersTypes[3]=="Easy Computer" && playersTypes[1]=="AI Computer") ||
				    (playersTypes[1]==playersTypes[3] && playersTypes[2]=="AI Computer" && playersTypes[1]=="Easy Computer") ||
				    (playersTypes[1]==playersTypes[3] && playersTypes[2]=="Easy Computer" && playersTypes[1]=="AI Computer") ||
				    (playersTypes[2]==playersTypes[3] && playersTypes[1]=="AI Computer" && playersTypes[2]=="Easy Computer") ||
				    (playersTypes[2]==playersTypes[3] && playersTypes[1]=="Easy Computer" && playersTypes[2]=="AI Computer"))
				{
					warningLabel.Text = "The battle between 3 computers is not allowed.";
				}
				else
				{
					global.player13D = playersTypes[1];
					global.player23D = playersTypes[2];
					global.player33D = playersTypes[3];
					GetTree().ChangeSceneToFile("res://TicTacToeModes/TTT3D/TTT3D.tscn");
				}
			}
		}

		private void FirstPlayerSelect(long index)
		{
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
			if(playersTypes.ContainsKey(1)) playersTypes[1] = playerOneBtn.Text;
			else playersTypes.Add(1,playerOneBtn.Text);
		}
		private void SecondPlayerSelect(long index)
		{
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
			if(playersTypes.ContainsKey(2)) playersTypes[2] = playerTwoBtn.Text;
			else playersTypes.Add(2,playerTwoBtn.Text);
		}

		private void ThirdPlayerSelect(long index)
		{
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
			if(playersTypes.ContainsKey(3)) playersTypes[3] = playerThreeBtn.Text;
			else playersTypes.Add(3,playerThreeBtn.Text);
		}
		private void FourxfourGridmodeOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			global.player3DMode = "4x4x4";
			
			var playerThreeContainer = GetNode<Container>("Main/MainContainer/Container/right");
			playerThreeContainer.Visible = false;
		}
		private void ThreePModeOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			global.player3DMode = "3x3x3";
			
			var playerThreeContainer = GetNode<Container>("Main/MainContainer/Container/right");
			playerThreeContainer.Visible = true;
		}
		private void SettingsPress()
		{
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu3D/Settings3D.tscn");
		}
		public override void _Process(double delta)
		{
			if (Input.IsActionPressed("mainMenu")) GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
		}
	}
}
