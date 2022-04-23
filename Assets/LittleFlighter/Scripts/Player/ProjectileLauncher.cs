using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace LittleFlighter
{
    public class ProjectileLauncher : MonoBehaviour
    {
        private Rigidbody spaceCraftRbody;
        private bool isLeft = true, isAttack;
        

        [Header("References")]
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform gunPivotLeft, gunPivotRight;
        [SerializeField] private VisualEffect muzzleEffectLeft, muzzleEffectRight;
        [SerializeField] private AudioSource shootSoundLeft, shootSoundRight;


        [Header("Attack Settings")]
        [SerializeField] private int damage = 5;
        [SerializeField] private float projectileSpeed;
        [SerializeField, Range(0.001f, 10f)] private float rateOfFire = 0.1f;


        private void Awake() {
            this.spaceCraftRbody = GetComponent<Rigidbody>();

            this.muzzleEffectLeft.enabled = true;
            this.muzzleEffectRight.enabled = true;
        }

        // Start is called before the first frame update
        private void Start()
        {
            
            
            StartCoroutine(this.Shoot());
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                if (isAttack)
                {       
                    Transform projectile = Instantiate(this.projectile).transform;

                    projectile.gameObject.tag = "PlayerBullet";

                    if(this.isLeft)
                    {
                        projectile.transform.position = this.gunPivotLeft.transform.position;
                        projectile.transform.rotation = this.gunPivotLeft.transform.rotation;

                        this.muzzleEffectLeft.Play();
                        this.shootSoundLeft.Play();
                    }

                    if(!this.isLeft)
                    {
                        projectile.transform.position = this.gunPivotRight.transform.position;
                        projectile.transform.rotation = this.gunPivotRight.transform.rotation;

                        this.muzzleEffectRight.Play();
                        this.shootSoundRight.Play();
                    }

                    Rigidbody projectileRbody = projectile.GetComponent<Rigidbody>();
                    projectileRbody.velocity = transform.forward * this.projectileSpeed + this.spaceCraftRbody.velocity;

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
