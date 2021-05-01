using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// When picked up, sets actor's Gun.currentAmmo to max.
    /// </summary>
    public class AmmoPickup : Pickup
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var gun = collision.GetComponent<Gun>();
            if (gun != null)
            {
                gun.currentAmmo = Gun.MaxAmmo;
                Regenerate();
            }
        }
    }
}
