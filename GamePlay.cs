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
		restartButton.Pressed += RestartGame;
		
		HBoxContainer lay1 = GetNode<HBoxContainer>("firstD/lay1");
		HBoxContainer lay2 = GetNode<HBoxContainer>("firstD/lay2");
		HBoxContainer lay3 = GetNode<HBoxContainer>("firstD/lay3");

		HBoxContainer lay4 = GetNode<HBoxContainer>("secondD/lay4");
		HBoxContainer lay5 = GetNode<HBoxContainer>("secondD/lay5");
		HBoxContainer lay6 = GetNode<HBoxContainer>("secondD/lay6");

		HBoxContainer lay7 = GetNode<HBoxContainer>("thirdD/lay7");
		HBoxContainer lay8 = GetNode<HBoxContainer>("thirdD/lay8");
		HBoxContainer lay9 = GetNode<HBoxContainer>("thirdD/lay9");
		
		Main main = GetNode<Main>("/root/Main");
		main.Create_Visualisation();
		Create_Dimensions(lay1,lay2, lay3, lay4, lay5, lay6, lay7, lay8, lay9);
		
		AddToDictionary();
	}
	public void RestartGame()
	{
		foreach (Button button in TttBtns)
		{
			button.Text = "";
		}

		foreach (Label3D label in Labels)
		{
			label.Text = "";
		}
		_playerTurn = !_playerTurn;
    
		Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
		playerTurnLabel.Text = _playerTurn ? "Player turn: X" : "Player turn: O";
		
		BlockBtns(false);
		foreach (var btn in TttBtns)
		{
			btn.MouseEntered += () => OnMouseEntered(btn);
			btn.SetDefaultCursorShape(CursorShape.PointingHand);
		}
		if (!_playerTurn) 
		{
			LogicComputer(); 
		}
	}
	
	public void Create_Dimensions(HBoxContainer lay1, HBoxContainer lay2, HBoxContainer lay3,
		HBoxContainer lay4, HBoxContainer lay5, HBoxContainer lay6,
		HBoxContainer lay7, HBoxContainer lay8, HBoxContainer lay9)
	{
		Global global = GetNode<Global>("/root/Global");
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				for (int z = 0; z < 3; z++)
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
						FirstDBtn.Add(btn);
						bgColor.BgColor = new Color("0013ff");
						btn.AddThemeStyleboxOverride("normal", bgColor);
					}

					if (y == 1)
					{
						if (z == 0) lay4.AddChild(btn);
						if (z == 1) lay5.AddChild(btn);
						if (z == 2) lay6.AddChild(btn);
						SecondDBtn.Add(btn);
						bgColor.BgColor = new Color("#ff00e8");
						btn.AddThemeStyleboxOverride("normal", bgColor);
					}

					if (y == 2)
					{
						if (z == 0) lay7.AddChild(btn);
						if (z == 1) lay8.AddChild(btn);
						if (z == 2) lay9.AddChild(btn);
						ThirdDBtn.Add(btn);
						bgColor.BgColor = new Color("#fff300");
						btn.AddThemeStyleboxOverride("normal", bgColor);
					}

					TttBtns.Add(btn);
					Buttons.Add(btn);

					if (global.buttonName == "OfflineBtn")
					{
						btn.Pressed += () => LogicOffline(btn);
					}

					if (global.buttonName == "ComputerBtn")
					{
						Button capturedButton = btn; 
						capturedButton.Pressed += () => LogicComputer(capturedButton);
					}
					btn.MouseEntered += () => OnMouseEntered(btn);
					btn.MouseExited += () => OnMouseExited(btn);
				}
			}
		}
	}
	private void LogicComputer(Button btn = null)
	{
		Main main = GetNode<Main>("/root/Main");

		for (int i = 0; i < TttBtns.Count; i++)
		{
			if (i < 27)
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
		
		if (_playerTurn && btn != null)
		{
			if (btn.Text == "")
			{
				btn.Text = "X";
				Label3D label3D = main.BtnAndboxMeshLabel3DDictionary[btn];
				label3D.Text = "X";
				playerTurnLabel.Text = "Player turn: O";
				_playerTurn = false;
			}
		}
		if(!_playerTurn)
		{
			Random random = new Random();
			List<Button> availableButtons = TttBtns.Where(b => b.Text == "").ToList();
        
			if (availableButtons.Count > 0)
			{
				Button computerMove = availableButtons[random.Next(availableButtons.Count)];
				computerMove.Text = "O";
				main.BtnAndboxMeshLabel3DDictionary[computerMove].Text = "O";
				playerTurnLabel.Text = "Player turn: X";
				_playerTurn = true;
			}
		}
		WhoWon();
	}
	public void LogicOffline(Button btn)
	{
		Main main = GetNode<Main>("/root/Main");
		
		for (int i = 0; i < TttBtns.Count; i++)
		{
			if (i < 27)
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

		if (btn.Text != "")
		{
			return;
		}
		if (_playerTurn)
		{
			playerTurnLabel.Text = "Player turn : O";
			btn.Text = "X";
			label3D.Text = btn.Text;
			_playerTurn = false;
		}
		else
		{
			playerTurnLabel.Text = "Player turn : X";
			btn.Text = "O";
			_playerTurn = true;
			label3D.Text = btn.Text;
		}
		WhoWon();
	}
	public void OnMouseEntered(Button btn)
	{
		Main main = GetNode<Main>("/root/Main");
		if (main.BtnAndMeshInstanceDictionary.ContainsKey(btn))
		{
			StandardMaterial3D miniMaterial = new StandardMaterial3D();
			miniMaterial.AlbedoColor = new Color(255, 0, 0);
			main.BtnAndMeshInstanceDictionary[btn].MaterialOverride = miniMaterial;
		}
	}
	public void OnMouseExited(Button btn)
	{
		Main main = GetNode<Main>("/root/Main");
		if (main.BtnAndMeshInstanceDictionary.ContainsKey(btn))
		{
			StandardMaterial3D miniMaterial = new StandardMaterial3D();
			miniMaterial.AlbedoColor = new Color("#000000");
			main.BtnAndMeshInstanceDictionary[btn].MaterialOverride = miniMaterial;
		}
	}
	public void WhoWon()
	{
		Button[,] wins =
		{
			// grid 1 H
			{ FirstDBtn[0], FirstDBtn[1], FirstDBtn[2] },
			{ FirstDBtn[3], FirstDBtn[4], FirstDBtn[5] },
			{ FirstDBtn[6], FirstDBtn[7], FirstDBtn[8] },
			{ FirstDBtn[0], FirstDBtn[3], FirstDBtn[6] },
			{ FirstDBtn[1], FirstDBtn[4], FirstDBtn[7] },
			{ FirstDBtn[2], FirstDBtn[5], FirstDBtn[8] },
			{ FirstDBtn[0], FirstDBtn[4], FirstDBtn[8] },
			{ FirstDBtn[2], FirstDBtn[4], FirstDBtn[6] },

			//grid 2 H
			{ SecondDBtn[0], SecondDBtn[1], SecondDBtn[2] },
			{ SecondDBtn[3], SecondDBtn[4], SecondDBtn[5] },
			{ SecondDBtn[6], SecondDBtn[7], SecondDBtn[8] },
			{ SecondDBtn[0], SecondDBtn[3], SecondDBtn[6] },
			{ SecondDBtn[1], SecondDBtn[4], SecondDBtn[7] },
			{ SecondDBtn[2], SecondDBtn[5], SecondDBtn[8] },
			{ SecondDBtn[0], SecondDBtn[4], SecondDBtn[8] },
			{ SecondDBtn[2], SecondDBtn[4], SecondDBtn[6] },

			//grid 3 H
			{ ThirdDBtn[0], ThirdDBtn[1], ThirdDBtn[2] },
			{ ThirdDBtn[3], ThirdDBtn[4], ThirdDBtn[5] },
			{ ThirdDBtn[6], ThirdDBtn[7], ThirdDBtn[8] },
			{ ThirdDBtn[0], ThirdDBtn[3], ThirdDBtn[6] },
			{ ThirdDBtn[1], ThirdDBtn[4], ThirdDBtn[7] },
			{ ThirdDBtn[2], ThirdDBtn[5], ThirdDBtn[8] },
			{ ThirdDBtn[0], ThirdDBtn[4], ThirdDBtn[8] },
			{ ThirdDBtn[2], ThirdDBtn[4], ThirdDBtn[6] },

			//grid 1 V
			{ FirstDBtn[0], SecondDBtn[0], ThirdDBtn[0] },
			{ FirstDBtn[1], SecondDBtn[1], ThirdDBtn[1] },
			{ FirstDBtn[2], SecondDBtn[2], ThirdDBtn[2] },
			{ FirstDBtn[3], SecondDBtn[3], ThirdDBtn[3] },
			{ FirstDBtn[4], SecondDBtn[4], ThirdDBtn[4] },
			{ FirstDBtn[5], SecondDBtn[5], ThirdDBtn[5] },
			{ FirstDBtn[6], SecondDBtn[6], ThirdDBtn[6] },
			{ FirstDBtn[7], SecondDBtn[7], ThirdDBtn[7] },
			{ FirstDBtn[8], SecondDBtn[8], ThirdDBtn[8] },

			{ FirstDBtn[6], SecondDBtn[3], ThirdDBtn[0] },
			{ FirstDBtn[7], SecondDBtn[4], ThirdDBtn[1] },
			{ FirstDBtn[8], SecondDBtn[5], ThirdDBtn[2] },

			{ FirstDBtn[0], SecondDBtn[3], ThirdDBtn[6] },
			{ FirstDBtn[1], SecondDBtn[4], ThirdDBtn[7] },
			{ FirstDBtn[2], SecondDBtn[5], ThirdDBtn[8] },

			{ FirstDBtn[0], SecondDBtn[4], ThirdDBtn[8] },
			{ FirstDBtn[2], SecondDBtn[4], ThirdDBtn[6] },
			{ FirstDBtn[6], SecondDBtn[4], ThirdDBtn[2] },
			{ FirstDBtn[8], SecondDBtn[4], ThirdDBtn[0] },
		};
		
		var popUp = GD.Load<PackedScene>("res://WinPopUp.tscn");
		var scoreScene = GetNode<Score>("/root/Main/leftSide/Score");
		for (int i = 0; i < wins.GetLength(0); i++)
		{
			if (wins[i, 0].Text == wins[i, 1].Text && wins[i, 1].Text == wins[i, 2].Text &&
			    (wins[i, 0].Text == "X" || wins[i, 0].Text == "O"))
			{
				var popUpInstant = popUp.Instantiate();
				win = true;
				popUpInstant.GetNode<Label>("winLabel").Text = $"Won : {wins[i, 0].Text}";
				
				if (wins[i, 0].Text == "X") scoreScene.x_wins = scoreScene.x_wins + 1;
				else scoreScene.o_wins = scoreScene.o_wins+1;
				scoreScene.ScoreSystem();
				GetTree().Root.AddChild(popUpInstant);
				if (win)
				{
					BlockBtns(true);
				}
			}
		}
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
		Main main = GetNode<Main>("/root/Main");
		//GD.Print("console z game play:" + MeshInstances.Count);
		for (int i = 0; i < MeshInstances.Count; i++)
		{
			MeshInstance3D meshInstance3D = MeshInstances[i];
			Button button = TttBtns[i];
			if (i < 27)
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