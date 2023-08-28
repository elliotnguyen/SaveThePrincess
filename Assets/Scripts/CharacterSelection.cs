using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;

    [SerializeField] public string APILink;
    public void SetLink(string value)
    {
        APILink = value;
    }
    public string GetLink()
    {
        return APILink;
    }

    [SerializeField]
    private GameObject[] characters;

    private int _charIndex;
    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Vector3 position = new Vector3(-292, 53, 0);
        if (scene.name == "GamePlay")
        {
            Instantiate(characters[CharIndex], position, Quaternion.identity);
            QuestionLoader questionLoader = GameObject.FindWithTag("questionLoader").GetComponent<QuestionLoader>();
            questionLoader.SetLink(APILink);
        }
    }
}
