using UnityEngine;
using UnityEngine.VFX;

namespace LittleFlighter
{
    public class SheildAbility : MonoBehaviour
    {
        [SerializeField] private VisualEffect vfxEffect;
        private bool isSheild = false;

        private void Start() {
            vfxEffect.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isSheild)
                vfxEffect.enabled = true;
            else
                vfxEffect.enabled = false;
        }

        public void ReceiveInput(bool isSheild) {
            this.isSheild = isSheild;
        }
    }
}

