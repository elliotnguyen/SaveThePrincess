using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Sprite[] characterSprite;
    public SpriteRenderer artWorkSprite;

    private int selectedOption = 0;

    private void Start()
    {
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterSprite.Length)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterSprite.Length - 1;
        }

        UpdateCharacter(selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        artWorkSprite.sprite = characterSprite[selectedOption];
    }

    public void PlayGame()
    {
        //int selectedCharacter = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        //CharacterSelection.instance.CharIndex = selectedCharacter;
        CharacterSelection.instance.CharIndex = selectedOption;

        SceneManager.LoadScene("GamePlay");

    }
}
