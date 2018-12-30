using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLightController : MonoBehaviour
{
    [SerializeField] float rate = 1;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at a specified rate (degree per second)
        transform.Rotate(Vector3.right, Time.deltaTime * rate);
    }
}