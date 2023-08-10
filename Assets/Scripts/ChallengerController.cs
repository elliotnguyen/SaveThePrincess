using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ChallengerController : MonoBehaviour, Interactable
{
    public Task Interact()
    {
        return new Task();
        //Debug.Log("We will have a battle!");
    }
}
