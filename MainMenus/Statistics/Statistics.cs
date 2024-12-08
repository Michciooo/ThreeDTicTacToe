using Godot;
namespace threeDTicTacToe;

public partial class Statistics : Control
{
	public override void _Ready()
	{
		WinsInfoUpdate();
		LosesInfoUpdate();
	}

	private void LosesInfoUpdate()
	{
		var global = (Global)GetNode("/root/Global");
		
		var allLoses = GetNode<Label>("loseInfo/allLoses");
		var oLoses = GetNode<Label>("loseInfo/oLoses");
		var xLoses = GetNode<Label>("loseInfo/xLoses");
		var loses2D = GetNode<Label>("loseInfo/loses2D");
		var loses3D = GetNode<Label>("loseInfo/loses3D");
		var loses3D2P = GetNode<Label>("loseInfo/loses3D2P");
		var loses3D3P = GetNode<Label>("loseInfo/loses3D3P");
		
		allLoses.Text = $"LOSES : {global.content["allLoses"].ToString()}";
		oLoses.Text = $"LOSES AS O: {global.content["oLoses"].ToString()}";
		xLoses.Text = $"LOSES AS X: {global.content["xLoses"].ToString()}";
		loses2D.Text = $"2D LOSES : {global.content["loses2D"].ToString()}";
		loses3D.Text = $"3D LOSES : {global.content["loses3D"].ToString()}";
		loses3D2P.Text = $"3D LOSES 2 PLAYER MODE: {global.content["loses3D2P"].ToString()}";
		loses3D3P.Text = $"3D LOSES 3 PLAYER MODE: {global.content["loses3D3P"].ToString()}";
	}

	private void WinsInfoUpdate()
	{
		var global = GetNode<Global>("/root/Global");
		
		var allWins = GetNode<Label>("winsInfo/allWins");
		var oWins = GetNode<Label>("winsInfo/oWins");
		var xWins = GetNode<Label>("winsInfo/xWins");
		var wins2D = GetNode<Label>("winsInfo/wins2D");
		var wins3D = GetNode<Label>("winsInfo/wins3D");
		var wins3D2P = GetNode<Label>("winsInfo/wins3D2P");
		var wins3D3P = GetNode<Label>("winsInfo/wins3D3P");
		
		allWins.Text = $"WINS : {global.content["allWins"].ToString()}";
		oWins.Text = $"WINS AS O: {global.content["oWins"].ToString()}";
		xWins.Text = $"WINS AS X: {global.content["xWins"].ToString()}";
		wins2D.Text = $"2D WINS : {global.content["wins2D"].ToString()}";
		wins3D.Text = $"3D WINS : {global.content["wins3D"].ToString()}";
		wins3D2P.Text = $"3D WINS 2 PLAYER MODE: {global.content["wins3D2P"].ToString()}";
		wins3D3P.Text = $"3D WINS 3 PLAYER MODE: {global.content["wins3D3P"].ToString()}";
	}

	public override void _Process(double delta)
	{
		var backBtn = GetNode<Button>("backBtn");
		if (backBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenus/Settings.tscn");
	}
}
