using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatStats : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI textMeshPro;

    public int health;
    public int damage;

    public float attackCooldown = 1f;

    private void Awake()
    {
        healthSlider.maxValue = health;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (attackCooldown == 1)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public IEnumerator GetDamage(int dmg)
    {
        if (attackCooldown <= 0)
        {
            attackCooldown = 1;
            health -= dmg;
            UpdateHealthBar();
        }
        if (health <= 0)
        {
            Die();
        }
        yield return new WaitForSeconds(1f);
    }

    public void UpdateHealthBar()
    {
        healthSlider.value = health;
        textMeshPro.text = health.ToString();
    }

    public void Die()
    {
        Debug.Log("i has dieded!");
        Destroy(gameObject);
    }
}
