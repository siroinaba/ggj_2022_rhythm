using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameDefine : SingletonMonoBehaviour<MainGameDefine>
{
    public List<Vector3> lenPosList = new List<Vector3>();

    protected override void Awake()
    {
        base.Awake();
    }
}
