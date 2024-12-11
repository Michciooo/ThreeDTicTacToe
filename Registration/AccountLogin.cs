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
		var adminPanelBtn = GetNode<Button>("adminPanelBtn");

		loginBtn.Pressed += LoginBtnOnPressed;
		registerBtn.Pressed += RegisterBtnOnPressed;
		logOutBtn.Pressed += LogOutBtnOnPressed;

		if (global.isLogged)
		{
			loginContainer.Visible = false;
			logOutBtn.Visible = true;
		}
		if (global.isAdmin) adminPanelBtn.Visible = true;
	}

	private void LogOutBtnOnPressed()
	{
		var global = GetNode<Global>("/root/Global");
		var logOutBtn = GetNode<Button>("logOutBtn");
		var loginContainer = GetNode<Panel>("LoginContainer");
		var adminPanelBtn = GetNode<Button>("adminPanelBtn");
		
		global.isLogged = false;
		global.isAdmin = false;
		global.accName = "Guest";
		
		logOutBtn.Visible = false;
		adminPanelBtn.Visible = false;
		loginContainer.Visible = true;
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
		
		//Admin panel
		
		if (loginTextEdit.Text == "admin" && passwordTextEdit.Text == "password")
		{
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
			global.isLogged = true;
			global.isAdmin = true;
			global.accName = loginTextEdit.Text;
		}
		validLabel.Visible = true;
		loginTextEdit.Text = "";
		passwordTextEdit.Text = "";
	}

	public override void _Process(double delta)
	{
		var backBtn = GetNode<Button>("backBtn");
		var adminPanelBtn = GetNode<Button>("adminPanelBtn");
		if (backBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
		if(adminPanelBtn.IsPressed()) GetTree().ChangeSceneToFile("res://Registration/AdminPanel.tscn");
	}
}
