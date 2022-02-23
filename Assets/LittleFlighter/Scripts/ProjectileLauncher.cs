using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject refProjectile;
    [SerializeField, Range(0.001f, float.PositiveInfinity)] private float rateOfFire = 0.1f;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform refGunPivotL, refGunPivotR;
    private GameObject projectile;
    private Rigidbody projectileRbody, spaceCraftRbody;
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
                if(this.isLeft)
                {
                    this.projectile = Instantiate(this.refProjectile);
                    this.projectile.transform.position = this.refGunPivotL.transform.position;
                    this.projectile.transform.rotation = this.refGunPivotL.transform.rotation;

                    this.projectileRbody = this.projectile.GetComponent<Rigidbody>();
                    this.projectileRbody.velocity = transform.forward * this.projectileSpeed + this.spaceCraftRbody.velocity;
                }

                if(!this.isLeft)
                {
                    this.projectile = Instantiate(this.refProjectile);
                    this.projectile.transform.position = this.refGunPivotR.transform.position;
                    this.projectile.transform.rotation = this.refGunPivotR.transform.rotation;

                    this.projectileRbody = this.projectile.GetComponent<Rigidbody>();
                    this.projectileRbody.velocity = transform.forward * this.projectileSpeed + this.spaceCraftRbody.velocity;
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
