using UnityEngine;
using UnityEngine.UI;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// Manages an actor's health. When current health hits zero, set the
    /// isDead animator parameter.
    /// </summary>
    public class Health : MonoBehaviour
    {
        public const int MaxHealth = 100;

        public int currentHealth = MaxHealth;
        public Slider healthSlider;

        private int lastSliderValue = -1;

        private void Update()
        {
            if (currentHealth != lastSliderValue)
            {
                healthSlider.value = currentHealth;
                lastSliderValue = currentHealth;
                if (currentHealth <= 0)
                {
                    GetComponentInChildren<Animator>().SetBool("isDead", true);
                }
            }
        }

    }
}
