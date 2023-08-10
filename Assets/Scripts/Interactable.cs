using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface Interactable
{
    //public async void Interact();
    public Task Interact();
}
