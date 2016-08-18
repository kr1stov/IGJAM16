using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class ContentLoader : MonoBehaviour {


    private List<string> _scenes;

    public List<string> Scenes
    {
        get
        {
            return _scenes;
        }
    }

    private static ContentLoader _instance = null;

    public static ContentLoader Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ContentLoader();

            return _instance;
        }
    }

    // Use this for initialization
	IEnumerator Start () {
	    if(_instance != null)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(this);

        yield return StartCoroutine(LoadContent());

        // start game
	}

    IEnumerator LoadContent()
    {
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
            }
        }

        yield return null;
    }


}
