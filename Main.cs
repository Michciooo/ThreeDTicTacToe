using System;
using System.Collections.Generic;
using Godot;

namespace threeDTicTacToe;

public partial class Main : Node3D
{
	public List<String> BtnsContent2 = new List<String>();
	public override void _Ready()
	{
		GamePlay gamePlay = GetNode<GamePlay>("/root/Main/rightSide/GamePlay"); 
		Console.WriteLine(String.Join(", ", BtnsContent2));
		
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				for (int z = 0; z < 3; z++)
				{
					StandardMaterial3D miniMaterial = new StandardMaterial3D();
					miniMaterial.AlbedoColor = new Color("#d2d2e8");
					
					//miniMaterial.Transparency = BaseMaterial3D.TransparencyEnum.Alpha; 
					
					MeshInstance3D minicube = new MeshInstance3D();
					BoxMesh minibox = new BoxMesh();
					
					minicube.Mesh = minibox;
					minibox.Size = new Vector3(50, 50, 50);
					minicube.Position = new Vector3(80 * z, 80 * y, -80 * x);
					minicube.MaterialOverride = miniMaterial;
					
					Label3D label = new Label3D();
					label.Text = "";
					label.PixelSize = 1;
					label.Modulate = new Color(0, 0, 0);
					label.FontSize = 100;
					label.Position=new Vector3(80 * z, 80 * y, -80*x);
					
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