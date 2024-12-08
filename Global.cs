using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace threeDTicTacToe;

public partial class Global : Node
{
    public Key mainMenuKey = Key.Escape;
    public Key appExitKey = Key.Q;
    public Key restartPosCubeKey = Key.R;
    public Key shiftLockKey = Key.Shift;
    public Key unShiftLockKey = Key.Ctrl;

    public String player12D;
    public String player22D;
    public String player13D;
    public String player23D;
    public String player33D;
    public String player3DMode;
    public String Mode;
    
    public Dictionary<string , int> content = new Dictionary<string, int>();
    private String path = "statistics.json";
    
    public List<Button> FirstDBtn = new List<Button>(16);
    public List<Button> SecondDBtn = new List<Button>(16);
    public List<Button> ThirdDBtn = new List<Button>(16);
    public List<Button> FourthDBtn = new List<Button>(16);
    
    public List<Button> FirstDBtn3 = new List<Button>(9);
    public List<Button> SecondDBtn3 = new List<Button>(9);
    public List<Button> ThirdDBtn3 = new List<Button>(9);

    public Button[,] wins4x4x4;
    public Button[,] wins3x3x3;

    public void Statistics()
    {
	    if (File.Exists(path))
	    {
		    string jsonContent = File.ReadAllText(path);
		    content = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonContent);
			
		    if (!content.ContainsKey("allWins")) content["allWins"] = 0;
		    if (!content.ContainsKey("oWins")) content["oWins"] = 0;
		    if (!content.ContainsKey("xWins")) content["xWins"] = 0;
		    if (!content.ContainsKey("wins2D")) content["wins2D"] = 0;
		    if (!content.ContainsKey("wins3D")) content["wins3D"] = 0;
		    if (!content.ContainsKey("wins3D2P")) content["wins3D2P"] = 0;
		    if (!content.ContainsKey("wins3D3P")) content["wins3D3P"] = 0;
		    
		    if (!content.ContainsKey("allLoses")) content["allLoses"] = 0;
		    if (!content.ContainsKey("oLoses")) content["oLoses"] = 0;
		    if (!content.ContainsKey("xLoses")) content["xLoses"] = 0;
		    if (!content.ContainsKey("loses2D")) content["loses2D"] = 0;
		    if (!content.ContainsKey("loses3D")) content["loses3D"] = 0;
		    if (!content.ContainsKey("loses3D2P")) content["loses3D2P"] = 0;
		    if (!content.ContainsKey("loses3D3P")) content["loses3D3P"] = 0;
	    }
	    else
	    {
		    content["allWins"] = 0;
		    content["oWins"] = 0;
		    content["xWins"] = 0;
		    content["wins2D"] = 0;
		    content["wins3D"] = 0;
		    content["wins3D2P"] = 0;
		    content["wins3D3P"] = 0;
		    
		    content["allLoses"] = 0;
		    content["oLoses"] = 0;
		    content["xLoses"] = 0;
		    content["loses2D"] = 0;
		    content["loses3D"] = 0;
		    content["loses3D2P"] = 0;
		    content["loses3D3P"] = 0;

		    File.WriteAllText(path, JsonSerializer.Serialize(content));
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