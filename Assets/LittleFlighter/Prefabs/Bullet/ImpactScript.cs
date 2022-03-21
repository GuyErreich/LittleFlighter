using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class ImpactScript : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject effect;

    private void OnCollisionStay(Collision collision) {
        if(!collision.gameObject.CompareTag("Player"))
        {
            var position = collision.contacts[0].point;
            var effect = Instantiate(this.effect, position, new Quaternion(0,0,0,0));
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider collision) {
        if(!collision.CompareTag("Player"))
        {
            var position = collision.ClosestPointOnBounds(this.transform.position);
            position += -this.transform.forward;
            var effect = Instantiate(this.effect, position, new Quaternion(0,0,0,0));
            Destroy(this.gameObject);
        }
    }
}
