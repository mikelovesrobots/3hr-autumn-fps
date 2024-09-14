using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float shootCooldown = 0.5f;
    public AudioSource shootingAudioSource;
    public AudioClip shootSound;
    public float maxShootDistance = 100f;

    private Camera mainCamera;
    private float lastShootTime;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShootTime + shootCooldown)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        lastShootTime = Time.time;

        if (shootSound != null)
        {
            shootingAudioSource.PlayOneShot(shootSound);
        }

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                var enemy = hit.collider.gameObject.GetComponent<Enemy>();
                var knockbackDirection = (enemy.transform.position - transform.position).normalized;

                // aim up a bit to make the enemy fly up
                knockbackDirection.y = 0.5f;
                knockbackDirection.Normalize();

                enemy.Die(knockbackDirection);
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * maxShootDistance, Color.red, 1f);
    }
}