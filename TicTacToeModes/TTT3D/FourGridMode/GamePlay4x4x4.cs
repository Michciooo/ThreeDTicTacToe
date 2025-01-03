	using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace threeDTicTacToe;

public partial class GamePlay4x4x4 : Control
{
	public List<Button> TttBtns = new List<Button>(64);
	private List<Button> Buttons = new List<Button>();
	public List<Label3D> Labels = new List<Label3D>(64);
	public List<MeshInstance3D> MeshInstances = new List<MeshInstance3D>(64);

	private bool _playerTurn = true;
	private String statsPath = "settings.json";
	bool win = false;
	private int moves = 64;

	Button[,,] board = new Button[4,4,4];

	private int SimulateBoard(Button[,,] tttBoard)
	{
		for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
				// horizontal i vertical 2D
				if (tttBoard[y, 0, x].Text == tttBoard[y, 1, x].Text &&
				    tttBoard[y, 1, x].Text == tttBoard[y, 2, x].Text &&
				    tttBoard[y, 2, x].Text == tttBoard[y, 3, x].Text)
				{
					if (tttBoard[y, 0, x].Text == "X") return -10;
					if (tttBoard[y, 0, x].Text == "O") return 10;

					if (tttBoard[y, 1, x].Text == "X") return -10;
					if (tttBoard[y, 1, x].Text == "O") return 10;

					if (tttBoard[y, 2, x].Text == "X") return -10;
					if (tttBoard[y, 2, x].Text == "O") return 10;

					if (tttBoard[y, 3, x].Text == "X") return -10;
					if (tttBoard[y, 3, x].Text == "O") return 10;
				}
				if (tttBoard[0, y, x].Text == tttBoard[1, y, x].Text &&
				    tttBoard[1, y, x].Text == tttBoard[2, y, x].Text &&
				    tttBoard[2, y, x].Text == tttBoard[3, y, x].Text)
				{
					if (tttBoard[0, y, x].Text == "X") return -10;
					if (tttBoard[0, y, x].Text == "O") return 10;

					if (tttBoard[1, y, x].Text == "X") return -10;
					if (tttBoard[1, y, x].Text == "O") return 10;

					if (tttBoard[2, y, x].Text == "X") return -10;
					if (tttBoard[2, y, x].Text == "O") return 10;

					if (tttBoard[3, y, x].Text == "X") return -10;
					if (tttBoard[3, y, x].Text == "O") return 10;
				}
				if (tttBoard[y,0, x].Text == tttBoard[y,1, x].Text &&
				    tttBoard[y, 1,x].Text == tttBoard[y,2, x].Text &&
				    tttBoard[y,2, x].Text == tttBoard[y,3,x].Text)
				{
					if(tttBoard[y,0,x].Text == "X") return -10;
					if(tttBoard[y,0,x].Text == "O") return 10;
					
					if(tttBoard[y,1,x].Text == "X") return -10;
					if(tttBoard[y,1,x].Text == "O") return 10;
					
					if(tttBoard[y,2,x].Text == "X") return -10;
					if(tttBoard[y,2,x].Text == "O") return 10;
					
					if(tttBoard[y,3,x].Text == "X") return -10;
					if(tttBoard[y,3,x].Text == "O") return 10;
				}
				// skosy 2D
				if (tttBoard[0, 0, x].Text == tttBoard[1, 1, x].Text &&
				    tttBoard[1, 1, x].Text == tttBoard[2, 2, x].Text &&
				    tttBoard[2, 2, x].Text == tttBoard[3, 3, x].Text)
				{
					if (tttBoard[0, 0, x].Text == "X") return -10;
					if (tttBoard[0, 0, x].Text == "O") return 10;

					if (tttBoard[1, 1, x].Text == "X") return -10;
					if (tttBoard[1, 1, x].Text == "O") return 10;

					if (tttBoard[2, 2, x].Text == "X") return -10;
					if (tttBoard[2, 2, x].Text == "O") return 10;

					if (tttBoard[3, 3, x].Text == "X") return -10;
					if (tttBoard[3, 3, x].Text == "O") return 10;
				}
				// i tak nie dziala XD
				if (tttBoard[0, 3, x].Text == tttBoard[1, 2, x].Text &&
				    tttBoard[1, 2, x].Text == tttBoard[2, 1, x].Text &&
				    tttBoard[2, 1, x].Text == tttBoard[3, 0, x].Text)
				{
					if(tttBoard[0,3,x].Text == "X") return -10;
					if(tttBoard[0,3,x].Text == "O") return 10;
					
					if(tttBoard[1,2,x].Text == "X") return -10;
					if(tttBoard[1,2,x].Text == "O") return 10;
					
					if(tttBoard[2,1,x].Text == "X") return -10;
					if(tttBoard[2,1,x].Text == "O") return 10;
					
					if(tttBoard[3,0,x].Text == "X") return -10;
					if(tttBoard[3,0,x].Text == "O") return 10;
				}
				// horizontal 3D
				if (tttBoard[y, x, 0].Text == tttBoard[y, x, 1].Text &&
				    tttBoard[y, x, 1].Text == tttBoard[y, x, 2].Text &&
				    tttBoard[y, x, 2].Text == tttBoard[y, x, 3].Text)
				{
					if (tttBoard[y, x, 0].Text == "X") return -10;
					if (tttBoard[y, x, 0].Text == "O") return 10;

					if (tttBoard[y, x, 1].Text == "X") return -10;
					if (tttBoard[y, x, 1].Text == "O") return 10;

					if (tttBoard[y, x, 2].Text == "X") return -10;
					if (tttBoard[y, x, 2].Text == "O") return 10;

					if (tttBoard[y, x, 3].Text == "X") return -10;
					if (tttBoard[y, x, 3].Text == "O") return 10;
				}
				// skosy 3D vertical
				if (tttBoard[0, y, 0].Text == tttBoard[1, y, 1].Text &&
				    tttBoard[1, y, 1].Text == tttBoard[2, y, 2].Text &&
				    tttBoard[2, y, 2].Text == tttBoard[3, y, 3].Text)
				{
					if(tttBoard[0, y, 0].Text == "X") return -10;
					if(tttBoard[0, y, 0].Text == "O") return 10;
					
					if(tttBoard[1, y, 1].Text == "X") return -10;
					if(tttBoard[1, y, 1].Text == "O") return 10;
					
					if(tttBoard[2, y, 2].Text == "X") return -10;
					if(tttBoard[2, y, 2].Text == "O") return 10;
					
					if(tttBoard[3, y, 3].Text == "X") return -10;
					if(tttBoard[3, y, 3].Text == "O") return 10;
				}
				// skosy 3D horizontal
				if (tttBoard[y, 0, 0].Text == tttBoard[y, 1, 1].Text &&
				    tttBoard[y, 1, 1].Text == tttBoard[y, 2, 2].Text &&
				    tttBoard[y, 2, 2].Text == tttBoard[y, 3, 3].Text)
				{
					if (tttBoard[y, 0, 0].Text == "X") return -10;
					if (tttBoard[y, 0, 0].Text == "O") return 10;
					
					if (tttBoard[y, 1, 1].Text == "X") return -10;
					if (tttBoard[y, 1, 1].Text == "O") return 10;
					
					if (tttBoard[y, 2, 2].Text == "X") return -10;
					if (tttBoard[y, 2, 2].Text == "O") return 10;
					
					if (tttBoard[y, 3, 3].Text == "X") return -10;
					if (tttBoard[y, 3, 3].Text == "O") return 10;
				}
			}
		}
		// skosy 3D
		if (tttBoard[0, 0, 0].Text == tttBoard[1, 1, 1].Text
		    && tttBoard[1, 1, 1].Text == tttBoard[2, 2, 2].Text &&
		    tttBoard[2, 2, 2].Text == tttBoard[3, 3, 3].Text)
		{
			if (tttBoard[0,0,0].Text == "X") return -10;
			if (tttBoard[0,0,0].Text == "O") return 10;
		}
		if (tttBoard[0, 3, 0].Text == tttBoard[1, 2, 1].Text && 
		    tttBoard[1, 2, 1].Text == tttBoard[2,1, 2].Text &&
		    tttBoard[2, 1, 2].Text == tttBoard[3, 0, 3].Text)
		{
			if (tttBoard[0, 3, 0].Text == "X") return -10;
			if (tttBoard[0, 3, 0].Text == "O") return 10;
		}
		if (tttBoard[3, 3, 0].Text == tttBoard[2, 2, 1].Text && 
		    tttBoard[2, 2, 1].Text == tttBoard[1, 1, 2].Text &&
		    tttBoard[1,1, 2].Text == tttBoard[0,0,3].Text)
		{
			if (tttBoard[3, 3, 0].Text == "X") return -10;
			if (tttBoard[3, 3, 0].Text == "O") return 10;
		}
		if (tttBoard[3, 0, 0].Text == tttBoard[2, 1, 1].Text &&
		    tttBoard[2, 1, 1].Text == tttBoard[1, 2, 2].Text &&
		    tttBoard[1, 2, 2].Text == tttBoard[0, 3, 3].Text)
		{
			if (tttBoard[3, 0, 0].Text == "X") return -10;
			if (tttBoard[3, 0, 0].Text == "O") return 10;
		}
		return 0;
	}
	private int MiniMax(Button[,,] tttBoard, int depth, bool isMaximizing, int maxDepth)
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
			bestScore = 1000;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						if (tttBoard[i,j,k].Text == "")
						{
							tttBoard[i, j, k].Text = "O";
							bestScore = Math.Max(bestScore, MiniMax(tttBoard, depth + 1, false, maxDepth));
							tttBoard[i, j, k].Text = "";
						}
					}
				}
			}
		}
		else
		{
			bestScore = -1000;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						if (tttBoard[i,j,k].Text == "")
						{
							tttBoard[i,j,k].Text = "X";
							bestScore = Math.Min(bestScore, MiniMax(tttBoard, depth + 1, true, maxDepth));
							tttBoard[i,j,k].Text = "";
						}
					}
				}
			}
		}
		return bestScore;
	}
	private Button BestMove()
	{
		var global = GetNode<Global>("/root/Global");
		int bestScore = -1000;
		int moveX = -1, moveY = -1, moveZ = -1;
		
		
		int moveScore = int.MaxValue;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				for (int k = 0; k < 4; k++)
				{
					if (board[i, j, k].Text == "")
					{
						if (global.player13D == "AI Computer")
						{
							board[i, j, k].Text = "X";
							moveScore = MiniMax(board, 0, false, 1);
							board[i, j, k].Text = "";
						}
						if (global.player23D == "AI Computer")
						{
							board[i, j, k].Text = "O";
							moveScore = MiniMax(board, 0, false, 1);
							board[i, j, k].Text = "";
						}
						if (moveScore > bestScore)
						{
							bestScore = moveScore;
							moveX = i;
							moveY = j;
							moveZ = k;
						}
						else if (bestScore == moveScore)
						{
							moveX = i;
							moveY = j;
							moveZ = k;
						}
					}
				}
			}
		}
		var aiButton = board[moveX, moveY, moveZ];
		if (global.player13D == "AI Computer") aiButton.Text = "O";
		if (global.player23D == "AI Computer") aiButton.Text = "X";
		return aiButton;
	}
	private void AddBtnsToList()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				for (int k = 0; k < 4; k++)
				{
					int index = i * 16 + j * 4 + k;
					board[i, j, k] = TttBtns[index];
				}
			}
		}
	}
	private void AddToDictionary()
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		for (int i = 0; i < MeshInstances.Count; i++)
		{
			MeshInstance3D meshInstance3D = MeshInstances[i];
			Button button = TttBtns[i];
			if (i < 64)
			{
				if (!main.BtnAndMeshInstanceDictionary.ContainsKey(button))
				{
					main.BtnAndMeshInstanceDictionary.Add(button , meshInstance3D);
				}
			}
		}
	}
	public override void _Ready()
	{
		Button restartButton = GetNode<Button>("restartBtn");
		var global = GetNode<Global>("/root/Global");
		restartButton.Pressed += RestartGame;
		
		HBoxContainer lay1 = GetNode<HBoxContainer>("firstD/lay1");
		HBoxContainer lay2 = GetNode<HBoxContainer>("firstD/lay2");
		HBoxContainer lay3 = GetNode<HBoxContainer>("firstD/lay3");
		HBoxContainer lay4 = GetNode<HBoxContainer>("firstD/lay4");
		
		HBoxContainer lay5 = GetNode<HBoxContainer>("secondD/lay5");
		HBoxContainer lay6 = GetNode<HBoxContainer>("secondD/lay6");
		HBoxContainer lay7 = GetNode<HBoxContainer>("secondD/lay7");
		HBoxContainer lay8 = GetNode<HBoxContainer>("secondD/lay8");
		
		HBoxContainer lay9 = GetNode<HBoxContainer>("thirdD/lay9");
		HBoxContainer lay10 = GetNode<HBoxContainer>("thirdD/lay10");
		HBoxContainer lay11 = GetNode<HBoxContainer>("thirdD/lay11");
		HBoxContainer lay12 = GetNode<HBoxContainer>("thirdD/lay12");
		
		HBoxContainer lay13 = GetNode<HBoxContainer>("fourthD/lay13");
		HBoxContainer lay14 = GetNode<HBoxContainer>("fourthD/lay14");
		HBoxContainer lay15 = GetNode<HBoxContainer>("fourthD/lay15");
		HBoxContainer lay16 = GetNode<HBoxContainer>("fourthD/lay16");

		if (global.player3DMode == "4x4x4")
		{
			TTT3D main = GetNode<TTT3D>("/root/TTT3D");
			main.Create_Visualisation4x4x4();
			Create_Dimensions4x4x4(lay1, lay2, lay3, lay4, lay5, lay6, lay7, lay8, lay9, lay10, lay11, lay12, lay13,
				lay14, lay15, lay16);
			
			if (global.player13D == "Easy Computer" || global.player23D == "Easy Computer")
			{
				if (global.player13D == "Easy Computer" && _playerTurn)  EasyComputer();
				if (global.player23D == "Easy Computer" && !_playerTurn)  EasyComputer();
			}
			else if (global.player13D == "AI Computer" || global.player23D == "AI Computer")
			{
				if (global.player13D == "AI Computer" && _playerTurn)  AiComputer();
				if (global.player23D == "AI Computer" && !_playerTurn)  AiComputer();
			}
			AddBtnsToList();
		}
		AddToDictionary();
	}
	public void RestartGame()
	{
		var global = GetNode<Global>("/root/Global");
		moves = 64;
		global.ClickSFX("res://sfx/btn_click.wav");
		StandardMaterial3D restartCubeColor = new StandardMaterial3D();
		restartCubeColor.AlbedoColor = new Color("#000000");

		foreach (var meshInstance in MeshInstances) meshInstance.MaterialOverride = restartCubeColor;

		foreach (Button btn in TttBtns) btn.Text = "";
		
		foreach (Label3D label in Labels) label.Text = "";

		BlockBtns(false);
		
		if ((global.player13D == "Human" && global.player23D=="Easy Computer")
		    || (global.player13D == "Easy Computer" && global.player23D=="Human"))
		{
			EasyComputer();
		}

		if ((global.player13D == "Human" && global.player23D == "AI Computer") ||
		    (global.player13D == "AI Computer" && global.player23D == "Human"))
		{
			AiComputer();
		}
	}
	public void Create_Dimensions4x4x4(HBoxContainer lay1, HBoxContainer lay2, 
		HBoxContainer lay3, HBoxContainer lay4, HBoxContainer lay5,
		HBoxContainer lay6, HBoxContainer lay7, HBoxContainer lay8, HBoxContainer lay9 ,
		HBoxContainer lay10, HBoxContainer lay11, HBoxContainer lay12, HBoxContainer lay13,
		HBoxContainer lay14,HBoxContainer lay15, HBoxContainer lay16)
	{
		var global = GetNode<Global>("/root/Global");
		global.FirstDBtn.Clear();
		global.SecondDBtn.Clear();
		global.ThirdDBtn.Clear();
		global.FourthDBtn.Clear();
		for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
				for (int z = 0; z < 4; z++)
				{
					Button btn = new Button();
				
					var theme = new Theme();
					var styleBox = (StyleBoxFlat)GD.Load("res://TicTacToeModes/TTTButtons.tres");
					theme.SetStylebox("normal", "Button", styleBox);
					theme.SetStylebox("disabled", "Button", styleBox);
					btn.Theme = theme;
					
					btn.CustomMinimumSize = new Vector2(100, 100);
					
					if (y == 0)
					{
						if (z == 0) lay1.AddChild(btn);
						if (z == 1) lay2.AddChild(btn);
						if (z == 2) lay3.AddChild(btn);
						if (z == 3) lay4.AddChild(btn);
						global.FirstDBtn.Add(btn);
					}

					if (y == 1)
					{
						if (z == 0) lay5.AddChild(btn);
						if (z == 1) lay6.AddChild(btn);
						if (z == 2) lay7.AddChild(btn);
						if (z == 3) lay8.AddChild(btn);
						global.SecondDBtn.Add(btn);
					}

					if (y == 2)
					{
						if (z == 0) lay9.AddChild(btn);
						if (z == 1) lay10.AddChild(btn);
						if (z == 2) lay11.AddChild(btn);
						if (z == 3) lay12.AddChild(btn);
						global.ThirdDBtn.Add(btn);
					}

					if (y == 3)
					{
						if (z == 0) lay13.AddChild(btn);
						if (z == 1) lay14.AddChild(btn);
						if (z == 2) lay15.AddChild(btn);
						if (z == 3) lay16.AddChild(btn);
						global.FourthDBtn.Add(btn);
					}

					TttBtns.Add(btn);
					Buttons.Add(btn);

					btn.Pressed += () => PlayGame(btn);
					btn.MouseEntered += () => OnMouseEntered(btn);
					btn.MouseExited += () => OnMouseExited(btn);
				}
			}
		}
	}
	private void PlayGame(Button btn)
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		var global = GetNode<Global>("/root/Global");
		for (int i = 0; i < TttBtns.Count; i++)
		{
			if (i < 64)
			{
				Button button = TttBtns[i];
				Label3D label = Labels[i];
				if (!main.BtnAndboxMeshLabel3DDictionary.ContainsKey(button))
				{
					main.BtnAndboxMeshLabel3DDictionary.Add(button, label);
				}
			}
		}
		Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
		Label3D label3D = main.BtnAndboxMeshLabel3DDictionary[btn];
		global.ClickSFX("res://sfx/ttt_btn_click.wav");
		if (global.player13D == "Human" && global.player23D == "Human")
		{
			if (btn.Text == "")
			{
				if (_playerTurn)
				{
					btn.Text = "O";
					playerTurnLabel.Text = "Player Turn : X";
					label3D.Text = btn.Text;
					moves -= 1;
					_playerTurn = false;
				}
				else
				{
					btn.Text = "X";
					playerTurnLabel.Text = "Player Turn : O";
					moves -= 1;
					_playerTurn = true;
					label3D.Text = btn.Text;
				}
			}
			WhoWon();
		}

		if (global.player13D == "Human" && global.player23D == "Easy Computer")
		{
			if (btn.Text == "")
			{
				if (_playerTurn)
				{
					btn.Text = "O";
					label3D.Text = btn.Text;
					playerTurnLabel.Text = "Player Turn : O";
					_playerTurn = false;
					moves -= 1;
					if (WhoWon()) return;
				}
				if(!_playerTurn)
				{
					EasyComputer();
				}
			}
		}
		if (global.player13D == "Easy Computer" && global.player23D == "Human")
		{
			if (btn.Text == "")
			{
				if (!_playerTurn)
				{
					btn.Text = "X";
					label3D.Text = btn.Text;
					playerTurnLabel.Text = "Player Turn : O";
					_playerTurn = true;
					moves -= 1;
					if (WhoWon()) return;
				}
				if(_playerTurn)
				{
					EasyComputer();
				}
			}
		}
		if (global.player13D == "Human" && global.player23D == "AI Computer")
		{
			if (btn.Text == "")
			{
				if (_playerTurn)
				{
					btn.Text = "O";
					label3D.Text = btn.Text;
					playerTurnLabel.Text = "Player Turn : O";
					_playerTurn = false;
					moves -= 1;
					if (WhoWon()) return;
				}
				if(!_playerTurn) AiComputer();
			}
		} 
		if (global.player13D == "AI Computer" && global.player23D == "Human")
		{
			if (btn.Text == "")
			{
				if (!_playerTurn)
				{
					btn.Text = "X";
					label3D.Text = btn.Text;
					playerTurnLabel.Text = "Player Turn : X";
					_playerTurn = true;
					moves -= 1;
					if (WhoWon()) return;
				}
				if(_playerTurn) AiComputer();
			}
		}
	}
	private async void EasyComputer()
	{
		var global = GetNode<Global>("/root/Global");
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		for (int i = 0; i < TttBtns.Count; i++)
		{
			if (i < 64)
			{
				Button button = TttBtns[i];
				Label3D label = Labels[i];
				if (!main.BtnAndboxMeshLabel3DDictionary.ContainsKey(button))
				{
					main.BtnAndboxMeshLabel3DDictionary[button] = label;
				}
			}
		}
		Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
		BlockBtns(true);
		await WaitingMove();
		global.ClickSFX("res://sfx/ttt_btn_click.wav");
		
		Random random = new Random();
		List<Button> availableButtons = TttBtns.Where(b => b.Text == "").ToList();

		if (availableButtons.Count > 0)
		{
			Button computerMove = availableButtons[random.Next(availableButtons.Count)];
			if (global.player13D == "Easy Computer")
			{
				computerMove.Text = "O";
				main.BtnAndboxMeshLabel3DDictionary[computerMove].Text = "O";
				playerTurnLabel.Text = "Player Turn : X";
				_playerTurn = false;
			}
			else if (global.player23D == "Easy Computer")
			{
				computerMove.Text = "X";
				main.BtnAndboxMeshLabel3DDictionary[computerMove].Text = "X";
				playerTurnLabel.Text = "Player Turn : O";
				_playerTurn = true;
			}
			moves -= 1;
			if (WhoWon()) return;
		}
		BlockBtns(false);
	}
	private async void AiComputer()
	{
		var global = GetNode<Global>("/root/Global");
		Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		for (int i = 0; i < TttBtns.Count; i++)
		{
			if (i < 64)
			{
				Button button = TttBtns[i];
				Label3D label = Labels[i];
				if (!main.BtnAndboxMeshLabel3DDictionary.ContainsKey(button))
				{
					main.BtnAndboxMeshLabel3DDictionary[button] = label;
				}
			}
		}
		
		BlockBtns(true);
		await WaitingMove();
		Button aiBtn = BestMove();
		if (aiBtn != null)
		{
			if (global.player13D == "AI Computer")
			{
				aiBtn.Text = "O";
				main.BtnAndboxMeshLabel3DDictionary[aiBtn].Text = "O";
				playerTurnLabel.Text = "Player Turn : X";
				_playerTurn = false;
				global.ClickSFX("res://sfx/ttt_btn_click.wav");
			}
			else if (global.player23D == "AI Computer")
			{
				aiBtn.Text = "X";
				main.BtnAndboxMeshLabel3DDictionary[aiBtn].Text = "X";
				playerTurnLabel.Text = "Player Turn : O";
				_playerTurn = true;
				global.ClickSFX("res://sfx/ttt_btn_click.wav");
			}
			moves -= 1;

			if (WhoWon())
			{
				BlockBtns(true);
			}
			BlockBtns(false);
		}
	}
	private async Task WaitingMove()
	{
		var waitingLabel = GetNode<Label>("/root/TTT3D/leftSide/VBoxContainer/waitingMoveLabel");
		waitingLabel.Text = "THINKING...";
		await Task.Delay(500);
		waitingLabel.Text = "";
		BlockBtns(false);
	}
	private void OnMouseEntered(Button btn)
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		StandardMaterial3D newMaterial = new StandardMaterial3D();
		if (main.BtnAndMeshInstanceDictionary.ContainsKey(btn))
		{
			var meshInstance = main.BtnAndMeshInstanceDictionary[btn];

			if (meshInstance.MaterialOverride is StandardMaterial3D material)
			{
				if (material.AlbedoColor == new Color(0, 255, 0))
					return;
			} 
			newMaterial.AlbedoColor = new Color(255, 0, 0);
			meshInstance.MaterialOverride = newMaterial;
		}
	}

	private void OnMouseExited(Button btn)
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");

		if (main.BtnAndMeshInstanceDictionary.ContainsKey(btn))
		{
			var meshInstance = main.BtnAndMeshInstanceDictionary[btn];

			if (meshInstance.MaterialOverride is StandardMaterial3D material)
			{
				if (material.AlbedoColor == new Color(0, 255, 0))
					return;
				material.AlbedoColor = new Color("#000000");
			}
		}
	}

	private bool WhoWon()
	{
		var global = GetNode<Global>("/root/Global");
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");

		var popUp = GD.Load<PackedScene>("res://WinPopUp/WinPopUp.tscn");
		var scoreScene = GetNode<Score>("/root/TTT3D/leftSide/Score");
		var popUpInstant = popUp.Instantiate();
		global.InitializeWins4x4x4();
		
		StandardMaterial3D wonColor = new StandardMaterial3D();
		wonColor.AlbedoColor = new Color(0,255,0);
		for (int i = 0; i < global.wins4x4x4.GetLength(0); i++)
		{
			if (global.wins4x4x4[i, 0].Text == global.wins4x4x4[i, 1].Text && global.wins4x4x4[i, 1].Text == global.wins4x4x4[i, 2].Text &&
			    global.wins4x4x4[i, 2].Text == global.wins4x4x4[i, 3].Text && (global.wins4x4x4[i, 0].Text == "X" || global.wins4x4x4[i, 0].Text == "O"))
			{
				win = true;
				popUpInstant.GetNode<Label>("winLabel").Text = $"Won : {global.wins4x4x4[i, 0].Text}";
				
				if (global.wins4x4x4[i, 0].Text == "X")
				{
					scoreScene.x_wins += 1;
					if (global.player13D == "Human" && global.player23D != "Human")
					{
						global.ClickSFX("res://sfx/lost_sound.wav");
						global.statistics["allLoses"] += 1;
						global.statistics["oLoses"] += 1;
						global.statistics["loses3D"] += 1;
						global.statistics["loses3D2P"] += 1;
					}
					else if (global.player13D != "Human" && global.player23D == "Human")
					{
						global.ClickSFX("res://sfx/win_sound.wav");
						global.statistics["allWins"] += 1;
						global.statistics["xWins"] += 1;
						global.statistics["wins3D"] += 1;
						global.statistics["wins3D2P"] += 1;
					}
					else
					{
						global.ClickSFX("res://sfx/end_of_game.mp3");
					}
				}
				else if (global.wins4x4x4[i, 0].Text == "O")
				{
					scoreScene.o_wins += 1;

					if (global.player13D != "Human" && global.player23D == "Human")
					{
						global.ClickSFX("res://sfx/lost_sound.wav");
						global.statistics["allLoses"] += 1;
						global.statistics["xLoses"] += 1;
						global.statistics["loses3D"] += 1;
						global.statistics["loses3D2P"] += 1;
					}
					else if (global.player13D == "Human" && global.player23D != "Human")
					{
						global.ClickSFX("res://sfx/win_sound.wav");
						global.statistics["allWins"] += 1;
						global.statistics["oWins"] += 1;
						global.statistics["wins3D"] += 1;
						global.statistics["wins3D2P"] += 1;
					}
					else
					{
						global.ClickSFX("res://sfx/end_of_game.mp3");
					}
				}
				File.WriteAllText(statsPath, JsonSerializer.Serialize(global.data));
				
				scoreScene.ScoreSystem();

				main.BtnAndMeshInstanceDictionary[global.wins4x4x4[i, 0]].MaterialOverride = wonColor;
				main.BtnAndMeshInstanceDictionary[global.wins4x4x4[i, 1]].MaterialOverride = wonColor;
				main.BtnAndMeshInstanceDictionary[global.wins4x4x4[i, 2]].MaterialOverride = wonColor;
				main.BtnAndMeshInstanceDictionary[global.wins4x4x4[i, 3]].MaterialOverride = wonColor;
				
				GetTree().Root.AddChild(popUpInstant);
				BlockBtns(true);
				return true;
			}
		}
		if (moves == 0)
		{
			win = false;
			popUpInstant.GetNode<Label>("winLabel").Text = "Draw !";
			global.ClickSFX("res://sfx/end_of_game.mp3");
			GetTree().Root.AddChild(popUpInstant);
			BlockBtns(true);
			return true;
		}
		return false;
	}
	private void BlockBtns(bool disable)
	{
		var restartBtn = GetNode<Button>("restartBtn");
		Buttons.Add(restartBtn);
		foreach (var button in Buttons) button.Disabled = disable;
		win = false;
	}
	public override void _Process(double delta)
	{
	}
}