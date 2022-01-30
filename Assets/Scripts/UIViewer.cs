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

    public void SetActiveResultUI(bool isActive, int score)
    {
        _resultUI.SetActive(isActive);

        if (isActive)
        {
            Text scoreText = _resultUI.transform.GetChild(2).GetComponent<Text>();
            scoreText.text = "Score : " + score.ToString();
        }
    }

    public void SetActiveCountDownUI(bool isActive)
    {
        _countDownText.SetActive(isActive);
    }

    public void SetCountDownText(string text)
    {
        var countDownText = _countDownText.GetComponent<Text>();
        countDownText.text = text;
    }

    public void SetActiveScoreText(bool isActive)
    {
        _scoreText.SetActive(isActive);
    }

    public void SetScoreText(int score)
    {
        var scoreText = _scoreText.GetComponent<Text>();
        scoreText.text = "SCORE : " + score.ToString();
    }

    [SerializeField]
    private GameObject _titleUI;

    [SerializeField]
    private GameObject _resultUI;

    [SerializeField]
    private GameObject _countDownText;

    [SerializeField]
    private GameObject _scoreText;

    [SerializeField]
    private GameObject[] _hartImages;
}
