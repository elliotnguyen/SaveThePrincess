using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionLoader : MonoBehaviour
{
    public List<Question> QuizList;
    [SerializeField] GameObject NPC;
    public static QuestionLoader Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this; 
    }

    public async void Start()
    {
        List<Vector3> NPCPositions = new List<Vector3>(); 
        NPCPositions.Add(new Vector3(-290f, 50.4f, 0));
        NPCPositions.Add(new Vector3(-242.5f, 44f, 0));
        NPCPositions.Add(new Vector3(-215.9f, 55.7f, 0));
        NPCPositions.Add(new Vector3(-192f, 40.4f, 0));
        NPCPositions.Add(new Vector3(-167.9f,57.8f,0));
        NPCPositions.Add(new Vector3(-163.1f, 77.5f, 0));

        NPCPositions.Add(new Vector3(-135.3f, 95.1f, 0));
        NPCPositions.Add(new Vector3(-196.6f, 75.4f, 0));
        NPCPositions.Add(new Vector3(-229.7f, 79.4f, 0));
        NPCPositions.Add(new Vector3(-216.1f, 97.4f, 0));
        NPCPositions.Add(new Vector3(-244.3f,93.4f,0));
        NPCPositions.Add(new Vector3(-266.1f, 92.6f, 0));

        var url = "https://mocki.io/v1/46f4b3a0-690d-4308-b897-1bef1df4c7e3";

        var httpClient = new APIController();
        QuizList = await httpClient.Get<List<Question>>(url);
        for (int i = 0; i < QuizList.Count; i++)
        {
            int index = Random.Range(0, NPCPositions.Count);
            GameObject npc = Instantiate(NPC, NPCPositions[index], Quaternion.identity) as GameObject;
            NPCPositions.RemoveAt(index);
            Debug.Log(QuizList[i].GetCorrectAnswer());
        }
    }
}
