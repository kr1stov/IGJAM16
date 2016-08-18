using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {

    public Text title;
    public Text subTitle;
    public float delay;

    private int sceneIndex;

    // Use this for initialization
    IEnumerator Start () {
        sceneIndex = PlayerPrefs.GetInt("IGJAM16_SCENE", 0);
        string lang = PlayerPrefs.GetString("IGJAM16_LANG", "de");
        
        if(lang == "de")
            title.text = "SZENE " + (sceneIndex + 1).ToString();
        else
            title.text = "SCENE " + (sceneIndex + 1).ToString();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main");
	}
}
