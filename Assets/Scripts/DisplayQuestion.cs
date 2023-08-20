using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayQuestion : MonoBehaviour
{
    [SerializeField] GameObject MCQBox;
    [SerializeField] Text MCQText;
    [SerializeField] Button[] MCQOptions;

    [SerializeField] GameObject FillInBox;
    [SerializeField] Text FillInText;
    [SerializeField] InputField inputField;
    [SerializeField] Button SubmitButton;

    [SerializeField] int lettersPerSecond;

    public event Action OnShowQuiz;
    public event Action OnHideQuiz;

    Question data;

    [SerializeField] HealthController player;
    public GameObject npc;

    public static DisplayQuestion Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("player").GetComponent<HealthController>();
    }

    bool isTyping;

    private void CloseTheBox()
    {
        if (data is MCQuestion)
        {
            MCQText.text = "";

            for (int i = 0; i < MCQOptions.Length; i++)
            {
                MCQOptions[i].GetComponentInChildren<Text>().text = "";
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
            //Debug.Log("HandleUpdate");
            CloseTheBox();
        }
    }

    
    public IEnumerator ShowQuestion (Question data, GameObject npc)
    {
        this.npc = npc;

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

            for (int i = 0; i < MCQOptions.Length; i++)
            {
                foreach (var letter in data.GetAnswerOptions()[i].ToCharArray())
                {
                    MCQOptions[i].GetComponentInChildren<Text>().text += letter;
                    yield return new WaitForSeconds(1f / lettersPerSecond);
                }
            }

            for (int i = 0; i < MCQOptions.Length; i++)
            {
                MCQOptions[i].onClick.RemoveAllListeners();

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
            FillInText.text = "";
            inputField.text = "";

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
        CloseTheBox();
        player.Damage();
        /*
        if(!player.Damage())
        {
            SceneManager.LoadScene("MainMenu");
        }
        */
    }

    private void Right()
    {
        CloseTheBox();
        Destroy(this.npc);
        player.Boost();
        
    }

    public void Check()
    {
        string ans = inputField.text;
        Debug.Log(ans+":"+ data.GetCorrectAnswer());
        if (ans.ToLower().Equals(data.GetCorrectAnswer().ToLower()))
        {
            CloseTheBox();
            Debug.Log("You entered the correct answer!");
            player.Boost();
        } else
        {
            CloseTheBox();
            Debug.Log("You entered the wrong answer!");
            /*
            if(!player.Damage())
            {
                SceneManager.LoadScene("MainMenu");
            }
            */
        }
    }
}
