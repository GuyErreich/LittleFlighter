using System.Collections.Generic;
using UnityEngine;
using DataObjects;
using LittleFlighter.Custom.Attributes;

namespace LittleFlighter.Enemy
{
    public class EnemyController : MonoBehaviour, ISerializationCallbackReceiver
    {
        public static List<string> shootTypes;
        private bool isInRange = false;
        private Transform target;
        private Quaternion nextRotation;



        [Header("References")]
        [SerializeField] private GameObjectsStorage shootTypesRefs;

        
        [Header("Attack Settings")]

        [SerializeField, ListToPopup(typeof(EnemyController), "shootTypes")] 
        private string type;
        [SerializeField] private float damage = 5f;
        [SerializeField] private float rateOfFire = 2f;


        [Header("Movement Settings")]

        [SerializeField] private float rotationSpeed = 3f;

        #region Getters and Setters
        public bool IsInRange { get {return this.isInRange; } }
        #endregion

        private void Awake()
        {
            this.nextRotation = Random.rotation;
        }

        public void TrackMode()
        {
            this.RandomRoataion();
        }

        public void AttackMode() 
        {
            this.FollowTarget();
        }

        private void FollowTarget()
        {
            Vector3 dir = this.target.position - this.transform.position;

            this.nextRotation = Quaternion.LookRotation(dir);

            this.transform.rotation =  Quaternion.Slerp(this.transform.rotation, this.nextRotation, this.rotationSpeed * Time.deltaTime);
        }

        private void RandomRoataion()
        {            
            while (Quaternion.Angle(this.transform.rotation, this.nextRotation) == 0)
            {
                this.nextRotation = Random.rotation;
            }

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.nextRotation, this.rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider collider) 
        {
            this.target = collider.gameObject.transform;
            if (collider.CompareTag("Player"))
                this.isInRange = true;
        }

        private void OnTriggerExit(Collider collider) 
        {
            if (collider.CompareTag("Player"))
                this.isInRange = false;
        }

        public void OnBeforeSerialize()
        {
            shootTypes = this.shootTypesRefs.Keys;
        }

        public void OnAfterDeserialize() {}

    }
}
