using UnityEngine;

public class BowMovement : MonoBehaviour
{
    public GameObject Projectile;
    public float launchForce;
    public Transform shootingPoint;

    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - bowPosition;

        transform.up = direction;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(Projectile, shootingPoint.transform.position, shootingPoint.rotation);
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = transform.up * launchForce;
    }
}
