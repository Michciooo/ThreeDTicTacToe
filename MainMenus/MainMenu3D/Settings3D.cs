using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace threeDTicTacToe
{
    public partial class Settings3D : Control
    {
        private List<Key> keysList = new List<Key>();
        private String statsPath = "settings.json";
        private Global global;

        public override void _Ready()
        {
            Binding();
        }
        
        private void Binding()
        {
            global = (Global)GetNode("/root/Global");
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLockKey");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLockKey");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCubeKey");

            keysList.Clear();
            keysList.Add(global.KeyBind["shiftLockKey"]);
            keysList.Add(global.KeyBind["unShiftLockKey"]);
            keysList.Add(global.KeyBind["restartPosCubeKey"]);
            
            shiftLockTextInput.Text = global.KeyBindValue["shiftLockKey"];
            unshiftLockTextInput.Text = global.KeyBindValue["unShiftLockKey"];
            restartPosCubeTextInput.Text = global.KeyBindValue["restartPosCubeKey"];
        }
        public override void _Input(InputEvent @event)
        {
            global = (Global)GetNode("/root/Global");
            var shiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/shiftLockKey");
            var unshiftLockTextInput = this.GetNode<TextEdit>("Main/KeysInput/unShiftLockKey");
            var restartPosCubeTextInput = this.GetNode<TextEdit>("Main/KeysInput/restartPosCubeKey");
            
            InputMap.ActionEraseEvents("shiftLockKey");
            InputMap.ActionEraseEvents("unShiftLockKey");
            InputMap.ActionEraseEvents("restartPosCubeKey");
    
            InputEventKey shiftLockKey = new InputEventKey { Keycode = global.shiftLockKey };
            InputEventKey unShiftLockKey = new InputEventKey { Keycode = global.unShiftLockKey };
            InputEventKey restartPosCubeKey = new InputEventKey { Keycode = global.restartPosCubeKey };

            InputMap.ActionAddEvent("shiftLockKey", shiftLockKey);
            InputMap.ActionAddEvent("unShiftLockKey", unShiftLockKey);
            InputMap.ActionAddEvent("restartPosCubeKey", restartPosCubeKey);
            
            if (@event is InputEventKey eventKey && eventKey.Pressed)
            {
                if (shiftLockTextInput.HasFocus())
                {
                    AssignKey(eventKey, shiftLockTextInput, ref global.shiftLockKey, ref global.shiftLockKeyValue);
                    shiftLockTextInput.Text = global.KeyBindValue["shiftLockKey"];
                }
                else if (unshiftLockTextInput.HasFocus())
                {
                    AssignKey(eventKey, shiftLockTextInput, ref global.unShiftLockKey, ref global.unShiftLockKeyValue);
                    unshiftLockTextInput.Text = global.KeyBindValue["unShiftLockKey"];
                }
                else if (restartPosCubeTextInput.HasFocus())
                {
                    AssignKey(eventKey, restartPosCubeTextInput, ref global.restartPosCubeKey, ref global.restartPosCubeKeyValue);
                    restartPosCubeTextInput.Text = global.KeyBindValue["restartPosCubeKey"];
                }
            }
        }

        private void AssignKey(InputEventKey eventKey, TextEdit focusedTextInput, ref Key globalKey, ref string keyValue)
        {
            Key newKey = (Key)eventKey.Keycode;
            string keyText;
            
            if (!keysList.Contains(newKey))
            {
                if (globalKey != Key.Unknown)
                {
                    keysList.Remove(globalKey);
                    RemoveKeyFromAction(focusedTextInput.Name, globalKey);
                }
                keyText = OS.GetKeycodeString(eventKey.Keycode);
                focusedTextInput.Text = keyText;

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
                
                keyValue = keyText;
                keysList.Add(newKey);
                globalKey = newKey;

                AddKeyToAction(focusedTextInput.Name, newKey);
                
                global.KeyBind[focusedTextInput.Name] = globalKey;
                global.KeyBindValue[focusedTextInput.Name] = keyText;
                
                File.WriteAllText(statsPath, JsonSerializer.Serialize(global.data));
            }
        }
        private void AddKeyToAction(string actionName, Key key)
        {
            if (!InputMap.HasAction(actionName))
            {
                InputMap.AddAction(actionName);
            }
            InputMap.ActionEraseEvents(actionName);
            
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
            global = (Global)GetNode("/root/Global");
            var mainMenuBtn = this.GetNode<Button>("mainMenu");
            if (mainMenuBtn.IsPressed())
            {
                global.ClickSFX("res://sfx/btn_click.wav");
                GetTree().ChangeSceneToFile("res://MainMenus/MainMenu3D/MainMenu3D.tscn");
            }
        }
    }
}
