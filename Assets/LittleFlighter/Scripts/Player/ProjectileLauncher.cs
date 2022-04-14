using System.Collections;
using UnityEngine;
using LittleFlighter.System;

using LittleFlighter.Bullets;

namespace LittleFlighter
{
    public class ProjectileLauncher : MonoBehaviour
    {
        private Rigidbody spaceCraftRbody;
        private bool isLeft = true, isAttack;
        

        [Header("References")]
        [SerializeField] private GameObject refProjectile;
        [SerializeField] private Transform refGunPivotL, refGunPivotR;


        [Header("Attack Settings")]
        [SerializeField] private int damage = 5;
        [SerializeField] private float projectileSpeed;
        [SerializeField, Range(0.001f, 10f)] private float rateOfFire = 0.1f;


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
                    }

                    if(!this.isLeft)
                    {
                        projectile.transform.position = this.refGunPivotR.transform.position;
                        projectile.transform.rotation = this.refGunPivotR.transform.rotation;
                    }

                    Rigidbody projectileRbody = projectile.GetComponent<Rigidbody>();
                    projectileRbody.velocity = transform.forward * this.projectileSpeed + new Vector3 (0, 0, this.spaceCraftRbody.velocity.z);

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
