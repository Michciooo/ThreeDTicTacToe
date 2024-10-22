using Godot;
using System;

namespace threeDTicTacToe
{
    public partial class MainMenu : Node3D
    {
        public override void _Ready()
        {
            TextureButton settings = GetNode<TextureButton>("Settings/SettingsBtn");
            Button friendGameButton = GetNode<Button>("MainContainer/Container/FriendPlay");
            
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
            //Settings settings = GetNode<Settings>("");
            if(Input.IsActionPressed("appExit")) GetTree().Quit();
        }
    }
}