using System;
using System.Collections.Generic;
using Godot;

namespace threeDTicTacToe;

public partial class Main : Node3D
{
	public Dictionary<Button , Label3D> BtnAndboxMesLabel3DDictionary = new Dictionary<Button,Label3D>();
	private Camera3D camera;
	private Transform3D initialTransform;
	
	public float mouseSensitivity = 0.05f; 
	private Vector2 mouseDelta = Vector2.Zero;
	public override void _Ready()
	{
		Create_Visualisation();
		
		camera = GetNode<Camera3D>("/root/Main/Camera/camera");
		initialTransform = camera.Transform; 
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
		if (@event is InputEventMouseMotion mouseEvent)
		{
			if (Input.IsMouseButtonPressed(MouseButton.Right))
			{
				mouseDelta = mouseEvent.Relative;
			}
		}
	}
	public override void _Process(double delta)
	{
		if (Input.IsPhysicalKeyPressed(Key.Q))
		{
			GetTree().Quit();
		}

		if (Input.IsPhysicalKeyPressed(Key.R))
		{
			var restartCamera = GetNode<Camera3D>("/root/Main/Camera/camera");
			restartCamera.Transform = initialTransform;
		}
		CameraMovement(mouseDelta);
	}
	public void CameraMovement(Vector2 delta)
	{
		camera = GetNode<Camera3D>("/root/Main/Camera/camera");
		
		float horizontalRotation = -delta.X * mouseSensitivity;
		float verticalRotation = -delta.Y * mouseSensitivity;
		
		camera.RotationDegrees = new Vector3(
			Mathf.Clamp(camera.RotationDegrees.X + verticalRotation, -90, 90), // Ograniczenie kąta pionowego
			camera.RotationDegrees.Y + horizontalRotation,
			camera.RotationDegrees.Z
		);
	}
}
