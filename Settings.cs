using Godot;
using System;

namespace threeDTicTacToe
{
    public partial class Settings : Control
    {
        public override void _Ready()
        {
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLock");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLock");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCube");
            var appExitTextInput = this.GetNode<TextEdit>("Main/KeysInput/appExit");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/KeysInput/mainMenu");
            
            var btn1 = this.GetNode<Button>("Main/RestartBtns/btn1");
            var btn2 = this.GetNode<Button>("Main/RestartBtns/btn2");
            var btn3 = this.GetNode<Button>("Main/RestartBtns/btn3");
            var btn4 = this.GetNode<Button>("Main/RestartBtns/btn4");
            var btn5 = this.GetNode<Button>("Main/RestartBtns/btn5");
            
            Binding();

            btn1.Pressed += btnPress;
            btn2.Pressed += btnPress;
            btn3.Pressed += btnPress;
            btn4.Pressed += btnPress;
            btn5.Pressed += btnPress;
        }

        private void Binding()
        {
            Global global = (Global)GetNode("/root/Global");
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLock");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLock");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCube");
            var appExitTextInput = this.GetNode<TextEdit>("Main/KeysInput/appExit");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/KeysInput/mainMenu");
            

            shiftLockTextInput.Text = global.shiftLockKey.ToString();
            unshiftLockTextInput.Text = global.unShiftLockKey.ToString();
            restartPosCubeTextInput.Text = global.restartPosCubeKey.ToString();
            appExitTextInput.Text = global.appExitKey.ToString();
            mainMenuTextInput.Text = global.mainMenuKey.ToString();
            
        }

        private void btnPress()
        {
            Global global = (Global)GetNode("/root/Global");
            var btn1 = this.GetNode<Button>("Main/RestartBtns/btn1");
            var btn2 = this.GetNode<Button>("Main/RestartBtns/btn2");
            var btn3 = this.GetNode<Button>("Main/RestartBtns/btn3");
            var btn4 = this.GetNode<Button>("Main/RestartBtns/btn4");
            var btn5 = this.GetNode<Button>("Main/RestartBtns/btn5");
            
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLock");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLock");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCube");
            var appExitTextInput = this.GetNode<TextEdit>("Main/KeysInput/appExit");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/KeysInput/mainMenu");
            if (btn1.IsPressed())
            {
                shiftLockTextInput.Text = "Shift";
                global.shiftLockKey = shiftLockTextInput.Text;
                AddKeyToAction("shiftLock" , Key.Shift);
            }
            if (btn2.IsPressed())
            {
                unshiftLockTextInput.Text = "Ctrl";
                global.unShiftLockKey = unshiftLockTextInput.Text;
                AddKeyToAction("unshiftLock" , Key.Ctrl);
            }
            if (btn3.IsPressed())
            {
                restartPosCubeTextInput.Text = "R";
                global.restartPosCubeKey = restartPosCubeTextInput.Text;
                AddKeyToAction("resetPosCube" , Key.R);
            }
            if (btn4.IsPressed())
            {
                mainMenuTextInput.Text = "Escape";
                global.mainMenuKey = mainMenuTextInput.Text;
                AddKeyToAction("mainMenu" , Key.Escape);
            }
            if (btn5.IsPressed())
            {
                appExitTextInput.Text = "Q";
                global.appExitKey = appExitTextInput.Text;
                AddKeyToAction("appExit" , Key.Q);
            }
        }


        public override void _Input(InputEvent @event)
        {
            Global global = (Global)GetNode("/root/Global");
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLock");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLock");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCube");
            var appExitTextInput = this.GetNode<TextEdit>("Main/KeysInput/appExit");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/KeysInput/mainMenu");
            if (@event is InputEventKey eventKey && eventKey.Pressed)
            {
                if (shiftLockTextInput.HasFocus())
                {
                    if (unshiftLockTextInput.Text == shiftLockTextInput.Text ||
                        appExitTextInput.Text == shiftLockTextInput.Text ||
                        restartPosCubeTextInput.Text == shiftLockTextInput.Text
                        || mainMenuTextInput.Text == shiftLockTextInput.Text)
                    {
                        shiftLockTextInput.Text = shiftLockTextInput.Text;
                    }
                    else
                    {
                        shiftLockTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                        global.shiftLockKey = shiftLockTextInput.Text;
                        AddKeyToAction("shiftLock" , eventKey.Keycode);
                    }
                }
                if (unshiftLockTextInput.HasFocus())
                {
                    unshiftLockTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                    global.unShiftLockKey = unshiftLockTextInput.Text;
                    AddKeyToAction("unshiftLock" , eventKey.Keycode);
                }
                if (restartPosCubeTextInput.HasFocus())
                {
                    restartPosCubeTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                    global.restartPosCubeKey = restartPosCubeTextInput.Text;
                    AddKeyToAction("resetPosCube" , eventKey.Keycode);
                }
                if (appExitTextInput.HasFocus())
                {
                    appExitTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                    global.appExitKey = appExitTextInput.Text;
                    AddKeyToAction("appExit" , eventKey.Keycode);
                }
                if (mainMenuTextInput.HasFocus())
                {
                    if (unshiftLockTextInput.Text == mainMenuTextInput.Text ||
                        appExitTextInput.Text == mainMenuTextInput.Text ||
                        restartPosCubeTextInput.Text == mainMenuTextInput.Text
                        || shiftLockTextInput.Text == mainMenuTextInput.Text)
                    {
                        mainMenuTextInput.Text = mainMenuTextInput.Text;
                    }
                    else
                    {
                        mainMenuTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                        global.mainMenuKey = mainMenuTextInput.Text;
                        AddKeyToAction("mainMenu" , eventKey.Keycode);
                    }
                }
            }
            
        }

        private void AddKeyToAction(string actionName, Key keycode)
        {
            if (InputMap.HasAction(actionName))
            {
                InputMap.ActionEraseEvents(actionName);
                InputEventKey inputEventKey = new InputEventKey();
                inputEventKey.Keycode = keycode;
                InputMap.ActionAddEvent(actionName, inputEventKey);
            }
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