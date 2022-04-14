using UnityEngine;

namespace LittleFlighter.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage;

        #region Getters and Setters

        public int Damage { get { return this.damage; } } 

        #endregion

        public void SetDamage(int amount)
        {
            this.damage = amount;
        }
    }
}