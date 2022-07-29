using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleHit : MonoBehaviour
{
    [SerializeField] float hitForce = 500f;
    [SerializeField] float coolDownTime = 1f;
    [SerializeField] private Rigidbody myRb = null;
    [SerializeField] private bool justHit = false;

    public delegate void EnemyIsHit();
    public event EnemyIsHit onEnemyIsHIt;
    public delegate void EnemyHitCooldownElapsed();
    public event EnemyHitCooldownElapsed onEnemyHitCooldownElapsed;


    private void Awake()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Collision!");

        if (other.gameObject.CompareTag("Puncher") 
            && other.gameObject.GetComponentInParent<PlayerAttack>().IsPunching())
        {
            //Debug.Log("Ouch!");
            Vector3 hitDirection = new Vector3(
                transform.position.x - other.transform.position.x,
                0.0f,
                transform.position.z - other.transform.position.z);

            hitDirection.Normalize();

            Debug.Log(hitDirection);

           myRb.AddForce(hitForce * hitDirection);
            if (!justHit)
            {
                justHit = true;
                StartCoroutine(nameof(CoolDown));
                onEnemyIsHIt?.Invoke();
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);
        justHit = false;
        onEnemyHitCooldownElapsed?.Invoke();
    }
}
