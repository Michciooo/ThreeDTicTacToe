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
	
	private Dictionary<String , int> MiniMaxScore = new Dictionary<String , int>();
	
	private List<Button> AvaiableButtons()
	{
		List<Button> avaiableButtons = new List<Button>();
		foreach (var button in ticTacToeButtons)
		{
			if (button.Text == "")
			{
				avaiableButtons.Add(button);
			}
		}
		GD.Print(string.Join(',', avaiableButtons));
		return avaiableButtons;
	}

	private int EvaluateBoard()
	{
		int[,] board = new int[3, 3];
		int player = 1;
		int ai = -1;
		int draw = 0;
		
		for (int i = 0; i < 3; i++)
		{
			if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
			{
				if (board[i, 0] == player)
				{
					MiniMaxScore.Add("X", player);
					return player;
				}

				if (board[i, 0] == ai)
				{
					MiniMaxScore.Add("O", ai);
					return ai;
				}
			}

			if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
			{
				if (board[0, i] == player)
				{
					MiniMaxScore.Add("X", player);
					return player;
				}
				if (board[0, i] == ai)
				{
					MiniMaxScore.Add("O", ai);
					return ai;
				}
			}
		}

		if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
		{
			if (board[0, 0] == player) 
			{
				MiniMaxScore.Add("X", player);
				return player;
			}
			if (board[0, 0] == ai)
			{
				MiniMaxScore.Add("O", ai);
				return ai;
			}
		}

		if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
		{
			if (board[0, 2] == player)
			{
				MiniMaxScore.Add("X", player);
				return player;
			}
			if (board[0, 2] == ai)
			{
				MiniMaxScore.Add("O", ai);
				return ai;
			}
		}

		return draw;
	}

	private int MiniMax(int depth, bool isMaximizingPlayer)
	{
		int score = EvaluateBoard();

		return score;
	}
	public override void _Ready()
	{
		AddBtnsToList();
		var global = GetNode<Global>("/root/Global");
		foreach (var button in ticTacToeButtons)
		{
			if (global.buttonName2D == "OfflineBtn") button.Pressed += () => Offline(button);
			if (global.buttonName2D == "EasyModeBtn") button.Pressed += () => EasyComputer(button);
			if (global.buttonName2D == "AiModeBtn") button.Pressed += () => AiComputer(button);
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
		if (global.buttonName2D == "AiModeBtn")
		{
			if (clicks % 2 != 0)
			{
				AiComputer();
			}
			else
			{
				AiComputer();
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

	private void AiComputer(Button button = null)
	{
		button.Text = "X";
		var avaiableMoves = AvaiableButtons();
		foreach (var element in MiniMaxScore)
		{
			GD.Print($"Klucz : {element.Key} , Wartosc : {element.Value}");
		}
		MiniMax(avaiableMoves.Count , true);
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
				if (wins[i,0].Text == "O") scoreScene.o_wins += 1;

				scoreScene.ScoreSystem();
				GetTree().Root.AddChild(popUpInstant);
				return true;
			}
		}
		if (moves == 0)
		{
			win = false;
			var popUpInstant = popUp.Instantiate();
			popUpInstant.GetNode<Label>("winLabel").Text = "Draw !";
			GetTree().Root.AddChild(popUpInstant);
			return true;
		}

		return false;
	}
	private void AddBtnsToList()
	{
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn1"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn2"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn3"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn4"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn5"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn6"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn7"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn8"));
		ticTacToeButtons.Add(GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn9"));
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
