using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform punchTransform = null;
    [SerializeField] private bool isPunching = false;
    [SerializeField] private float punchTime = 0.1f;
    [SerializeField] private float resetTime = 0.05f;

    private Vector3 punchOffset = new Vector3(0.65f, 0f, 0f);
    private Vector3 restOffset = new Vector3(0.2f, 0.4f, 0f);
    private Vector3 startPos;

    private void Update()
    {
        //if (!isPunching && Keyboard.current.rightCtrlKey.wasPressedThisFrame)
        //{
        //    isPunching = true;
        //    StartCoroutine(Punch());
        //}
        
    }

    private void OnPunch()
    {
        if (!isPunching)
        {
            isPunching = true;
            StartCoroutine(Punch());
        }
    }

    public bool IsPunching()
    {
        return isPunching;
    }

    public IEnumerator Punch()
    {
        float elapsedTime = 0f;
        startPos = punchTransform.localPosition;
        while (elapsedTime < punchTime)
        {
            punchTransform.localPosition = Vector3.Lerp(startPos, startPos + punchOffset, elapsedTime / punchTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        while (elapsedTime < resetTime)
        {
            punchTransform.localPosition = Vector3.Lerp(startPos + punchOffset, startPos, elapsedTime / punchTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        punchTransform.localPosition = startPos;
        isPunching = false;
        yield return null;
    }
}
