using UnityEngine;
using UnityEngine.Events;
using System;

namespace LittleFlighter.System
{
    public class Health : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int maxHealth = 100;
        public UnityEvent<float> OnHealthChanged;

        public int CurrentHealth { get; private set; }


        private void Awake()
        {
            this.CurrentHealth = this.maxHealth;
        }
    }
}