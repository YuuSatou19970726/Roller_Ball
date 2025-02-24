using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    void OnCollisionEnter(Collision other)
    {
        if (target != null)
            Destroy(target.gameObject);
    }
}
