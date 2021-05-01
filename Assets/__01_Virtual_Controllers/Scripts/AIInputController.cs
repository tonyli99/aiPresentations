namespace BIG
{
    using UnityEngine;

    // Feed input to a virtual controller from AI decision-making.
    [RequireComponent(typeof(VirtualController))]
    public class AIInputController : MonoBehaviour
    {
        private VirtualController virtualController;
        private int direction; // -1 left, +1 right

        private void Awake()
        {
            virtualController = GetComponent<VirtualController>();
            direction = (int)Mathf.Sign(Random.Range(-1, 1));
        }

        private void Update()
        {
            // If farther than 5 units from center, turn around:
            if (Mathf.Abs(transform.position.x) > 5)
            { 
                direction = -direction; 
            }
            virtualController.movement = new Vector2(direction, 0);
        }
    }
}
