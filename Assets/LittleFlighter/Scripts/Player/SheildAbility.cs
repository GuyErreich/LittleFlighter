using UnityEngine;
using UnityEngine.VFX;

namespace LittleFlighter
{
    public class SheildAbility : MonoBehaviour
    {
        [SerializeField] private VisualEffect vfxEffect;
        [SerializeField] private Collider collider;
        private bool isSheild = false;

        private void Start() {
            vfxEffect.enabled = false;
            collider.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isSheild){
                vfxEffect.enabled = true;
                collider.enabled = true;
            }
            else {
                vfxEffect.enabled = false;
                collider.enabled = false;
            }

        }

        public void ReceiveInput(bool isSheild) {
            this.isSheild = isSheild;
        }
    }
}

