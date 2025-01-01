using Godot;
using System;
using System.Collections.Generic;

namespace threeDTicTacToe
{
    public partial class Settings3D : Control
    {
        private List<Key> keysList = new List<Key>();

        public override void _Ready()
        {
            Binding();
            SetupButtons();
        }

        private void SetupButtons()
        {
            var btn1 = this.GetNode<Button>("Main/RestartBtns/btn1");
            var btn2 = this.GetNode<Button>("Main/RestartBtns/btn2");
            var btn3 = this.GetNode<Button>("Main/RestartBtns/btn3");

            btn1.Pressed += () => btnPress(btn1);
            btn2.Pressed += () => btnPress(btn2);
            btn3.Pressed += () => btnPress(btn3);
        }

        private void Binding()
        {
            Global global = (Global)GetNode("/root/Global");
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLock");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLock");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCube");

            shiftLockTextInput.Text = global.shiftLockKey.ToString();
            unshiftLockTextInput.Text = global.unShiftLockKey.ToString();
            restartPosCubeTextInput.Text = global.restartPosCubeKey.ToString();

            keysList.Clear();
            keysList.Add(global.shiftLockKey);
            keysList.Add(global.unShiftLockKey);
            keysList.Add(global.restartPosCubeKey);
        }

        private void btnPress(Button button)
        {
            Global global = (Global)GetNode("/root/Global");
            TextEdit targetTextInput = null;
            Key oldKey = Key.Unknown;

            if (button.Name == "btn1")
            {
                global.ClickSFX("res://sfx/btn_click.wav");
                targetTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLock");
                oldKey = global.shiftLockKey;
            }
            else if (button.Name == "btn2")
            {
                global.ClickSFX("res://sfx/btn_click.wav");
                targetTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLock");
                oldKey = global.unShiftLockKey;
            }
            else if (button.Name == "btn3")
            {
                global.ClickSFX("res://sfx/btn_click.wav");
                targetTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCube");
                oldKey = global.restartPosCubeKey;
            }
            if (targetTextInput != null)
            {
                UpdateKeyBinding(targetTextInput, button.Name, global, oldKey);
            }
        }
        private void UpdateKeyBinding(TextEdit textInput, string actionName, Global global, Key oldKey)
        {
            if (oldKey != Key.Unknown)
            {
                keysList.Remove(oldKey);
            }

            textInput.Text = "";
        }

        public override void _Input(InputEvent @event)
        {
            Global global = (Global)GetNode("/root/Global");
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLock");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLock");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCube");

            if (@event is InputEventKey eventKey && eventKey.Pressed)
            {
                if (shiftLockTextInput.HasFocus())
                {
                    AssignKey(eventKey, shiftLockTextInput, ref global.shiftLockKey, ref global.shiftLockKeyValue);
                }
                else if (unshiftLockTextInput.HasFocus())
                {
                    AssignKey(eventKey, shiftLockTextInput, ref global.unShiftLockKey, ref global.unShiftLockKeyValue);
                }
                else if (restartPosCubeTextInput.HasFocus())
                {
                    AssignKey(eventKey, restartPosCubeTextInput, ref global.restartPosCubeKey, ref global.restartPosCubeKeyValue);
                }
            }
        }

        private void AssignKey(InputEventKey eventKey, TextEdit focusedTextInput, ref Key globalKey,
            ref string keyValue)
        {
            Key newKey = (Key)eventKey.Keycode;

            if (!keysList.Contains(newKey))
            {
                if (globalKey != Key.Unknown)
                {
                    keysList.Remove(globalKey);
                    RemoveKeyFromAction(focusedTextInput.Name, globalKey);
                }

                string keyText;
                switch (newKey)
                {
                    case Key.Quoteleft:
                        keyText = "`";
                        break;
                    case Key.Minus:
                    case Key.KpSubtract:
                        keyText = "-";
                        break;
                    case Key.Equal:
                        keyText = "=";
                        break;
                    case Key.Backslash:
                        keyText = "\\";
                        break;
                    case Key.Bracketleft:
                        keyText = "[";
                        break;
                    case Key.Bracketright:
                        keyText = "]";
                        break;
                    case Key.Semicolon:
                        keyText = ";";
                        break;
                    case Key.Apostrophe:
                        keyText = "'";
                        break;
                    case Key.Comma:
                        keyText = ",";
                        break;
                    case Key.Period:
                        keyText = ".";
                        break;
                    case Key.Slash:
                    case Key.KpDivide:
                        keyText = "/";
                        break;
                    case Key.KpMultiply:
                        keyText = "*";
                        break;
                    case Key.KpAdd:
                        keyText = "+";
                        break;
                    case Key.KpEnter:
                        keyText = "Enter";
                        break;
                    default:
                        keyText = OS.GetKeycodeString(eventKey.Keycode);
                        break;
                }

                focusedTextInput.Text = keyText;

                keyValue = keyText;
                keysList.Add(newKey);
                globalKey = newKey;

                AddKeyToAction(focusedTextInput.Name, newKey);
            }
        }
        private void AddKeyToAction(string actionName, Key key)
        {
            InputMap.AddAction(actionName);
            InputEventKey inputEventKey = new InputEventKey { Keycode = key };
            InputMap.ActionAddEvent(actionName, inputEventKey);
        }
        private void RemoveKeyFromAction(string actionName, Key key)
        {
            if (InputMap.HasAction(actionName))
            {
                InputMap.ActionEraseEvents(actionName);
            }
        }

        public override void _Process(double delta)
        {
            var global = (Global)GetNode("/root/Global");
            var mainMenuBtn = this.GetNode<Button>("mainMenu");
            if (mainMenuBtn.IsPressed())
            {
                global.ClickSFX("res://sfx/btn_click.wav");
                GetTree().ChangeSceneToFile("res://MainMenus/MainMenu3D/MainMenu3D.tscn");
            }
        }
    }
}
