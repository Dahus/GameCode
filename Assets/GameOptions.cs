using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.GridSystem;

public class GameOptions
{
    public static GameOptions instance = new GameOptions();

    public float GridOffset = 0;

    /*
    Под индексом 0 горы, под 1 Пересеченная местность, под  2 Равнина и т.д. с остальными настрйками.
    В этом индексе эллемент от 0 до 3, где 0 - мало, 1 - средне, 3 - много.

    //Мало Средне Много

    */

    public sbyte[] ReliefSettings = new sbyte[4];
    public sbyte[] BiomeSettings = new sbyte[5];
    public sbyte[] ResourceSettings = new sbyte[1];
    public sbyte[] BotSettings = new sbyte[1];


    public void Log ()
    {
        Debug.Log(ReliefSettings[0]);
    }
}