using UnityEngine;

public class BowMovement : MonoBehaviour
{
    public GameObject Projectile;
    public Transform shootingPoint;
    public Animator Animator;

    public float launchForce;
    public float bowSpeed;

    private bool canShoot;

    private void Awake()
    {
        canShoot = true;
    }

    void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - bowPosition;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.rotation.eulerAngles.z;

        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, bowSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }
    float animSpeed = 1f;
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            animSpeed += .1f;

            Animator.SetBool("Shoot", true);

            GameObject newProjectile = Instantiate(Projectile, shootingPoint.transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().linearVelocity = transform.up * launchForce;

            canShoot = false;

            Animator.SetFloat("SpeedMult", animSpeed);

            Destroy(newProjectile, 5f);
        }
    }

    public void AnimationEventSHOOTANIMATION()
    {
        Animator.SetBool("Shoot", false);
    }

    public void AnimationEventCANSHOOT()
    {
        canShoot = true;
    }
}
