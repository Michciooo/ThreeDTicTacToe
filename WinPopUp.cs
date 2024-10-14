using System.Collections.Generic;
using Godot;

namespace threeDTicTacToe;

public partial class WinPopUp : Control
{
	public override void _Ready()
	{
		Button closeBtn = GetNode<Button>("closeBtn");
		closeBtn.Pressed += ClosePopUp;
	}
	private void ClosePopUp()
	{
		GamePlay gamePlay = GetNode<GamePlay>("/root/Main/rightSide/GamePlay");
		QueueFree(); // usuwa ci instancje popatrz podczas na te zdalny podgląd jak się tworzy
		gamePlay.RestartGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
