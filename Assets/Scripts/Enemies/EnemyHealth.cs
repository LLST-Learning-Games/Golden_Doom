using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth;
    [SerializeField] private int maxEnemyHealth = 3;
    [SerializeField] private bool isDead = false;
    [SerializeField] private EnemyHandleHit enemyHandleHit;

    private void Awake()
    {
        if (!enemyHandleHit)
            enemyHandleHit = GetComponent<EnemyHandleHit>();
        InitEnemy();
    }

    private void OnEnable()
    {
        enemyHandleHit.onEnemyIsHIt += GetHit;
    }

    public void InitEnemy()
    {
        enemyHealth = maxEnemyHealth;
        isDead = false;
    }

    private void GetHit()
    {
        enemyHealth--;
        if (enemyHealth == 0)
            this.gameObject.SetActive(false);

    }

    public bool CheckDead()
    {
        return isDead;
    }
    
}
