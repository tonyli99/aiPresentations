using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// Base class for pickups.
    /// </summary>
    public class Pickup : MonoBehaviour
    {
        public const float RegenerateDuration = 5;

        public bool IsAvailable()
        {
            return GetComponent<Collider2D>().enabled;
        }

        public void Regenerate()
        {
            SetPresence(false);
            Invoke(nameof(Reappear), RegenerateDuration);
        }

        private void Reappear()
        {
            SetPresence(true);
        }

        private void SetPresence(bool value)
        {
            GetComponent<SpriteRenderer>().enabled = value;
            GetComponent<Collider2D>().enabled = value;
        }

    }
}
