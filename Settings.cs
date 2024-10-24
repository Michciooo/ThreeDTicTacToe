using Godot;
using System;
using System.Collections.Generic;

namespace threeDTicTacToe
{
    public partial class Settings : Control
    {
        private List<Key> keysList = new List<Key>();
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
            
            keysList.Add(global.shiftLockKey);
            keysList.Add(global.unShiftLockKey);
            keysList.Add(global.restartPosCubeKey);
            keysList.Add(global.mainMenuKey);
            keysList.Add(global.appExitKey);
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
                keysList.Remove(global.shiftLockKey);
                global.shiftLockKey = (Key)OS.FindKeycodeFromString(shiftLockTextInput.Text);
                keysList.Add(global.shiftLockKey);
                AddKeyToAction("shiftLock" , Key.Shift);
            }
            if (btn2.IsPressed())
            {
                unshiftLockTextInput.Text = "Ctrl";
                keysList.Remove(global.unShiftLockKey);
                global.unShiftLockKey = (Key)OS.FindKeycodeFromString(unshiftLockTextInput.Text);
                keysList.Add(global.unShiftLockKey);
                AddKeyToAction("unshiftLock" , Key.Ctrl);
            }
            if (btn3.IsPressed())
            {
                restartPosCubeTextInput.Text = "R";
                keysList.Remove(global.restartPosCubeKey);
                global.restartPosCubeKey = (Key)OS.FindKeycodeFromString(restartPosCubeTextInput.Text);
                keysList.Add(global.restartPosCubeKey);
                AddKeyToAction("resetPosCube" , Key.R);
            }
            if (btn4.IsPressed())
            {
                mainMenuTextInput.Text = "Escape";
                keysList.Remove(global.mainMenuKey);
                global.mainMenuKey = (Key)OS.FindKeycodeFromString(mainMenuTextInput.Text);
                keysList.Add(global.mainMenuKey);
                AddKeyToAction("mainMenu" , Key.Escape);
            }
            if (btn5.IsPressed())
            {
                appExitTextInput.Text = "Q";
                keysList.Remove(global.appExitKey);
                global.appExitKey = (Key)OS.FindKeycodeFromString(appExitTextInput.Text);
                keysList.Add(global.appExitKey);
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
                    if (eventKey.Keycode == keysList[0] || eventKey.Keycode == keysList[1] ||
                        eventKey.Keycode == keysList[2] || eventKey.Keycode == keysList[3] ||
                        eventKey.Keycode == keysList[4])
                    {
                        shiftLockTextInput.Text = shiftLockTextInput.Text;
                    }
                    else
                    {
                        shiftLockTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                        keysList.Remove(global.shiftLockKey);
                        global.shiftLockKey = (Key)OS.FindKeycodeFromString(shiftLockTextInput.Text);
                        keysList.Add(global.shiftLockKey);
                        AddKeyToAction("shiftLock" , eventKey.Keycode);
                    }
                }
                if (unshiftLockTextInput.HasFocus())
                {
                    if (eventKey.Keycode == keysList[0] || eventKey.Keycode == keysList[1] ||
                        eventKey.Keycode == keysList[2] || eventKey.Keycode == keysList[3] ||
                        eventKey.Keycode == keysList[4])
                    {
                        unshiftLockTextInput.Text = unshiftLockTextInput.Text;
                    }
                    else
                    {
                        unshiftLockTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                        keysList.Remove(global.unShiftLockKey);
                        global.unShiftLockKey = (Key)OS.FindKeycodeFromString(unshiftLockTextInput.Text);
                        keysList.Add(global.unShiftLockKey);
                        AddKeyToAction("unshiftLock", eventKey.Keycode);
                    }
                }
                if (restartPosCubeTextInput.HasFocus())
                {
                    if (eventKey.Keycode == keysList[0] || eventKey.Keycode == keysList[1] ||
                        eventKey.Keycode == keysList[2] || eventKey.Keycode == keysList[3] ||
                        eventKey.Keycode == keysList[4])
                    {
                        restartPosCubeTextInput.Text = restartPosCubeTextInput.Text;
                    }
                    else
                    {
                        restartPosCubeTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                        keysList.Remove(global.restartPosCubeKey);
                        global.restartPosCubeKey = (Key)OS.FindKeycodeFromString(restartPosCubeTextInput.Text);
                        keysList.Add(global.restartPosCubeKey);
                        AddKeyToAction("resetPosCube", eventKey.Keycode);
                    }
                }
                if (mainMenuTextInput.HasFocus())
                {
                    if (eventKey.Keycode == keysList[0] || eventKey.Keycode == keysList[1] ||
                        eventKey.Keycode == keysList[2] || eventKey.Keycode == keysList[3] ||
                        eventKey.Keycode == keysList[4])
                    {
                        mainMenuTextInput.Text = mainMenuTextInput.Text;
                    }
                    else
                    {
                        mainMenuTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                        keysList.Remove(global.mainMenuKey);
                        global.mainMenuKey = (Key)OS.FindKeycodeFromString(mainMenuTextInput.Text);
                        keysList.Add(global.mainMenuKey);
                        AddKeyToAction("mainMenu" , eventKey.Keycode);
                    }
                }
                if (appExitTextInput.HasFocus())
                {
                    if (eventKey.Keycode == keysList[0] || eventKey.Keycode == keysList[1] ||
                        eventKey.Keycode == keysList[2] || eventKey.Keycode == keysList[3] ||
                        eventKey.Keycode == keysList[4])
                    {
                        appExitTextInput.Text = appExitTextInput.Text;
                    }
                    else
                    {
                        appExitTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
                        keysList.Remove(global.appExitKey);
                        global.appExitKey = (Key)OS.FindKeycodeFromString(appExitTextInput.Text);
                        keysList.Add(global.appExitKey);
                        AddKeyToAction("appExit", eventKey.Keycode);
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