using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionLoader : MonoBehaviour
{
    public List<Question> QuizList;

    public static QuestionLoader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public async void Start()
    {
        var url = "https://mocki.io/v1/46f4b3a0-690d-4308-b897-1bef1df4c7e3";

        var httpClient = new APIController();
        QuizList = await httpClient.Get<List<Question>>(url);
      
        for (int i = 0; i < QuizList.Count; i++)
        {
            Debug.Log(QuizList[i].GetCorrectAnswer());
        }
    }
}
