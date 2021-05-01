namespace BIG
{
    using UnityEngine;

    // When clicked, switches between player control and AI control.
    public class Possessable : MonoBehaviour
    {
        private PlayerInputController playerInputController;
        private AIInputController aiInputController;

        private void Awake()
        {
            playerInputController = GetComponent<PlayerInputController>();
            aiInputController = GetComponent<AIInputController>();
        }

        private void OnMouseDown()
        {
            playerInputController.enabled = !playerInputController.enabled;
            aiInputController.enabled = !aiInputController.enabled;
        }
    }
}
