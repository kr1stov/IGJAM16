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
        DontDestroyOnLoad(gameObject);

        Debug.Log("sceneIndex just before content load: " + Time.timeSinceLevelLoad);
        yield return StartCoroutine(LoadContent());
        Debug.Log("sceneIndex just after content load: "+ Time.timeSinceLevelLoad);


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
                    Debug.Log("scene added:" + line);
                    _scenes.Add(line);
                }

                line = reader.ReadLine();

                yield return null;

            }
        }

        Debug.Log("scenes after load:" + _scenes.Count);
    }


}
