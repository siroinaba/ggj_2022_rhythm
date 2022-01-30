using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public MainGameDefine.GameType gameType { get { return _gameType; } }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        GameExecute();
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
        _status = MainGameDefine.GameStatus.Result;
        AudioManager.Instance.StopBGM(MainGameDefine.Instance.bgmName[(int)_gameType]);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameExecute()
    {
        var currentStatus = _status;

        switch (_status)
        {
            case MainGameDefine.GameStatus.Title:
                if(_uiViewer == null)
                {
                    _uiViewer = GameObject.Find("Canvas").GetComponent<UIViewer>();
                }

                if(_noteAssembly == null)
                {
                    _noteAssembly = GameObject.Find("NotesAssembly").GetComponent<NotesAssembly>();
                }

                if (_status != _beforeStatus)
                {
                    _uiViewer.SetActiveTitleUI(true);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _status = MainGameDefine.GameStatus.InGame;
                }
                break;
            case MainGameDefine.GameStatus.InGame:
                if (_status != _beforeStatus)
                {
                    _uiViewer.SetActiveTitleUI(false);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AudioManager.Instance.PlayBGM(MainGameDefine.Instance.bgmName[(int)gameType]);
                }

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
                break;
            case MainGameDefine.GameStatus.Result:
                break;
        }

        _beforeStatus = currentStatus;
    }

    private void Initialize()
    {
        _gameType = MainGameDefine.GameType.Darkness;
        _targetBeat = 1;
        _status = MainGameDefine.GameStatus.Title;
        _noteAssembly.NotesReset();
    }

    [SerializeField]
    NotesAssembly _noteAssembly;

    MainGameDefine.GameType _gameType;
    private int _targetBeat;
    MainGameDefine.GameStatus _status = MainGameDefine.GameStatus.None;
    MainGameDefine.GameStatus _beforeStatus = MainGameDefine.GameStatus.None;

    private UIViewer _uiViewer;
}
