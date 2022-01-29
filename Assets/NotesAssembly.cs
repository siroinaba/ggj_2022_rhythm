using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesAssembly : MonoBehaviour
{
    public const int LEN_MAX = 10;
    public const int NOTES_MAX = 100;


    // Start is called before the first frame update
    void Start()
    {
        //NoteCreate();
        NotesLoad();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(test());

        }
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
        var note = Instantiate(_lightningNote, Vector3.zero, Quaternion.identity).GetComponent<NoteEntity>();
        note.Initialize();
        NotesList.Add(note);
    }

    private void ActivateNote()
    {
        for(int i = 0; i < NOTES_MAX; i++)
        {
            if (!NotesList[i].isActivate)
            {
                Debug.Log(i);
                int lenIdx = Random.Range(0, LEN_MAX);
                NotesList[i].SetActivate(true, _lenPosList[lenIdx]);

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

    private IEnumerator test()
    {
        int count = 0;
        while(count < 10)
        {
            ActivateNote();
            NotesMove();
            count++;
            yield return new WaitForSeconds(1.0f);
        }

        yield break;
    }

    [SerializeField]
    private GameObject _lightningNote;

    [SerializeField]
    private GameObject _darknessNote;

    [SerializeField]
    private List<Vector3> _lenPosList = new List<Vector3>();

    private List<NoteEntity> NotesList = new List<NoteEntity>();
}
