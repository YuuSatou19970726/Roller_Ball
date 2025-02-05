using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            LevelManager.Instance.EventVictory();
        }
    }
}
