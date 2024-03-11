using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Transform mesh;

    private void Awake()
    {
        mesh = transform.GetChild(0);
    }

    public void Move(Vector3 direction)
    {
        gameObject.transform.position += direction;
    }

    public void LookAt(Vector3 target)
    {
        mesh.LookAt(target);
    }
}
