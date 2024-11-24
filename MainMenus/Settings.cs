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
            Binding();
            SetupButtons();
        }

        private void SetupButtons()
        {
            var btn1 = this.GetNode<Button>("Main/RestartBtns/btn1");
            var btn2 = this.GetNode<Button>("Main/RestartBtns/btn2");

            btn1.Pressed += () => btnPress(btn1);
            btn2.Pressed += () => btnPress(btn2);
        }

        private void Binding()
        {
            Global global = (Global)GetNode("/root/Global");
            var appExitTextInput = this.GetNode<TextEdit>("Main/KeysInput/appExit");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/KeysInput/mainMenu");
            
            appExitTextInput.Text = global.appExitKey.ToString();
            mainMenuTextInput.Text = global.mainMenuKey.ToString();

            keysList.Clear();
            keysList.Add(global.mainMenuKey);
            keysList.Add(global.appExitKey);
        }

        private void btnPress(Button button)
        {
            Global global = (Global)GetNode("/root/Global");
            TextEdit targetTextInput = null;
            Key oldKey = Key.Unknown;

            if (button.Name == "btn1")
            {
                targetTextInput = this.GetNode<TextEdit>("Main/KeysInput/mainMenu");
                oldKey = global.shiftLockKey;
            }
            else if (button.Name == "btn2")
            {
                targetTextInput = this.GetNode<TextEdit>("Main/KeysInput/appExit");
                oldKey = global.unShiftLockKey;
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
            var appExitTextInput = this.GetNode<TextEdit>("Main/KeysInput/appExit");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/KeysInput/mainMenu");

            if (@event is InputEventKey eventKey && eventKey.Pressed)
            {
                if (mainMenuTextInput.HasFocus())
                {
                    AssignKey(eventKey, mainMenuTextInput, ref global.mainMenuKey);
                }
                else if (appExitTextInput.HasFocus())
                {
                    AssignKey(eventKey, appExitTextInput, ref global.appExitKey);
                }
            }
        }

        private void AssignKey(InputEventKey eventKey, TextEdit focusedTextInput, ref Key globalKey)
        {
            Key newKey = (Key)eventKey.Keycode;
            if (!keysList.Contains(newKey))
            {
                if (globalKey != Key.Unknown) 
                {
                    keysList.Remove(globalKey);
                    RemoveKeyFromAction(focusedTextInput.Name, globalKey);
                }
                focusedTextInput.Text = OS.GetKeycodeString(eventKey.Keycode);
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
            var mainMenuBtn = this.GetNode<Button>("mainMenu");
            if (mainMenuBtn.IsPressed()) GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
        }
    }
}
