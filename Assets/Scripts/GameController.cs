using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, SolveQuiz, Battle}

public class GameController : MonoBehaviour
{
    //[SerializeField] PlayerController playerController;
    [SerializeField] HeroKnight player;

    GameState state;

    private int strength = 0;
    private int tries = 3;

    private void Start()
    {
        player = GameObject.FindWithTag("player").GetComponent<HeroKnight>();

        DisplayQuestion.Instance.OnShowQuiz += () =>
        {
            state = GameState.SolveQuiz;
        };
        DisplayQuestion.Instance.OnHideQuiz += () =>
        {
            if (state == GameState.SolveQuiz)
                state = GameState.FreeRoam;
        };
    }

    private void Update()
    {
       if (state == GameState.FreeRoam)
        {
            player.HandleUpdate();
            //playerController.HandleUpdate();
        } else if (state == GameState.SolveQuiz)
        {
            DisplayQuestion.Instance.HandleUpdate();
        } else if (state == GameState.Battle)
        {

        }
    }

    public void addTry(int value)
    {
        tries += value;
        if (tries <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    public void addStrength(int value)
    {
        strength += value;
        if (strength <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

}
