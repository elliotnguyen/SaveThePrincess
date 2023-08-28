using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class UserInfo
{
    public string CourseID;
    public string StudentID;
}

public class VerifyController : MonoBehaviour
{
    [SerializeField] InputField courseIDInput;
    
    [SerializeField] Text result;
    [SerializeField] Button mint;

    public static string outputArea;

    // Start is called before the first frame update
    void Start()
    {
        result.text = "";
        courseIDInput.text = ""/*"64dd204d1c157746f11b69c6"*/;
        outputArea = "http://localhost:3001/api/contract/get_questions/";
        mint.onClick.AddListener(PostData);
    }


    async void PostData()
    {
        await PostDataCoroutine();
    }

    private async Task PostDataCoroutine()
    {
        string url = outputArea + courseIDInput.text;

        //Debug.Log(courseIDInput.text);
        Debug.Log(url);

        using var webRequest = UnityWebRequest.Get(url);
        webRequest.SetRequestHeader("Content-Type", "application/json");

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("hello");
            result.text = "Invalid CourseID";
        } else
        {
            outputArea += courseIDInput.text;
            //yield return null;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
