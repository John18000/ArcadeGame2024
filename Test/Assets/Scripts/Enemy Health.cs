using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    private float currentHealth;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //hit criteria
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage) //called to inflict damage
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / 100f;
    }
}
