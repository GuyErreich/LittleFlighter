using UnityEngine;

namespace LittleFlighter.Utilities
{
    public class FollowTargetDir : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 15;
        private RectTransform pointerRectTransform;

        // Update is called once per frame
        void Update()
        {
            this.FollowTarget();
        }

        private void FollowTarget()
        {
            Vector3 dir = this.target.position - this.transform.position;

            Quaternion nextRotation = Quaternion.LookRotation(dir, this.target.up);;

            this.transform.rotation =  Quaternion.Slerp(this.transform.rotation, nextRotation, this.rotationSpeed * Time.deltaTime);
        }
    }
}
