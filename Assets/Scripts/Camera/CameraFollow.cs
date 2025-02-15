using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTranform;
    [SerializeField] private float speed;
    [SerializeField] private float offset;

    void Update()
    {
        Vector3 target = new Vector3(targetTranform.position.x, targetTranform.position.y, targetTranform.position.y - offset);
        this.transform.position = Vector3.Lerp(this.transform.position, target, speed *  Time.deltaTime);
    }
}
