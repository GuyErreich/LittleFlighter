using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace LittleFlighter
{
    [RequireComponent(typeof(VisualEffect))]
    public class ImpactScript : MonoBehaviour
    {
        [Header ("References")]
        [SerializeField] private GameObject effect;
        
        private void OnTriggerEnter(Collider collision) {
            bool succeeded = this.gameObject.tag switch
            {
                "PlayerBullet" => EnemyHit(collision),
                "EnemyBullet" => PlayerHit(collision),
                _ => throw new Exception("Unexpected error")
            };
        }

        private bool EnemyHit(Collider collision)
        {
            if(collision.CompareTag("Enemy"))
            {
                var position = collision.ClosestPointOnBounds(this.transform.position);
                position += -this.transform.forward;
                var effect = Instantiate(this.effect, position, new Quaternion(0,0,0,0));
                Destroy(this.gameObject);

                return true;
            }

            return false;
        }

        private bool PlayerHit(Collider collision)
        {
            if(collision.CompareTag("Player"))
            {                
                var position = collision.ClosestPointOnBounds(this.transform.position);
                position += -this.transform.forward;
                var effect = Instantiate(this.effect, position, new Quaternion(0,0,0,0));
                Destroy(this.gameObject);

                return true;
            }

            return false;
        }
    }
}
