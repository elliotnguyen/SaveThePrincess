using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CertInfo
{
    public string message;
    //public CertMinted certMinted;
    public Dictionary<string, string> certMinted { get; set; }

    public string success;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Reward reward;
}

/*
public class CertMinted 
{
    //public Dictionary<string, string> numericFields;
    
    public string First;
    public string CertID;
    public string Second;
   
    public int    __length__;

    public string CourseID;
    public string owner;
    public string TokenID;
}
*/

public class Reward 
{
    public string First;
    public string Second;
    public string Third;

    public string Length;
    public string To;
    public string CourseID;

    public string TokenID;
}

public class Congratulation : MonoBehaviour
{
    public GameObject WinnerBox;

    [SerializeField] Button mint;
    [SerializeField] Text certID;
    [SerializeField] Text reward;
    [SerializeField] Button exit;
    [SerializeField] Text loading;

    public static Congratulation Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
        }
    }

    public void Start()
    {
        //certID.text = "CertID: ";
        reward.text = "";
        exit.enabled = false;
        //exit.onClick.AddListener(ExitScene);
        //mint.onClick.AddListener(Mint);
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("Login");
    }

    public async void Mint()
    {
        await MintCert();
    }

    public async Task MintCert()
    {
        string url = QuestionLoader.Instance.GetLink().Replace("get_questions", "mint_cert");

        Debug.Log(url);

        using var webRequest = UnityWebRequest.Get(url);
        webRequest.SetRequestHeader("Content-Type", "application/json");

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
            loading.text = "loading...";
        }

        loading.text = "";
        mint.enabled = false;
        exit.enabled = true;

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {webRequest.error}");
        }

        var jsonResponse = webRequest.downloadHandler.text;

        try
        {
            CertInfo result = JsonConvert.DeserializeObject<CertInfo>(jsonResponse);

            //mint.enabled = false;
            //Destroy(mint);

            if (result.certMinted != null)
            {
                certID.text = "CertID: ";
                certID.text += result.certMinted["tokenID"];
            }

            if (result.reward != null)
            {
                reward.text = "Yahooo, you also get a reward!";
            }
            //Debug.Log(result.info.CertID);
            //Destroy(mint);
            //certID.text += "hello";
            //certID.text += result.info.CertID;

            /*
            if (result.reward != null)
            {
                reward.text = "Yahooo, you also get a reward!";
            }
            */
        }
        catch (Exception ex)
        {
            Debug.LogError($"{this} could not parse the response {jsonResponse}. {ex.Message}");
        }
    }
}
