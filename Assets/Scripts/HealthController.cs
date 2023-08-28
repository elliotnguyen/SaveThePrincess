using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    private int numsOfWrongAnswer = 0;
    private int numsOfCorrectAnswer = 0;
    private int maximumWrongAnswer = 0;
    private int numsOfQuestions = 0;

    private HeroKnight player;

    //[SerializeField] GameObject congratulation;
    //[SerializeField] GameObject gameover;
    
    // Start is called before the first frame update
    void Start()
    {
        //congratulation = GameObject.FindGameObjectWithTag("congratulation");
        //gameover = GameObject.FindGameObjectWithTag("gameover");

        player = GameObject.FindGameObjectWithTag("player").GetComponent<HeroKnight>();
        numsOfQuestions = QuestionLoader.Instance.QuizList.Count; 
        maximumWrongAnswer = numsOfQuestions / 5;

        healthBar.maxValue = maximumWrongAnswer != 0? maximumWrongAnswer : 1;
        healthBar.value = healthBar.maxValue;
    }

    void Update()
    {
        if (numsOfQuestions == 0)
        {
            numsOfQuestions = QuestionLoader.Instance.QuizList.Count;
            maximumWrongAnswer = numsOfQuestions / 5;
            healthBar.maxValue = maximumWrongAnswer!= 0? maximumWrongAnswer : 1;
            healthBar.value = maximumWrongAnswer;
        }
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("player").GetComponent<HeroKnight>();
            Debug.Log("Player");
        }
    }
    
    public void Boost()
    {
        player.m_jumpForce += 5f;
        player.Attack();

        numsOfCorrectAnswer++;

        Debug.Log(numsOfCorrectAnswer);
        Debug.Log(numsOfQuestions);

        if (numsOfCorrectAnswer == numsOfQuestions)
        {
            Congratulation.Instance.WinnerBox.SetActive(true);
        }
    }

    public void Damage()
    {
        numsOfWrongAnswer++;
        healthBar.value -= 1;

        if (numsOfWrongAnswer == maximumWrongAnswer)
        {
            player.Death();
            //gameover.Display();
            GameOver.Instance.Over.SetActive(true);
            //return true;
        }
        
        player.Hurt();

        //return false;
    }
}
