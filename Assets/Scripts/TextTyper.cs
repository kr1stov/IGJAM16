using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextTyper : MonoBehaviour {

    private Text _textObject;
    private string _text;
    private string _currentText;

    private string _indicator;
    
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
        if (_indicator == "*")
            return;

        _textObject.fontStyle = (_textObject.fontStyle == FontStyle.Italic) ? FontStyle.BoldAndItalic : FontStyle.Bold;
    }

    public void OnExit()
    {
        if (_indicator == "*")
            return;

        _textObject.fontStyle = (_textObject.fontStyle == FontStyle.BoldAndItalic) ? FontStyle.Italic : FontStyle.Normal;
    }

    public void OnClick()
    {
        if(_indicator == "+")
        {
            _textObject.color = Color.green;

            Canvas canvas = FindObjectOfType<Canvas>();
            for(int i = 0; i< canvas.transform.childCount; i++)
            {
                canvas.transform.GetChild(i).GetComponent<EventTrigger>().enabled = false;
            }

            GameMaster gm = FindObjectOfType<GameMaster>();
            StartCoroutine(gm.SayNextLine());

        }
        else
        {
            _textObject.color = Color.green;
        }
    }
}
