using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        int selectedCharacter = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        CharacterSelection.instance.CharIndex = selectedCharacter;

        SceneManager.LoadScene("GamePlay");

    }
}
