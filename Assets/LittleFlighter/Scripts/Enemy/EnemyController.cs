using System;
using System.Collections.Generic;
using UnityEngine;
using LittleFlighter.DataObjects;
using LittleFlighter.Custom.Attributes;
using LittleFlighter.System;

namespace LittleFlighter.Enemy
{
    public class EnemyController : MonoBehaviour, ISerializationCallbackReceiver
    {
        public static List<string> shootTypes;


        [Header("References")]
        [SerializeField] private GameObjectsStorage shootTypesRefs;
        [SerializeField] private Transform refEnemyModel;
        [SerializeField] private Transform refGunPivot;


        [Header("Defense Settings")]
        [SerializeField] private int health = 100;


        [Header("Attack Settings")]
        [SerializeField, ListToPopup(typeof(EnemyController), "shootTypes")] private string type;
        [SerializeField] private float projectileSpeed = 2f;
        [SerializeField] private float rateOfFire = 2f;


        [Header("Movement Settings")]
        [SerializeField] private float rotationSpeed = 3f;


        private bool isInRange = false;
        private Transform target;
        private Quaternion nextRotation;
        private float cooldownDuration = 0f;
        private Health healthComponent;

        public event Action<float> OnHealthChanged;


        #region Getters and Setters
        public bool IsInRange { get { return this.isInRange; } }

        public int CurrentHealth { get; private set; }

        #endregion



        private void OnEnable()
        {
            this.CurrentHealth = this.health;
        }

        public void TrackMode()
        {
            this.RandomRoataion();
        }

        public void AttackMode()
        {
            this.FollowTarget();

            this.Shoot();
        }

        private void Shoot()
        {
            if (this.cooldownDuration <= 0f)
            {
                int index = this.shootTypesRefs.Keys.IndexOf(this.type);

                var projectile = Instantiate(this.shootTypesRefs.Values[index]).transform;

                projectile.gameObject.tag = "EnemyBullet";

                projectile.transform.position = this.refGunPivot.transform.position;
                projectile.transform.rotation = this.refGunPivot.transform.rotation;

                Rigidbody projectileRbody = projectile.GetComponent<Rigidbody>();
                projectileRbody.velocity = this.refEnemyModel.forward * this.projectileSpeed;

                this.cooldownDuration = rateOfFire;
            }

            this.cooldownDuration -= Time.deltaTime;
        }

        private void FollowTarget()
        {
            Vector3 dir = this.target.position - this.refEnemyModel.position;

            this.nextRotation = Quaternion.LookRotation(dir);

            this.refEnemyModel.rotation = Quaternion.Slerp(this.refEnemyModel.rotation, this.nextRotation, this.rotationSpeed * Time.deltaTime);
        }

        private void RandomRoataion()
        {
            while (Quaternion.Angle(this.refEnemyModel.rotation, this.nextRotation) == 0)
            {
                this.nextRotation = UnityEngine.Random.rotation;
            }

            this.refEnemyModel.rotation = Quaternion.Slerp(this.refEnemyModel.rotation, this.nextRotation, this.rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                this.target = collider.gameObject.transform;
                this.isInRange = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.CompareTag("Player"))
                this.isInRange = false;
        }


        #region Events Handlers

        public void HandleHit(int amount, Collider collider)
        {
            if (collider.CompareTag("PlayerBullet"))
                this.ModifyHealth(-amount);
        }

        #endregion Events Handlers


        #region Health Control

        private void ModifyHealth(int amount)
        {
            this.CurrentHealth += amount;

            float currentHealthPct = (float)this.CurrentHealth / (float)this.health;

            this.OnHealthChanged?.Invoke(currentHealthPct);
        }

        #endregion Health Control

        #region Editor Control

        public void OnBeforeSerialize()
        {
            shootTypes = this.shootTypesRefs.Keys;
        }


        public void OnAfterDeserialize() { }

        #endregion
    }
}
