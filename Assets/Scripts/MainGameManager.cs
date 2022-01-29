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
        _targetBeat = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioManager.Instance.GetTime(MainGameDefine.Instance.bgmName[0]) > 0.0f)
        {
            float checkTime = AudioManager.Instance.GetGapFromBeat(MainGameDefine.Instance.bgmName[0], _targetBeat);

            if (checkTime == -1)
                return;

            if (checkTime <= 0.02f)
            {
                Debug.Log(AudioManager.Instance.GetGapFromBeat("create!!!!!!!!!!!!! : " + MainGameDefine.Instance.bgmName[0], _targetBeat));
                _noteAssembly.ActivateNote();
                _noteAssembly.NotesMove();
                _targetBeat++;
            }
            Debug.Log("check ~~~~~~~~~~~~~~~~~~~~~~~~~ : " + checkTime);
        }
    }

    [SerializeField]
    NotesAssembly _noteAssembly;

    MainGameDefine.GameType _gameType;
    private int _targetBeat;
}
