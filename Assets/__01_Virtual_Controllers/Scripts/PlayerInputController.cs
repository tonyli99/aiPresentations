namespace BIG
{
    using UnityEngine;

    // Feeds input to a virtual controller from the Unity Input Manager.
    [RequireComponent(typeof(VirtualController))]
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Input Mapping")]
        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";
        public string jumpButton = "Jump";

        private VirtualController virtualController;

        private void Awake()
        {
            virtualController = GetComponent<VirtualController>();
        }

        private void Update()
        {
            virtualController.movement = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            virtualController.jump = Input.GetButtonDown(jumpButton);
        }
    }
}
