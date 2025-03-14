using System.Collections;
using UnityEngine;

public class OnBrickDestroyed : MonoBehaviour
{
    private ParticleSystem _particles;
    void Start()
    {
        _particles = GetComponent<ParticleSystem>();
        if (_particles)
        {
            StartCoroutine(OnDestroyed());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator OnDestroyed()
    {
        _particles?.Play();
        yield return new WaitForSeconds(_particles?.main.startLifetime.constantMax ?? 0);
        Destroy(gameObject);
    }
}
