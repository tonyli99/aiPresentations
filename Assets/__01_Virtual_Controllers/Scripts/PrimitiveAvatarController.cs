namespace BIG
{
    using UnityEngine;

    // Very simple avatar controller that moves horizontally and jumps.
    // Reads input from a virtual controller.
    [RequireComponent(typeof(VirtualController))]
    [RequireComponent(typeof(Animator))]
    public class PrimitiveAvatarController : MonoBehaviour
    {
        public float walkSpeed = 2;
        public float jumpSpeed = 5;

        [Header("Animator Parameters")]
        public string walkingAnimatorParameter = "Walking";
        public string jumpingAnimatorParameter = "Jumping";

        private VirtualController virtualController;
        private Animator animator;
        private bool isJumping;
        private float jumpVelocity = 0;

        private const float Gravity = 20f;

        private void Awake()
        {
            virtualController = GetComponent<VirtualController>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            // Face input direction:
            if (virtualController.movement.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (virtualController.movement.x > 0) transform.localScale = new Vector3(1, 1, 1);

            // Update jumping:
            if (virtualController.jump && !isJumping)
            {
                isJumping = true;
                jumpVelocity = jumpSpeed;
            }
            else if (isJumping)
            {
                jumpVelocity -= Gravity * Time.deltaTime;
            }

            // Stop when we hit the floor:
            if (jumpVelocity < 0)
            {
                var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
                if (hit)
                {
                    isJumping = false;
                    jumpVelocity = 0;
                    transform.position = hit.point;
                }
            }

            // Move the GameObject:
            var movementVector = new Vector2(virtualController.movement.x * walkSpeed, jumpVelocity) * Time.deltaTime;
            transform.Translate(movementVector);

            // Update animator:
            animator.SetBool(walkingAnimatorParameter, Mathf.Abs(virtualController.movement.x) > 0.01f);
            animator.SetBool(jumpingAnimatorParameter, isJumping);
        }
    }
}
