using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Godot;

namespace threeDTicTacToe;

public partial class GamePlay : Control
{
	bool _playerTurn = true;
	bool win = false;
	private int moves = 64;
	public List<Button> TttBtns = new List<Button>();
	public List<Button> FirstDBtn = new List<Button>();
	public List<Button> SecondDBtn = new List<Button>();
	public List<Button> ThirdDBtn = new List<Button>();
	private List<Button> Buttons = new List<Button>();

	public List<Label3D> Labels = new List<Label3D>();
	public List<MeshInstance3D> MeshInstances = new List<MeshInstance3D>();
	
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
		
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		if (global.player3DMode == "4x4x4")
		{
			main.Create_Visualisation();
			Create_Dimensions(lay1, lay2, lay3, lay4, lay5, lay6, lay7, lay8, lay9, lay10, lay11, lay12);
		}
		if(global.player3DMode == "3x3x3") GD.Print("3x3x3");

		AddToDictionary();
	}

	public void RestartGame()
	{
		Global global = GetNode<Global>("/root/Global");
		foreach (Button button in TttBtns)
		{
			button.Text = "";
		}

		foreach (Label3D label in Labels)
		{
			label.Text = "";
		}

		Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
		playerTurnLabel.Text = _playerTurn ? "Player turn : O" : "Player turn : X";

		BlockBtns(false);
		foreach (var btn in TttBtns)
		{
			btn.MouseEntered += () => OnMouseEntered(btn);
			btn.SetDefaultCursorShape(CursorShape.PointingHand);
		}
	}
	public void Create_Dimensions(HBoxContainer lay1, HBoxContainer lay2, 
		HBoxContainer lay3, HBoxContainer lay4, HBoxContainer lay5,
		HBoxContainer lay6, HBoxContainer lay7, HBoxContainer lay8, HBoxContainer lay9 ,
		HBoxContainer lay10, HBoxContainer lay11, HBoxContainer lay12)
	{
		Global global = GetNode<Global>("/root/Global");
		for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
				for (int z = 0; z < 4; z++)
				{
					Button btn = new Button();
					btn.SetDefaultCursorShape(CursorShape.PointingHand);
					btn.CustomMinimumSize = new Vector2(100, 100);

					StyleBoxFlat bgColor = new StyleBoxFlat();
					
					if (y == 0)
					{
						if (z == 0) lay1.AddChild(btn);
						if (z == 1) lay2.AddChild(btn);
						if (z == 2) lay3.AddChild(btn);
						if (z == 3) lay4.AddChild(btn);
						FirstDBtn.Add(btn);
						bgColor.BgColor = new Color("0013ff");
						btn.AddThemeStyleboxOverride("normal", bgColor);
					}

					if (y == 1)
					{
						if (z == 0) lay5.AddChild(btn);
						if (z == 1) lay6.AddChild(btn);
						if (z == 2) lay7.AddChild(btn);
						if (z == 3) lay8.AddChild(btn);
						SecondDBtn.Add(btn);
						bgColor.BgColor = new Color("#ff00e8");
						btn.AddThemeStyleboxOverride("normal", bgColor);
					}

					if (y == 2)
					{
						if (z == 0) lay9.AddChild(btn);
						if (z == 1) lay10.AddChild(btn);
						if (z == 2) lay11.AddChild(btn);
						if (z == 3) lay12.AddChild(btn);
						ThirdDBtn.Add(btn);
						bgColor.BgColor = new Color("#fff300");
						btn.AddThemeStyleboxOverride("normal", bgColor);
					}

					TttBtns.Add(btn);
					Buttons.Add(btn);

					if (global.player13D == "Human" && global.player23D=="Human") btn.Pressed += () => Offline(btn);
					if (global.player12D == "EasyModeBtn")
					{
						Button capturedButton = btn; 
						capturedButton.Pressed += () => EasyComputer(capturedButton);
					}
				
					if (global.player12D == "AiModeBtn")
					{
						Button capturedButton = btn; 
						capturedButton.Pressed += () => AiComputer(capturedButton);
					}
					btn.MouseEntered += () => OnMouseEntered(btn);
					btn.MouseExited += () => OnMouseExited(btn);
				}
			}
		}
	}
	public void EasyComputer(Button btn = null)
	{
		if (win || WhoWon())
		{
			return;
		}
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
		if (_playerTurn && btn != null && btn.Text == "")
		{
			btn.Text = "O";
			Label3D label3D = main.BtnAndboxMeshLabel3DDictionary[btn];
			label3D.Text = "O";
			playerTurnLabel.Text = "Player turn : X";

			moves -= 1;
			_playerTurn = false;
			if (WhoWon()) return;
		}

		if (!_playerTurn)
		{
			Random random = new Random();
			List<Button> availableButtons = TttBtns.Where(b => b.Text == "").ToList();

			if (availableButtons.Count > 0)
			{
				Button computerMove = availableButtons[random.Next(availableButtons.Count)];
				computerMove.Text = "X";
				main.BtnAndboxMeshLabel3DDictionary[computerMove].Text = "X";
				playerTurnLabel.Text = "Player turn : O";

				moves -= 1;
				_playerTurn = true;
				if (WhoWon()) return;
			}
		}
	}
	public void Offline(Button btn)
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		
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
		if (btn.Text == "")
		{
			if (_playerTurn)
			{
				btn.Text = "O";
				playerTurnLabel.Text = "Player turn : X";
				label3D.Text = btn.Text;
				moves -= 1;
				_playerTurn = false;
			}
			else
			{
				btn.Text = "X";
				playerTurnLabel.Text = "Player turn : O";
				moves -= 1;
				_playerTurn = true;
				label3D.Text = btn.Text;
			}
		}

		WhoWon();
	}

	private void AiComputer(Button btn = null)
	{
		GD.Print("AI Computer");
	}
	public void OnMouseEntered(Button btn)
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		if (main.BtnAndMeshInstanceDictionary.ContainsKey(btn))
		{
			StandardMaterial3D miniMaterial = new StandardMaterial3D();
			miniMaterial.AlbedoColor = new Color(255, 0, 0);
			main.BtnAndMeshInstanceDictionary[btn].MaterialOverride = miniMaterial;
		}
	}
	public void OnMouseExited(Button btn)
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		if (main.BtnAndMeshInstanceDictionary.ContainsKey(btn))
		{
			StandardMaterial3D miniMaterial = new StandardMaterial3D();
			miniMaterial.AlbedoColor = new Color("#000000");
			main.BtnAndMeshInstanceDictionary[btn].MaterialOverride = miniMaterial;
		}
	}
	public bool WhoWon()
	{
		Button[,] wins =
		{
			{ FirstDBtn[0], FirstDBtn[1], FirstDBtn[2] },
			{ FirstDBtn[3], FirstDBtn[4], FirstDBtn[5] },
			{ FirstDBtn[6], FirstDBtn[7], FirstDBtn[8] },
			{ FirstDBtn[0], FirstDBtn[3], FirstDBtn[6] },
			{ FirstDBtn[1], FirstDBtn[4], FirstDBtn[7] },
			{ FirstDBtn[2], FirstDBtn[5], FirstDBtn[8] },
			{ FirstDBtn[0], FirstDBtn[4], FirstDBtn[8] },
			{ FirstDBtn[2], FirstDBtn[4], FirstDBtn[6] },
			
			{ SecondDBtn[0], SecondDBtn[1], SecondDBtn[2] },
			{ SecondDBtn[3], SecondDBtn[4], SecondDBtn[5] },
			{ SecondDBtn[6], SecondDBtn[7], SecondDBtn[8] },
			{ SecondDBtn[0], SecondDBtn[3], SecondDBtn[6] },
			{ SecondDBtn[1], SecondDBtn[4], SecondDBtn[7] },
			{ SecondDBtn[2], SecondDBtn[5], SecondDBtn[8] },
			{ SecondDBtn[0], SecondDBtn[4], SecondDBtn[8] },
			{ SecondDBtn[2], SecondDBtn[4], SecondDBtn[6] },
			
			{ ThirdDBtn[0], ThirdDBtn[1], ThirdDBtn[2] },
			{ ThirdDBtn[3], ThirdDBtn[4], ThirdDBtn[5] },
			{ ThirdDBtn[6], ThirdDBtn[7], ThirdDBtn[8] },
			{ ThirdDBtn[0], ThirdDBtn[3], ThirdDBtn[6] },
			{ ThirdDBtn[1], ThirdDBtn[4], ThirdDBtn[7] },
			{ ThirdDBtn[2], ThirdDBtn[5], ThirdDBtn[8] },
			{ ThirdDBtn[0], ThirdDBtn[4], ThirdDBtn[8] },
			{ ThirdDBtn[2], ThirdDBtn[4], ThirdDBtn[6] },
			
			{FirstDBtn[0] , SecondDBtn[0] , ThirdDBtn[0]},
			{FirstDBtn[1] , SecondDBtn[1] , ThirdDBtn[1]},
			{FirstDBtn[2] , SecondDBtn[2] , ThirdDBtn[2]},
			{FirstDBtn[3] , SecondDBtn[3] , ThirdDBtn[3]},
			{FirstDBtn[4] , SecondDBtn[4] , ThirdDBtn[4]},
			{FirstDBtn[5] , SecondDBtn[5] , ThirdDBtn[5]},
			{FirstDBtn[6] , SecondDBtn[6] , ThirdDBtn[6]},
			{FirstDBtn[7] , SecondDBtn[7] , ThirdDBtn[7]},
			{FirstDBtn[8] , SecondDBtn[8] , ThirdDBtn[8]},
			
			{FirstDBtn[0] , SecondDBtn[1] , ThirdDBtn[2]},
			{FirstDBtn[2] , SecondDBtn[1] , ThirdDBtn[0]},
			
			{FirstDBtn[3] , SecondDBtn[4] , ThirdDBtn[5]},
			{FirstDBtn[5] , SecondDBtn[4] , ThirdDBtn[3]},
			
			{FirstDBtn[6] , SecondDBtn[7] , ThirdDBtn[8]},
			{FirstDBtn[8] , SecondDBtn[7] , ThirdDBtn[6]},
			
			{FirstDBtn[0] , SecondDBtn[3] , ThirdDBtn[6]},
			{FirstDBtn[6] , SecondDBtn[3] , ThirdDBtn[0]},
			
			{FirstDBtn[1] , SecondDBtn[4] , ThirdDBtn[7]},
			{FirstDBtn[7] , SecondDBtn[4] , ThirdDBtn[1]},
			
			{FirstDBtn[2] , SecondDBtn[5] , ThirdDBtn[8]},
			{FirstDBtn[8] , SecondDBtn[5] , ThirdDBtn[2]},
			
			{FirstDBtn[0] , SecondDBtn[4] , ThirdDBtn[8]},
			{FirstDBtn[8] , SecondDBtn[4] , ThirdDBtn[0]},
			{FirstDBtn[2] , SecondDBtn[4] , ThirdDBtn[6]},
			{FirstDBtn[6] , SecondDBtn[4] , ThirdDBtn[2]},
		};
		
		var popUp = GD.Load<PackedScene>("res://WinPopUp/WinPopUp.tscn");
		var scoreScene = GetNode<Score>("/root/TTT3D/leftSide/Score");
		for (int i = 0; i < wins.GetLength(0); i++)
		{
			var popUpInstant = popUp.Instantiate();
			if (wins[i, 0].Text == wins[i, 1].Text && wins[i, 1].Text == wins[i, 2].Text &&
			    (wins[i, 0].Text == "X" || wins[i, 0].Text == "O"))
			{
				win = true;
				popUpInstant.GetNode<Label>("winLabel").Text = $"Won : {wins[i, 0].Text}";

				if (wins[i, 0].Text == "X") scoreScene.x_wins += 1;
				else scoreScene.o_wins += 1;
				
				scoreScene.ScoreSystem();
				GetTree().Root.AddChild(popUpInstant);
				BlockBtns(true);
				return true;  
			}

			if ((wins[i, 0].Text != wins[i, 1].Text && wins[i, 1].Text != wins[i, 2].Text &&
			     (wins[i, 0].Text == "X" || wins[i, 0].Text == "O")) && moves == 0)
			{
				win = false;
				popUpInstant.GetNode<Label>("winLabel").Text = "Draw !";
				GetTree().Root.AddChild(popUpInstant);
				BlockBtns(true);
				return true;
			}
		}
		return false;
	}
	private void BlockBtns(bool disable)
	{
		var restartBtn = GetNode<Button>("restartBtn");
		Buttons.Add(restartBtn);
		foreach (var button in Buttons)
		{
			button.Disabled = disable;
			button.SetDefaultCursorShape(CursorShape.Arrow);
		}

		foreach (var btn in TttBtns)
		{
			btn.MouseEntered += () => OnMouseExited(btn);
		}
		win = false;
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
	public override void _Process(double delta)
	{
	}
}