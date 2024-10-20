using System;
using System.Collections.Generic;
using Godot;

namespace threeDTicTacToe;

public partial class Main : Node3D
{
    public Dictionary<Button, Label3D> BtnAndboxMeshLabel3DDictionary = new Dictionary<Button, Label3D>();
    public Dictionary<Button, MeshInstance3D> BtnAndMeshInstanceDictionary = new Dictionary<Button, MeshInstance3D>();
    private List<Godot.Vector3> LabelPositions = new List<Godot.Vector3>();
    
    private PackedScene visScene = GD.Load<PackedScene>("res://Visualisation.tscn");
    private Node3D visRoot;
    
    private bool shiftLock = false;
    private float mouseSensitivity = 0.001f;

    public override void _Ready()
    {
    }

    public void Create_Visualisation()
    {
        Node3D visualisation = GetNode<Node3D>("/root/Main/Visualisation");
        GamePlay gamePlay = GetNode<GamePlay>("/root/Main/rightSide/GamePlay");

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
        GamePlay gamePlay = GetTree().Root.GetNode<GamePlay>("/root/Main/rightSide/GamePlay");
        Node3D visualisation = GetNode<Node3D>("/root/Main/Visualisation");
        if (@event.IsActionPressed("shift"))
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            shiftLock = true;
        }

        if (@event.IsActionPressed("restart"))
        {
            List<String> savedTexts = new List<String>();
            for (int i = 0; i < gamePlay.TttBtns.Count; i++)
            {
                Button btn = gamePlay.TttBtns[i];
                if (BtnAndboxMeshLabel3DDictionary.ContainsKey(btn))
                {
                    savedTexts.Add(BtnAndboxMeshLabel3DDictionary[btn].Text);
                }
                else
                {
                    savedTexts.Add("");
                }
                Create_Visualisation();
                if (BtnAndboxMeshLabel3DDictionary.ContainsKey(btn))
                {
                    BtnAndboxMeshLabel3DDictionary[btn].Text = savedTexts[i];
                    //GD.Print("SKIBIDI");
                }
            }
        }
        if (shiftLock)
        {
            if (@event.IsActionPressed("control"))
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
                shiftLock = false;
            }
            if (@event is InputEventMouseMotion mouseMotionEvent)
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
        }
    }
    public override void _Process(double delta)
    {
        if (Input.IsPhysicalKeyPressed(Key.Escape))
        {
            //var MainScene = GD.Load<PackedScene>("res://MainMenu.tscn");
            GetTree().ChangeSceneToFile("res://MainMenu.tscn");
        }
    }
}
