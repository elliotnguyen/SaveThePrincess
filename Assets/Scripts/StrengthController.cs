using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthController : MonoBehaviour
{
    [SerializeField] Slider strengthBar;
    [SerializeField] int upStrength;
    [SerializeField] int maxValue;
    // Start is called before the first frame update
    void Start()
    {
        strengthBar.maxValue = maxValue;
        strengthBar.value = 0;
    }

    // Update is called once per frame
    public void StrongerPlayer()
    {
        Debug.Log("Stronger");
        strengthBar.value += upStrength;
    }
}
