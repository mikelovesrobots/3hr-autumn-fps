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
                Debug.Log("Enemy hit: " + hit.collider.gameObject.name);
                // You can add more code here to damage the enemy or trigger other effects
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * maxShootDistance, Color.red, 1f);
    }
}