using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LittleFlighter
{
    [RequireComponent(typeof(GameManager))]
    public class MiscInputManager : MonoBehaviour
    {
    
        private GameManager gameManager;

        private PlayerControls controls;
        private PlayerControls.MiscActions MiscInput;

        private void Awake()
        {
            this.controls = new PlayerControls();
            this.MiscInput = this.controls.Misc;

            gameManager = this.GetComponent<GameManager>();

            this.MiscInput.PauseGame.performed += _ => gameManager.PauseGame();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDestroy()
        {
            controls.Disable();
        }
    }
}