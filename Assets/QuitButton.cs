using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{

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

        Application.Quit();


    }
}
