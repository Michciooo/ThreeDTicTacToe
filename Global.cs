using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NAudio.Wave;

namespace threeDTicTacToe;

public partial class Global : Node
{
    public List<DisplayServer.WindowMode> WindowModes = new List<DisplayServer.WindowMode>()
    {
	    DisplayServer.WindowMode.Fullscreen,
	    DisplayServer.WindowMode.ExclusiveFullscreen,
	    DisplayServer.WindowMode.Windowed,
	    DisplayServer.WindowMode.Maximized,
	    DisplayServer.WindowMode.Minimized
    }; 
    public int windowModeIndex = 0;

    public String player12D;
    public String player22D;
    public String player13D;
    public String player23D;
    public String player33D;
    public String player3DMode = "4x4x4";
    public String Mode;
    public bool isModeChecked = false;
    
    public Dictionary<int, string> playersTypes2D = new Dictionary<int, string>
    {
	    { 1, "Human" },
	    { 2, "Human" }
    };
    
    public Dictionary<int,string> playersTypes3D = new Dictionary<int,string>
    {
	    { 1, "Human" },
	    { 2, "Human" },
	    { 3, "Human"}
    };

    public String accName = "Guest";
    public bool isLogged = false;
    
    private String path = "settings.json";
    
    public List<Button> FirstDBtn = new List<Button>(16);
    public List<Button> SecondDBtn = new List<Button>(16);
    public List<Button> ThirdDBtn = new List<Button>(16);
    public List<Button> FourthDBtn = new List<Button>(16);
    
    public List<Button> FirstDBtn3 = new List<Button>(9);
    public List<Button> SecondDBtn3 = new List<Button>(9);
    public List<Button> ThirdDBtn3 = new List<Button>(9);

    public Button[,] wins4x4x4;
    public Button[,] wins3x3x3;
    
    public Key mainMenuKey;
    public Key appExitKey;
    public Key restartPosCubeKey;
    public Key shiftLockKey;
    public Key unShiftLockKey;

    public String mainMenuKeyValue;
    public String appExitKeyValue;
    public String restartPosCubeKeyValue;
    public String shiftLockKeyValue;
    public String unShiftLockKeyValue;
    
    public Dictionary<string , object> data = new Dictionary<string, object>();
    public Dictionary<string , float> statistics = new Dictionary<string , float>();
    public Dictionary<string , object> settings = new Dictionary<string , object>();
    
    public Dictionary<string , Key> KeyBind = new Dictionary<string , Key>();
    public Dictionary<string , string> KeyBindValue = new Dictionary<string , string>();
   public void Statistics()
{
    if (File.Exists(path))
    {
        string jsonContent = File.ReadAllText(path);
        data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonContent);
        
        if (!data.ContainsKey("statistics"))
        {
            data["statistics"] = new Dictionary<string, float>();
        }
        if (!data.ContainsKey("settings"))
        {
            data["settings"] = new Dictionary<string, object>();
        }

        if (data.ContainsKey("statistics"))
        {
	        if (data["statistics"] is JsonElement jsonElement)
	        {
		        statistics = JsonSerializer.Deserialize<Dictionary<string, float>>(jsonElement.GetRawText());
	        }
	        else if (data["statistics"] is string jsonString)
	        {
		        statistics = JsonSerializer.Deserialize<Dictionary<string, float>>(jsonString);
	        }
	        else if (data["statistics"] is Dictionary<string, float> existingStatistics)
	        {
		        statistics = existingStatistics;
	        }
	        else
	        {
		        throw new Exception("Nieoczekiwany typ danych dla 'statistics'.");
	        }
        }
        if (data.ContainsKey("settings"))
        {
	        if (data["settings"] is JsonElement jsonElement)
	        {
		        settings = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonElement.GetRawText());
	        }
	        else if (data["settings"] is string jsonString)
	        {
		        settings = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
	        }
	        else if (data["settings"] is Dictionary<string, object> existingSettings)
	        {
		        settings = existingSettings;
	        }
	        else
	        {
		        throw new Exception("Nieoczekiwany typ danych dla 'settings'.");
	        }
        }
        if (!settings.ContainsKey("KeyBind"))
        {
	        settings["KeyBind"] = new Dictionary<string, Key>();
        }
        if (!settings.ContainsKey("KeyBindValue"))
        {
	        settings["KeyBindValue"] = new Dictionary<string, string>();
        }

