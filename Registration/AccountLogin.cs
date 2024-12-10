using Godot;
using System;
using System.Threading;

namespace threeDTicTacToe;

public partial class AccountLogin : Control
{
	public override void _Ready()
	{
		var global = GetNode<Global>("/root/Global");
		
		var loginBtn = GetNode<Button>("LoginContainer/loginBtn");
		var registerBtn = GetNode<Button>("RegisterContainer/registerBtn");
		
		var logOutBtn = GetNode<Button>("logOutBtn");
		var loginContainer = GetNode<Panel>("LoginContainer");
		loginBtn.Pressed += LoginBtnOnPressed;
		registerBtn.Pressed += RegisterBtnOnPressed;
		logOutBtn.Pressed += LogOutBtnOnPressed;
		
		if (global.isLogged)
		{
			loginContainer.Visible = false;
			logOutBtn.Visible = true;
		}
	}

	private void LogOutBtnOnPressed()
	{
		var global = GetNode<Global>("/root/Global");
		var logOutBtn = GetNode<Button>("logOutBtn");
		var loginContainer = GetNode<Panel>("LoginContainer");
		
		global.isLogged = false;
		logOutBtn.Visible = false;
		loginContainer.Visible = true;
		global.accName = "Guest";
	}

	private void RegisterBtnOnPressed()
	{
		var emailTextEdit = GetNode<LineEdit>("RegisterContainer/rightSide/emailTextEdit");
		var loginTextEdit = GetNode<LineEdit>("RegisterContainer/rightSide/loginTextEdit");
		var passwordTextEdit = GetNode<LineEdit>("RegisterContainer/rightSide/passwordTextEdit");
	}
	private void LoginBtnOnPressed()
	{
		var global = GetNode<Global>("/root/Global");
		
		var loginTextEdit = GetNode<LineEdit>("LoginContainer/rightSide/loginTextEdit");
		var passwordTextEdit = GetNode<LineEdit>("LoginContainer/rightSide/passwordTextEdit");
		var validLabel = GetNode<Label>("LoginContainer/validLabel");
		
		if (loginTextEdit.Text == "abc" && passwordTextEdit.Text == "123")
		{
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
			global.isLogged = true;
			global.accName = loginTextEdit.Text;
		}
		validLabel.Visible = true;
		loginTextEdit.Text = "";
		passwordTextEdit.Text = "";
	}

	public override void _Process(double delta)
	{
		var backBtn = GetNode<Button>("backBtn");
		if (backBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
	}
}
