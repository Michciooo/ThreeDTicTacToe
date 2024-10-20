using Godot;
using System;

namespace threeDTicTacToe
{
    //to chyba dziala
    
    public partial class Settings : Control
    {
        public Key[] keysList = new[]
        {
            Key.Key0, Key.Key1, Key.Key2, Key.Key3, Key.Key4, Key.Key5, Key.Key6, Key.Key7, Key.Key8, Key.Key9,
            Key.Minus, Key.Equal, Key.Backslash, Key.Backspace, Key.Escape,
            Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12,
            Key.Tab, Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I, Key.O, Key.P,
            Key.Bracketleft, Key.Bracketright, Key.Enter, Key.Capslock, Key.A, Key.S, Key.D, Key.F, Key.G, 
            Key.H, Key.J, Key.K, Key.L, Key.Semicolon, Key.Apostrophe, Key.Shift, 
            Key.Z, Key.X, Key.C, Key.V, Key.B, Key.N, Key.M,
            Key.Comma, Key.Period, Key.Slash, Key.Ctrl, Key.Alt, Key.Space,
            Key.Up, Key.Down, Key.Left, Key.Right, 
            Key.Delete, Key.Home, Key.End, Key.Pageup, Key.Pagedown,
            Key.Print, Key.Scrolllock, Key.Pause, 
            Key.KpDivide, Key.KpMultiply, Key.KpSubtract, 
            Key.KpAdd, Key.KpEnter, Key.KpPeriod, 
            Key.Kp0, Key.Kp1, Key.Kp2, Key.Kp3, Key.Kp4, Key.Kp5, Key.Kp6, Key.Kp7, Key.Kp8, Key.Kp9
        };

        public override void _Ready()
        {	
            KeyListExhale();
        }

        public void KeyListExhale()
        {
            OptionButton cubeRotating = GetNode<OptionButton>("Main/KeysInputs/cubeRotating");
            OptionButton markingFields = GetNode<OptionButton>("Main/KeysInputs/markingFields");
            OptionButton restartCube = GetNode<OptionButton>("Main/KeysInputs/restartCube");
            OptionButton mainMenu = GetNode<OptionButton>("Main/KeysInputs/mainMenu");
            OptionButton appExit = GetNode<OptionButton>("Main/KeysInputs/appExit");
            
            if (cubeRotating == null || markingFields == null || restartCube == null || mainMenu == null || appExit == null)
            {
                return;
            }
            foreach (var el in keysList)
            {
                string keyName = el.ToString();
                cubeRotating.AddItem(keyName);
                markingFields.AddItem(keyName);
                restartCube.AddItem(keyName);
                mainMenu.AddItem(keyName);
                appExit.AddItem(keyName);
            }
            SetDefaultKeys(cubeRotating, markingFields, restartCube, mainMenu, appExit);
        }

        private void SetDefaultKeys(OptionButton cubeRotating, OptionButton markingFields, OptionButton restartCube, OptionButton mainMenu, OptionButton appExit)
        {
            int key1 = Array.IndexOf(keysList, Key.Shift);
            int key2 = Array.IndexOf(keysList, Key.Ctrl);
            int key3 = Array.IndexOf(keysList, Key.R);
            int key4 = Array.IndexOf(keysList, Key.Escape);
            int key5 = Array.IndexOf(keysList, Key.Q);
            
            if (key1 >= 0) cubeRotating.Select(key1);
            if (key2 >= 0) markingFields.Select(key2);
            if (key3 >= 0) restartCube.Select(key3);
            if (key4 >= 0) mainMenu.Select(key4);
            if (key5 >= 0) appExit.Select(key5);
        }

        public override void _Process(double delta)
        {
            var mainMenu = GetNode<OptionButton>("Main/KeysInputs/mainMenu");
            var key = keysList[mainMenu.Selected];

            if (Input.IsPhysicalKeyPressed(key))
            {
                GetTree().ChangeSceneToFile("res://MainMenu.tscn");
            }
        }
    }
}