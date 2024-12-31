using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace threeDTicTacToe
{
    public partial class Settings : Control
    {
        private List<Key> keysList = new List<Key>();
        private String statsPath = "info.json";

        public override void _Ready()
        {
            Binding();
            SetupButtons();
            MusicSettings();
        }

        private void MusicSettings()
        {
            var global = GetNode<Global>("/root/Global");
            var musicLabel = GetNode<Label>("volumeSettings/settings/musicLabel");
            musicLabel.Text = $"MUSIC : {global.content["musicVolume"] *100 }%";
            
            var sfxLabel = GetNode<Label>("volumeSettings/settings/sfxLabel");
            sfxLabel.Text = $"SFX : {global.content["sfxVolume"] *100 }%";
            
            var musicSlider = GetNode<Slider>("volumeSettings/settings/musicSlider");
            var sfxSlider = GetNode<Slider>("volumeSettings/settings/sfxSlider");
            
            musicSlider.Value = global.content["musicVolume"] *100;
            sfxSlider.Value = global.content["sfxVolume"] *100;
            
            musicSlider.ValueChanged += MusicSliderOnValueChanged;
            sfxSlider.ValueChanged += SfxSliderOnValueChanged;
        }

        private void SfxSliderOnValueChanged(double value)
        {
            var global = GetNode<Global>("/root/Global");
            var sfxLabel = GetNode<Label>("volumeSettings/settings/sfxLabel");
            var sfxVolume = (decimal)value / 100;
            
            sfxLabel.Text = $"SFX : {value}%";
            global.content["sfxVolume"] = (float)Math.Round(sfxVolume, 2);
            File.WriteAllText(statsPath, JsonSerializer.Serialize(global.content));
        }

        private void MusicSliderOnValueChanged(double value)
        {
            var global = GetNode<Global>("/root/Global");
            var musicLabel = GetNode<Label>("volumeSettings/settings/musicLabel");
            var musicVolume = (decimal)value / 100;
            
            musicLabel.Text = $"MUSIC : {value}%";
            global.content["musicVolume"] = (float)Math.Round(musicVolume, 2);
            File.WriteAllText(statsPath, JsonSerializer.Serialize(global.content));
            
            global.SetVolume(global.content["musicVolume"]);
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

            appExitTextInput.Text = global.appExitKeyValue;
            mainMenuTextInput.Text = global.mainMenuKeyValue;

            keysList.Clear();
            keysList.Add(global.mainMenuKey);
            keysList.Add(global.appExitKey);
        }

        private void btnPress(Button button)
        {
            Global global = (Global)GetNode("/root/Global");
            global.ClickSFX("res://sfx/btn_click.wav");
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
                    AssignKey(eventKey, mainMenuTextInput, ref global.mainMenuKey, ref global.mainMenuKeyValue);
                }
                else if (appExitTextInput.HasFocus())
                {
                    AssignKey(eventKey, appExitTextInput, ref global.appExitKey, ref global.appExitKeyValue);
                }
            }
        }
        private void AssignKey(InputEventKey eventKey, TextEdit focusedTextInput, ref Key globalKey, ref string keyValue)
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
            var global = (Global)GetNode("/root/Global");
            var mainMenuBtn = this.GetNode<Button>("mainMenu");
            var statsBtn = this.GetNode<Button>("stats");
            if (mainMenuBtn.IsPressed())
            {
                global.ClickSFX("res://sfx/btn_click.wav");
                GetTree().ChangeSceneToFile("res://MainMenus/MainMenu.tscn");
            }
            if (statsBtn.IsPressed())
            {
                global.ClickSFX("res://sfx/btn_click.wav");
                GetTree().ChangeSceneToFile("res://MainMenus/Statistics/Statistics.tscn");
            }
        }
    }
}
