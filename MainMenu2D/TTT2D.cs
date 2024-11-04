using Godot;
using System;

namespace threeDTicTacToe;

public partial class TTT2D : Control
{
	public override void _Ready()
	{
		ButtonListener();
	}

	private void ButtonListener()
	{
		var btn1 = GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn1");
		var btn2 = GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn2");
		var btn3 = GetNode<Button>("rightSide/Main/Grid/FirstLayer/btn3");
		
		var btn4 = GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn4");
		var btn5 = GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn5");
		var btn6 = GetNode<Button>("rightSide/Main/Grid/SecondLayer/btn6");
		
		var btn7 = GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn7");
		var btn8 = GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn8");
		var btn9 = GetNode<Button>("rightSide/Main/Grid/ThirdLayer/btn9");
	}

	public override void _Process(double delta)
	{
		var mainMenuBtn = this.GetNode<Button>("leftSide/mainMenu");
		if (mainMenuBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenu2D/MainMenu2D.tscn");
	}
}
