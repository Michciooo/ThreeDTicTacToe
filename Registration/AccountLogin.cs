using Godot;
using System;
using System.Text.RegularExpressions;
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
		
		global.ClickSFX("res://sfx/btn_click.wav");
		global.isLogged = false;
		global.accName = "Guest";
		logOutBtn.Visible = false;
		loginContainer.Visible = true;
	}

	private void RegisterBtnOnPressed()
	{
		var global = GetNode<Global>("/root/Global");
		
		var emailTextEdit = GetNode<LineEdit>("RegisterContainer/rightSide/emailTextEdit");
		var loginTextEdit = GetNode<LineEdit>("RegisterContainer/rightSide/loginTextEdit");
		var passwordTextEdit = GetNode<LineEdit>("RegisterContainer/rightSide/passwordTextEdit");
		var validPanel = GetNode<Panel>("ValidPanel");
		var validationLabel = GetNode<Label>("ValidPanel/validationLabel");

		Regex emailRegex = new Regex("^[a-zA-Z0-9._%+-]+@[a-z.-]+\\.[a-z]{2,}$");
		Regex loginRegex = new Regex(@"^\S+$");
		Regex passwordRegex = new Regex("^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*()]).{8,}$");

		if (!emailRegex.IsMatch(emailTextEdit.Text))
		{
			validPanel.Visible = true;
			validationLabel.Text = "Incorrect email address";
			emailTextEdit.Text = "";
		}
		else if(loginTextEdit.Text.Length < 7)
		{
			validPanel.Visible = true;
			validationLabel.Text = "Login must be at least 7 characters";
			loginTextEdit.Text = "";
		}
		else if (!loginRegex.IsMatch(loginTextEdit.Text))
		{
			validPanel.Visible = true;
			validationLabel.Text = "Login can't contains spaces";
			loginTextEdit.Text = "";
		}
		else if (passwordTextEdit.Text.Length < 8)
		{
			validPanel.Visible = true;
			validationLabel.Text = "Password must be at least 8 characters";
			passwordTextEdit.Text = "";
		}
		else if (!passwordRegex.IsMatch(passwordTextEdit.Text))
		{
			validPanel.Visible = true;
			validationLabel.Text = "Password must contain at least one uppercase letter, one number, one lowercase letter, and one special character";
			passwordTextEdit.Text = "";
		}
		else
		{
			validPanel.Visible = true;
			validationLabel.Text = "Account created";
			emailTextEdit.Text = "";
			loginTextEdit.Text = "";
			passwordTextEdit.Text = "";
		}
		global.ClickSFX("res://sfx/btn_click.wav");
	}
	private void LoginBtnOnPressed()
	{
		var global = GetNode<Global>("/root/Global");
		
		var loginTextEdit = GetNode<LineEdit>("LoginContainer/rightSide/loginTextEdit");
		var passwordTextEdit = GetNode<LineEdit>("LoginContainer/rightSide/passwordTextEdit");
		var validPanel = GetNode<Panel>("ValidPanel");
		var validationLabel = GetNode<Label>("ValidPanel/validationLabel");
		
		if (loginTextEdit.Text == "admin" && passwordTextEdit.Text == "password")
		{
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
			global.isLogged = true;
			global.accName = loginTextEdit.Text;
		}
		validPanel.Visible = true;
		validationLabel.Text = "Incorrect login or password";
		passwordTextEdit.Text = "";
		global.ClickSFX("res://sfx/btn_click.wav");
	}

	public override void _Process(double delta)
	{
		var global = GetNode<Global>("/root/Global");
		var backBtn = GetNode<Button>("backBtn");
		var okBtn = GetNode<Button>("ValidPanel/okBtn");
		var validPanel = GetNode<Panel>("ValidPanel");

		if (backBtn.IsPressed())
		{
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
		}
		if (okBtn.IsPressed())
		{
			global.ClickSFX("res://sfx/btn_click.wav");
			validPanel.Visible = false;
		}
	}
}