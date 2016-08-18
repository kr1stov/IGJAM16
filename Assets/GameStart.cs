using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	    if(Input.anyKeyDown)
        {
            PlayerPrefs.SetInt("IGJAM16_SCENE", 0);
            SceneManager.LoadScene("Transition");
        }
    }
}
