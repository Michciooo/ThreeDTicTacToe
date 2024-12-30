using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace threeDTicTacToe;

public partial class TTT2D : Control
{	
	private String statsPath = "info.json";
	private List<Button> ticTacToeButtons = new List<Button>(9);
	private List<Button> allBtns = new List<Button>();
	private bool playerTurn;
	bool win = false;
	private int moves = 9;
	
	private string aiSymbol;
	private string playerSymbol;
	
	Button[,] board = new Button[3, 3];
	int[] bestMove = new int[2];
	
	
	private int SimulateBoard(Button[,] tttBoard)
	{
		for (int i = 0; i < 3; i++)
		{
			if (tttBoard[i, 0].Text == tttBoard[i, 1].Text && tttBoard[i, 1].Text == tttBoard[i, 2].Text && !string.IsNullOrEmpty(tttBoard[i, 0].Text))
			{
				if (tttBoard[i, 0].Text == aiSymbol) return 10;
				if (tttBoard[i, 0].Text == playerSymbol) return -10;
			}
			if (tttBoard[0, i].Text == tttBoard[1, i].Text && tttBoard[1, i].Text == tttBoard[2, i].Text && !string.IsNullOrEmpty(tttBoard[0, i].Text))
			{
				if (tttBoard[0, i].Text == aiSymbol) return 10;
				if (tttBoard[0, i].Text == playerSymbol) return -10;
			}
		}
		if (tttBoard[0, 0].Text == tttBoard[1, 1].Text && tttBoard[1, 1].Text == tttBoard[2, 2].Text && !string.IsNullOrEmpty(tttBoard[0, 0].Text))
		{
			if (tttBoard[0, 0].Text == aiSymbol) return 10;
			if (tttBoard[0, 0].Text == playerSymbol) return -10;
		}
		if (tttBoard[0, 2].Text == tttBoard[1, 1].Text && tttBoard[1, 1].Text == tttBoard[2, 0].Text && !string.IsNullOrEmpty(tttBoard[0, 2].Text))
		{
			if (tttBoard[0, 2].Text == aiSymbol) return 10;
			if (tttBoard[0, 2].Text == playerSymbol) return -10;
		}

		return 0;
	}
	private int MiniMax(Button[,] tttBoard, int depth, bool isMaximizing, int maxDepth)
	{
		int score = SimulateBoard(tttBoard);
		
		if (score == 10 || score == -10 || depth == maxDepth) return score;

		if (!tttBoard.Cast<Button>().Any(b => b.Text == "")) return 0;

		int bestScore = isMaximizing ? -1000 : 1000;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (tttBoard[i, j].Text == "")
				{
					tttBoard[i, j].Text = isMaximizing ? aiSymbol : playerSymbol;

					int currentScore = MiniMax(tttBoard, depth + 1, !isMaximizing, maxDepth);

					bestScore = isMaximizing
						? Math.Max(bestScore, currentScore)
						: Math.Min(bestScore, currentScore);

					tttBoard[i, j].Text = "";
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
					board[i, j].Text = aiSymbol;

					int moveScore = MiniMax(board, 0, false, 5);

					board[i, j].Text = "";

					if (moveScore >= bestScore)
					{
						bestScore = moveScore;
						moveX = i;
						moveY = j;
					}
				}
			}
		}
		if (moveX != -1 && moveY != -1)
		{
			board[moveX, moveY].Text = aiSymbol;
			return board[moveX, moveY];
		}
		return null;
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

		if (global.player12D == "AI Computer")
		{
			aiSymbol = "O";
			playerSymbol = "X";
		}
		else if (global.player22D == "AI Computer")
		{
			aiSymbol = "X";
			playerSymbol = "O";
		}
		
		foreach (var button in ticTacToeButtons) button.Pressed += () => PlayGame(button);

		if (global.player12D == "Easy Computer" || global.player22D == "Easy Computer")
		{
			playerTurn = global.player22D == "Easy Computer";
			if (!playerTurn) EasyComputer();
		}

		if (global.player12D == "AI Computer" || global.player22D == "AI Computer")
		{
			playerTurn = global.player22D == "AI Computer";
			if (!playerTurn) AiComputer();
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
		global.ClickSFX("res://sfx/btn_click.wav");
		
		foreach (var button in ticTacToeButtons)
		{
			button.Text = "";
			restartBtn.Text = "Restart";
		}
		BlockBtns(false);
		moves = 9;
		
		if ((global.player12D == "Human" && global.player22D=="Easy Computer")
		    || (global.player12D == "Easy Computer" && global.player22D=="Human")) EasyComputer();

		if ((global.player12D == "Human" && global.player22D == "AI Computer") ||
		    (global.player12D == "AI Computer" && global.player22D == "Human")) AiComputer();
	}
	private void PlayGame(Button button)
	{
	    var playerTurnLabel = GetNode<Label>("rightSide/Info/playerTurn");
	    var global = GetNode<Global>("/root/Global");

	    global.ClickSFX("res://sfx/ttt_btn_click.wav");
	    
	    if (button.Text == "")
	    {
		    if (global.player12D == "Human" && global.player22D == "Human")
		    {
			    if (!playerTurn)
			    {
				    button.Text = "O";
				    playerTurnLabel.Text = "Player Turn : X";
				    playerTurn = true;
				    moves -= 1;

				    if (WhoWon())
				    {
					    BlockBtns(true);
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
					    BlockBtns(true);
					    return;
				    }
			    }
		    }
		    else if ((global.player12D == "Human" && global.player22D == "AI Computer") ||
		        (global.player12D == "AI Computer" && global.player22D == "Human"))
		    {
	            if (playerTurn)
	            {
		            if (global.player12D == "Human")
		            {
			            button.Text = "O";
			            playerTurnLabel.Text = "Player Turn : X";
		            }
		            if (global.player22D == "Human")
		            {
			            button.Text = "X";
			            playerTurnLabel.Text = "Player Turn : O";
		            }
		            playerTurn = false;
	                moves -= 1;

	                if (WhoWon())
	                {
	                    BlockBtns(true);
	                    return;
	                }

	                BlockBtns(true);
	                AiComputer();
	            }
	        }
		    else if((global.player12D == "Human" && global.player22D == "Easy Computer") ||
		            (global.player12D == "Easy Computer" && global.player22D == "Human"))
		    {
			    if (playerTurn)
			    {
				    if (global.player12D == "Human")
				    {
					    button.Text = "O";
					    playerTurnLabel.Text = "Player Turn : X";
				    }
				    if (global.player22D == "Human")
				    {
					    button.Text = "X";
					    playerTurnLabel.Text = "Player Turn : O";
				    }
				    playerTurn = false;
				    moves -= 1;

				    if (WhoWon())
				    {
					    BlockBtns(true);
					    return;
				    }

				    BlockBtns(true);
				    EasyComputer();
			    }
		    }
	    }
	}
	private async void EasyComputer()
	{
		var playerTurnLabel = GetNode<Label>("rightSide/Info/playerTurn");
		var global = GetNode<Global>("/root/Global");
		
		BlockBtns(true);
		await WaitingMove();
		global.ClickSFX("res://sfx/ttt_btn_click.wav");
		var random = new Random();
		List<Button> availableButtons = ticTacToeButtons.Where(b => b.Text == "").ToList();

		if (availableButtons.Count > 0)
		{
			Button computerMove = availableButtons[random.Next(availableButtons.Count)];
			if (global.player12D == "Easy Computer")
			{
				computerMove.Text = "O";
				playerTurnLabel.Text = "Player Turn : X";
			}
			if (global.player22D == "Easy Computer")
			{
				computerMove.Text = "X";
				playerTurnLabel.Text = "Player Turn : O";
			}
			moves -= 1;
			playerTurn = true;
		}
		if (WhoWon())
		{
			BlockBtns(true);
			return;
		}
		BlockBtns(false);
	}
	private async void AiComputer()
	{
		var playerTurnLabel = GetNode<Label>("rightSide/Info/playerTurn");
		var global = GetNode<Global>("/root/Global");
		
		BlockBtns(true);
		await WaitingMove();
		global.ClickSFX("res://sfx/ttt_btn_click.wav");
		Button aiBtn = BestMove();
		if (aiBtn != null)
		{
			if (global.player12D == "AI Computer")
			{ 
				aiBtn.Text = "O";
				playerTurnLabel.Text = "Player Turn : X";
			}
			if (global.player22D == "AI Computer")
			{ 
				aiBtn.Text = "X";
				playerTurnLabel.Text = "Player Turn : O";
			}
			moves -= 1;
			playerTurn = true;
			if (WhoWon()) BlockBtns(true);
		}
	}
	private async Task WaitingMove()
	{
		var waitingLabel = GetNode<Label>("rightSide/waitingMoveLabel");
		waitingLabel.Text = "THINKING...";
		await Task.Delay(500);
		waitingLabel.Text = "";
		BlockBtns(false);
	}
	private void BlockBtns(bool block)
	{
		foreach (var button in allBtns) button.Disabled = block;
	}
	private bool WhoWon()
	{
		var global = GetNode<Global>("/root/Global");
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

				if (wins[i, 0].Text == "X")
				{
					scoreScene.x_wins += 1;
					
					if (global.player12D == "Human" && global.player22D != "Human")
					{
						global.content["allLoses"] += 1;
						global.content["loses2D"] += 1;
						global.content["oLoses"] += 1;
						
						global.ClickSFX("res://sfx/lost_sound.wav");
					}
					if (global.player22D == "Human" && global.player12D != "Human")
					{
						global.content["allWins"] += 1;
						global.content["wins2D"] += 1;
						global.content["xWins"] += 1;
						
						global.ClickSFX("res://sfx/win_sound.wav");
					}
					File.WriteAllText(statsPath, JsonSerializer.Serialize(global.content));
				}

				if (wins[i, 0].Text == "O")
				{
					scoreScene.o_wins += 1;
					
					if (global.player12D == "Human" && global.player22D != "Human")
					{
						global.content["allWins"] += 1;
						global.content["wins2D"] += 1;
						global.content["oWins"] += 1;
						
						global.ClickSFX("res://sfx/win_sound.wav");
					}
					if (global.player22D == "Human" && global.player12D != "Human")
					{
						global.content["allLoses"] += 1;
						global.content["loses2D"] += 1;
						global.content["xLoses"] += 1;
						
						global.ClickSFX("res://sfx/lost_sound.wav");
					}
					File.WriteAllText(statsPath, JsonSerializer.Serialize(global.content));
				}

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
		var global = GetNode<Global>("/root/Global");
		var mainMenuBtn = this.GetNode<Button>("leftSide/mainMenu");
		if (mainMenuBtn.IsPressed())
		{
			WinPopUp popUp = GetNodeOrNull<WinPopUp>("/root/WinPopUp");
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu2D/MainMenu2D.tscn");
			if(popUp != null) popUp.QueueFree();
		}
	}
}
