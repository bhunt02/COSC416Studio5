using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Movement")]
    [SerializeField] private float ballLaunchSpeed;
    [SerializeField] private float minBallBounceBackSpeed;
    [SerializeField] private float maxBallBounceBackSpeed;
    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paddleHitClip;
    [SerializeField] private AudioClip brickHitClip;
    [Header("References")]
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private Rigidbody rb;

    private bool isBallActive;
    private ParticleSystem hitParticles;
    private ParticleSystem bounceParticles;
    private TrailRenderer trailRenderer;

    private void Start()
    {
        bounceParticles = transform.Find("BounceParticles")?.GetComponent<ParticleSystem>();
        hitParticles = transform.Find("HitParticles")?.GetComponent<ParticleSystem>();
        trailRenderer = transform.Find("Trail").GetComponent<TrailRenderer>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        bounceParticles.Play();
        if(other.gameObject.CompareTag("Paddle"))
        {
            // Play paddle hit sound
            audioSource?.PlayOneShot(paddleHitClip);
            Vector3 directionToFire = (transform.position - other.transform.position).normalized;
            float angleOfContact = Vector3.Angle(transform.forward, directionToFire);
            float returnSpeed = Mathf.Lerp(minBallBounceBackSpeed, maxBallBounceBackSpeed, angleOfContact / 90f);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(directionToFire * returnSpeed, ForceMode.Impulse);
        } 
        else if (other.gameObject.GetComponent<Brick>() != null)
        {
            // Play brick hit sound
            audioSource?.PlayOneShot(brickHitClip);
            hitParticles?.Play();
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
