using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleFlighter.UI;
using LittleFlighter.UI.HealthBars;
using LittleFlighter.Enemy;

namespace LittleFlighter.System
{
    public class EnemyManager : MonoBehaviour
    {
        public enum ObjectType
        {
            enemy = 0,
            pointer = 1,
            healthBar = 2
        }

        [Header("References")]
        [SerializeField] private GameObject enemy;
        [SerializeField] private Transform enemiesContainer;
        [SerializeField] private GameObject pointer;
        [SerializeField] private Transform pointersCanvasContianer;
        [SerializeField] private GameObject dynamicHealthBar;
        [SerializeField] private Transform healthBarCanvasContianer;


        [Header("Spawn Settings")]
        [SerializeField] private int maxEnemies = 10;
        [SerializeField] private float spawnZoneRange = 100f;
        [SerializeField, Range(2, 10)] private int spawnSpeed = 5;

        
        // [Header("Enemy Settings")]
        // [SerializeField] private int maxHealth = 100;

        private Queue<GameObject> enemiesPool = new Queue<GameObject>();
        private Queue<GameObject> pointersPool = new Queue<GameObject>();
        private Queue<GameObject> healthBarsPool = new Queue<GameObject>();

        private List<GameObject> currentEnemies = new List<GameObject>();
        private List<GameObject> currentPointers = new List<GameObject>();
        private List<GameObject> currenthealthBars = new List<GameObject>();

        private int enemiesCurrentAmount = 0;  

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(this.SpawnRoutine());
        }

        private void Update()
        {

        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                float posX = Random.Range(-spawnZoneRange, spawnZoneRange);
                float posY = Random.Range(-spawnZoneRange, spawnZoneRange);
                float posZ = Random.Range(-spawnZoneRange, spawnZoneRange);

                Vector3 spawnPosition =  new Vector3(posX, posY, posZ);

                if(this.enemiesCurrentAmount < this.maxEnemies)
                {
                    var enemy = Instantiate(this.enemy, spawnPosition, Quaternion.identity, this.enemiesContainer);
                    enemy.SetActive(false);
                    this.enemiesPool.Enqueue(enemy);

                    var pointer = Instantiate(this.pointer, this.pointersCanvasContianer);
                    pointer.SetActive(false);
                    pointer.GetComponent<EnemyPointer>().Target = enemy;
                    this.pointersPool.Enqueue(pointer);

                    var hleahtbar = Instantiate(this.dynamicHealthBar, this.healthBarCanvasContianer);
                    hleahtbar.SetActive(false);
                    hleahtbar.GetComponent<DynamicHealthBar>().Target = enemy;
                    this.healthBarsPool.Enqueue(hleahtbar);

                    this.enemiesCurrentAmount++;
                }

                if (this.enemiesPool.Count > 0)
                {
                    var enemy = this.enemiesPool.Dequeue();
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);

                    currentEnemies.Add(enemy);

                    if (this.pointersPool.Count > 0)
                    {
                        var pointer = this.pointersPool.Dequeue();
                        pointer.SetActive(true);

                        currentPointers.Add(pointer);
                    }

                    if (this.healthBarsPool.Count > 0)
                    {
                        var healthBar = this.healthBarsPool.Dequeue();

                        enemy.GetComponent<EnemyController>().OnHealthChanged += healthBar.GetComponent<DynamicHealthBar>().HandleHealthChanged;

                        healthBar.SetActive(true);

                        currenthealthBars.Add(healthBar);
                    }
                }
                

                yield return new WaitForSeconds(spawnSpeed);
            }
        }

        #region Events Handlers

        public void HandleDeath(GameObject obj, ObjectType type)
        {
            this.RemoveFromQueue(obj, type);
        }

        // public void HandlePointerDestroy(GameObject pointer)
        // {
        //     pointer.SetActive(false);
            
        //     this.enemiesPool.Enqueue(pointer);
        // }

        // public void HandleHealthBarthDestroy(GameObject pointer)
        // {
        //     pointer.SetActive(false);
            
        //     this.enemiesPool.Enqueue(pointer);
        // }

        #endregion

        private void RemoveFromQueue(GameObject obj, ObjectType type)
        {
            obj.SetActive(false);

            if (type == ObjectType.enemy)
                this.enemiesPool.Enqueue(obj);

            if (type == ObjectType.pointer)
                this.pointersPool.Enqueue(obj);

            if (type == ObjectType.healthBar)
                this.healthBarsPool.Enqueue(obj);
        }
    }
}
