using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// Flies in the direction instantiated until it hits an actor with Health or
    /// times out.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public float lifetime = 3;
        public float speed = 30;
        public int damage = 25;

        private float timeLeft;

        private void Awake()
        {
            timeLeft = lifetime;
            var rb2d = GetComponent<Rigidbody2D>();
            rb2d.AddForce(transform.right * speed);
        }

        private void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.currentHealth -= damage;
                Destroy(gameObject);
            }
        }
    }
}
