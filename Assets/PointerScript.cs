using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    private Transform targetPosition;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition.position - transform.position),
            100f * Time.deltaTime);
    }
    public void UpdateTargetPosition(Transform newPos)
    {
        targetPosition = newPos;
    }
}
