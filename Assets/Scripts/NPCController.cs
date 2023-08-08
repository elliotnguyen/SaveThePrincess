using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        //QuestionData[] questions = APIController.Instance.questions;
        //dialog.AddLine(questions[0].questionText);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
