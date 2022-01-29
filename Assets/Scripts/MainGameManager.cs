using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : SingletonMonoBehaviour<MainGameManager>
{
    public MainGameDefine.GameType gameType { get { return _gameType; } }

    protected override void Awake()
    {
        base.Awake();
        _gameType = MainGameDefine.GameType.Lightning;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    MainGameDefine.GameType _gameType;
}
