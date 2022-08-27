using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

namespace LittleFlighter.Bullets
{
    [RequireComponent(typeof(VisualEffect))]
    [RequireComponent(typeof(Collider))]
    public class ImpactScript : MonoBehaviour
    {

        [Header("References")]
        [SerializeField] private GameObject effect;

        private void OnTriggerEnter(Collider collider)
        {
            bool succeeded = this.gameObject.tag switch
            {
                "PlayerBullet" => EnemyHit(collider),
                "EnemyBullet" => PlayerHit(collider),
                _ => throw new Exception("Unexpected error")
            };
        }

        private bool EnemyHit(Collider collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                var position = collider.ClosestPointOnBounds(this.transform.position);
                position += -this.transform.forward;
                var effect = Instantiate(this.effect, position, new Quaternion(0f, 0f, 0f, 0f));

                Destroy(this.gameObject);

                return true;
            }

            return false;
        }

        private bool PlayerHit(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                var position = collider.ClosestPointOnBounds(this.transform.position);
                position += -this.transform.forward;
                var effect = Instantiate(this.effect, position, new Quaternion(0f, 0f, 0f, 0f));

                Destroy(this.gameObject);

                return true;
            }

            return false;
        }
    }
}
