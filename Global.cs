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
    public String buttonName3D;
    public String Mode;
}