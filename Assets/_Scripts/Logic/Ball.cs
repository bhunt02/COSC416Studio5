using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Movement")]
    [SerializeField] private float ballLaunchSpeed;
    [SerializeField] private float minBallBounceBackSpeed;
    [SerializeField] private float maxBallBounceBackSpeed;
    [Header("References")]
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private Rigidbody rb;

    private bool isBallActive;
    private ParticleSystem hitParticles;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        var particlesChild = transform.Find("HitParticles");
        if (particlesChild)
        {
            hitParticles = particlesChild.GetComponent<ParticleSystem>();
        }
        var trail = transform.Find("Trail");
        if (trail)
        {
            trailRenderer = trail.GetComponent<TrailRenderer>();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (hitParticles)
        {
            hitParticles.Play();
        }
        if(other.gameObject.CompareTag("Paddle"))
        {
            Vector3 directionToFire = (transform.position - other.transform.position).normalized;
            float angleOfContact = Vector3.Angle(transform.forward, directionToFire);
            float returnSpeed = Mathf.Lerp(minBallBounceBackSpeed, maxBallBounceBackSpeed, angleOfContact / 90f);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(directionToFire * returnSpeed, ForceMode.Impulse);
        }
    }

    public void ResetBall()
    {
        if (trailRenderer)
        {
            trailRenderer.emitting = false;
        }
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isBallActive = false;
    }

    public void FireBall()
    {
        if (isBallActive) return;
        if (trailRenderer)
        {
            trailRenderer.emitting = true;
        }
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * ballLaunchSpeed, ForceMode.Impulse);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        isBallActive = true;
    }
}
