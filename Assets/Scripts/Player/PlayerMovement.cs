using System;
using UnityEngine;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput;


namespace Nightmare
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : PausibleObject
    {
        public Camera playerCamera;
        public float speed = 6f;
        public float runningSpeedMultiplier = 2f;
        public static float speedBuffMultiplier = 1.2f;
        public static float speedBuffTimer = 0f;
        public static GameObject buffHUD = null;
        public static float mobDebuff = 1f;
        public float godBuffMultiplier = 2f;
        public bool godMode = false;

        public float jumpPower = 7f;
        public float gravity = 10f;

        public float lookSpeed = 2f;
        public float lookXLimit = 45f;

        Vector3 moveDirection = Vector3.zero;
        float rotationX = 0;

        public bool canMove = true;

        CharacterController characterController;
        Animator anim;


        void Awake()
        {
            anim = GetComponent<Animator>();
            StartPausible();
        }

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public override void OnPause()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public override void OnUnPause()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update()
        {
            if (isPaused)
                return;

            if (speedBuffTimer > 0f)
            {
                speedBuffTimer -= Time.deltaTime;
                setSpeedDurationText(speedBuffTimer);
            }
            else if (buffHUD is not null)
            {
                BuffManager.RemoveBuff(buffHUD);
                buffHUD = null;
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Move(h, v);
            Animating(h, v);
        }

        void Move(float h, float v)
        {
            #region Handles Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float speedBuff = speed * mobDebuff;
            
            if (speedBuffTimer > 0)
                speedBuff *= speedBuffMultiplier;

            if (isRunning)
                speedBuff *= runningSpeedMultiplier;

            if (godMode)
                speedBuff *= godBuffMultiplier;

            float curSpeedX = v * speedBuff;
            float curSpeedY = h * speedBuff;

            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            #endregion

            #region Handles Jumping
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            #endregion

            #region Handles Rotation
            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            #endregion
        }

        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", walking);
        }

        public static void setSpeedDurationText(float duration) {
            Text stackText = buffHUD.GetComponentInChildren<Text>();
            stackText.text = ((int)Math.Ceiling(duration)).ToString();
        }
    }
}