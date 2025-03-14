using System.Collections;
using _Scripts.Utils;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;
    
    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;
        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        GameManager.Instance.OnBrickDestroyed(transform.position);
        PlayDestroyedEffect();
        Destroy(gameObject);
    }

    private void PlayDestroyedEffect()
    {
        var effect = Resources.Load<GameObject>("Particle Effects/BrickDestroyedEffect");
        var obj = Instantiate(effect, transform.position, Quaternion.identity);
        obj.transform.localScale = Vector3.one;
        var particles = obj.GetComponent<ParticleSystem>();
        
        var hotswapColor = GetComponent<HotSwapColor>();
        if (particles == null || hotswapColor == null) return;
        var ms = particles.main;
        var c = hotswapColor.GetColor();
        c.a = 1;
        ms.startColor = new ParticleSystem.MinMaxGradient(c);
    }
}
