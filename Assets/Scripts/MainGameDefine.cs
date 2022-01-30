using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameDefine : SingletonMonoBehaviour<MainGameDefine>
{
    public List<Vector3> lenPosList = new List<Vector3>();

    public enum GameType
    {
        Lightning = 0,
        Darkness,
    }

    public string[] bgmName = new string[2] { "j1_81", "j2_83" };

    public enum GameStatus
    {
        None,
        Title,
        InGame,
        Result,
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
