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
		global.ClickSFX("res://sfx/btn_click.wav");
		QueueFree(); // usuwa ci instancje popatrz podczas na te zdalny podgląd jak się tworzy
		if (global.Mode == "2D")
		{
			TTT2D ttt2D = GetNode<TTT2D>("/root/TTT2D");
			ttt2D.RestartGame();
		}
		if (global.player3DMode == "4x4x4")
		{
			GamePlay4x4x4 gamePlay = GetNode<GamePlay4x4x4>("/root/TTT3D/rightSide/GamePlay4x4x4");
			gamePlay.RestartGame();
		}

		if (global.player3DMode == "3x3x3")
		{
			GamePlay3x3x3 gamePlay = GetNode<GamePlay3x3x3>("/root/TTT3D/rightSide/GamePlay3x3x3");
			gamePlay.RestartGame();
		}
	}
	public override void _Process(double delta)
	{
	}
}
