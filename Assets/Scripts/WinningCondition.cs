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

    public IEnumerator LoadNextScene(Animator anim, float speedIncreas)
    {
        yield return IncreaseAnimationSpeed(anim, speedIncrease);

        SceneManager.LoadScene("Transition");
    }

    IEnumerator IncreaseAnimationSpeed(Animator anim, float speedIncrease)
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            anim.speed += speedIncrease;
        }
    }

    public IEnumerator ShowWinScreen()
    {
        gm.NextSceneIndex += 1;
        PlayerPrefs.SetInt("IGJAM16_SCENE", gm.NextSceneIndex);

        GetComponent<Animator>().SetTrigger("win");

        yield return null;
    }
}
