using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace threeDTicTacToe;

public partial class TTT2D : Control
{	
	private List<Button> ticTacToeButtons = new List<Button>(9);
	private List<Button> allBtns = new List<Button>();
	private bool playerTurn;
	bool win = false;
	private int moves = 9;
	
	Button[,] board = new Button[3, 3];
	int[] bestMove = new int[2];
	private int SimulateBoard(Button[,] tttBoard)
	{
		for (int i = 0; i < 3; i++)
		{
			if (tttBoard[i, 0].Text == tttBoard[i, 1].Text && tttBoard[i, 1].Text == tttBoard[i, 2].Text)
			{
				if (tttBoard[i, 0].Text == "X") return 10;
				if (tttBoard[i, 0].Text == "O") return -10;
			}
			if (tttBoard[0, i].Text == tttBoard[1, i].Text && tttBoard[1, i].Text == tttBoard[2, i].Text)
			{
				if (tttBoard[0, i].Text == "X") return 10;
				if (tttBoard[0, i].Text == "O") return -10;
			}
		}
		if (tttBoard[0, 0].Text == tttBoard[1, 1].Text && tttBoard[1, 1].Text == tttBoard[2, 2].Text)
		{
			if (tttBoard[0, 0].Text == "X") return 10;
			if (tttBoard[0, 0].Text == "O") return -10;
		}
		if (tttBoard[0, 2].Text == tttBoard[1, 1].Text && tttBoard[1, 1].Text == tttBoard[2, 0].Text)
		{
			if (tttBoard[0, 2].Text == "X") return 10;
			if (tttBoard[0, 2].Text == "O") return -10;
		}

		return 0;
	}
	private int MiniMax(Button[,] tttBoard, int depth, bool isMaximizing, int maxDepth)
	{
		if (depth == maxDepth)
			return SimulateBoard(tttBoard);

		int score = SimulateBoard(tttBoard);
		if (score == 10 || score == -10) return score; 

		if (!tttBoard.Cast<Button>().Any(b => b.Text == ""))
			return 0; 

		int bestScore;
		if (isMaximizing)
		{
			bestScore = -1000;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (tttBoard[i, j].Text == "")
					{
						tttBoard[i, j].Text = "X";
						bestScore = Math.Max(bestScore, MiniMax(tttBoard, depth + 1, false, maxDepth));
						tttBoard[i, j].Text = "";
					}
				}
			}
		}
		else
		{
			bestScore = 1000;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (tttBoard[i, j].Text == "")
					{
						tttBoard[i, j].Text = "O";
						bestScore = Math.Min(bestScore, MiniMax(tttBoard, depth + 1, true, maxDepth));
						tttBoard[i, j].Text = "";
					}
				}
			}
		}

		return bestScore;
	}
	private Button BestMove()
	{
		int bestScore = -1000;
		int moveX = -1, moveY = -1;
		
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (board[i, j].Text == "")
				{
					board[i, j].Text = "X";
					int moveScore = MiniMax(board, 0, false,5); 
					board[i, j].Text = "";

					if (moveScore > bestScore)
					{
						bestScore = moveScore;
						moveX = i;
						moveY = j;
					}
				}
			}
		}
		var aiButton = board[moveX, moveY];
		aiButton.Text = "X";
		return aiButton;
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
		
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				board[i, j] = ticTacToeButtons[i * 3 + j];
			}
		}
	}

	public override void _Ready()
	{
		AddBtnsToList();
		var global = GetNode<Global>("/root/Global");

		foreach (var button in ticTacToeButtons)
		{
			if ((global.player12D == "Human" && global.player22D == "Human")|| 
			     (global.player12D=="Human" && global.player22D=="AI Computer")||
			     (global.player12D=="AI Computer" && global.player22D=="Human")) button.Pressed += () => Human(button);
			
			if ((global.player12D == "Human" && global.player22D == "Easy Computer") ||
			    (global.player12D == "Easy Computer" && global.player22D == "Human"))
				button.Pressed += () => EasyComputer(button);
		}

		if (global.player12D == "Easy Computer" || global.player22D == "Easy Computer")
		{
			playerTurn = global.player22D == "Easy Computer";
			if (!playerTurn)
			{
				EasyComputer();
			}
		}

		if (global.player12D == "AI Computer" || global.player22D == "AI Computer")
		{
			playerTurn = global.player22D == "AI Computer";
			if (!playerTurn)
			{
				AiComputer();
			}
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
		
		if ((global.player12D == "Human" && global.player22D=="Easy Computer")
		    || (global.player12D == "Easy Computer" && global.player22D=="Human"))
		{
			EasyComputer();
		}

		if ((global.player12D == "Human" && global.player22D == "AI Computer") ||
		    (global.player12D == "AI Computer" && global.player22D == "Human"))
		{
			AiComputer();
		}
	}
	private void Human(Button button)
	{
		var playerTurnLabel = GetNode<Label>("rightSide/Info/playerTurn");
		var global = GetNode<Global>("/root/Global");
		if (button.Text == "")
		{
			if (global.player12D=="Human" && global.player22D=="Human")
			{
				if (!playerTurn)
				{
					button.Text = "O";
					playerTurnLabel.Text = "Player Turn : X";
					playerTurn = true;
					moves -= 1;
					if (WhoWon())
					{
						BlockBtns(true, CursorShape.Arrow);
						return;
					}
				}
				else
				{
					button.Text = "X";
					playerTurnLabel.Text = "Player Turn : O";
					playerTurn = false;
					moves -= 1;
					if (WhoWon())
					{
						BlockBtns(true, CursorShape.Arrow);
						return;
					}
				}
			}
			if ((global.player12D=="Human" && global.player22D=="AI Computer") ||
			    (global.player12D == "AI Computer" && global.player22D=="Human"))
			{
				if (playerTurn)
				{
					button.Text = "O";
					playerTurnLabel.Text = "Player Turn : O";
					playerTurn = false;
					moves -= 1;
					if (WhoWon())
					{
						BlockBtns(true, CursorShape.Arrow);
						return;
					}
				}
				if(!playerTurn)
				{
					AiComputer();
				}
			}
		}
	}
	private void EasyComputer(Button button = null)
	{
		if (playerTurn && button != null && button.Text == "")
		{
			button.Text = "O";
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
				computerMove.Text = "X";

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
	private void AiComputer()
	{
		if (!playerTurn)
		{
			Button aiBtn = BestMove();
			if (aiBtn != null)
			{
				aiBtn.Text = "X";
				moves -= 1;
				playerTurn = true;

				if (WhoWon())
				{
					BlockBtns(true, CursorShape.Arrow);
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
	public override void _Process(double delta)
	{
		var mainMenuBtn = this.GetNode<Button>("leftSide/mainMenu");
		
		if (mainMenuBtn.IsPressed())
		{
			WinPopUp popUp = GetNodeOrNull<WinPopUp>("/root/WinPopUp");
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu2D/MainMenu2D.tscn");
			if(popUp != null) popUp.QueueFree();
		}
	}
}
