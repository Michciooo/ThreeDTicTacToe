using System;
using System.Collections.Generic;
using Godot;

namespace threeDTicTacToe;

public partial class Main : Node3D
{
	public Dictionary<Button , Label3D> BtnAndboxMesLabel3DDictionary = new Dictionary<Button,Label3D>();
	
	private bool shiftLock = false; 
	private float rotationSpeed = 0.05f;
	private float mouseSensitivity = 0.1f;

	public override void _Ready()
	{
		Create_Visualisation();
	}

	public void Create_Visualisation()
	{
		GamePlay gamePlay = GetNode<GamePlay>("/root/Main/rightSide/GamePlay");
		Node3D visualisation = GetNode<Node3D>("/root/Main/Visualisation");
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
					label.Modulate = fgColor;
					label.Scale = new Vector3(label.Scale.X, label.Scale.Y, 200.0f);
					label.PixelSize = 1;
					label.FontSize = 100;
					label.Position = new Vector3(80 * x, 80 * y, -80 * z);

					gamePlay.Labels.Add(label);
					
					visualisation.AddChild(label);
					visualisation.AddChild(minicube);
				}
			}
		}
	}
	public override void _Input(InputEvent @event)
	{
		Node3D visualisation = GetNode<Node3D>("/root/Main/Visualisation");
		if (@event.IsActionPressed("shift"))
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
			shiftLock = true;
		}
		if (shiftLock && @event.IsActionPressed("control"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
			shiftLock = false;
		}
		if (shiftLock)
		{
			if (@event is InputEventMouseMotion mouseMotionEvent)
			{
				Vector2 mouseDelta = mouseMotionEvent.Relative;
			
				visualisation.Translate(new Vector3(mouseDelta.X, -mouseDelta.Y, 0) * mouseSensitivity);
			}
		
			if (Input.IsKeyPressed(Key.W))
			{
				visualisation.RotateObjectLocal(Vector3.Right, -rotationSpeed);
			}
			if (Input.IsKeyPressed(Key.S))
			{
				visualisation.RotateObjectLocal(Vector3.Right, rotationSpeed);
			}
			if (Input.IsKeyPressed(Key.A)) 
			{
				visualisation.RotateObjectLocal(Vector3.Up, rotationSpeed);
			}
			if (Input.IsKeyPressed(Key.D)) 
			{
				visualisation.RotateObjectLocal(Vector3.Up, -rotationSpeed);
			}
			if (Input.IsKeyPressed(Key.Q)) 
			{
				visualisation.RotateObjectLocal(Vector3.Forward, -rotationSpeed);
			}
			if (Input.IsKeyPressed(Key.E)) 
			{
				visualisation.RotateObjectLocal(Vector3.Forward, rotationSpeed);
			}
		}
	}

	public override void _Process(double delta)
	{
		if (Input.IsPhysicalKeyPressed(Key.Escape))
		{
			GetTree().Quit();
		}
	}
}
