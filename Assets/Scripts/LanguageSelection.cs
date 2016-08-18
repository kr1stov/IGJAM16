using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageSelection : MonoBehaviour {

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }


    public void OnHover()
    {
        text.fontStyle = FontStyle.Bold;

    }
    public void OnExit()
    {
        text.fontStyle = FontStyle.Normal;


    }

    public void OnClick()
    {
        text.fontStyle = FontStyle.Bold;
        text.fontSize++;

        PlayerPrefs.SetInt("IGJAM16_SCENE", 0);
        PlayerPrefs.SetString("IGJAM16_LANG", text.text);

        SceneManager.LoadScene("Transition");


    }
}
