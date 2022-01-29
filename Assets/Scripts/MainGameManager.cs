using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : SingletonMonoBehaviour<MainGameManager>
{
    public MainGameDefine.GameType gameType { get { return _gameType; } }

    protected override void Awake()
    {
        base.Awake();
        _gameType = MainGameDefine.GameType.Darkness;
        _targetBeat = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioManager.Instance.GetTime(MainGameDefine.Instance.bgmName[(int)_gameType]) > 0.0f)
        {
            float checkTime = AudioManager.Instance.GetGapFromBeat(MainGameDefine.Instance.bgmName[(int)_gameType], _targetBeat);

            if (checkTime == -1)
                return;

            if (checkTime <= 0.02f)
            {
                _noteAssembly.ActivateNote();
                _noteAssembly.NotesMove();
                _targetBeat++;
            }
        }
    }

    public void ChangeGameType()
    {
        AudioManager.Instance.StopBGM(MainGameDefine.Instance.bgmName[(int)_gameType]);

        _gameType = _gameType == MainGameDefine.GameType.Lightning ? MainGameDefine.GameType.Darkness : MainGameDefine.GameType.Lightning;
        AudioManager.Instance.PlayBGM(MainGameDefine.Instance.bgmName[(int)_gameType]);

        _targetBeat = 1;
    }

    public void GameOver()
    {
        AudioManager.Instance.StopBGM(MainGameDefine.Instance.bgmName[(int)_gameType]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [SerializeField]
    NotesAssembly _noteAssembly;

    MainGameDefine.GameType _gameType;
    private int _targetBeat;
}
