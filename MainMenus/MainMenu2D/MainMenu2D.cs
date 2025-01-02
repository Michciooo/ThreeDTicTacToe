using Godot;
using System;
using System.Collections.Generic;

namespace threeDTicTacToe
{
	public partial class MainMenu2D : Control
	{
		public override void _Ready()
		{
			var global = (Global)GetNode("/root/Global");
			
			var playerOneBtn = GetNode<OptionButton>("Main/MainContainer/Container/left/playerOneBtn");
			var playerTwoBtn = GetNode<OptionButton>("Main/MainContainer/Container/right/playerTwoBtn");
			var playBtn = GetNode<Button>("Main/MainContainer/playBtn");

			Input.MouseMode = Input.MouseModeEnum.Confined;
			playerOneBtn.ItemSelected += FirstPlayerSelect;
			playerTwoBtn.ItemSelected += SecondPlayerSelect;

			var firstPIndex = -1;
			var secondPIndex = -1;

			if (global.playersTypes2D[1] == "Human") firstPIndex = 0;
			else if (global.playersTypes2D[1] == "Easy Computer") firstPIndex = 1;
			else if(global.playersTypes2D[1] == "AI Computer") firstPIndex = 2;
			
			if(global.playersTypes2D[2] == "Human") secondPIndex = 0;
			else if(global.playersTypes2D[2] == "Easy Computer") secondPIndex = 1;
			else if(global.playersTypes2D[2] == "AI Computer") secondPIndex = 2;

			playerOneBtn.Selected = firstPIndex;
			playerTwoBtn.Selected = secondPIndex;
			
			playBtn.Pressed += PlayBtnOnPressed;
		}

		private void PlayBtnOnPressed()
		{
			var global = GetNode<Global>("/root/Global");
			var warningLabel = GetNode<Label>("Main/MainContainer/warningLabel");

			if ((global.playersTypes2D[1] == global.playersTypes2D[2] && global.playersTypes2D[1] == "Easy Computer") || 
			    (global.playersTypes2D[1] == global.playersTypes2D[2] && global.playersTypes2D[1] == "AI Computer") || 
			    (global.playersTypes2D[1] == "Easy Computer" && global.playersTypes2D[2] == "AI Computer") ||  
			    (global.playersTypes2D[1] == "AI Computer" && global.playersTypes2D[2] == "Easy Computer"))
			{
				warningLabel.Text = "The battle between 2 computers is not allowed.";
			}
			else if ((global.playersTypes2D[1] == global.playersTypes2D[2] && global.playersTypes2D[1] == "Human") || 
			         (global.playersTypes2D[1] == "Human" && global.playersTypes2D[2] == "Easy Computer") ||
			         (global.playersTypes2D[1] == "Easy Computer" && global.playersTypes2D[2] == "Human") || 
			         (global.playersTypes2D[1] == "Human" && global.playersTypes2D[2] == "AI Computer") ||
			         (global.playersTypes2D[1] == "AI Computer" && global.playersTypes2D[2] == "Human"))
			{
				global.player12D = global.playersTypes2D[1];
				global.player22D = global.playersTypes2D[2];
				GetTree().ChangeSceneToFile("res://TicTacToeModes/TTT2D/TTT2D.tscn");
			}
			global.ClickSFX("res://sfx/btn_click.wav");
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
			if(global.playersTypes2D.ContainsKey(1)) global.playersTypes2D[1] = playerOneBtn.Text;
			else global.playersTypes2D.Add(1,playerOneBtn.Text);
		}
		private void SecondPlayerSelect(long index)
		{
			var global = GetNode<Global>("/root/Global");
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
			global.ClickSFX("res://sfx/btn_click.wav");
			if(global.playersTypes2D.ContainsKey(2)) global.playersTypes2D[2] = playerTwoBtn.Text;
			else global.playersTypes2D.Add(2,playerTwoBtn.Text);
		}
		public override void _Process(double delta)
		{
			var global = GetNode<Global>("/root/Global");
			if (Input.IsActionPressed("mainMenu"))
			{
				global.ClickSFX("res://sfx/btn_click.wav");
				GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
			}
		}
	}
}
