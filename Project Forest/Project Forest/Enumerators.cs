using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Forest
{
    public enum CharacterStates
    {
        FaceRight,
        FaceLeft, 
        WalkRight,
        WalkLeft,
        MeleeAttack,
    }

    public enum ViewStates
    {
        Stationary, 
        Moving,
    }

    enum GameStates
    {
        Menu,
        Game,
    }

    enum MenuStates
    {
        MainMenu,
        Controls,
        Credits,
    }

    enum ArrowSelection
    {
        Play,
        Quit,
        Credits,
        Controls,
        Back,
        MeleeAttack,
        MidRangeAttack,
        RangeAttack,
        MoveLeft,
        MoveRight,

    }
}