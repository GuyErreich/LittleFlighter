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
            Debug.Log(collision.gameObject.tag);
            if(!collision.CompareTag("Player") && !collision.CompareTag("NoCollision"))
            {
                var position = collision.ClosestPointOnBounds(this.transform.position);
                position += -this.transform.forward;
                var effect = Instantiate(this.effect, position, new Quaternion(0,0,0,0));
                Destroy(this.gameObject);
            }
        }
    }
}
