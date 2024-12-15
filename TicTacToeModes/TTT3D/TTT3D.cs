using System;
using System.Collections.Generic;
using Godot;

namespace threeDTicTacToe;

public partial class TTT3D : Node3D
{
    public Dictionary<Button, Label3D> BtnAndboxMeshLabel3DDictionary = new Dictionary<Button, Label3D>(64);
    public Dictionary<Button, MeshInstance3D> BtnAndMeshInstanceDictionary = new Dictionary<Button, MeshInstance3D>(64);

    private List<Godot.Vector3> LabelPositions = new List<Godot.Vector3>();

    private bool shiftLock = false;
    private float mouseSensitivity = 0.001f;

    public override void _Ready()
    {
        var global = GetNode<Global>("/root/Global");
        if (global.player3DMode == "4x4x4")
        {
            GamePlay4x4x4 gamePlay4X4X4 = GetNode<GamePlay4x4x4>("/root/TTT3D/rightSide/GamePlay4x4x4");
            gamePlay4X4X4.Visible = true;
        }
        if (global.player3DMode == "3x3x3")
        {
            GamePlay3x3x3 gamePlay3X3X3 = GetNode<GamePlay3x3x3>("/root/TTT3D/rightSide/GamePlay3x3x3");
            gamePlay3X3X3.Visible = true;
        }
    }
    public void Create_Visualisation4x4x4()
    {
        Node3D visualisation = GetNode<Node3D>("/root/TTT3D/Visualisation4x4x4");
        GamePlay4x4x4 gamePlay = GetNode<GamePlay4x4x4>("/root/TTT3D/rightSide/GamePlay4x4x4");
        LabelPositions.Clear();
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    StandardMaterial3D miniMaterial = new StandardMaterial3D();
                    miniMaterial.AlbedoColor = new Color("#000000");
                    
                    MeshInstance3D minicube = new MeshInstance3D();
                    Color fgColor = new Color("#FFFFFF");
                    BoxMesh minibox = new BoxMesh();

                    minicube.Mesh = minibox;
                    minibox.Size = new Vector3(50, 50, 50);
                    minicube.Position = new Vector3(80 * x, -80 * z, -80 * y);
                    
                    minicube.MaterialOverride = miniMaterial;

                    Label3D label = new Label3D();
                    label.Text = "";
                    label.Modulate = fgColor;
                    label.Scale = new Vector3(label.Scale.X, label.Scale.Y, 200.0f);
                    label.PixelSize = 1;
                    label.FontSize = 100;
                    label.Position = new Vector3(80 * x, -80 * z, -80 * y);
                    LabelPositions.Add(label.Position);

                    gamePlay.Labels.Add(label);
                    gamePlay.MeshInstances.Add(minicube);

                    visualisation.AddChild(label);
                    visualisation.AddChild(minicube);
                }
            }
        }
    }
    public void Create_Visualisation3x3x3()
    {
        Node3D visualisation = GetNode<Node3D>("/root/TTT3D/Visualisation3x3x3");
        GamePlay3x3x3 gamePlay = GetNode<GamePlay3x3x3>("/root/TTT3D/rightSide/GamePlay3x3x3");
        LabelPositions.Clear();
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    StandardMaterial3D miniMaterial = new StandardMaterial3D();
                    miniMaterial.AlbedoColor = new Color("#000000");

                    MeshInstance3D minicube = new MeshInstance3D();
                    Color fgColor = new Color("#FFFFFF");
                    BoxMesh minibox = new BoxMesh();

                    minicube.Mesh = minibox;
                    minibox.Size = new Vector3(50, 50, 50);
                    minicube.Position = new Vector3(80 * x, -80 * z, -80 * y);
                    minicube.MaterialOverride = miniMaterial;

                    Label3D label = new Label3D();
                    label.Text = "";
                    label.Modulate = fgColor;
                    label.Scale = new Vector3(label.Scale.X, label.Scale.Y, 200.0f);
                    label.PixelSize = 1;
                    label.FontSize = 100;
                    label.Position = new Vector3(80 * x, -80 * z, -80 * y);
                    LabelPositions.Add(label.Position);

                    gamePlay.Labels.Add(label);
                    gamePlay.MeshInstances.Add(minicube);

                    visualisation.AddChild(label);
                    visualisation.AddChild(minicube);
                }
            }
        }
    }
    public override void _Input(InputEvent @event)
    {
        var global = GetNode<Global>("/root/Global");
        WinPopUp popUp = GetNodeOrNull<WinPopUp>("/root/WinPopUp");
        Button mainMenu = GetNode<Button>("leftSide/VBoxContainer/mainMenu");

        if (mainMenu.IsPressed())
        {
            GetTree().ChangeSceneToFile("res://MainMenus/MainMenu3D/MainMenu3D.tscn");
            if (popUp != null) popUp.QueueFree();
        }

        if (Input.IsActionPressed("shiftLock"))
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            shiftLock = true;
        }
        
        if (global.player3DMode == "4x4x4")
        {
            Node3D visualisation = GetNodeOrNull<Node3D>("/root/TTT3D/Visualisation4x4x4");
            if (Input.IsActionPressed("resetPosCube"))
            {
                GetNode<Node3D>("Visualisation4x4x4").Rotation = new Vector3(0, 0, 0);
            }

            if (shiftLock)
            {
                if (Input.IsActionPressed("unshiftLock"))
                {
                    Input.MouseMode = Input.MouseModeEnum.Visible;
                    shiftLock = false;
                }

                if (@event is InputEventMouseMotion mouseMotionEvent)
                {
                    HandleMouseMotion(mouseMotionEvent, visualisation);
                }
            }
        }

        if (global.player3DMode == "3x3x3")
        {
            Node3D visualisation = GetNodeOrNull<Node3D>("/root/TTT3D/Visualisation3x3x3");
            if (Input.IsActionPressed("resetPosCube"))
            {
                GetNode<Node3D>("Visualisation3x3x3").Rotation = new Vector3(0, 0, 0);
            }

            if (shiftLock)
            {
                if (Input.IsActionPressed("unshiftLock"))
                {
                    Input.MouseMode = Input.MouseModeEnum.Visible;
                    shiftLock = false;
                }

                if (@event is InputEventMouseMotion mouseMotionEvent)
                {
                    HandleMouseMotion(mouseMotionEvent, visualisation);
                }
            }
        }
    }
    private void HandleMouseMotion(InputEventMouseMotion mouseMotionEvent, Node3D visualisation)
    {
        Vector2 mouseDelta = mouseMotionEvent.Relative;

        if (mouseDelta.X != 0)
        {
            visualisation.RotateObjectLocal(Vector3.Up, -mouseDelta.X * mouseSensitivity);
        }

        if (mouseDelta.Y != 0)
        {
            visualisation.RotateObjectLocal(Vector3.Right, -mouseDelta.Y * mouseSensitivity);
        }
    }
    public override void _Process(double delta)
    {
    }
}
