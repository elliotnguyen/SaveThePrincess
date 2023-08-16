using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;


public class DisplayQuestion : MonoBehaviour
{
    [SerializeField] GameObject MCQBox;
    [SerializeField] Text MCQText;
    [SerializeField] Button[] MCQOptions;

    [SerializeField] GameObject FillInBox;
    [SerializeField] Text FillInText;
    [SerializeField] InputField inputField;
    [SerializeField] Button SubmitButton;
    [SerializeField] HealthController healthController;
    [SerializeField] StrengthController strengthController;
    [SerializeField] HeroKnight heroKnight;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowQuiz;
    public event Action OnHideQuiz;

    Question data;

    public static DisplayQuestion Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    bool isTyping;

    private void CloseTheBox()
    {
        if (data is MCQuestion)
        {
            MCQText.text = "";

            if (data is MCQuestion)
            {
                for (int i = 0; i < MCQOptions.Length; i++)
                {
                    MCQOptions[i].GetComponentInChildren<Text>().text = "";
                }
            }

            MCQBox.SetActive(false);
        } else
        {
            FillInText.text = "";
            FillInBox.SetActive(false);
        }


        OnHideQuiz?.Invoke();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            CloseTheBox();
        }
    }

    
    public IEnumerator ShowQuestion (Question data)
    {
        yield return new WaitForEndOfFrame();

        OnShowQuiz?.Invoke();

        this.data = data;

        if (data is MCQuestion)
        {
            MCQBox.SetActive(true);
        }
        else FillInBox.SetActive(true);

        StartCoroutine(TypeQuestion(data));
    }
    

    public IEnumerator TypeQuestion(Question data) 
    {
        isTyping = true;

        if (data is MCQuestion)
        {
            MCQText.text = "";
            foreach (var letter in data.GetQuestionText().ToCharArray())
            {
                MCQText.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }

            Debug.Log(MCQText.text);

            for (int i = 0; i < MCQOptions.Length; i++)
            {
                MCQOptions[i].GetComponentInChildren<Text>().text = "";
                foreach (var letter in data.GetAnswerOptions()[i].ToCharArray())
                {
                    MCQOptions[i].GetComponentInChildren<Text>().text += letter;
                    yield return new WaitForSeconds(1f / lettersPerSecond);
                }
            }

            for (int i = 0; i < MCQOptions.Length; i++)
            {
                if (MCQOptions[i].GetComponentInChildren<Text>().text.Equals(data.GetCorrectAnswer()))
                {
                    MCQOptions[i].onClick.RemoveAllListeners();
                    MCQOptions[i].onClick.AddListener(delegate { Right(); });
                }
                else
                {
                    MCQOptions[i].onClick.RemoveAllListeners();
                    MCQOptions[i].onClick.AddListener(delegate { Wrong(); });
                }
            }
        } else
        {
            FillInText.text="";
            inputField.text="";
            foreach (var letter in data.GetQuestionText().ToCharArray())
            {
                FillInText.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }

            SubmitButton.onClick.RemoveAllListeners();
            SubmitButton.onClick.AddListener(delegate { Check(); });
        }
       
        isTyping = false;
    }

    private void Wrong()
    {
        Debug.Log("You chose the wrong answer!");
        CloseTheBox();
        if(healthController.DamagePlayer())
            heroKnight.Hurt();
        else
            heroKnight.Death();
        
    }

    private void Right()
    {
        Debug.Log("You chose the correct answer!");
        CloseTheBox();
        strengthController.StrongerPlayer();
        
    }

    public void Check()
    {
        string ans = inputField.text;
        Debug.Log(ans+":"+ data.GetCorrectAnswer());
        if (ans.ToLower().Equals(data.GetCorrectAnswer().ToLower()))
        {
            Debug.Log("You entered the correct answer!");
            CloseTheBox();
            strengthController.StrongerPlayer();
            
        } else
        {
            CloseTheBox();
            Debug.Log("You entered the wrong answer!");
            if(healthController.DamagePlayer())
                heroKnight.Hurt();
            else
                heroKnight.Death();
        
            
        }
    }
}
