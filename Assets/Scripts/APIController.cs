using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class APIController
{
    public async Task<TResultType> Get<TResultType>(string url)
    {
        using var webRequest = UnityWebRequest.Get(url);
        webRequest.SetRequestHeader("Content-Type", "application/json");

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {webRequest.error}");
        }

        var jsonResponse = webRequest.downloadHandler.text;
        Debug.Log(jsonResponse.GetType());

        try
        {
            TResultType result = JsonConvert.DeserializeObject<TResultType>(jsonResponse, /*new SOConverter<QuestionData>()*/ new QuestionConverter());
        
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this} could not parse the response {jsonResponse}. {ex.Message}");
            return default;
        }
    }
}
