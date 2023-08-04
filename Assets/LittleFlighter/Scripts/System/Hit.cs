using UnityEngine;
using UnityEngine.Events;
using LittleFlighter.Bullets;

namespace LittleFlighter.System
{
    public class Hit : MonoBehaviour
    {
        public UnityEvent<int, Collider> OnBulletHit;

        private void OnTriggerEnter(Collider collider) {
            if (collider.GetComponent<Bullet>() != null)
            {
                this.OnBulletHit.Invoke(collider.GetComponent<Bullet>().Damage, collider);
            }
        }
    }
}