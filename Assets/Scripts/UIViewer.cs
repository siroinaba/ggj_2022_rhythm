using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveTitleUI(bool isActive)
    {
        _titleUI.SetActive(isActive);
    }

    public void SetActiveResultUI(bool isActive)
    {
        _resultUI.SetActive(isActive);
    }

    [SerializeField]
    private GameObject _titleUI;

    [SerializeField]
    private GameObject _resultUI;
}
