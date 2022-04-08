using System.Collections;
using UnityEngine;

namespace LittleFlighter
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject refProjectile;
        [SerializeField, Range(0.001f, 10f)] private float rateOfFire = 0.1f;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private Transform refGunPivotL, refGunPivotR;
        // private GameObject projectile;
        private Rigidbody spaceCraftRbody;
        private bool isLeft = true, isAttack;

        // Start is called before the first frame update
        void Start()
        {
            this.spaceCraftRbody = GetComponent<Rigidbody>();
            
            StartCoroutine(this.Shoot());
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                if (isAttack)
                {
                    Transform projectile = Instantiate(this.refProjectile).transform;
                    projectile.gameObject.tag = "PlayerBullet";

                    if(this.isLeft)
                    {
                        projectile.transform.position = this.refGunPivotL.transform.position;
                        projectile.transform.rotation = this.refGunPivotL.transform.rotation;

                        Rigidbody projectileRbody = projectile.GetComponent<Rigidbody>();
                        projectileRbody.velocity = transform.forward * this.projectileSpeed + this.spaceCraftRbody.velocity;
                    }

                    if(!this.isLeft)
                    {
                        projectile.transform.position = this.refGunPivotR.transform.position;
                        projectile.transform.rotation = this.refGunPivotR.transform.rotation;

                        Rigidbody projectileRbody = projectile.GetComponent<Rigidbody>();
                        projectileRbody.velocity = transform.forward * this.projectileSpeed + this.spaceCraftRbody.velocity;
                    }

                    this.isLeft = !this.isLeft;

                    yield return new WaitForSeconds(this.rateOfFire);
                }

                yield return null;
            }
        }

        public void ReceiveInput(bool isAttack) {
            this.isAttack = isAttack;
        }
    }
}
