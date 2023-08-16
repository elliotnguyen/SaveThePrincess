using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthController : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] int damage;
    [SerializeField] int maxValue;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = maxValue;
        healthBar.value = maxValue;
    }
    public bool DamagePlayer()
    {
        Debug.Log("Damage");
        healthBar.value -= damage;
        if (healthBar.value <= 0)
        {
            Debug.Log("Death.");
            return false;
        }
            
        return true;
    }
}