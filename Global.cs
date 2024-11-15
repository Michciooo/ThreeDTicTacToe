using Godot;
using System;

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
}