        if (settings.ContainsKey("KeyBind"))
        {
	        if (settings["KeyBind"] is JsonElement jsonElement)
	        {
		        KeyBind = JsonSerializer.Deserialize<Dictionary<string, Key>>(jsonElement.GetRawText())
		                  ?? new Dictionary<string, Key>();
	        }
	        else if (settings["KeyBind"] is string jsonString)
	        {
		        KeyBind = JsonSerializer.Deserialize<Dictionary<string, Key>>(jsonString)
		                  ?? new Dictionary<string, Key>();
	        }
	        else if (settings["KeyBind"] is Dictionary<string, Key> existingSettings)
	        {
		        KeyBind = existingSettings;
	        }
	        else
	        {
		        KeyBind = new Dictionary<string, Key>();
	        }
        }
        else
        {
	        KeyBind = new Dictionary<string, Key>();
        }

        if (settings.ContainsKey("KeyBindValue"))
        {
	        if (settings["KeyBindValue"] is JsonElement jsonElement)
	        {
		        KeyBindValue = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonElement.GetRawText())
		                       ?? new Dictionary<string, string>();
	        }
	        else if (settings["KeyBindValue"] is string jsonString)
	        {
		        KeyBindValue = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString)
		                       ?? new Dictionary<string, string>();
	        }
	        else if (settings["KeyBindValue"] is Dictionary<string, string> existingSettings)
	        {
		        KeyBindValue = existingSettings;
	        }
	        else
	        {
		        KeyBindValue = new Dictionary<string, string>();
	        }
        }
        else
        {
	        KeyBindValue = new Dictionary<string, string>();
        }

        if (!statistics.ContainsKey("allWins")) statistics["allWins"] = 0;
        if (!statistics.ContainsKey("oWins")) statistics["oWins"] = 0;
        if (!statistics.ContainsKey("xWins")) statistics["xWins"] = 0;
        if (!statistics.ContainsKey("tWins")) statistics["tWins"] = 0;
        if (!statistics.ContainsKey("wins2D")) statistics["wins2D"] = 0;
        if (!statistics.ContainsKey("wins3D")) statistics["wins3D"] = 0;
        if (!statistics.ContainsKey("wins3D2P")) statistics["wins3D2P"] = 0;
        if (!statistics.ContainsKey("wins3D3P")) statistics["wins3D3P"] = 0;

        if (!statistics.ContainsKey("allLoses")) statistics["allLoses"] = 0;
        if (!statistics.ContainsKey("oLoses")) statistics["oLoses"] = 0;
        if (!statistics.ContainsKey("xLoses")) statistics["xLoses"] = 0;
        if (!statistics.ContainsKey("tLoses")) statistics["tLoses"] = 0;
        if (!statistics.ContainsKey("loses2D")) statistics["loses2D"] = 0;
        if (!statistics.ContainsKey("loses3D")) statistics["loses3D"] = 0;
        if (!statistics.ContainsKey("loses3D2P")) statistics["loses3D2P"] = 0;
        if (!statistics.ContainsKey("loses3D3P")) statistics["loses3D3P"] = 0;

        if (!statistics.ContainsKey("musicVolume")) statistics["musicVolume"] = 1f;
        if (!statistics.ContainsKey("sfxVolume")) statistics["sfxVolume"] = 1f;
        
        if(!KeyBind.ContainsKey("mainMenuKey")) KeyBind["mainMenuKey"] = mainMenuKey;
        if(!KeyBind.ContainsKey("appExitKey")) KeyBind["appExitKey"] = appExitKey;
        if(!KeyBind.ContainsKey("restartPosCubeKey")) KeyBind["restartPosCubeKey"] = restartPosCubeKey;
        if(!KeyBind.ContainsKey("shiftLockKey")) KeyBind["shiftLockKey"] = shiftLockKey;
        if(!KeyBind.ContainsKey("unShiftLockKey")) KeyBind["unShiftLockKey"] = unShiftLockKey;
        
        if(!KeyBindValue.ContainsKey("mainMenuKey")) KeyBindValue["mainMenuKey"] = mainMenuKeyValue;
        if(!KeyBindValue.ContainsKey("appExitKey")) KeyBindValue["appExitKey"] = appExitKeyValue;
        if(!KeyBindValue.ContainsKey("restartPosCubeKey")) KeyBindValue["restartPosCubeKey"] = restartPosCubeKeyValue;
        if(!KeyBindValue.ContainsKey("shiftLockKey")) KeyBindValue["shiftLockKey"] = shiftLockKeyValue;
        if(!KeyBindValue.ContainsKey("unShiftLockKey")) KeyBindValue["unShiftLockKey"] = unShiftLockKeyValue;
        
        data["statistics"] = statistics;
        data["settings"] = settings;
        
        settings["KeyBind"] = KeyBind;
        settings["KeyBindValue"] = KeyBindValue;
        
        File.WriteAllText(path, JsonSerializer.Serialize(data));
    }
    else
    {
        var content = new Dictionary<string, object>
        {
            ["statistics"] = new Dictionary<string, float>
            {
                { "allWins", 0 },
                { "oWins", 0 },
                { "xWins", 0 },
                { "tWins", 0 },
                { "wins2D", 0 },
                { "wins3D", 0 },
                { "wins3D2P", 0 },
                { "wins3D3P", 0 },
                { "allLoses", 0 },
                { "oLoses", 0 },
                { "xLoses", 0 },
                { "tLoses", 0 },
                { "loses2D", 0 },
                { "loses3D", 0 },
                { "loses3D2P", 0 },
                { "loses3D3P", 0 },
                { "musicVolume", 1f },
                { "sfxVolume", 1f },
            },
            ["settings"] = new Dictionary<string, object>
            {
	            ["KeyBind"] = new Dictionary<string, Key>
	            {
		            {"mainMenuKey", Key.Escape},
		            {"appExitKey", Key.Q},
		            {"restartPosCubeKey", Key.R},
		            {"shiftLockKey", Key.Shift},
		            {"unShiftLockKey", Key.Ctrl},
	            },
	            ["KeyBindValue"] = new Dictionary<string, string>
	            {
		            {"mainMenuKey", "Escape"},
		            {"appExitKey", "Q"},
		            {"restartPosCubeKey", "R"},
		            {"shiftLockKey", "Shift"},
		            {"unShiftLockKey", "Ctrl"},
	            }
            }
        };
        statistics = content["statistics"] as Dictionary<string, float>;
        settings = content["settings"] as Dictionary<string, object>;
        
        KeyBind = settings["KeyBind"] as Dictionary<string, Key>;
        KeyBindValue = settings["KeyBindValue"] as Dictionary<string, string>;
        File.WriteAllText(path, JsonSerializer.Serialize(content));
        
        mainMenuKey = KeyBind["mainMenuKey"];
        appExitKey = KeyBind["appExitKey"];
        restartPosCubeKey = KeyBind["restartPosCubeKey"];
        shiftLockKey = KeyBind["shiftLockKey"];
        unShiftLockKey = KeyBind["unShiftLockKey"];
        
        mainMenuKeyValue = KeyBindValue["mainMenuKey"];
        appExitKeyValue = KeyBindValue["appExitKey"];
        restartPosCubeKeyValue = KeyBindValue["restartPosCubeKey"];
        shiftLockKeyValue = KeyBindValue["shiftLockKey"];
        unShiftLockKeyValue = KeyBindValue["unShiftLockKey"];
    }
}

    public void ErasePlayers2D()
    {
	    playersTypes2D = new Dictionary<int,string>
	    {
		    { 1, "Human" },
		    { 2, "Human" },
	    };
    }
    public void ErasePlayers3D()
    {
	    playersTypes3D = new Dictionary<int,string>
	    {
		    { 1, "Human" },
		    { 2, "Human" },
		    { 3, "Human"}
	    };
    }
    public void ClickSFX(string pathName)
    {
		WaveOutEvent outputDevice = new WaveOutEvent();
		string virtualPath = pathName;
	    string realPath = ProjectSettings.GlobalizePath(virtualPath);
	    
	    var audioFile = new AudioFileReader(realPath);

	    var sfxVolume = statistics["sfxVolume"];
	    audioFile.Volume = sfxVolume;

	    outputDevice.Init(audioFile);
	    outputDevice.Play();
    }
    
    private WaveOutEvent outputDevice;
    private AudioFileReader audioFile;

    public void PlayLooping(string filePath)
    {
	    if (audioFile != null && audioFile.FileName == filePath) return;

	    if (outputDevice != null)
	    {
		    outputDevice.Stop();
		    outputDevice.Dispose();
		    outputDevice = null;
	    }

	    audioFile?.Dispose();

	    audioFile = new AudioFileReader(filePath);
	    audioFile.Volume = statistics["musicVolume"];

	    void OnPlaybackStopped(object sender, StoppedEventArgs e)
	    {
		    if (audioFile != null)
		    {
			    audioFile.Position = 0;
			    outputDevice.Play();
		    }
	    }

	    outputDevice = new WaveOutEvent();
	    outputDevice.PlaybackStopped += OnPlaybackStopped;

	    outputDevice.Init(audioFile);
	    outputDevice.Play();
    }
    public void SetVolume(float newVolume)
    {
	    if (audioFile != null)
	    {
		    audioFile.Volume = newVolume;
	    }
    }


    public void InitializeWins4x4x4()
    {
	    for (int i = 0; i < 16; i++)
	    {
		    FirstDBtn.Add(new Button());
		    SecondDBtn.Add(new Button());
		    ThirdDBtn.Add(new Button());
		    FourthDBtn.Add(new Button());
	    }
	    wins4x4x4 = new Button[,]
	    {
		    //first grid
		    { FirstDBtn[0], FirstDBtn[1], FirstDBtn[2], FirstDBtn[3] },
		    { FirstDBtn[4], FirstDBtn[5], FirstDBtn[6], FirstDBtn[7] },
		    { FirstDBtn[8], FirstDBtn[9], FirstDBtn[10], FirstDBtn[11] },
		    { FirstDBtn[12], FirstDBtn[13], FirstDBtn[14], FirstDBtn[15] },

		    { FirstDBtn[0], FirstDBtn[4], FirstDBtn[8], FirstDBtn[12] },
		    { FirstDBtn[1], FirstDBtn[5], FirstDBtn[9], FirstDBtn[13] },
		    { FirstDBtn[2], FirstDBtn[6], FirstDBtn[10], FirstDBtn[14] },
		    { FirstDBtn[3], FirstDBtn[7], FirstDBtn[11], FirstDBtn[15] },

		    { FirstDBtn[0], FirstDBtn[5], FirstDBtn[10], FirstDBtn[15] },
		    { FirstDBtn[3], FirstDBtn[6], FirstDBtn[9], FirstDBtn[12] },

		    //second grid
		    { SecondDBtn[0], SecondDBtn[1], SecondDBtn[2], SecondDBtn[3] },
		    { SecondDBtn[4], SecondDBtn[5], SecondDBtn[6], SecondDBtn[7] },
		    { SecondDBtn[8], SecondDBtn[9], SecondDBtn[10], SecondDBtn[11] },
		    { SecondDBtn[12], SecondDBtn[13], SecondDBtn[14], SecondDBtn[15] },

		    { SecondDBtn[0], SecondDBtn[4], SecondDBtn[8], SecondDBtn[12] },
		    { SecondDBtn[1], SecondDBtn[5], SecondDBtn[9], SecondDBtn[13] },
		    { SecondDBtn[2], SecondDBtn[6], SecondDBtn[10], SecondDBtn[14] },
		    { SecondDBtn[3], SecondDBtn[7], SecondDBtn[11], SecondDBtn[15] },

		    { SecondDBtn[0], SecondDBtn[5], SecondDBtn[10], SecondDBtn[15] },
		    { SecondDBtn[3], SecondDBtn[6], SecondDBtn[9], SecondDBtn[12] },

		    //third grid
		    { ThirdDBtn[0], ThirdDBtn[1], ThirdDBtn[2], ThirdDBtn[3] },
		    { ThirdDBtn[4], ThirdDBtn[5], ThirdDBtn[6], ThirdDBtn[7] },
		    { ThirdDBtn[8], ThirdDBtn[9], ThirdDBtn[10], ThirdDBtn[11] },
		    { ThirdDBtn[12], ThirdDBtn[13], ThirdDBtn[14], ThirdDBtn[15] },

		    { ThirdDBtn[0], ThirdDBtn[4], ThirdDBtn[8], ThirdDBtn[12] },
		    { ThirdDBtn[1], ThirdDBtn[5], ThirdDBtn[9], ThirdDBtn[13] },
		    { ThirdDBtn[2], ThirdDBtn[6], ThirdDBtn[10], ThirdDBtn[14] },
		    { ThirdDBtn[3], ThirdDBtn[7], ThirdDBtn[11], ThirdDBtn[15] },

		    { ThirdDBtn[0], ThirdDBtn[5], ThirdDBtn[10], ThirdDBtn[15] },
		    { ThirdDBtn[3], ThirdDBtn[6], ThirdDBtn[9], ThirdDBtn[12] },

		    //fourth grid
		    { FourthDBtn[0], FourthDBtn[1], FourthDBtn[2], FourthDBtn[3] },
		    { FourthDBtn[4], FourthDBtn[5], FourthDBtn[6], FourthDBtn[7] },
		    { FourthDBtn[8], FourthDBtn[9], FourthDBtn[10], FourthDBtn[11] },
		    { FourthDBtn[12], FourthDBtn[13], FourthDBtn[14], FourthDBtn[15] },

		    { FourthDBtn[0], FourthDBtn[4], FourthDBtn[8], FourthDBtn[12] },
		    { FourthDBtn[1], FourthDBtn[5], FourthDBtn[9], FourthDBtn[13] },
		    { FourthDBtn[2], FourthDBtn[6], FourthDBtn[10], FourthDBtn[14] },
		    { FourthDBtn[3], FourthDBtn[7], FourthDBtn[11], FourthDBtn[15] },

		    { FourthDBtn[0], FourthDBtn[5], FourthDBtn[10], FourthDBtn[15] },
		    { FourthDBtn[3], FourthDBtn[6], FourthDBtn[9], FourthDBtn[12] },

		    //crosses
		    { FirstDBtn[0], SecondDBtn[1], ThirdDBtn[2], FourthDBtn[3] },
		    { FirstDBtn[3], SecondDBtn[2], ThirdDBtn[2], FourthDBtn[0] },

		    { FirstDBtn[4], SecondDBtn[5], ThirdDBtn[6], FourthDBtn[7] },
		    { FirstDBtn[7], SecondDBtn[6], ThirdDBtn[5], FourthDBtn[4] },

		    { FirstDBtn[8], SecondDBtn[9], ThirdDBtn[10], FourthDBtn[11] },
		    { FirstDBtn[11], SecondDBtn[10], ThirdDBtn[9], FourthDBtn[8] },

		    { FirstDBtn[12], SecondDBtn[13], ThirdDBtn[14], FourthDBtn[15] },
		    { FirstDBtn[15], SecondDBtn[14], ThirdDBtn[13], FourthDBtn[12] },

		    { FirstDBtn[0], SecondDBtn[4], ThirdDBtn[8], FourthDBtn[12] },
		    { FirstDBtn[12], SecondDBtn[8], ThirdDBtn[4], FourthDBtn[0] },

		    { FirstDBtn[1], SecondDBtn[5], ThirdDBtn[9], FourthDBtn[13] },
		    { FirstDBtn[13], SecondDBtn[9], ThirdDBtn[5], FourthDBtn[1] },

		    { FirstDBtn[2], SecondDBtn[6], ThirdDBtn[10], FourthDBtn[14] },
		    { FirstDBtn[14], SecondDBtn[10], ThirdDBtn[6], FourthDBtn[2] },

		    { FirstDBtn[3], SecondDBtn[7], ThirdDBtn[11], FourthDBtn[15] },
		    { FirstDBtn[15], SecondDBtn[11], ThirdDBtn[7], FourthDBtn[3] },

		    { FirstDBtn[0], SecondDBtn[5], ThirdDBtn[10], FourthDBtn[15] },
		    { FirstDBtn[15], SecondDBtn[10], ThirdDBtn[5], FourthDBtn[0] },

		    { FirstDBtn[12], SecondDBtn[9], ThirdDBtn[6], FourthDBtn[3] },
		    { FirstDBtn[3], SecondDBtn[6], ThirdDBtn[9], FourthDBtn[12] },
		    
		    //huj wie jak to sie nazywa
		    { FirstDBtn[0], SecondDBtn[0], ThirdDBtn[0],FourthDBtn[0] },
		    { FirstDBtn[1], SecondDBtn[1], ThirdDBtn[1],FourthDBtn[1] },
		    { FirstDBtn[2], SecondDBtn[2], ThirdDBtn[2],FourthDBtn[2] },
		    { FirstDBtn[3], SecondDBtn[3], ThirdDBtn[3],FourthDBtn[3] },
		    
		    { FirstDBtn[4], SecondDBtn[4], ThirdDBtn[4],FourthDBtn[4] },
		    { FirstDBtn[5], SecondDBtn[5], ThirdDBtn[5],FourthDBtn[5] },
		    { FirstDBtn[6], SecondDBtn[6], ThirdDBtn[6],FourthDBtn[6] },
		    { FirstDBtn[7], SecondDBtn[7], ThirdDBtn[7],FourthDBtn[7] },
		    
		    { FirstDBtn[8], SecondDBtn[8], ThirdDBtn[8],FourthDBtn[8] },
		    { FirstDBtn[9], SecondDBtn[9], ThirdDBtn[9],FourthDBtn[9] },
		    { FirstDBtn[10], SecondDBtn[10], ThirdDBtn[10],FourthDBtn[10] },
		    { FirstDBtn[11], SecondDBtn[11], ThirdDBtn[11],FourthDBtn[11] },
		    
		    { FirstDBtn[12], SecondDBtn[12], ThirdDBtn[12],FourthDBtn[12] },
		    { FirstDBtn[13], SecondDBtn[13], ThirdDBtn[13],FourthDBtn[13] },
		    { FirstDBtn[14], SecondDBtn[14], ThirdDBtn[14],FourthDBtn[14] },
		    { FirstDBtn[15], SecondDBtn[15], ThirdDBtn[15],FourthDBtn[15] },
	    };
    }
    public void InitializeWins3x3x3()
    {
	    for (int i = 0; i < 9; i++)
	    {
		    FirstDBtn3.Add(new Button());
		    SecondDBtn3.Add(new Button());
		    ThirdDBtn3.Add(new Button());
	    }
	    wins3x3x3 = new Button[,]
	    {
		    //first grid
		    {FirstDBtn3[0], FirstDBtn3[3], FirstDBtn3[6]},
		    {FirstDBtn3[1], FirstDBtn3[4], FirstDBtn3[7]},
		    {FirstDBtn3[2], FirstDBtn3[5], FirstDBtn3[8]},
		    
		    {FirstDBtn3[0], FirstDBtn3[1], FirstDBtn3[2]},
		    {FirstDBtn3[3], FirstDBtn3[4], FirstDBtn3[5]},
		    {FirstDBtn3[6], FirstDBtn3[7], FirstDBtn3[8]},
		    
		    {FirstDBtn3[0], FirstDBtn3[4], FirstDBtn3[8]},
		    {FirstDBtn3[2], FirstDBtn3[4], FirstDBtn3[6]},
		    
		    //second grid
		    {SecondDBtn3[0], SecondDBtn3[3], SecondDBtn3[6]},
		    {SecondDBtn3[1], SecondDBtn3[4], SecondDBtn3[7]},
		    {SecondDBtn3[2], SecondDBtn3[5], SecondDBtn3[8]},
            
		    {SecondDBtn3[0], SecondDBtn3[1], SecondDBtn3[2]},
		    {SecondDBtn3[3], SecondDBtn3[4], SecondDBtn3[5]},
		    {SecondDBtn3[6], SecondDBtn3[7], SecondDBtn3[8]},
            
		    {SecondDBtn3[0], SecondDBtn3[4], SecondDBtn3[8]},
		    {SecondDBtn3[2], SecondDBtn3[4], SecondDBtn3[6]},
		    
		    //third grid
		    {ThirdDBtn3[0], ThirdDBtn3[3], ThirdDBtn3[6]},
		    {ThirdDBtn3[1], ThirdDBtn3[4], ThirdDBtn3[7]},
		    {ThirdDBtn3[2], ThirdDBtn3[5], ThirdDBtn3[8]},
            
		    {ThirdDBtn3[0], ThirdDBtn3[1], ThirdDBtn3[2]},
		    {ThirdDBtn3[3], ThirdDBtn3[4], ThirdDBtn3[5]},
		    {ThirdDBtn3[6], ThirdDBtn3[7], ThirdDBtn3[8]},
            
		    {ThirdDBtn3[0], ThirdDBtn3[4], ThirdDBtn3[8]},
		    {ThirdDBtn3[2], ThirdDBtn3[4], ThirdDBtn3[6]},

		    //crosses
		    { FirstDBtn3[0], SecondDBtn3[3], ThirdDBtn3[6] },
		    { FirstDBtn3[6], SecondDBtn3[3], ThirdDBtn3[0] },
		    
		    { FirstDBtn3[1], SecondDBtn3[4], ThirdDBtn3[7] },
		    { FirstDBtn3[7], SecondDBtn3[4], ThirdDBtn3[1] },
		    
		    { FirstDBtn3[2], SecondDBtn3[5], ThirdDBtn3[8] },
		    { FirstDBtn3[8], SecondDBtn3[5], ThirdDBtn3[2] },
		    
		    { FirstDBtn3[0], SecondDBtn3[1], ThirdDBtn3[2] },
		    { FirstDBtn3[2], SecondDBtn3[1], ThirdDBtn3[0] },
		    
		    { FirstDBtn3[3], SecondDBtn3[4], ThirdDBtn3[5] },
		    { FirstDBtn3[5], SecondDBtn3[4], ThirdDBtn3[3] },
		    
		    { FirstDBtn3[6], SecondDBtn3[7], ThirdDBtn3[8] },
		    { FirstDBtn3[8], SecondDBtn3[7], ThirdDBtn3[6] },
		    
		    { FirstDBtn3[0], SecondDBtn3[4], ThirdDBtn3[8] },
		    { FirstDBtn3[8], SecondDBtn3[4], ThirdDBtn3[0] },
		    
		    { FirstDBtn3[2], SecondDBtn3[4], ThirdDBtn3[6] },
		    { FirstDBtn3[6], SecondDBtn3[4], ThirdDBtn3[2] },
		    
		    //huj wie jak to sie nazywa
		    { FirstDBtn3[0], SecondDBtn3[0], ThirdDBtn3[0] },
		    { FirstDBtn3[1], SecondDBtn3[1], ThirdDBtn3[1] },
		    { FirstDBtn3[2], SecondDBtn3[2], ThirdDBtn3[2] },
		    
		    { FirstDBtn3[3], SecondDBtn3[3], ThirdDBtn3[3] },
		    { FirstDBtn3[4], SecondDBtn3[4], ThirdDBtn3[4] },
		    { FirstDBtn3[5], SecondDBtn3[5], ThirdDBtn3[5] },
		    
		    { FirstDBtn3[6], SecondDBtn3[6], ThirdDBtn3[6] },
		    { FirstDBtn3[7], SecondDBtn3[7], ThirdDBtn3[7] },
		    { FirstDBtn3[8], SecondDBtn3[8], ThirdDBtn3[8] },
	    };
    }
}