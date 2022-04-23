using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace LittleFlighter
{
    public class SpacecraftController : MonoBehaviour
    {


        [Header("References")]
        [SerializeField] private Transform spaceCraftModel;
        [SerializeField] private Material boosterFlameMaterial;


        [Header("Defense Settings")]
        [SerializeField] private int health;


        [Header("Movement Settings")]
        [SerializeField] private float forwardThrust = 200;
        [SerializeField] private float strafeBoost = 150;

        [Header("Events")]
        public UnityEvent<float> OnHealthChanged;

        private bool isTrust = false;
        private Vector2 mouseLook;
        private Rigidbody rb;
        private float currentMaxVelocity = 1f;



        #region Getters and Setters

        public int CurrentHealth { get; private set; }

        #endregion


        private void Awake()
        {
            this.rb = this.GetComponent<Rigidbody>();

            this.CurrentHealth = this.health;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void FixedUpdate()
        {
            this.Torque();
            this.Thrust();
            this.Tilt();
            this.ControlEngineVFX();
        }

        private void Thrust()
        {
            if (!this.isTrust)
                return;

            this.rb.AddForce(this.transform.forward * this.forwardThrust, ForceMode.Acceleration);


        }

        public void Dash(float dir)
        {
            this.rb.AddForce(this.transform.right * dir * this.strafeBoost, ForceMode.VelocityChange);
        }

        private void Torque()
        {
            float verticalAxis = this.mouseLook.y;
            float horizonalAxis = this.mouseLook.x;

            this.transform.Rotate(this.transform.right, verticalAxis, Space.World);
            this.transform.Rotate(this.transform.up, horizonalAxis, Space.World);
        }

        private void Tilt()
        {
            float horizonalAxis = this.mouseLook.x;

            if (this.rb.velocity.x == 0f && this.currentMaxVelocity < this.rb.velocity.magnitude)
                this.currentMaxVelocity = this.rb.velocity.magnitude;

            float velocityNormlized = this.rb.velocity.magnitude / this.currentMaxVelocity;

            float horizontalAxisNormalized = horizonalAxis / this.GetComponent<CharacterInputManager>().LookSensitivityX;

            float angle = (90 * (-horizontalAxisNormalized) * Mathf.Clamp(velocityNormlized, 0.15f, 1f));

            Vector3 newRotaion = new Vector3(0, 0, angle);

            this.spaceCraftModel.DOLocalRotate(newRotaion, 30f * Time.fixedDeltaTime, RotateMode.Fast);
        }

        private void ControlEngineVFX()
        {
            var changeTime = 2f * Time.fixedDeltaTime;

            if (this.isTrust)
            {
                var flameSize = Mathf.Lerp(this.boosterFlameMaterial.GetFloat("_FlameSize"), 0.75f, changeTime);
                this.boosterFlameMaterial.SetFloat("_FlameSize", flameSize);

                var disolveSpeed = Vector4.Lerp(this.boosterFlameMaterial.GetVector("_DisolveSpeed"), new Vector4(0.5f, -1.75f, 0f, 0f), changeTime);
                this.boosterFlameMaterial.SetVector("_DisolveSpeed", disolveSpeed);
            }
            else
            {
                var flameSize = Mathf.Lerp(this.boosterFlameMaterial.GetFloat("_FlameSize"), 3f, changeTime);
                this.boosterFlameMaterial.SetFloat("_FlameSize", flameSize);

                var disolveSpeed = Vector4.Lerp(this.boosterFlameMaterial.GetVector("_DisolveSpeed"), new Vector4(0.5f, -0.25f, 0f, 0f), changeTime);
                this.boosterFlameMaterial.SetVector("_DisolveSpeed", disolveSpeed);
            }
        }

        #region Events Handlers

        public void HandleHit(int amount, Collider collider)
        {
            if (collider.CompareTag("EnemyBullet"))
                this.ModifyHealth(-amount);
        }

        #endregion Events Handlers

        private void ModifyHealth(int amount)
        {
            this.CurrentHealth += amount;

            float currentHealthPct = (float)this.CurrentHealth / (float)this.health;

            OnHealthChanged?.Invoke(currentHealthPct);
        }

        public void ReceiveInput(bool isTrust, Vector2 mouseLook)
        {
            this.isTrust = isTrust;
            this.mouseLook = mouseLook;
        }
    }
}
