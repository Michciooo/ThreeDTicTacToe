using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace threeDTicTacToe;

public partial class TTT2D : Control
{	
	private List<Button> ticTacToeButtons = new List<Button>(9);
	private List<Button> allBtns = new List<Button>();
	private bool playerTurn = true;
	bool win = false;
	private int moves = 9;
	private int clicks = 0;

	public override void _Ready()
	{
		AddBtnsToList();
		var global = GetNode<Global>("/root/Global");
		foreach (var button in ticTacToeButtons)
		{
			if (global.buttonName2D == "OfflineBtn") button.Pressed += () => Offline(button);
			if (global.buttonName2D == "EasyModeBtn") button.Pressed += () => EasyComputer(button);
		}

		var restartBtn = GetNode<Button>("rightSide/Info/restartBtn");
		restartBtn.Pressed += RestartGame;

		allBtns = ticTacToeButtons;
		allBtns.Add(restartBtn);
	}
	public void RestartGame()
	{
		var restartBtn = GetNode<Button>("rightSide/Info/restartBtn");
		var global = GetNode<Global>("/root/Global");
		
		foreach (var button in ticTacToeButtons)
		{
			button.Text = "";
			restartBtn.Text = "Restart";
		}
		BlockBtns(false , CursorShape.PointingHand);
		moves = 9;
		
		if (global.buttonName2D == "EasyModeBtn")
		{
			if (clicks % 2 != 0)
			{
				EasyComputer();
			}
			else
			{
				EasyComputer();
			}
		}
	}
	private void Offline(Button button)
	{
		var playerTurnLabel = GetNode<Label>("rightSide/Info/playerTurn");
		if (button.Text != "") return;
		
		if (playerTurn)
		{
			button.Text = "X";
			playerTurnLabel.Text = "Player Turn : O";
			playerTurn = false;
			moves -= 1;
			if (WhoWon()) BlockBtns(true, CursorShape.Arrow);
		}
		else
		{
			button.Text = "O";
			playerTurnLabel.Text = "Player Turn : X";
			playerTurn = true;
			moves -= 1;
			if(WhoWon()) BlockBtns(true , CursorShape.Arrow);
		}
	}
	private void EasyComputer(Button button = null)
	{
		if (playerTurn && button != null && button.Text == "")
		{
			button.Text = "X";
			playerTurn = false;
			moves -= 1;
			if (WhoWon())
			{
				BlockBtns(true, CursorShape.Arrow);
				return;
			}
		}
		if (!playerTurn)
		{
			var random = new Random();
			List<Button> availableButtons = ticTacToeButtons.Where(b => b.Text == "").ToList();

			if (availableButtons.Count > 0)
			{
				Button computerMove = availableButtons[random.Next(availableButtons.Count)];
				computerMove.Text = "O";

				moves -= 1;
				playerTurn = true;
				if (WhoWon())
				{
					BlockBtns(true, CursorShape.Arrow);
					return;
				}
			}
		}
	}
	private void BlockBtns(bool block , CursorShape cursor)
	{
		foreach (var button in allBtns)
		{
			button.Disabled = block;
			button.MouseDefaultCursorShape = cursor;
		}
	}

	private bool WhoWon()
	{
		Button[,] wins =
		{
			{ticTacToeButtons[0] , ticTacToeButtons[1] , ticTacToeButtons[2]},
			{ticTacToeButtons[3] , ticTacToeButtons[4] , ticTacToeButtons[5]},
			{ticTacToeButtons[6] , ticTacToeButtons[7] , ticTacToeButtons[8]},
			
			{ticTacToeButtons[0] , ticTacToeButtons[3] , ticTacToeButtons[6]},
			{ticTacToeButtons[1] , ticTacToeButtons[4] , ticTacToeButtons[7]},
			{ticTacToeButtons[2] , ticTacToeButtons[5] , ticTacToeButtons[8]},
			
			{ticTacToeButtons[0] , ticTacToeButtons[4] , ticTacToeButtons[8]},
			{ticTacToeButtons[2] , ticTacToeButtons[4] , ticTacToeButtons[6]},
		};
		var popUp = GD.Load<PackedScene>("res://WinPopUp/WinPopUp.tscn");
		var scoreScene = GetNode<Score>("leftSide/Score");
		for (int i = 0; i < wins.GetLength(0); i++)
		{
			if (wins[i, 0].Text == wins[i, 1].Text && wins[i, 1].Text == wins[i, 2].Text &&
			    (wins[i, 0].Text == "X" || wins[i, 0].Text == "O"))
			{
				win = true;
				var popUpInstant = popUp.Instantiate();
				popUpInstant.GetNode<Label>("winLabel").Text = $"Won : {wins[i, 0].Text}";

				if (wins[i, 0].Text == "X") scoreScene.x_wins += 1;
				if(wins[i,0].Text == "O") scoreScene.o_wins += 1;
				
				scoreScene.ScoreSystem();
				GetTree().Root.AddChild(popUpInstant);
				return true;
			}

			if (moves == 0)
			{
				win = false;
				var popUpInstant = popUp.Instantiate();
				popUpInstant.GetNode<Label>("winLabel").Text = "Draw !";
				GetTree().Root.AddChild(popUpInstant);
				return true;
			}
		}
		return false;
	}

	private void AddBtnsToList()
	{
		var btn1 = GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn1");
		var btn2 = GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn2");
		var btn3 = GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn3");
		
		var btn4 = GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn4");
		var btn5 = GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn5");
		var btn6 = GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn6");
		
		var btn7 = GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn7");
		var btn8 = GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn8");
		var btn9 = GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn9");
		
		ticTacToeButtons.Add(btn1);
		ticTacToeButtons.Add(btn2);
		ticTacToeButtons.Add(btn3);
		ticTacToeButtons.Add(btn4);
		ticTacToeButtons.Add(btn5);
		ticTacToeButtons.Add(btn6);
		ticTacToeButtons.Add(btn7);
		ticTacToeButtons.Add(btn8);
		ticTacToeButtons.Add(btn9);
	}
	
	public override void _Process(double delta)
	{
		var mainMenuBtn = this.GetNode<Button>("leftSide/mainMenu");
		
		if (mainMenuBtn.IsPressed())
		{
			WinPopUp popUp = GetNodeOrNull<WinPopUp>("/root/WinPopUp");
			GetTree().ChangeSceneToFile("res://MainMenu2D/MainMenu2D.tscn");
			if(popUp != null) popUp.QueueFree();
		}
	}
}
