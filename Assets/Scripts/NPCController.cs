using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] int quizIndex;
    
    public void SetQuizIndex(int value)
    {
        quizIndex = value;
    }
    
    //public async Task Interact()
    public void Interact()
    {
        Question tmp = QuestionLoader.Instance.QuizList[quizIndex];
        StartCoroutine(DisplayQuestion.Instance.ShowQuestion(tmp, gameObject));
    }
}
