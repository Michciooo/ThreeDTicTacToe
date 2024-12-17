using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace threeDTicTacToe;

public partial class GamePlay3x3x3 : Control
{
	private string[] playerTurns = new[] { "O", "X", "\u25b3" };
	private string playerTurn;
	bool win = false;
	private int moves = 27;
	
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
		AddToDictionary();
		playerTurn = playerTurns[0];
		if (global.player13D == "Easy Computer" && playerTurn == playerTurns[0])  EasyComputer();
		if (global.player23D == "Easy Computer" && playerTurn == playerTurns[1])  EasyComputer();
	}

	public async void RestartGame()
	{
		var global = GetNode<Global>("/root/Global");
		moves = 27;
		foreach (Button button in TttBtns) button.Text = "";

		foreach (Label3D label in Labels) label.Text = "";
		
		StandardMaterial3D restartCubeColor = new StandardMaterial3D();
		restartCubeColor.AlbedoColor = new Color("#000000");
		foreach (var meshInstance in MeshInstances) meshInstance.MaterialOverride = restartCubeColor;

		BlockBtns(false);

		if (global.player13D == "Easy Computer" && playerTurn == playerTurns[0]) await EasyComputer();
		if (global.player23D == "Easy Computer" && playerTurn == playerTurns[1]) await EasyComputer();
		if (global.player33D == "Easy Computer" && playerTurn == playerTurns[2]) await EasyComputer();
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
					
					var theme = new Theme();
					var styleBox = (StyleBoxFlat)GD.Load("res://TicTacToeModes/TTTButtons.tres");
					theme.SetStylebox("normal", "Button", styleBox);
					theme.SetStylebox("disabled", "Button", styleBox);
					btn.Theme = theme;
					
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
					
					btn.Pressed += () => PlayGame(btn);
					btn.MouseEntered += () => OnMouseEntered(btn);
					btn.MouseExited += () => OnMouseExited(btn);
				}
			}
		}
	}
	private async Task EasyComputer()
	{
		var global = GetNode<Global>("/root/Global");
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");
		
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

		BlockBtns(true);
		Random random = new Random();
		List<Button> availableButtons = TttBtns.Where(b => b.Text == "").ToList();

		if (availableButtons.Count > 0)
		{
			await WaitingMove();
			Button computerMove = availableButtons[random.Next(availableButtons.Count)];
			computerMove.Text = playerTurn;
			Label3D label3D = main.BtnAndboxMeshLabel3DDictionary[computerMove];
			label3D.Text = computerMove.Text;
			
			playerTurn = playerTurns[(Array.IndexOf(playerTurns, playerTurn) + 1) % 3];
			Label playerTurnLabel = GetNode<Label>("playerTurnLabel");
			playerTurnLabel.Text = $"Player Turn : {playerTurn}";

			moves -= 1;
			if (WhoWon()) return;

			if (playerTurn == playerTurns[0] && global.player13D == "Easy Computer") await EasyComputer();
			else if (playerTurn == playerTurns[1] && global.player23D == "Easy Computer") await EasyComputer();
			else if (playerTurn == playerTurns[2] && global.player33D == "Easy Computer") await EasyComputer();
		}
		
	}
	private async void PlayGame(Button btn)
	{
	    var global = GetNode<Global>("/root/Global");
	    TTT3D main = GetNode<TTT3D>("/root/TTT3D");
	    
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
	    
	    if (btn.Text == "")
	    {
	        if (playerTurn == playerTurns[0] && global.player13D == "Human")
	        {
	            btn.Text = playerTurn;
	            label3D.Text = btn.Text;
	            moves -= 1;
	            playerTurn = playerTurns[1];
	            playerTurnLabel.Text = $"Player Turn : {playerTurn}";
	        }
	        else if (playerTurn == playerTurns[1] && global.player23D == "Human")
	        {
	            btn.Text = playerTurn;
	            label3D.Text = btn.Text;
	            moves -= 1;
	            playerTurn = playerTurns[2];
	            playerTurnLabel.Text = $"Player Turn : {playerTurn}";
	        }
	        else if (playerTurn == playerTurns[2] && global.player33D == "Human")
	        {
	            btn.Text = playerTurn;
	            label3D.Text = btn.Text;
	            moves -= 1;
	            playerTurn = playerTurns[0];
	            playerTurnLabel.Text = $"Player Turn : {playerTurn}";
	        }
	        if (WhoWon()) return;
	        
	        if (playerTurn == playerTurns[0] && global.player13D == "Easy Computer") await EasyComputer();
	        else if (playerTurn == playerTurns[1] && global.player23D == "Easy Computer") await EasyComputer();
	        else if (playerTurn == playerTurns[2] && global.player33D == "Easy Computer") await EasyComputer();
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
	public bool WhoWon()
	{
		var global = GetNode<Global>("/root/Global");
		StandardMaterial3D wonColor = new StandardMaterial3D();
		wonColor.AlbedoColor = new Color(0,255,0);
		TTT3D main = GetNode<TTT3D>("/root/TTT3D");

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

				main.BtnAndMeshInstanceDictionary[global.wins3x3x3[i,0]].MaterialOverride = wonColor;
				main.BtnAndMeshInstanceDictionary[global.wins3x3x3[i,1]].MaterialOverride = wonColor;
				main.BtnAndMeshInstanceDictionary[global.wins3x3x3[i,2]].MaterialOverride = wonColor;
				
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
	private void BlockBtns(bool disable)
	{
		var restartBtn = GetNode<Button>("restartBtn");
		Buttons.Add(restartBtn);
		foreach (var button in Buttons) button.Disabled = disable;

		foreach (var btn in TttBtns)
		{
			btn.MouseEntered += () => OnMouseEntered(btn);
		}
		win = false;
	}
	public override void _Process(double delta)
	{
	}
}