﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public class DialogLine
    {
        public int actorId;
        public List<Choice> choices = new List<Choice>();
    }

    public class Choice
    {
        public string indicator;
        public string text;
    }

    public GameObject _textObject;

    public List<DialogLine> lines = new List<DialogLine>();
    private int nextLineIndex = 0;
    public float talkDelay;
    public Color partnerColor;
    public GameObject contentArea;
    private string lang;

    private int nextSceneIndex = 0;
    public int NextSceneIndex {
        get { return nextSceneIndex; }
        set { nextSceneIndex = value; }
    }
    private ContentLoader contentLoader;

    private List<GameObject> _dialogueLinesSoFar;

    public List<GameObject> DialogueLinesSoFar
    {
        get { return _dialogueLinesSoFar; }
    }

    public Canvas secondCanvas;
    private WinningCondition winCondition;

    void Awake()
    {
        nextSceneIndex = PlayerPrefs.GetInt("IGJAM16_SCENE", 0);
        lang = PlayerPrefs.GetString("IGJAM16_LANG", "de");
        contentLoader = FindObjectOfType<ContentLoader>();
        InitScene(contentLoader.Scenes[nextSceneIndex]);
        _dialogueLinesSoFar = new List<GameObject>();

        winCondition = FindObjectOfType<WinningCondition>();

    }

    IEnumerator Start()
    {
        yield return StartCoroutine(SayNextLine());
        StartCoroutine(SayNextLine());
    }

    public IEnumerator SayNextLine()
    {
        if (nextLineIndex > lines.Count -1)
        {
            // animation

            // next scene
            StartCoroutine(winCondition.ShowWinScreen());
        }
        else
        { 

            foreach(Choice choice in lines[nextLineIndex].choices)
            {
                GameObject lineSaidObject = Instantiate(_textObject, contentArea.transform) as GameObject;
                _dialogueLinesSoFar.Add(lineSaidObject);
                lineSaidObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                Text lineSaid = lineSaidObject.GetComponent<Text>();
                //lineSaid.alignment = ((lines[nextLineIndex].actorId % 2 == 0) ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight);
                if(lines[nextLineIndex].actorId % 2 == 1)
                {
                    lineSaid.fontStyle = FontStyle.Italic;
                    lineSaid.color = partnerColor;
                }

                lineSaid.text = choice.text;
                lineSaid.GetComponent<TextTyper>().Init();
                yield return StartCoroutine(lineSaid.GetComponent<TextTyper>().Speak(choice.indicator, talkDelay));
            }
            nextLineIndex++;
        }

    }

    public void InitScene(string filename)
    {
        string line = null;
        int currentActorId = 0;
        string[] elements;

        using (TextReader reader = File.OpenText(Application.dataPath + "/StreamingAssets/" + lang + "/" +filename + ".txt"))
        {
            line = reader.ReadLine();

            while(line != null)
            {
                if (line.StartsWith("a"))
                {
                    int.TryParse(line.Substring(1), out currentActorId);
                    line = reader.ReadLine();
                }
                else if(line.StartsWith("c"))
                {
                    DialogLine newLine = new DialogLine();

                    while (line.StartsWith("c"))
                    {
                        elements = line.Split('\t');
                        string indicator = elements[1];
                        string text = elements[2];
                        Choice newChoice = new Choice();
                        newChoice.text = text;
                        newChoice.indicator = indicator;

                        newLine.choices.Add(newChoice);
                        newLine.actorId = currentActorId;

                        line = reader.ReadLine();

                        if (line == null)
                            break;
                    }

                    lines.Add(newLine);
                }

            }
        }
    }
}
