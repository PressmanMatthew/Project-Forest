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
        RangedAttack,
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
        Pause,
        GameOver,
    }

    enum MenuStates
    {
        MainMenu,
        Controls,
        Credits,
        PauseMenu,
        GameOverMenu,
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
        Resume,
        MainMenu,
    }

    enum EnemyStates
    {
        InActive,
        Active,
        Attacking
    }
}