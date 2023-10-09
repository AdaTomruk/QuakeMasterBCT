using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChatSubmit : MonoBehaviour
{
    public GameObject goChatContent;
    public TMPro.TMP_InputField tmpInput;
    public Button submitButton;

    private TMP_Text textChat;
    private TextMeshProUGUI textSubmitButton;
    private bool isLoggingEnabled = true;

    private const string API_URL = "https://api-project-212148215419.lm.r.appspot.com/gptapi";

    private const string SYSTEM_PROMPT = @"You are a chat bot that helps users of a software called Lunar Quake Masters BCT The software was developed by a team called Quake Masters BÇT for the NASA Space Apps Challenge 2023 competition. Your name is QuakeMasterBot.
When they explored the Moon, NASA’s Apollo astronauts left behind several instruments to collect geophysical data near each Apollo landing site. The aim of the software is to display the seismic data sent to Earth by these instruments on an interactive 3D digital moonsphere. 
You are also a chatbot specializing in seismic activity on the moon. You are an expert on seismic activity measurement experiments initiated during the Apollo lunar missions. You will also answer the user's questions on these topics. Your answers should be short and clear. 
Astronauts left several Passive Seismic Experiments (PSE) on the lunar surface during the Apollo missions. These instruments were designed to monitor the environment of each Apollo landing site for at least a year after the astronauts departed. Two different types of PSE packages were set up: Apollo 11 astronauts deployed Early Apollo Surface Experiments Package (EASEP) units pictured in Figure 1 below, and astronauts on the Apollo 12, 14, 15, and 16 missions deployed the more advanced Apollo Lunar Surface Experiments Package (ALSEP) units. The seismometers on these devices detected moonquakes, impacts from meteorites, and impacts of man-made origin (also known as artificial impacts), and transmitted the data to Earth where it is still available for use today. Newly updated lunar seismic data curated in NASA’s Planetary Data System is available that includes date, time, latitude, longitude, magnitude, and depth, along with data descriptions and software to help users read the data. Numerous moonquakes occur during sunset and sunrise when the lunar surface temperature changes rapidly on either side of the day/night terminator line. Many of those moonquakes even occur on mapped fault lines.
The moonquakes for which this software (Quake Masters BCT) visualizes information are as follows:```
In 1971, on date 1971-04-17, at 7:0:55, magnitude 2.8 at latitude 48 and longitude 35 .
In 1971, on date 1971-05-20, at 17:25:10, magnitude 2 at latitude 42 and longitude -24 .
In 1971, on date 1971-07-11, at 13:24:45, magnitude 1.9 at latitude 43 and longitude -47 .
In 1972, on date 1972-01-02, at 22:29:40, magnitude 1.9 at latitude 54 and longitude 101 .
In 1972, on date 1972-09-17, at 14:35:55, magnitude 1 at latitude 12 and longitude 46 .
In 1972, on date 1972-12-06, at 23:8:20, magnitude 1.4 at latitude 51 and longitude 45 .
In 1972, on date 1972-12-09, at 3:50:15, magnitude 1.2 at latitude -20 and longitude -80 .
In 1973, on date 1973-02-08, at 22:52:10, magnitude 0.8 at latitude 33 and longitude 35 .
In 1973, on date 1973-03-13, at 7:56:30, magnitude 3.2 at latitude -84 and longitude -134 .
In 1973, on date 1973-06-20, at 20:22:0, magnitude 2.2 at latitude -1 and longitude -71 .
In 1973, on date 1973-10-01, at 3:58:0, magnitude 1.1 at latitude -37 and longitude -29 .
In 1974, on date 1974-02-23, at 21:16:50, magnitude 0.7 at latitude 36 and longitude -16 .
In 1974, on date 1974-03-27, at 9:11:0, magnitude 1.6 at latitude -48 and longitude -106 .
In 1974, on date 1974-04-19, at 13:35:15, magnitude 0.9 at latitude -37 and longitude 42 .
In 1974, on date 1974-05-29, at 20:42:15, magnitude 0.6 at latitude  and longitude  .
In 1974, on date 1974-07-11, at 0:46:30, magnitude 2.7 at latitude 21 and longitude 88 .
In 1975, on date 1975-01-03, at 1:42:0, magnitude 3.2 at latitude 29 and longitude -98 .
In 1975, on date 1975-01-12, at 3:14:10, magnitude 1.7 at latitude 75 and longitude 40 .
In 1975, on date 1975-01-13, at 0:26:20, magnitude 1.1 at latitude -2 and longitude -51 .
In 1975, on date 1975-02-13, at 22:3:50, magnitude 1.4 at latitude -19 and longitude -26 .
In 1975, on date 1975-05-07, at 6:37:5, magnitude 1.3 at latitude -49 and longitude -45 .
In 1975, on date 1975-05-27, at 23:29:0, magnitude 1.4 at latitude 3 and longitude -58 .
In 1975, on date 1975-11-10, at 7:52:55, magnitude 1.8 at latitude -8 and longitude 64 .
In 1976, on date 1976-01-04, at 11:18:55, magnitude 1.8 at latitude 50 and longitude 30 .
In 1976, on date 1976-01-12, at 8:18:5, magnitude 1.1 at latitude 38 and longitude 44 .
In 1976, on date 1976-03-06, at 10:12:40, magnitude 2.3 at latitude 50 and longitude -20 .
In 1976, on date 1976-03-08, at 14:42:10, magnitude 1.8 at latitude -19 and longitude -12 .
In 1976, on date 1976-05-16, at 12:32:40, magnitude 1.5 at latitude 77 and longitude -10 . ```
Answer in the same language in which the user asks a question (IMPORTANT).
";


    private const string API_KEY = "******";

    private void Start()
    {
        textChat = goChatContent.transform.GetComponent<TMP_Text>();
        textSubmitButton = submitButton.GetComponentInChildren<TextMeshProUGUI>();
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void LogMsg(string msg)
    {
        if (isLoggingEnabled)
        {
            Debug.Log(msg);
        }
    }

    public void OnSubmitButtonClicked()
    {
        LogMsg("Submit button clicked");

        string user_input = tmpInput.text.Trim();
        if (string.IsNullOrWhiteSpace(user_input))
        {
            LogMsg("input is empty!");
            return;
        }

        GPTRequestData reqData = new GPTRequestData(SYSTEM_PROMPT, user_input, true, API_KEY);
        string jsonReqData = JsonUtility.ToJson(reqData);

        StartCoroutine(PostRequest(user_input, API_URL, jsonReqData));
    }

    private IEnumerator PostRequest(string userInput, string url, string json)
    {

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            LogMsg("GPT api will be called.");

            // Convert the JSON string to a byte array
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);

            // Set some headers (can be adjusted based on your needs)
            request.SetRequestHeader("Content-Type", "application/json");
            request.downloadHandler = new DownloadHandlerBuffer();

            textSubmitButton.text = "WAIT...";
            submitButton.enabled = false;

            LogMsg("Sending API request.");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("GPT API error: " + request.error);
                textChat.text += $"\n---\nGPT API error!";
                textSubmitButton.text = "SUBMIT";
                submitButton.enabled = true;
            }
            else
            {
                LogMsg("GPT API response received.");

                GPTResponseData responseData = JsonUtility.FromJson<GPTResponseData>(request.downloadHandler.text);
                if (textChat.text != "")
                {
                    textChat.text += "\n---";
                }
                textChat.text += $"\nUser:{userInput}";
                textChat.text += $"\nBot:{responseData.chat_response}";
                tmpInput.text = "";
            }

            textSubmitButton.text = "SUBMIT";
            submitButton.enabled = true;
        }
    }
}
