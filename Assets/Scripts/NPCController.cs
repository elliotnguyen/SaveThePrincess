using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    //[SerializeField] Dialog dialog;
    //[SerializeField] QuestionData quiz;

    public async Task Interact()
    {
        var url = "https://mocki.io/v1/46f4b3a0-690d-4308-b897-1bef1df4c7e3";

        var httpClient = new APIController();
        //var questions = await httpClient.Get<QuestionData[]>(url);
        List<Question> questions = await httpClient.Get<List<Question>>(url);
        //Debug.Log(questions[0].correctAnswerIndex);
        //questions[0].SetQuestion("asdbkasd");
        Debug.Log(questions.Count);
        //StartCoroutine(DisplayQuestion.Instance.ShowQuestion(questions[0]));
        //QuestionData[] questions = APIController.Instance.questions;
        //dialog.AddLine(questions[0].questionText);
        //StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
