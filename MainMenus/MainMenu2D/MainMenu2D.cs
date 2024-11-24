using Godot;
using System;
using System.Collections.Generic;

namespace threeDTicTacToe
{
	public partial class MainMenu2D : Control
	{
		private Dictionary<int, string> playersTypes = new Dictionary<int, string>
		{
			{ 1, "Human" },
			{ 2, "Human" }
		};
		public override void _Ready()
		{
			var playerOneBtn = GetNode<OptionButton>("Main/MainContainer/Container/left/playerOneBtn");
			var playerTwoBtn = GetNode<OptionButton>("Main/MainContainer/Container/right/playerTwoBtn");
			var playBtn = GetNode<Button>("Main/MainContainer/playBtn");

			Input.MouseMode = Input.MouseModeEnum.Confined;
			playerOneBtn.ItemSelected += FirstPlayerSelect;
			playerTwoBtn.ItemSelected += SecondPlayerSelect;
			playBtn.Pressed += PlayBtnOnPressed;
		}

		private void PlayBtnOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			var warningLabel = GetNode<Label>("Main/MainContainer/warningLabel");

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
				global.player12D = playersTypes[1];
				global.player22D = playersTypes[2];
				GetTree().ChangeSceneToFile("res://TicTacToeModes/TTT2D/TTT2D.tscn");
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
			var playerTwoBtn = GetNode<OptionButton>("Main/MainContainer/Container/right/playerTwoBtn");
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
		public override void _Process(double delta)
		{
			if (Input.IsActionPressed("mainMenu")) GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
		}
	}
}
