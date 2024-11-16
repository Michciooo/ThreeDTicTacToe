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
		var global = GetNode<Global>("/root/Global");
		QueueFree(); // usuwa ci instancje popatrz podczas na te zdalny podgląd jak się tworzy
		if (global.Mode == "2D")
		{
			TTT2D ttt2D = GetNode<TTT2D>("/root/TTT2D");
			ttt2D.RestartGame();
		}
		if (global.Mode == "3D")
		{
			GamePlay4x4x4 gamePlay = GetNode<GamePlay4x4x4>("/root/TTT3D/rightSide/GamePlay4x4x4");
			gamePlay.RestartGame();
		}
	}
	public override void _Process(double delta)
	{
	}
}
