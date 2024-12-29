using System;
using System.Collections.Generic;
using Godot;
namespace threeDTicTacToe;

public partial class Statistics : Control
{
	public override void _Ready()
	{
		WinsInfoUpdate();
		LosesInfoUpdate();
		CalculateWinRate();
		
		var global = GetNode<Global>("/root/Global");
		var backBtn = GetNode<Button>("backBtn");
		var infoBtn = GetNode<Button>("infoBtn");
		var infoPopUp = GetNode<PopupPanel>("infoPopUp");
		var closeBtn = GetNode<Button>("infoPopUp/VBoxContainer/closeBtn");
			
		infoBtn.Pressed += () => infoPopUp.Show();
		closeBtn.Pressed += () => infoPopUp.Hide();
		backBtn.Pressed += () =>
		{
			global.ClickSFX("res://sfx/btn_click.wav");
			GetTree().ChangeSceneToFile("res://MainMenus/Settings.tscn");
		};
	}

	private void CalculateWinRate()
	{
		var global = GetNode<Global>("/root/Global");

		var allWinRateLabel = GetNode<Label>("winRateInfo/VBoxContainer/allWinRate");
		var winRate2DLabel = GetNode<Label>("winRateInfo/VBoxContainer/winRate2D");
		var winRate3DLabel = GetNode<Label>("winRateInfo/VBoxContainer/winRate3D");

		decimal winRate = 0;
		decimal winRate2D = 0;
		decimal winRate3D = 0;
		
		if (global.content["allWins"] + global.content["allLoses"] == 0) winRate = 0;
		else winRate = (decimal)global.content["allWins"] / (global.content["allWins"] + global.content["allLoses"]) * 100;
		
		if (global.content["wins2D"] + global.content["loses2D"] == 0) winRate2D = 0;
		else winRate2D = (decimal)global.content["wins2D"] / (global.content["wins2D"] + global.content["loses2D"]) * 100;
		
		if (global.content["wins3D"] + global.content["loses3D"] == 0) winRate3D = 0;
		else winRate3D = (decimal)global.content["wins3D"] / (global.content["wins3D"] + global.content["loses3D"]) * 100;
		
		allWinRateLabel.Text = $"WINRATE : {Math.Round(winRate,2)} %";
		winRate2DLabel.Text = $"2D WINRATE : {Math.Round(winRate2D,2)} %";
		winRate3DLabel.Text = $"3D WINRATE : {Math.Round(winRate3D,2)} %";
	}
	private void LosesInfoUpdate()
	{
		var global = (Global)GetNode("/root/Global");
		
		var allLoses = GetNode<Label>("loseInfo/VBoxContainer/allLoses");
		var oLoses = GetNode<Label>("loseInfo/VBoxContainer/oLoses");
		var xLoses = GetNode<Label>("loseInfo/VBoxContainer/xLoses");
		var tLoses = GetNode<Label>("loseInfo/VBoxContainer/tLoses");
		var loses2D = GetNode<Label>("loseInfo/VBoxContainer/loses2D");
		var loses3D = GetNode<Label>("loseInfo/VBoxContainer/loses3D");
		var loses3D2P = GetNode<Label>("loseInfo/VBoxContainer/loses3D2P");
		var loses3D3P = GetNode<Label>("loseInfo/VBoxContainer/loses3D3P");
		
		allLoses.Text = $"LOSES : {global.content["allLoses"].ToString()}";
		oLoses.Text = $"LOSES AS O: {global.content["oLoses"].ToString()}";
		xLoses.Text = $"LOSES AS X: {global.content["xLoses"].ToString()}";
		tLoses.Text = $"LOSES AS \u25b3: {global.content["tLoses"].ToString()}";
		loses2D.Text = $"2D LOSES : {global.content["loses2D"].ToString()}";
		loses3D.Text = $"3D LOSES : {global.content["loses3D"].ToString()}";
		loses3D2P.Text = $"3D LOSES 2 PLAYER MODE: {global.content["loses3D2P"].ToString()}";
		loses3D3P.Text = $"3D LOSES 3 PLAYER MODE: {global.content["loses3D3P"].ToString()}";
	}

	private void WinsInfoUpdate()
	{
		var global = GetNode<Global>("/root/Global");
		
		var allWins = GetNode<Label>("winsInfo/VBoxContainer/allWins");
		var oWins = GetNode<Label>("winsInfo/VBoxContainer/oWins");
		var xWins = GetNode<Label>("winsInfo/VBoxContainer/xWins");
		var tWins = GetNode<Label>("winsInfo/VBoxContainer/tWins");
		var wins2D = GetNode<Label>("winsInfo/VBoxContainer/wins2D");
		var wins3D = GetNode<Label>("winsInfo/VBoxContainer/wins3D");
		var wins3D2P = GetNode<Label>("winsInfo/VBoxContainer/wins3D2P");
		var wins3D3P = GetNode<Label>("winsInfo/VBoxContainer/wins3D3P");
		
		allWins.Text = $"WINS : {global.content["allWins"].ToString()}";
		oWins.Text = $"WINS AS O: {global.content["oWins"].ToString()}";
		xWins.Text = $"WINS AS X: {global.content["xWins"].ToString()}";
		tWins.Text = $"WINS AS \u25b3: {global.content["tWins"].ToString()}";
		wins2D.Text = $"2D WINS : {global.content["wins2D"].ToString()}";
		wins3D.Text = $"3D WINS : {global.content["wins3D"].ToString()}";
		wins3D2P.Text = $"3D WINS 2 PLAYER MODE: {global.content["wins3D2P"].ToString()}";
		wins3D3P.Text = $"3D WINS 3 PLAYER MODE: {global.content["wins3D3P"].ToString()}";
	}

	public override void _Process(double delta)
	{
	}
}