using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;


public class QuestionLoader : MonoBehaviour
{
    public List<Question> QuizList;

    [SerializeField] GameObject[] npcToBeSpawned;
    [SerializeField] Collider2D[] currentRoomSpawnableArea;
    public static QuestionLoader Instance { get; private set; }

    private string APILink;

    public void SetLink(string link)
    {
        APILink = link;
    }

    private void Awake()
    {
        Instance = this;
    }
    public async void Start()
    {
        var url = "http://localhost:3001/api/test/get_questions/64dd204d1c157746f11b69c6"/*APILink*/;

        var httpClient = new APIController();
        QuizList = await httpClient.Get<List<Question>>(url);

        Debug.Log("The number of question is " + QuizList.Count);
        DisplayNPCs();
    }


    private void DisplayNPCs()
    {
        int numsOfSpawnableArea = currentRoomSpawnableArea.Length;

        List<List<int>> SpawnArea = new List<List<int>>();
        for (int i = 0; i < numsOfSpawnableArea; i++)
        {
            SpawnArea.Add(new List<int>());
        }
        for (int i = 0; i < QuizList.Count; i++)
        {
            int randomIndex = Random.Range(0, numsOfSpawnableArea);
            SpawnArea[randomIndex].Add(i);
        }

        for (int i = 0; i < numsOfSpawnableArea; i++)
        {
            SpawnNPC.instance.SpawnNPCs(currentRoomSpawnableArea[i], npcToBeSpawned, SpawnArea[i]);
            currentRoomSpawnableArea[i].enabled = false;
        }
    }
}
