using UnityEngine;
using UnityEngine.Events;

namespace LittleFlighter.System
{    
    public class EnemyManagerDeathController : MonoBehaviour {
        public UnityEvent<GameObject, EnemyManager.ObjectType> OnDeath;

        public void Die(EnemyManager.ObjectType type)
        {
            OnDeath.Invoke(this.gameObject, type);
        }
    }
}