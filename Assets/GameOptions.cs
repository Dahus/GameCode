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
    ��� �������� 0 ����, ��� 1 ������������ ���������, ���  2 ������� � �.�. � ���������� ����������.
    � ���� ������� �������� �� 0 �� 3, ��� 0 - ����, 1 - ������, 3 - �����.

    //���� ������ �����

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