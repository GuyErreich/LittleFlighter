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
        private Dictionary<GameObject, GameObject> currentPointers = new Dictionary<GameObject, GameObject>();
        private Dictionary<GameObject, GameObject> currenthealthBars = new Dictionary<GameObject, GameObject>();

        private int enemiesCurrentAmount = 0;  

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(this.SpawnRoutine());
            StartCoroutine(this.HealthSupervisor());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                // yield return HealthSupervisor();

                float posX = Random.Range(-this.spawnZoneRange, this.spawnZoneRange);
                float posY = Random.Range(-this.spawnZoneRange, this.spawnZoneRange);
                float posZ = Random.Range(-this.spawnZoneRange, this.spawnZoneRange);

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

                        currentPointers.Add(enemy, pointer);
                    }

                    if (this.healthBarsPool.Count > 0)
                    {
                        var healthBar = this.healthBarsPool.Dequeue();

                        enemy.GetComponent<EnemyController>().OnHealthChanged += healthBar.GetComponent<DynamicHealthBar>().HandleHealthChanged;

                        healthBar.SetActive(true);

                        currenthealthBars.Add(enemy, healthBar);
                    }
                }

                yield return new WaitForSeconds(spawnSpeed);
            }
        }

        #region Events Handlers

        private IEnumerator HealthSupervisor()
        {
            while (true)
            {
                foreach(var enemy in currentEnemies.ToArray())
                {
                    var enemyHealth = enemy.GetComponent<EnemyController>().CurrentHealth;

                    if (enemyHealth <= 0)
                        this.ReturnToQueue(enemy);
//                         yield return new WaitUntil(() => { this.ReturnToQueue(enemy); return true;});

                    yield return null;
                }

                yield return null;
            }
        }

        #endregion

        private void ReturnToQueue(GameObject enemy)
        {
            this.currentEnemies.Remove(enemy);
            enemy.SetActive(false);
            this.enemiesPool.Enqueue(enemy);

            var pointer = this.currentPointers[enemy];
            this.currentPointers.Remove(enemy);
            pointer.SetActive(false);
            this.pointersPool.Enqueue(pointer);

            var healthBar = this.currenthealthBars[enemy];
            this.currenthealthBars.Remove(enemy);
            healthBar.SetActive(false);
            enemy.GetComponent<EnemyController>().OnHealthChanged -= healthBar.GetComponent<DynamicHealthBar>().HandleHealthChanged;
            this.healthBarsPool.Enqueue(healthBar);
            
        }
    }
}
