using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum GameDifficulty
    {
        Easy,
        Hard
    }

    public enum GameMode
    {
        RedBloodCell,
        WhiteBloodCell,
        Human
    }

    public enum NotificationType
    {
        Default,
        Error,
        Info,
        Success,
        Warning
    }
}
