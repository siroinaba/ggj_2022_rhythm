using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                break;
            case MainGameDefine.GameType.Darkness:
                note.SetDeactive();
                OnDamage();
                break;
        }
    }

    [SerializeField]
    private int _hp;

    private int _lenIndex = 0;
}
