using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float bulletDelay = 1.4f;

    [Header("References")]
    [SerializeField] private GameObject firePoint;
    [SerializeField] private Transform player;
    [SerializeField] private ObjectPooling pooler;

     [Header("Shooting Settings")]
     [SerializeField] private float fireRate = 1f;
     [SerializeField] private float nextFireRate;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        pooler = FindObjectOfType<ObjectPooling>();
    }

    void FixedUpdate()
    {
        if(nextFireRate < Time.time)
        {
            GameObject enemyBullet = pooler.SpawnFromPools("EnemyBullet", firePoint.transform.position, Quaternion.identity);
            Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
            }

            nextFireRate = Time.time + fireRate;
        }
    }
}