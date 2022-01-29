using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEntity : MonoBehaviour
{
    public bool isActivate { get { return _isActivate; } }

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
    }

    public void Move()
    {
        Vector3 vec = this.transform.position;
        vec.z -= 1.0f;

        this.transform.DOMove(vec, 0.5f);
    }

    public void SetActivate(Vector3 pos, NotesAssembly.NoteType type)
    {
        _isActivate = true;

        SetPos(pos);

        _noteType = type;
        switch (type)
        {
            case NotesAssembly.NoteType.Lightning:
                _lightningObj.SetActive(true);
                break;
            case NotesAssembly.NoteType.Darkness:
                _darknessObj.SetActive(true);
                break;
        }
    }

    public void SetDeactive()
    {
        switch (_noteType)
        {
            case NotesAssembly.NoteType.Lightning:
                _lightningObj.SetActive(false);
                break;
            case NotesAssembly.NoteType.Darkness:
                _darknessObj.SetActive(false);
                break;
        }
        _isActivate = false;
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
}
