using Godot;
using System;

namespace threeDTicTacToe
{
    public partial class MainMenu : Node3D
    {
        public override void _Ready()
        {
            TextureButton settings = GetNode<TextureButton>("Main/Settings/SettingsBtn");
            Button friendGameButton = GetNode<Button>("Main/MainContainer/Container/FriendPlay");
            Input.MouseMode = Input.MouseModeEnum.Confined;
            
            if (settings != null)
            {
                settings.Pressed += SettingsPress;
            }
            if (friendGameButton != null)
            {
                friendGameButton.Pressed += FriendGameButtonOnPressed;
            }
        }

        private void SettingsPress()
        {
            GetTree().ChangeSceneToFile("res://Settings.tscn");
        }

        private void FriendGameButtonOnPressed()
        {
            GetTree().ChangeSceneToFile("res://Main.tscn");
        }

        public override void _Process(double delta)
        {
            if(Input.IsActionPressed("appExit")) GetTree().Quit();
        }
    }
}