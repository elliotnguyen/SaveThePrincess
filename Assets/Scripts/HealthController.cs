using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    //[SerializeField] Slider healthBar;

    private int numsOfWrongAnswer = 0;
    private int numsOfCorrectAnswer = 0;

    private HeroKnight player;

    // Start is called before the first frame update
    void Start()
    {
        //numsOfQuestions = QuestionLoader.Instance.QuizList.Count / 10;
        //maximumWrongAnswer = numsOfQuestions / 5;
        player = gameObject.GetComponent<HeroKnight>();
        
        //healthBar.maxValue = numsOfQuestions;
        //healthBar.value = numsOfQuestions;
    }

    public void Boost()
    {
        player.m_jumpForce += 5f;
        player.Attack();

        numsOfCorrectAnswer++;

        Debug.Log(numsOfCorrectAnswer);
        Debug.Log(QuestionLoader.Instance.QuizList.Count);

        if (numsOfCorrectAnswer == QuestionLoader.Instance.QuizList.Count / 20)
        {
            Debug.Log("You win!");
        }
    }

    public bool Damage()
    {
        numsOfWrongAnswer++;
        //healthBar.value -= 1;

        /*
        if (numsOfWrongAnswer == maximumWrongAnswer)
        {
            player.Death();
            return false;
        }
        */
        player.Hurt();

        return true;
    }
}
