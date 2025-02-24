using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;

namespace RollerBall
{
    public class DestroyableObject : MonoBehaviour
    {
        public float forceRequired = 10.0f;
        [SerializeField]
        private ParticleSystem burstPrefab;

        void OnCollisionEnter(Collision other)
        {
            if (other.impulse.magnitude > forceRequired)
            {
                Instantiate(this.burstPrefab, other.contacts[0].point, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
