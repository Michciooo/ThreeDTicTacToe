using Godot;
using System;
using System.Collections.Generic;

namespace threeDTicTacToe;

public partial class GamePlay3x3x3 : Control
{
	private string[] playerTurns = new[] { "O", "X", "\u25b3" };
	private string playerTurn;
	bool win = false;
	private int moves = 64;
	
	public List<Button> TttBtns = new List<Button>();
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
		
		HBoxContainer lay4 = GetNode<HBoxContainer>("secondD/lay4");
		HBoxContainer lay5 = GetNode<HBoxContainer>("secondD/lay5");
		HBoxContainer lay6 = GetNode<HBoxContainer>("secondD/lay6");
		
		HBoxContainer lay7 = GetNode<HBoxContainer>("thirdD/lay7");
		HBoxContainer lay8 = GetNode<HBoxContainer>("thirdD/lay8");
		HBoxContainer lay9 = GetNode<HBoxContainer>("thirdD/lay9");
		
		if (global.player3DMode == "3x3x3")
		{
			TTT3D main = GetNode<TTT3D>("/root/TTT3D");
			main.Create_Visualisation3x3x3();
			Create_Dimensions3x3x3(lay1, lay2, lay3, lay4, lay5, lay6, lay7, lay8, lay9);
		}

		playerTurn = playerTurns[0];
		AddToDictionary();
	}
	public void RestartGame()
	{
		moves = 64;
		foreach (Button button in TttBtns)
		{
			button.Text = "";
		}

		foreach (Label3D label in Labels)
		{
			label.Text = "";
		}
		BlockBtns(false);
		foreach (var btn in Buttons)
		{
			btn.MouseEntered += () => OnMouseEntered(btn);
			btn.SetDefaultCursorShape(CursorShape.PointingHand);
		}
	}
	private void Create_Dimensions3x3x3(HBoxContainer lay1, HBoxContainer lay2, HBoxContainer lay3 , 
		HBoxContainer lay4, HBoxContainer lay5, HBoxContainer lay6 , HBoxContainer lay7 ,
		HBoxContainer lay8 , HBoxContainer lay9)
	{
		var global = GetNode<Global>("/root/Global");
		global.FirstDBtn3.Clear();
		global.SecondDBtn3.Clear();
		global.ThirdDBtn3.Clear();

		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				for (int z = 0; z < 3; z++)
				{
					Button btn = new Button();
					btn.SetDefaultCursorShape(CursorShape.PointingHand);
					btn.CustomMinimumSize = new Vector2(100, 100);

					if (y == 0)
					{
						if(z==0) lay1.AddChild(btn);
						if(z==1) lay2.AddChild(btn);
						if(z==2) lay3.AddChild(btn);
						global.FirstDBtn3.Add(btn);
					}
					if (y == 1)
					{
						if(z==0) lay4.AddChild(btn);
						if(z==1) lay5.AddChild(btn);
						if(z==2) lay6.AddChild(btn);
						global.SecondDBtn3.Add(btn);
					}
					if (y == 2)
					{
						if(z==0) lay7.AddChild(btn);
						if(z==1) lay8.AddChild(btn);
						if(z==2) lay9.AddChild(btn);
						global.ThirdDBtn3.Add(btn);
					}
					
					TttBtns.Add(btn);
					Buttons.Add(btn);
					
					if (global.player13D == "Human" && global.player23D=="Human" && global.player33D=="Human") btn.Pressed += () => Human(btn);
					
					btn.MouseEntered += () => OnMouseEntered(btn);
					btn.MouseExited += () => OnMouseExited(btn);
				}
			}
		}
	}

	private void Human(Button btn)
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
			if (playerTurn==playerTurns[0])
			{
				btn.Text = playerTurn;
				playerTurn = playerTurns[1];
				playerTurnLabel.Text = $"Player turn : {playerTurn}";
				label3D.Text = btn.Text;
			}
			else if(playerTurn==playerTurns[1])
			{
				btn.Text = playerTurn;
				playerTurn = playerTurns[2];
				playerTurnLabel.Text = $"Player turn : {playerTurn}";
				moves -= 1;
				label3D.Text = btn.Text;
			}

			else
			{
				btn.Text = playerTurn;
				playerTurn = playerTurns[0];
				playerTurnLabel.Text = $"Player turn : {playerTurn}";
				moves -= 1;
				label3D.Text = btn.Text;
			}
		}
		WhoWon();
	}
	public bool WhoWon()
	{
		var global = GetNode<Global>("/root/Global");

		var popUp = GD.Load<PackedScene>("res://WinPopUp/WinPopUp.tscn");
		var scoreScene = GetNode<Score>("/root/TTT3D/leftSide/Score");
		var popUpInstant = popUp.Instantiate();
		global.InitializeWins3x3x3();
		
		for (int i = 0; i < global.wins3x3x3.GetLength(0); i++)
		{
			if (global.wins3x3x3[i, 0].Text == global.wins3x3x3[i, 1].Text && global.wins3x3x3[i, 1].Text == global.wins3x3x3[i, 2].Text 
			 && (global.wins3x3x3[i, 0].Text == "X" || global.wins3x3x3[i, 0].Text == "O" || global.wins3x3x3[i, 0].Text == "\u25b3"))
			{
				win = true;
				popUpInstant.GetNode<Label>("winLabel").Text = $"Won : {global.wins3x3x3[i, 0].Text}";

				if (global.wins3x3x3[i, 0].Text == "X") scoreScene.x_wins += 1;
				if(global.wins3x3x3[i, 0].Text == "O") scoreScene.o_wins += 1;
				if(global.wins3x3x3[i,0].Text =="\u25b3") scoreScene.triangle_wins += 1;

				scoreScene.ScoreSystem();
				GetTree().Root.AddChild(popUpInstant);
				BlockBtns(true);
				return true;
			}
		}
		if (moves == 0)
		{
			win = false;
			popUpInstant.GetNode<Label>("winLabel").Text = "Draw !";
			GetTree().Root.AddChild(popUpInstant);
			BlockBtns(true);
			return true;
		}
		return false;
	}
	private void AddToDictionary()
	{
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
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
	public override void _Process(double delta)
	{
	}
}