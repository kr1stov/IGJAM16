using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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

        int sceneIndex = PlayerPrefs.GetInt("IGJAM16_SCENE", 0);
        gm.InitScene(_scenes[sceneIndex]);

        yield return StartCoroutine(gm.SayNextLine());
        StartCoroutine(gm.SayNextLine());
    }

    IEnumerator LoadContent()
    {
        _scenes = new List<string>();
        string line = null;

        using (TextReader reader = File.OpenText(Application.dataPath + "/StreamingAssets/config.txt"))
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
