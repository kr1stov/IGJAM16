using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinningCondition : MonoBehaviour {

    GameMaster gm;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    public IEnumerator ShowFailScreen()
    {

        PlayerPrefs.SetInt("IGJAM16_SCENE", gm.NextSceneIndex);

        GetComponent<Animator>().SetTrigger("fail");
        yield return null;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("Transition");
    }

    public IEnumerator ShowWinScreen()
    {
        gm.NextSceneIndex += 1;
        PlayerPrefs.SetInt("IGJAM16_SCENE", gm.NextSceneIndex);

        GetComponent<Animator>().SetTrigger("win");

        yield return null;
    }
}
