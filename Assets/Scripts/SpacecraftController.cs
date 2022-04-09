using UnityEngine;
using DG.Tweening;

namespace LittleFlighter
{
    public class SpacecraftController : MonoBehaviour
    {
        private bool isTrust = false, isDash = false;
        private Vector2 mouseLook, movement;
        private Rigidbody rb;
        private float currentMaxVelocity = 0f;


        [Header("References")]
        [SerializeField] private Transform spaceCraftModel;


        [Header("Defense Settings")]
        [SerializeField] private int health;


        [Header("Movement Settings")]
        [SerializeField] private float forwardThrust = 200;
        [SerializeField] private float strafeThrust = 100;
        [SerializeField, Range(1, 3)] private float strafeBoost = 1.5f;


        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
            this.rb = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            this.TryApplyTorque();
            this.TryApplyThrust();
        }

        private void TryApplyThrust()
        {
            if (!this.isTrust && this.movement.x == 0f)
                return;

            // if(this.movement.magnitude == 0f)
            //     return;

            if (this.isTrust)
                this.rb.AddForce(this.transform.forward * this.forwardThrust, ForceMode.Acceleration);

            // this.rb.AddForce(this.transform.forward * maxSpeedDelta * this.forwardThrust * this.movement.y + 
            //                     this.transform.right * maxSpeedDelta * this.strafeThrust * this.movement.x, ForceMode.Acceleration);

            if (this.isDash)
            {
                this.rb.AddForce(this.transform.right * this.strafeThrust * this.movement.x * this.strafeBoost, ForceMode.VelocityChange);

                this.isDash = false;
            }



            if (this.currentMaxVelocity < this.rb.velocity.magnitude && this.movement.x == 0f)
                this.currentMaxVelocity = this.rb.velocity.magnitude;
        }

        // TODO: think about this shit
        private void TryApplyTorque()
        {
            float verticalAxis = this.mouseLook.y;
            float horizonalAxis = this.mouseLook.x;

            this.transform.Rotate(this.transform.right, verticalAxis, Space.World);
            this.transform.Rotate(this.transform.up, horizonalAxis, Space.World);

            float horizontalAxisNormalized = horizonalAxis / this.GetComponent<CharacterInputManager>().LookSensitivityX;

            float velocityNormlized = this.rb.velocity.magnitude / this.currentMaxVelocity;

            float angle = 90 * (-horizontalAxisNormalized) * Mathf.Clamp(velocityNormlized, 0.15f, 1f);

            this.spaceCraftModel.DOLocalRotate(new Vector3(0, 0, angle), 30f * Time.fixedDeltaTime, RotateMode.Fast);
        }

        // TODO: added boost option
        public void ReceiveInput(bool isTrust, Vector2 movement, Vector2 mouseLook, bool isDash)
        {
            this.isTrust = isTrust;
            this.movement = movement;
            this.mouseLook = mouseLook;
            this.isDash = isDash;
        }
    }
}
