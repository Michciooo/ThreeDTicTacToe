using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Godot;

namespace threeDTicTacToe;

public partial class GamePlay : Control
{
	bool _playerTurn = true;
	public List<Button> TttBtns = new List<Button>();
	private List<Button> AllBtns = new List<Button>();
	public List<Button> FirstDBtn = new List<Button>();
	public List<Button> SecondDBtn = new List<Button>();
	public List<Button> ThirdDBtn = new List<Button>();

	public List<Label3D> Labels = new List<Label3D>();
	public override void _Ready()
	{
		Button restartButton = GetNode<Button>("restartBtn");

		VBoxContainer lay1 = GetNode<VBoxContainer>("firstD/lay1");
		VBoxContainer lay2 = GetNode<VBoxContainer>("firstD/lay2");
		VBoxContainer lay3 = GetNode<VBoxContainer>("firstD/lay3");

		VBoxContainer lay4 = GetNode<VBoxContainer>("secondD/lay4");
		VBoxContainer lay5 = GetNode<VBoxContainer>("secondD/lay5");
		VBoxContainer lay6 = GetNode<VBoxContainer>("secondD/lay6");

		VBoxContainer lay7 = GetNode<VBoxContainer>("thirdD/lay7");
		VBoxContainer lay8 = GetNode<VBoxContainer>("thirdD/lay8");
		VBoxContainer lay9 = GetNode<VBoxContainer>("thirdD/lay9");

		Create_Dimensions(lay1, lay2, lay3, lay4, lay5, lay6, lay7, lay8, lay9);
		restartButton.Pressed += RestartGame;
	}

	public void RestartGame()
	{
		Main main = GetNode<Main>("/root/Main");
		foreach (Button btn in TttBtns)
		{
			btn.Text = "";
		}

		foreach (Label3D label in Labels)
		{
			label.Text = "";
		}
	}

	public void Create_Dimensions(VBoxContainer lay1, VBoxContainer lay2, VBoxContainer lay3,
		VBoxContainer lay4, VBoxContainer lay5, VBoxContainer lay6,
		VBoxContainer lay7, VBoxContainer lay8, VBoxContainer lay9)
	{
		StyleBoxFlat bgColor = new StyleBoxFlat();
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				for (int z = 0; z < 3; z++)
				{
					Button btn = new Button();
					btn.SetDefaultCursorShape(CursorShape.PointingHand);
					btn.CustomMinimumSize = new Vector2(100, 100);

					if (y == 0 && z == 0) lay1.AddChild(btn);
					if (y == 0 && z == 1) lay2.AddChild(btn);
					if (y == 0 && z == 2) lay3.AddChild(btn);

					if (y == 1 && z == 0) lay4.AddChild(btn);
					if (y == 1 && z == 1) lay5.AddChild(btn);
					if (y == 1 && z == 2) lay6.AddChild(btn);

					if (y == 2 && z == 0) lay7.AddChild(btn);
					if (y == 2 && z == 1) lay8.AddChild(btn);
					if (y == 2 && z == 2) lay9.AddChild(btn);

					if (y == 0) FirstDBtn.Add(btn);
					if (y == 1) SecondDBtn.Add(btn);
					if (y == 2) ThirdDBtn.Add(btn);

					bgColor.BgColor = new Color("#000000");

					btn.AddThemeStyleboxOverride("normal", bgColor);
					TttBtns.Add(btn);

					btn.Pressed += () => Logic(btn);
				}
			}
		}
	}
	public void Logic(Button btn)
	{
		Main main = GetNode<Main>("/root/Main");
		for (int i = 0; i < TttBtns.Count; i++)
		{
			if (i < 27)
			{
				Button button = TttBtns[i];
				Label3D label = Labels[i];
				if (!main.BtnAndboxMesLabel3DDictionary.ContainsKey(button))
				{
					main.BtnAndboxMesLabel3DDictionary.Add(button, label);
				}
			}
		}

		Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
		Label3D label3D = main.BtnAndboxMesLabel3DDictionary[btn];

		if (btn.Text != "")
		{
			return;
		}
		if (_playerTurn)
		{
			playerTurnLabel.Text = "Player Turn : O";
			btn.Text = "X";
			label3D.Text = btn.Text;
			_playerTurn = false;
		}
		else
		{
			playerTurnLabel.Text = "Player Turn : X";
			btn.Text = "O";
			label3D.Text = btn.Text;
			_playerTurn = true;
		}
		WhoWon();
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
			{ FirstDBtn[0] , SecondDBtn[0] , ThirdDBtn[0]},
			{ FirstDBtn[1] , SecondDBtn[1] , ThirdDBtn[1]},
			{ FirstDBtn[2] , SecondDBtn[2] , ThirdDBtn[2]},
			{ FirstDBtn[3] , SecondDBtn[3] , ThirdDBtn[3]},
			{ FirstDBtn[4] , SecondDBtn[4] , ThirdDBtn[4]},
			{ FirstDBtn[5] , SecondDBtn[5] , ThirdDBtn[5]},
			{ FirstDBtn[6] , SecondDBtn[6] , ThirdDBtn[6]},
			{ FirstDBtn[7] , SecondDBtn[7] , ThirdDBtn[7]},
			{ FirstDBtn[8] , SecondDBtn[8] , ThirdDBtn[8]},

			{ FirstDBtn[6] , SecondDBtn[3] , ThirdDBtn[0]},
			{ FirstDBtn[7] , SecondDBtn[4] , ThirdDBtn[1]},
			{ FirstDBtn[8] , SecondDBtn[5] , ThirdDBtn[2]},

			{ FirstDBtn[0] , SecondDBtn[3] , ThirdDBtn[6]},
			{ FirstDBtn[1] , SecondDBtn[4] , ThirdDBtn[7]},
			{ FirstDBtn[2] , SecondDBtn[5] , ThirdDBtn[8]},

			{ FirstDBtn[0], SecondDBtn[4], ThirdDBtn[8]},
			{ FirstDBtn[2], SecondDBtn[4], ThirdDBtn[6]},
			{ FirstDBtn[6] , SecondDBtn[4], ThirdDBtn[2]},
			{ FirstDBtn[8] , SecondDBtn[4], ThirdDBtn[0]},
		};
		var popUp = GD.Load<PackedScene>("res://WinPopUp.tscn");
		for (int i = 0; i < wins.GetLength(0); i++)
		{
			if (wins[i, 0].Text == wins[i, 1].Text && wins[i, 1].Text == wins[i, 2].Text &&
				(wins[i, 0].Text == "X" || wins[i, 0].Text == "O"))
			{
				// dodaje tekst ten co miałeś tylko że po zrobieniu instancji przed dodaniem na ekran
				var popUpInstant = popUp.Instantiate();
				popUpInstant.GetNode<Label>("winLabel").Text = $"Wygrał : {wins[i, 0].Text}";
				// dodanie na ekran
				GetTree().Root.AddChild(popUpInstant);
			}
		}
	}

	public override void _Process(double delta)
	{
	}
}