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
        _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;

        _isActivate = false;
    }

    public void Move()
    {
        Debug.Log(Time.deltaTime);

        Vector3 vec = this.transform.position;
        vec.z -= 1.0f;

        this.transform.DOMove(vec, 0.5f);
    }

    public void SetActivate(bool isActivate, Vector3 pos)
    {
        _mesh.enabled = isActivate;
        _isActivate = isActivate;

        if (isActivate)
        {
            SetPos(pos);
        }
    }

    private void SetPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    MeshRenderer _mesh;
    bool _isActivate = false;
}
