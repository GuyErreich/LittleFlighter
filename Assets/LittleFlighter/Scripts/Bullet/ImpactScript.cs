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
                _ => throw new Exception("Wrong bullet tag")
            };
        }

        private bool EnemyHit(Collider collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                SpawnImpactOnCollisionPoint(collider, 8f);

                Destroy(this.gameObject);

                return true;
            }

            return false;
        }

        private bool PlayerHit(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                SpawnImpactOnCollisionPoint(collider);

                Destroy(this.gameObject);

                return true;
            }

            return false;
        }

        private void SpawnImpactOnCollisionPoint(Collider collider, float offset = 0f) 
        {
            var position = collider.ClosestPointOnBounds(this.transform.position);

            position += (position - collider.transform.position).normalized * offset;
            Instantiate(this.effect, position, new Quaternion(0f, 0f, 0f, 0f));
        }
    }
}
