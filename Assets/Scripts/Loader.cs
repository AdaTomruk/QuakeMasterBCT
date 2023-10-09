using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickExample : MonoBehaviour
{
    public Button yourButton;
    public string Scene;

    void Start()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            //Make sure to assign the value of shakeFrequency in the inspector 
            //or uncomment the next line to assign it here.
            //shakeFrequency = 0.2f;

            
            SceneManager.LoadScene(Scene);
            Debug.Log("aaaaa");

        }

    }
        
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //Make sure to assign the value of shakeFrequency in the inspector 
            //or uncomment the next line to assign it here.
            //shakeFrequency = 0.2f;


            SceneManager.LoadScene("SampleScene");
            Debug.Log("aaaaa");

        }
    }


}