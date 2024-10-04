using System.Collections.Generic;
using System.Threading;
using Godot;

namespace threeDTicTacToe;

public partial class GamePlay : Control
{
	bool playerTurn = true;
	public List<Button> tttBtns = new List<Button>();
	public List<Button> firstDBtn = new List<Button>(); 
	public List<Button> secondDBtn = new List<Button>(); 
	public List<Button> thirdDBtn = new List<Button>();
	public string[] BtnsContent;
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

	private void RestartGame()
	{
		foreach (Button btn in tttBtns)
		{
			btn.Text = "";
		}
	}

	public void Create_Dimensions(VBoxContainer lay1, VBoxContainer lay2, VBoxContainer lay3,
		VBoxContainer lay4 , VBoxContainer lay5, VBoxContainer lay6,
		VBoxContainer lay7 , VBoxContainer lay8, VBoxContainer lay9)
	{
		StyleBoxFlat bgColor = new StyleBoxFlat();
		StyleBoxFlat fgColor = new StyleBoxFlat();
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				for (int z = 0; z < 3; z++)
				{
					Button btn = new Button();
					btn.SetDefaultCursorShape(CursorShape.PointingHand);
					btn.CustomMinimumSize = new Vector2(100, 100);
					
					if (y==0 && z == 0) lay1.AddChild(btn);
					if (y==0 && z == 1) lay2.AddChild(btn);
					if (y==0 && z == 2) lay3.AddChild(btn);
					
					if (y==1 && z == 0) lay4.AddChild(btn);
					if (y==1 && z == 1) lay5.AddChild(btn);
					if (y==1 && z == 2) lay6.AddChild(btn);
					
					if (y==2 && z == 0) lay7.AddChild(btn);
					if (y==2 && z == 1) lay8.AddChild(btn);
					if (y==2 && z == 2) lay9.AddChild(btn);
				
					if(x==0) firstDBtn.Add(btn);
					if(x==1) secondDBtn.Add(btn);
					if(x==2) thirdDBtn.Add(btn);
					
					bgColor.BgColor = new Color("#d2d2e8");
					fgColor.BgColor = new Color("#000000");
				
					btn.AddThemeStyleboxOverride("normal",bgColor);
					btn.AddThemeStyleboxOverride("tint",fgColor);
				
					tttBtns.Add(btn);
					btn.Text += BtnsContent;
					btn.Pressed += () => Logic(btn);
				}

			}
		}
	}

	public void Logic(Button btn)
	{
		Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
		if (btn.Text != "")
		{
			return;
		}
		if (playerTurn)
		{
			playerTurnLabel.Text = "Player Turn : O";
			btn.Text = "X";
			playerTurn = false;
		}
		else
		{
			playerTurnLabel.Text = "Player Turn : X";
			btn.Text = "O";
			playerTurn = true;
		}

		WhoWon();
	}

	public void WhoWon()
	{
		WinPopUp winPopUp= GetNode<WinPopUp>("/root/Main/WinPopUp");
		Label winLabel = GetNode<Label>("/root/Main/WinPopUp/winLabel");
		Button closeBtn = GetNode<Button>("/root/Main/WinPopUp/closeBtn");
		
		Button[,] wins =
		{
			// grid 1 H
			{ firstDBtn[0], firstDBtn[1], firstDBtn[2] },
			{ firstDBtn[3], firstDBtn[4], firstDBtn[5] },
			{ firstDBtn[6], firstDBtn[7], firstDBtn[8] },
			{ firstDBtn[0], firstDBtn[3], firstDBtn[6] },
			{ firstDBtn[1], firstDBtn[4], firstDBtn[7] },
			{ firstDBtn[2], firstDBtn[5], firstDBtn[8] },
			{ firstDBtn[0], firstDBtn[4], firstDBtn[8] },  
			{ firstDBtn[2], firstDBtn[4], firstDBtn[6] },
            
			//grid 2 H
			{ secondDBtn[0], secondDBtn[1], secondDBtn[2] },
			{ secondDBtn[3], secondDBtn[4], secondDBtn[5] },
			{ secondDBtn[6], secondDBtn[7], secondDBtn[8] },
			{ secondDBtn[0], secondDBtn[3], secondDBtn[6] },
			{ secondDBtn[1], secondDBtn[4], secondDBtn[7] },
			{ secondDBtn[2], secondDBtn[5], secondDBtn[8] },
			{ secondDBtn[0], secondDBtn[4], secondDBtn[8] },  
			{ secondDBtn[2], secondDBtn[4], secondDBtn[6] },
            
			//grid 3 H
			{ thirdDBtn[0], thirdDBtn[1], thirdDBtn[2] },
			{ thirdDBtn[3], thirdDBtn[4], thirdDBtn[5] },
			{ thirdDBtn[6], thirdDBtn[7], thirdDBtn[8] },
			{ thirdDBtn[0], thirdDBtn[3], thirdDBtn[6] },
			{ thirdDBtn[1], thirdDBtn[4], thirdDBtn[7] },
			{ thirdDBtn[2], thirdDBtn[5], thirdDBtn[8] },
			{ thirdDBtn[0], thirdDBtn[4], thirdDBtn[8] },  
			{ thirdDBtn[2], thirdDBtn[4], thirdDBtn[6] },
            
			//grid 1 V
			{ firstDBtn[0] , secondDBtn[0] , thirdDBtn[0]},
			{ firstDBtn[1] , secondDBtn[1] , thirdDBtn[1]},
			{ firstDBtn[2] , secondDBtn[2] , thirdDBtn[2]},
			{ firstDBtn[3] , secondDBtn[3] , thirdDBtn[3]},
			{ firstDBtn[4] , secondDBtn[4] , thirdDBtn[4]},
			{ firstDBtn[5] , secondDBtn[5] , thirdDBtn[5]},
			{ firstDBtn[6] , secondDBtn[6] , thirdDBtn[6]},
			{ firstDBtn[7] , secondDBtn[7] , thirdDBtn[7]},
			{ firstDBtn[8] , secondDBtn[8] , thirdDBtn[8]},
            
			{ firstDBtn[6] , secondDBtn[3] , thirdDBtn[0]},
			{ firstDBtn[7] , secondDBtn[4] , thirdDBtn[1]},
			{ firstDBtn[8] , secondDBtn[5] , thirdDBtn[2]},
            
			{ firstDBtn[0] , secondDBtn[3] , thirdDBtn[6]},
			{ firstDBtn[1] , secondDBtn[4] , thirdDBtn[7]},
			{ firstDBtn[2] , secondDBtn[5] , thirdDBtn[8]},
            
			{ firstDBtn[0], secondDBtn[4], thirdDBtn[8]},
			{ firstDBtn[2], secondDBtn[4], thirdDBtn[6]},
			{ firstDBtn[6] , secondDBtn[4], thirdDBtn[2]},
			{ firstDBtn[8] , secondDBtn[4], thirdDBtn[0]},
		};
		for (int i = 0; i < wins.GetLength(0); i++)
		{
			if (wins[i, 0].Text == wins[i, 1].Text && wins[i, 1].Text == wins[i, 2].Text &&
			    (wins[i, 0].Text == "X" || wins[i, 0].Text == "O"))
			{
				winPopUp.Visible = true;
				winLabel.Visible = true;
				closeBtn.Visible = true;
				//GD.Print(wins[i, 0].Text);
				winLabel.Text = $"Wygrał : {wins[i, 0].Text}";
				return;
			}
		}
	}

	public override void _Process(double delta)
	{
	}
}