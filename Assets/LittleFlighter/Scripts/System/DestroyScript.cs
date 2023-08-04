using UnityEngine;

namespace LittleFlighter.Utilities
{
    public class DestroyScript : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private float duration;
        private float endOfDuration;

        void Start()
        {
            this.endOfDuration = Time.time + this.duration;
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time > this.endOfDuration)
                Destroy(this.gameObject);
        }
    }
}
