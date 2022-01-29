using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesDeleteWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _unit = transform.parent.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                _unit.OnDamage();
                break;
            case MainGameDefine.GameType.Darkness:
                note.SetDeactive();
                break;
        }
    }

    Unit _unit;
}
