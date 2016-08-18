using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextTyper : MonoBehaviour {

    private Text _textObject;
    private string _text;
    private string _currentText;

    private string _indicator;

    private Animator _animator;
    public float _animationSpeedDelta;

    private GameObject contentArea;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        contentArea = GameObject.Find("Content");
    }

    public void Init()
    {
        _textObject = GetComponent<Text>();
        _text = _textObject.text;
        _textObject.text = "";
    }


    public IEnumerator Speak(string indicator, float delay)
    {
        _indicator = indicator;

        for (int i = 0; i < _text.Length; i++)
        {
            _currentText += _text[i];
            _textObject.text = _currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    public void OnHover()
    {
        if (_indicator != "*")
        {
            if (_textObject.fontStyle == FontStyle.Italic)
                _textObject.fontStyle = FontStyle.BoldAndItalic;
            else
                _textObject.fontStyle = FontStyle.Bold;
        }
    }

    public void OnExit()
    {
        if (_indicator != "*")
        {
            if (_textObject.fontStyle == FontStyle.BoldAndItalic)
                _textObject.fontStyle = FontStyle.Italic;
            else
                _textObject.fontStyle = FontStyle.Normal;
        }
    }

    public void OnClick()
    {
        if(_indicator == "+")
        {
            _textObject.color = Color.green;

            GameMaster gm = FindObjectOfType<GameMaster>();

            foreach (GameObject text in gm.texts)
            {
                text.GetComponent<EventTrigger>().enabled = false;
            }

            StartCoroutine(gm.SayNextLine());

        }
        else if(_indicator == "-")
        {
            _textObject.color = Color.red;
        }
    }
}
