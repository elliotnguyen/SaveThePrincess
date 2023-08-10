using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestion : MonoBehaviour
{
    [SerializeField] GameObject quizBox;
    [SerializeField] Text quiz;
    [SerializeField] Button[] options;

    [SerializeField] int lettersPerSecond;

    public event Action OnShowQuiz;
    public event Action OnHideQuiz;

    public static DisplayQuestion Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    bool isTyping;

    private void CloseTheBox()
    {
        quiz.text = "";
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponentInChildren<Text>().text = "";
        }
        quizBox.SetActive(false);
        OnHideQuiz?.Invoke();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            CloseTheBox();
        }

        //if ()
    }

    public IEnumerator ShowQuestion(QuestionData data)
    {
        yield return new WaitForEndOfFrame();

        OnShowQuiz?.Invoke();

        quizBox.SetActive(true);

        for (int i = 0; i < options.Length; i++)
        {
            if (i == data.correctAnswerIndex)
            {
                options[i].onClick.AddListener(delegate { Right(); });
            } else
            {
                options[i].onClick.AddListener(delegate { Wrong(); });
            }
        }

        StartCoroutine(TypeQuestion(data));
    }

    public IEnumerator TypeQuestion(QuestionData data)
    {
        isTyping = true;
        
        foreach (var letter in data.questionText.ToCharArray())
        {
            quiz.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        Debug.Log(quiz.text);

        for (int i = 0; i < options.Length; i++)
        {
            foreach (var letter in data.answerOptions[i].ToCharArray())
            {
                options[i].GetComponentInChildren<Text>().text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }
        }
        isTyping = false;
    }

    private void Wrong()
    {
        Debug.Log("You are wrong!");
        //CloseTheBox();
    }

    private void Right()
    {
        Debug.Log("You are right!");
        CloseTheBox();
    }
}
