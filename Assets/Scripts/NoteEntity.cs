using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEntity : MonoBehaviour
{
    public bool isActivate { get { return _isActivate; } }

    public int useCount { get { return _useCount; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        _isActivate = false;
        _useCount = 0;
    }

    public void Move()
    {
        Vector3 vec = this.transform.position;
        vec.z -= 1.0f;

        this.transform.DOMove(vec, 0.5f);
    }

    public void SetActivate(Vector3 pos, MainGameDefine.GameType type)
    {
        _isActivate = true;
        _useCount++;

        SetPos(pos);

        switch (type)
        {
            case MainGameDefine.GameType.Lightning:
                _lightningObj.SetActive(true);
                break;
            case MainGameDefine.GameType.Darkness:
                _darknessObj.SetActive(true);
                break;
        }
    }

    public void SetDeactive()
    {
        _lightningObj.SetActive(false);
        _darknessObj.SetActive(false);

        _isActivate = false;
    }

    public void NoteModelChange(MainGameDefine.GameType type)
    {
        switch (type)
        {
            case MainGameDefine.GameType.Darkness:
                _lightningObj.SetActive(false);
                _darknessObj.SetActive(true);
                break;
            case MainGameDefine.GameType.Lightning:
                _lightningObj.SetActive(true);
                _darknessObj.SetActive(false);
                break;
        }
    }

    private void SetPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    [SerializeField]
    private GameObject _lightningObj;

    [SerializeField]
    private GameObject _darknessObj;

    private bool _isActivate = false;
    private NotesAssembly.NoteType _noteType;

    private int _useCount;
}
