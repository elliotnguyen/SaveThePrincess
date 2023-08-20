using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject Over;
    [SerializeField] Button exit;

    public static GameOver Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        exit.onClick.AddListener(ExitScene);
    }

    private void ExitScene()
    {
        SceneManager.LoadScene("Login");
    }
}
