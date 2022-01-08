using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, Collision c);
    }

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    public class FireCollisionForwardScript : MonoBehaviour
    {
        public ICollisionHandler CollisionHandler;

        public void OnCollisionEnter(Collision col)
        {
            // if (col.collider.CompareTag("Crate") && explodeOnTouch) Destroy(col.gameObject);

            CollisionHandler.HandleCollision(gameObject, col);
            DealDamage();
        }

        public LayerMask whatIsEnemies;
        public LayerMask whatIsCrates;

        [Range(0f, 1f)]
        public float bounciness;
        public bool useGravity;

        public int explosionDamage;
        public float explosionRange;

        public int maxCollisions;
        public float maxLifetime;
        public bool explodeOnTouch = true;

        int collisions;
        PhysicMaterial physics_mat;

        private void Start()
        {
            Setup();
        }

        private void Update()
        {

        }

        private void DealDamage()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
            Collider[] crates = Physics.OverlapSphere(transform.position, explosionRange, whatIsCrates);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].tag == "Player")
                {
                    enemies[i].GetComponentInParent<Player>().TakeDamage(explosionDamage);
                }
                else
                {
                    enemies[i].GetComponentInParent<Enemy>().TakeDamage(explosionDamage);
                }
            }
            for (int i = 0; i < crates.Length; i++)
            {
                Debug.Log(crates[i].tag);
                if (crates[i].tag == "Crate")
                {
                    Debug.Log(crates[i]);
                };
            }
        }

        private void Setup()
        {
            physics_mat = new PhysicMaterial();
            physics_mat.bounciness = bounciness;
            physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
            physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

            GetComponentInChildren<SphereCollider>().material = physics_mat;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRange);
        }
    }
}
