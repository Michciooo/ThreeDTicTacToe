using Godot;
using System;

namespace threeDTicTacToe
{
    public partial class Settings : Control
    {
        // public Key[] keysList = new[]
        // {
        //     Key.Key0, Key.Key1, Key.Key2, Key.Key3, Key.Key4, Key.Key5, Key.Key6, Key.Key7, Key.Key8, Key.Key9,
        //     Key.Minus, Key.Equal, Key.Backslash, Key.Backspace, Key.Escape,
        //     Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12,
        //     Key.Tab, Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I, Key.O, Key.P,
        //     Key.Bracketleft, Key.Bracketright, Key.Enter, Key.Capslock, Key.A, Key.S, Key.D, Key.F, Key.G, 
        //     Key.H, Key.J, Key.K, Key.L, Key.Semicolon, Key.Apostrophe, Key.Shift, 
        //     Key.Z, Key.X, Key.C, Key.V, Key.B, Key.N, Key.M,
        //     Key.Comma, Key.Period, Key.Slash, Key.Ctrl, Key.Alt, Key.Space,
        //     Key.Up, Key.Down, Key.Left, Key.Right, 
        //     Key.Delete, Key.Home, Key.End, Key.Pageup, Key.Pagedown,
        //     Key.Print, Key.Scrolllock, Key.Pause, 
        //     Key.KpDivide, Key.KpMultiply, Key.KpSubtract, 
        //     Key.KpAdd, Key.KpEnter, Key.KpPeriod, 
        //     Key.Kp0, Key.Kp1, Key.Kp2, Key.Kp3, Key.Kp4, Key.Kp5, Key.Kp6, Key.Kp7, Key.Kp8, Key.Kp9
        // };

        public override void _Ready()
        {
            Binding();
        }

        private void Binding()
        {
            Global global = (Global)GetNode("/root/Global");
    
            var shiftLockTextInput = GetNodeOrNull<TextEdit>("/root/Settings/Main/KeysInput/shiftLock");
            if (shiftLockTextInput == null) 
            {
                GD.PrintErr("Node not found: shiftLock");
            }
            var unshiftLockTextInput = GetNodeOrNull<TextEdit>("/root/Settings/Main/KeysInput/unShiftLock");
            if (unshiftLockTextInput == null) 
            {
                GD.PrintErr("Node not found: unShiftLock");
            }
            var restartPosCubeTextInput = GetNodeOrNull<TextEdit>("/root/Settings/Main/KeysInput/restartPosCube");
            if (restartPosCubeTextInput == null) 
            {
                GD.PrintErr("Node not found: restartPosCube");
            }
            var mainMenuTextInput = GetNodeOrNull<TextEdit>("/root/Settings/Main/KeysInput/mainMenu");
            if (mainMenuTextInput == null) 
            {
                GD.PrintErr("Node not found: mainMenu");
            }
            var appExitTextInput = GetNodeOrNull<TextEdit>("/root/Settings/Main/KeysInput/appExit");
            if (appExitTextInput == null) 
            {
                GD.PrintErr("Node not found: appExit");
            }

            // Assign Text values only if nodes are not null
            if (shiftLockTextInput != null)
                shiftLockTextInput.Text = global.shiftLockKey.ToString();
            if (unshiftLockTextInput != null)
                unshiftLockTextInput.Text = global.unShiftLockKey.ToString();
            if (restartPosCubeTextInput != null)
                restartPosCubeTextInput.Text = global.restartPosCubeKey.ToString();
            if (appExitTextInput != null)
                appExitTextInput.Text = global.appExitKey.ToString();
            if (mainMenuTextInput != null)
                mainMenuTextInput.Text = global.mainMenuKey.ToString();
        }
        public override void _Process(double delta)
        {
            if (Input.IsActionPressed("mainMenu"))
            {
                GetTree().ChangeSceneToFile("res://MainMenu.tscn");
            }
        }
    }
}