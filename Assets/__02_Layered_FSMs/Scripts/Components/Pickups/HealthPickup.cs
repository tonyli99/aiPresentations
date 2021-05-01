using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// When picked up, sets actor's Health.currentHealth to max.
    /// </summary>
    public class HealthPickup : Pickup
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.currentHealth = Health.MaxHealth;
                Regenerate();
            }
        }
    }
}
