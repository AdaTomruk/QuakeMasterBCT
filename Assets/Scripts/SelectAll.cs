using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectAll : MonoBehaviour
{



    public GameObject[] togles;
    public Button bt;


    void Start()
    {
        togles = GameObject.FindGameObjectsWithTag("40");
        Button btn = bt.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);



    }

    // Update is called once per frame
    void Update()
    {
        togles = GameObject.FindGameObjectsWithTag("40");
        Button btn = bt.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        foreach (GameObject togle in togles)
        {
            togle.SendMessage("toggleActive", false);
        }
        Debug.LogWarning("presd");
    }


}
