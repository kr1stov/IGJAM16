using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    public Text title;
    public Text subTitle;


    void Start()
    {
        string lang = PlayerPrefs.GetString("IGJAM16_LANG", "de");
        if (lang == "de")
            title.text = "GRATULATION. DU BIST EIN WAHRER POET";
        else if(lang == "en")
        {
            title.text = "CONGRATULATIONS: YOU REALLY HAVE A WAY WITH WORDS";

        }
    }


}
