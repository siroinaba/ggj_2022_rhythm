using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _lenIndex = 3;
        Vector3 startPos = MainGameDefine.Instance.lenPosList[_lenIndex];
        startPos.z = 0;
        transform.localPosition = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        InputOperation();

        _debugHpText.text = "HP : " + _hp.ToString();
        _debugScoreText.text = "SCORE : " + _score.ToString();
    }

    private void InputOperation()
    {
        Vector3 pos = transform.localPosition;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(MainGameDefine.Instance.lenPosList.Count > _lenIndex + 1)
            {
                _lenIndex++;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_lenIndex - 1 > -1)
            {
                _lenIndex--;
            }
        }

        pos.x = MainGameDefine.Instance.lenPosList[_lenIndex].x;

        this.transform.DOLocalMove(pos, 0.25f);

    }

    public void OnDamage()
    {
        _hp--;

        if(_hp == 0)
        {
            // ゲームオーバー処理
            MainGameManager.Instance.GameOver();
        }
    }

    public void ScoreUp()
    {
        _score++;

        if(_score % 10 == 0)
        {
            MainGameManager.Instance.ChangeGameType();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<NoteEntity>().isActivate)
            return;

        var note = other.gameObject.GetComponent<NoteEntity>();

        switch (MainGameManager.Instance.gameType)
        {
            case MainGameDefine.GameType.Lightning:
                note.SetDeactive();
                ScoreUp();
                break;
            case MainGameDefine.GameType.Darkness:
                note.SetDeactive();
                OnDamage();
                break;
        }
    }

    [SerializeField]
    private int _hp;

    [SerializeField]
    private Text _debugHpText;

    [SerializeField]
    private Text _debugScoreText;

    private int _lenIndex = 0;

    private int _score = 0;
}
