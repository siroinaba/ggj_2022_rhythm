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
        string outName=MainGameDefine.Instance.bgmName[(int)_gameType];
        _gameType = _gameType == MainGameDefine.GameType.Lightning ? MainGameDefine.GameType.Darkness : MainGameDefine.GameType.Lightning;
        string inName=MainGameDefine.Instance.bgmName[(int)_gameType];

        _noteAssembly.NotesReset();

        AudioManager.Instance.FadeOutBGMCol(outName);
        if(_gameType==MainGameDefine.GameType.Darkness){
            AudioManager.Instance.FadeInBGMCol(inName, true, 0.2f);
        } else {
            AudioManager.Instance.FadeInBGMCol(inName, true, 0.1f);
        }

        ChangeSkybox(_gameType);

        _targetBeat = 1;
    }

    private void ChangeSkybox(MainGameDefine.GameType type)
    {
        Material material = null;
        switch(type){
            case MainGameDefine.GameType.Lightning:
            material = this._lightSkybox;
            break;

            case MainGameDefine.GameType.Darkness:
            material = this._darknessSkybox;
            break;   
        }

        RenderSettings.skybox = material;
    }

    public void GameOver(int score)
    {
        _status = MainGameDefine.GameStatus.Result;
        AudioManager.Instance.StopBGM(MainGameDefine.Instance.bgmName[(int)_gameType]);
        _uiViewer.SetActiveResultUI(true, score);
        _uiViewer.SetActiveScoreText(false);
        _uiViewer.SetActiveHeartImage(false);
    }

    private void GameExecute()
    {
        var currentStatus = _status;

        switch (_status)
        {
            case MainGameDefine.GameStatus.Title:
                AudioManager.Instance.PlayBGM("j1_81", true, 0.2f);

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
                    StartCoroutine(CountDown());
                }

                if (AudioManager.Instance.GetTime(MainGameDefine.Instance.bgmName[(int)_gameType]) > 0.0f)
                {
                    float checkTime = AudioManager.Instance.GetGapFromBeat(MainGameDefine.Instance.bgmName[(int)_gameType], _targetBeat);

                    if (checkTime == -1)
                        return;

                    if (checkTime <= 0.02f)
                    {
                        _noteAssembly.ActivateNote(_gameType);
                        _noteAssembly.NotesMove();
                        _targetBeat++;
                    }
                }
                break;
            case MainGameDefine.GameStatus.Result:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _uiViewer.SetActiveResultUI(false, 0);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
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

    private IEnumerator CountDown()
    {
        AudioManager.Instance.FadeOutBGMCol("j1_81");
        _uiViewer.SetActiveCountDownUI(true);

        _uiViewer.SetCountDownText("3");
        AudioManager.Instance.PlaySE("count");
        yield return new WaitForSeconds(1.0f);

        _uiViewer.SetCountDownText("2");
        AudioManager.Instance.PlaySE("count");
        yield return new WaitForSeconds(1.0f);

        _uiViewer.SetCountDownText("1");
        AudioManager.Instance.PlaySE("count");
        yield return new WaitForSeconds(1.0f);

        _uiViewer.SetCountDownText("START");
        AudioManager.Instance.PlaySE("count");
        yield return new WaitForSeconds(1.0f);

        _uiViewer.SetActiveCountDownUI(false);

        _uiViewer.SetActiveScoreText(true);
        _uiViewer.SetActiveHeartImage(true);

        AudioManager.Instance.PlayBGM(MainGameDefine.Instance.bgmName[(int)gameType], true, 0.2f);

        yield break;
    }

    public void ScoreTextUpdate(int score)
    {
        _uiViewer.SetScoreText(score);
    }

    public void HeartImageUpdate(int hp)
    {
        _uiViewer.UpdateHeartImage(false, hp);
    }

    [SerializeField]
    NotesAssembly _noteAssembly;

    [SerializeField]
    private UIViewer _uiViewer;

    [SerializeField]
    private Material _darknessSkybox;

    [SerializeField]
    private Material _lightSkybox;

    MainGameDefine.GameType _gameType;
    private int _targetBeat;
    MainGameDefine.GameStatus _status = MainGameDefine.GameStatus.None;
    MainGameDefine.GameStatus _beforeStatus = MainGameDefine.GameStatus.None;
}
