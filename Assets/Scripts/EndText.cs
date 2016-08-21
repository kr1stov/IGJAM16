using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndText : MonoBehaviour {

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();

        string lang = PlayerPrefs.GetString("IGJAM16_LANG", "de");
        if (lang == "de")
            text.text = "GRATULATION. DU BIST EIN WAHRER POET";
        else if (lang == "en")
            text.text = "CONGRATULATIONS. YOU REALLY HAVE A WAY WITH WORDS";

    }
}
