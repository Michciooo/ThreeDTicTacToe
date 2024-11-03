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
	public void ClosePopUp()
	{
		GamePlay gamePlay = GetNode<GamePlay>("/root/TTT3D/rightSide/GamePlay");
		QueueFree(); // usuwa ci instancje popatrz podczas na te zdalny podgląd jak się tworzy
		gamePlay.RestartGame();
	}
	public override void _Process(double delta)
	{
	}
}
