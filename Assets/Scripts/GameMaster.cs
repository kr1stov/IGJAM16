﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public GameObject _emptyTextObject;

    public List<DialogLine> lines = new List<DialogLine>();
    private int nextLineIndex = 0;
    public float talkDelay;
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


    private InfoArea infoArea;
    private Animator infoAreaAnimator;
    public Animator InfoAreaAnimator
    {
        get { return infoAreaAnimator; }
    }

    private TitleArea titleArea;
    private Text titleAreaText;

    public float infoAreaSpeedIncrease;

    public AnimationCurve curve;

    private bool finishedSpeaking;
    public bool FinishedSpeaking
    {
        get { return finishedSpeaking; }
    }

    void Awake()
    {
        finishedSpeaking = false;

        nextSceneIndex = PlayerPrefs.GetInt("IGJAM16_SCENE", 0);
        lang = PlayerPrefs.GetString("IGJAM16_LANG", "de");
        contentLoader = FindObjectOfType<ContentLoader>();
        InitScene(contentLoader.Scenes[nextSceneIndex]);
        _dialogueLinesSoFar = new List<GameObject>();

        winCondition = FindObjectOfType<WinningCondition>();

        infoArea = FindObjectOfType<InfoArea>();
        infoAreaAnimator = infoArea.GetComponent<Animator>();
    }

    IEnumerator Start()
    {
        yield return StartCoroutine(SayNextLine());
        StartCoroutine(SayNextLine());
    }

    void AnimationCurve()
    {
        float startTime = Time.timeSinceLevelLoad;
        while (Time.timeSinceLevelLoad <= startTime + 3)
        {
            infoAreaAnimator.speed += curve.Evaluate(Time.time);
        }


    }

    public IEnumerator SayNextLine()
    {
        finishedSpeaking = false;
        Debug.Log("nextLineIndex: " + nextLineIndex + " | lines.count-1: " + (lines.Count - 1).ToString());

        if (nextLineIndex >= lines.Count)
        {
            // animation

            // next scene
            //StartCoroutine(winCondition.ShowWinScreen());
            nextSceneIndex++;
            PlayerPrefs.SetInt("IGJAM16_SCENE", nextSceneIndex);
            StartCoroutine(winCondition.LoadNextScene(InfoAreaAnimator, infoAreaSpeedIncrease));
        }
        else
        {
            bool name1DisplayedOnce = false;
            bool name2DisplayedOnce = false;

            Instantiate(_emptyTextObject, contentArea.transform);

            foreach (Choice choice in lines[nextLineIndex].choices)
            {
                GameObject lineSaidObject = Instantiate(_textObject, contentArea.transform) as GameObject;
                GameObject lineText = lineSaidObject.transform.Find("Text").gameObject;
                GameObject lineName = lineSaidObject.transform.Find("Name").gameObject;
                //lineText.GetComponent<EventTrigger>().enabled = false;
                _dialogueLinesSoFar.Add(lineText);
                lineSaidObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                lineSaidObject.GetComponentInChildren<EventTrigger>().enabled = false;
                Text lineSaid = lineText.GetComponent<Text>();
                Text lineTalker = lineName.GetComponent<Text>();

                if (lines[nextLineIndex].actorId % 2 == 1)
                {
                    if(!name1DisplayedOnce)
                    { 
                        lineTalker.text = "Bab:";
                        name1DisplayedOnce = true;
                    }
                    else
                    {
                        lineTalker.text = "";

                    }
                }
                else
                {
                    if (!name2DisplayedOnce)
                    {
                        lineTalker.text = "Aba:";
                        name2DisplayedOnce = true;
                    }
                    else
                    {
                        lineTalker.text = "";

                    }
                }

                lineSaid.text = choice.text;
                lineSaid.GetComponent<TextTyper>().Init();


                yield return StartCoroutine(lineSaid.gameObject.GetComponent<TextTyper>().Speak(choice.indicator, talkDelay));
            }
            finishedSpeaking = true;
            nextLineIndex++;

        }

    }

    public void InitScene(string filename)
    {
       titleArea = FindObjectOfType<TitleArea>();
       titleAreaText = titleArea.GetComponent<Text>();

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
                else if (line.StartsWith("t"))
                {
                    elements = line.Split('\t');
                    string title = elements[1];
                    titleAreaText.text = title;
                }

            }
        }
    }
}
