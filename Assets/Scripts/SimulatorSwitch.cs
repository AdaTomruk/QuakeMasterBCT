using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulatorSwitch : MonoBehaviour
{
    public Button yourButton;
    public string Scene;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        SceneManager.LoadScene("FirstPeron");
        Debug.Log("aaaaa");
    }
}
