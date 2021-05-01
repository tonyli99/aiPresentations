using UnityEngine;
using UnityEngine.UI;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// Manages ammo and fires (instantiates) bullet prefabs.
    /// </summary>
    public class Gun : MonoBehaviour
    {
        public const int MaxAmmo = 20;
        public const float FiringDistance = 15;
        public const float FireRate = 1;

        public int currentAmmo = MaxAmmo;
        public Slider ammoSlider;
        public Transform bulletSpawnpoint;
        public Bullet bulletPrefab;

        private int lastSliderValue = -1;

        private void Update()
        {
            if (currentAmmo != lastSliderValue)
            {
                ammoSlider.value = currentAmmo;
                lastSliderValue = currentAmmo;
            }
        }

        public void Fire()
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                Instantiate<Bullet>(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
            }
        }

    }
}
