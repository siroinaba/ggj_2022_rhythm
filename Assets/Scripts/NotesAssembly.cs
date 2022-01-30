using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesAssembly : MonoBehaviour
{
    public const int NOTES_MAX = 100;

    public enum NoteType
    {
        Lightning = 0,
        Darkness = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        //NoteCreate();
        NotesLoad();
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void NotesLoad()
    {
        for(int i = 0; i < NOTES_MAX; i++)
        {
            NoteCreate();
        }
    }

    public void NoteCreate()
    {
        var note = Instantiate(_noteObj, Vector3.zero, Quaternion.identity).GetComponent<NoteEntity>();
        note.Initialize();
        NotesList.Add(note);
    }

    public void ActivateNote(MainGameDefine.GameType type)
    {
        for(int i = 0; i < NOTES_MAX; i++)
        {
            if (NotesList[i].useCount <= _noteCount)
            {
                if (!NotesList[i].isActivate)
                {
                    int lenIdx = Random.Range(0, MainGameDefine.Instance.lenPosList.Count);
                    int noteType = Random.Range(0, 1);

                    NotesList[i].SetActivate(MainGameDefine.Instance.lenPosList[lenIdx], type);

                    break;
                }
            }

            if(i == (NOTES_MAX - 1))
            {
                _noteCount++;
            }
        }
    }

    public void NotesMove()
    {
        for(int i = 0; i < NotesList.Count; i++)
        {
            if (!NotesList[i].isActivate)
                continue;

            NotesList[i].Move();
        }
    }

    public void NotesReset()
    {
        for (int i = 0; i < NotesList.Count; i++)
        {
            NotesList[i].SetDeactive();
        }

        _noteCount = 0;
    }

    public void NotesChangeModel(MainGameDefine.GameType type)
    {
        for (int i = 0; i < NotesList.Count; i++)
        {
            if (!NotesList[i].isActivate)
                continue;

            NotesList[i].NoteModelChange(type);
        }
    }

    [SerializeField]
    private GameObject _noteObj;

    private List<NoteEntity> NotesList = new List<NoteEntity>();

    private int _noteCount = 0;
}
