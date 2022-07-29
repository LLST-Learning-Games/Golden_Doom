using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private Rigidbody myRb = null;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 0.5f;
    //[SerializeField] private float recoveryTime = 0.1f;
    [SerializeField] private EnemyHandleHit enemyHandleHit;
    private float timeSincehit = -0.1f;
    private Vector3 targetDirection;
    private bool isRecovering = false;



    private void Awake()
    {
        if (!myRb)
            myRb = GetComponent<Rigidbody>();
        if (!enemyHandleHit)
            enemyHandleHit = GetComponent<EnemyHandleHit>();
    }

    private void Start()
    {
        if (!target)
            target = FindObjectOfType<PlayerAttack>().gameObject.transform;
    }

    private void OnEnable()
    {
        enemyHandleHit.onEnemyIsHIt += OnRecoveryStart; //+= StartRecoveryTime;
        enemyHandleHit.onEnemyHitCooldownElapsed += OnCooldownElapsed;
    }
    
    void Update()
    {
        targetDirection = target.position - transform.position;
        targetDirection.Normalize();
    }

    private void FixedUpdate()
    {
        if (isRecovering)
        {
            myRb.velocity = new Vector3
                (targetDirection.x, myRb.velocity.y, targetDirection.z);
        }
    }

    private void OnCooldownElapsed()
    {
        isRecovering = false;
    }

    private void OnRecoveryStart()
    {
        isRecovering = true;
    }

    //private void StartRecoveryTime()
    //{
    //    timeSincehit = recoveryTime;
    //}
}
