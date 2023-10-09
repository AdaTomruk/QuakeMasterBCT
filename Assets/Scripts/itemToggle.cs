using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class itemToggle : MonoBehaviour
{

    public Toggle toggle;
    public GameObject Target;
    public GameObject self;
    public GameObject Label;
    public GameObject MagnitudeLable;
    public string LabelText;
    public string MagnitudeLableText;
    public GameObject Test;


    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag(self.name);
        Target.SetActive(true);
        Label.GetComponent<TMP_Text>().text = LabelText;
        MagnitudeLable.GetComponent<TMP_Text>().text = MagnitudeLableText;


    }

    // Update is called once per frame
    void Update()
    {
        if (!toggle.isOn)
        {
            Target.SetActive(false);
        }
        else
        {
            Target.SetActive(true);
        }

    }
    public void toggleActive(bool state)
    {
        Target.SetActive(state);
        toggle.isOn = state;    
    }
}
