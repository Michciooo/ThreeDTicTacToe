using System;
using System.Collections.Generic;
using Godot;

namespace threeDTicTacToe;

public partial class Main : Node3D
{
	public Dictionary<Button , Label3D> BtnAndboxMesLabel3DDictionary = new Dictionary<Button,Label3D>();
	public override void _Ready()
	{
		Create_Visualisation();
	}

	public void Create_Visualisation()
	{
		GamePlay gamePlay = GetNode<GamePlay>("/root/Main/rightSide/GamePlay");
		for (int y = 0; y < 3; y++)
		{
			for (int z = 0; z < 3; z++)
			{
				for (int x = 0; x < 3; x++)
				{
					StandardMaterial3D miniMaterial = new StandardMaterial3D();
					miniMaterial.AlbedoColor = new Color("#000000");

					MeshInstance3D minicube = new MeshInstance3D();
					Color fgColor = new Color("#FFFFFF");
					BoxMesh minibox = new BoxMesh();

					minicube.Mesh = minibox;
					minibox.Size = new Vector3(50, 50, 50);
					minicube.Position = new Vector3(80 * x, 80 * y, -80 * z);
					minicube.MaterialOverride = miniMaterial;

					Label3D label = new Label3D();
					label.Text = "";
					label.PixelSize = 1;
					label.Modulate = fgColor;
					label.FontSize = 100;
					label.Position = new Vector3(80 * x, 80 * y, -80 * z);

					gamePlay.Labels.Add(label);

					this.AddChild(label);
					this.AddChild(minicube);
				}
			}
		}
	}

	public override void _Process(double delta)
	{
		if (Input.IsPhysicalKeyPressed(Key.Q))
		{
			GetTree().Quit();
		}
	}
}