using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ContentLoader : MonoBehaviour {

    private GameMaster gm;
    private List<string> _scenes;

    public List<string> Scenes
    {
        get
        {
            return _scenes;
        }
    }

    // Use this for initialization
	IEnumerator Start () {
        gm = FindObjectOfType<GameMaster>();

        StartCoroutine(LoadContent());

        int sceneIndex = PlayerPrefs.GetInt("IGJAM16_SCENE");
        gm.InitScene(_scenes[sceneIndex]);

        yield return StartCoroutine(gm.SayNextLine());
        StartCoroutine(gm.SayNextLine());
    }

    IEnumerator LoadContent()
    {
        _scenes = new List<string>();
        string line = null;

        using (TextReader reader = File.OpenText(Application.dataPath + "/StreamingAssets/config"))
        {
            line = reader.ReadLine();

            while (line != null)
            {
                if (line == "")
                { }
                else
                {
                    _scenes.Add(line);
                }

                yield return null;

            }
        }
    }


}
