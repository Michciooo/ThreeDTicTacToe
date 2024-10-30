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

    public String buttonName;

    public const char PLAYER = 'X';
    public const char COMPUTER = 'O';
    public const char EMPTY = ' ';
    char[,,] board3D = new char[3, 3, 3];

}