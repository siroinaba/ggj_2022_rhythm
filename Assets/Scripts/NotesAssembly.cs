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

    public void ActivateNote()
    {
        for(int i = 0; i < NOTES_MAX; i++)
        {
            if (!NotesList[i].isActivate)
            {
                int lenIdx = Random.Range(0, MainGameDefine.Instance.lenPosList.Count);
                int noteType = 0;

                NotesList[i].SetActivate(MainGameDefine.Instance.lenPosList[lenIdx], (NoteType)noteType);

                break;
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
    }

    private IEnumerator test()
    {
        int count = 0;
        while(count < 50)
        {
            //ActivateNote();
            //NotesMove();
            count++;
            yield return new WaitForSeconds(1.0f);
        }

        yield break;
    }

    [SerializeField]
    private GameObject _noteObj;

    private List<NoteEntity> NotesList = new List<NoteEntity>();
}
