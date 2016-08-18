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
        sceneIndex = PlayerPrefs.GetInt("IGJAM16_SCENE");
        title.text += (" " + sceneIndex + 1);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main");
	}
}
