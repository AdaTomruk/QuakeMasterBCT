using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textNamer : MonoBehaviour
{
    public GameObject Label;
    public GameObject magnitudeLabel;

    public string Text;
    public string magnitudeText;

    // Start is called before the first frame update
    void Start()
    {
        Label.GetComponent<TMP_Text>().text = Text;
        magnitudeLabel.GetComponent<TMP_Text>().text = magnitudeText;

    }
}
