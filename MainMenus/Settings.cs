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
        private String statsPath = "settings.json";
        private Global global;

        public override void _Ready()
        {
            Binding();
            SetupButtons();
            MusicSettings();
            
            global = GetNode<Global>("/root/Global");
            var windowTypeSizeLabel = GetNode<Label>("Main/VBoxContainer/WindowSizeSettings/windowTypeSizeLabel");

            windowTypeSizeLabel.Text = global.WindowModes[global.windowModeIndex].ToString();
            DisplayServer.WindowSetMode(global.WindowModes[global.windowModeIndex]);
        }

        private void MusicSettings()
        { 
            global = GetNode<Global>("/root/Global");
        
            var musicLabel = GetNode<Label>("volumeSettings/settings/musicLabel");
            
            var musicSlider = GetNode<Slider>("volumeSettings/settings/musicSlider");
            var sfxSlider = GetNode<Slider>("volumeSettings/settings/sfxSlider");
            var sfxLabel = GetNode<Label>("volumeSettings/settings/sfxLabel");
            
            musicLabel.Text = $"MUSIC : {global.statistics["musicVolume"] * 100}%";
            sfxLabel.Text = $"SFX : {global.statistics["sfxVolume"] * 100}%";

            musicSlider.Value = global.statistics["musicVolume"] * 100;
            sfxSlider.Value = global.statistics["sfxVolume"] * 100;
            musicSlider.ValueChanged += MusicSliderOnValueChanged;
            sfxSlider.ValueChanged += SfxSliderOnValueChanged;
        }

        private void SfxSliderOnValueChanged(double value)
        {
            global = GetNode<Global>("/root/Global");
            
            var sfxLabel = GetNode<Label>("volumeSettings/settings/sfxLabel");
            var sfxVolume = (decimal)value / 100;
            
            sfxLabel.Text = $"SFX : {value}%";
            global.statistics["sfxVolume"] = (float)Math.Round(sfxVolume, 2);
            File.WriteAllText(statsPath, JsonSerializer.Serialize(global.data));
        }

        private void MusicSliderOnValueChanged(double value)
        { 
            global = GetNode<Global>("/root/Global");
            var musicLabel = GetNode<Label>("volumeSettings/settings/musicLabel");
            var musicVolume = (decimal)value / 100;
            
            musicLabel.Text = $"MUSIC : {value}%";
            global.statistics["musicVolume"] = (float)Math.Round(musicVolume, 2);
            File.WriteAllText(statsPath, JsonSerializer.Serialize(global.data));
            
            global.SetVolume(global.statistics["musicVolume"]);
        }
        private void PreviousBtn_OnPressed()
        {
            global = GetNode<Global>("/root/Global");
            global.ClickSFX("res://sfx/btn_click.wav");
            var windowTypeSizeLabel = GetNode<Label>("Main/VBoxContainer/WindowSizeSettings/windowTypeSizeLabel");
            
            if (global.windowModeIndex == 0) global.windowModeIndex = global.WindowModes.Count - 1;
            else global.windowModeIndex -= 1;
            
            windowTypeSizeLabel.Text = global.WindowModes[global.windowModeIndex].ToString();
            DisplayServer.WindowSetMode(global.WindowModes[global.windowModeIndex]);
        }
        private void NextBtnOnPressed()
        {
            global = GetNode<Global>("/root/Global");
            global.ClickSFX("res://sfx/btn_click.wav");
            var windowTypeSizeLabel = GetNode<Label>("Main/VBoxContainer/WindowSizeSettings/windowTypeSizeLabel");
            
            if (global.windowModeIndex == global.WindowModes.Count - 1) global.windowModeIndex = 0;
            else global.windowModeIndex += 1;
            
            windowTypeSizeLabel.Text = global.WindowModes[global.windowModeIndex].ToString();
            DisplayServer.WindowSetMode(global.WindowModes[global.windowModeIndex]);
        }
        private void SetupButtons()
        {
            var btn1 = this.GetNode<Button>("Main/VBoxContainer/keyBindSettings/RestartBtns/btn1");
            var btn2 = this.GetNode<Button>("Main/VBoxContainer/keyBindSettings/RestartBtns/btn2");
            
            var previousBtn = this.GetNode<Button>("Main/VBoxContainer/WindowSizeSettings/PreviousBtn");
            var nextBtn = this.GetNode<Button>("Main/VBoxContainer/WindowSizeSettings/NextBtn");

            btn1.Pressed += () => btnPress(btn1);
            btn2.Pressed += () => btnPress(btn2);

            previousBtn.Pressed += PreviousBtn_OnPressed;
            nextBtn.Pressed += NextBtnOnPressed;
        }

        private void Binding()
        {
            global = (Global)GetNode("/root/Global");

            var appExitTextInput = this.GetNode<TextEdit>("Main/VBoxContainer/keyBindSettings/KeysInput/appExitKey");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/VBoxContainer/keyBindSettings/KeysInput/mainMenuKey");

            keysList.Clear();
            keysList.Add(global.KeyBind["appExitKey"]);
            keysList.Add(global.KeyBind["mainMenuKey"]);

            appExitTextInput.Text = global.KeyBindValue["appExitKey"];
            mainMenuTextInput.Text = global.KeyBindValue["mainMenuKey"];
        }


        private void btnPress(Button button)
        {
            global = (Global)GetNode("/root/Global");
            global.ClickSFX("res://sfx/btn_click.wav");
            TextEdit targetTextInput = null;
            Key oldKey = Key.Unknown;

            if (button.Name == "btn1")
            {
                targetTextInput = this.GetNode<TextEdit>("Main/VBoxContainer/keyBindSettings/KeysInput/mainMenuKey");
                oldKey = global.mainMenuKey;
            }
            else if (button.Name == "btn2")
            {
                targetTextInput = this.GetNode<TextEdit>("Main/VBoxContainer/keyBindSettings/KeysInput/appExitKey");
                oldKey = global.appExitKey;
            }

            if (targetTextInput != null)
            {
                UpdateKeyBinding(targetTextInput, button.Name, oldKey);
            }
        }

        private void UpdateKeyBinding(TextEdit textInput, string actionName,Key oldKey)
        {
            if (oldKey != Key.Unknown)
            {
                keysList.Remove(oldKey);
            }

            textInput.Text = "";
        }

        public override void _Input(InputEvent @event)
        {
            global = (Global)GetNode("/root/Global");
            var appExitTextInput = this.GetNode<TextEdit>("Main/VBoxContainer/keyBindSettings/KeysInput/appExitKey");
            var mainMenuTextInput = this.GetNode<TextEdit>("Main/VBoxContainer/keyBindSettings/KeysInput/mainMenuKey");
            
            InputMap.ActionEraseEvents("mainMenuKey");
            InputMap.ActionEraseEvents("appExitKey");
    
            InputEventKey mainMenuKey = new InputEventKey { Keycode = global.mainMenuKey };
            InputEventKey appExitKey = new InputEventKey { Keycode = global.appExitKey };

            InputMap.ActionAddEvent("mainMenuKey", mainMenuKey);
            InputMap.ActionAddEvent("appExitKey", appExitKey);

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
