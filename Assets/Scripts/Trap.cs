using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damageAmount = 10;
    public Animator spikeAnimator;
    public float delay = 3.0f;

    void Start()
    {
        if (spikeAnimator == null)
        {
            spikeAnimator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gọi hàm để làm tổn thương người chơi
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                StartCoroutine(ActivateSpikeWithDelay(playerHealth));
            }
        }
    }

    private IEnumerator ActivateSpikeWithDelay(PlayerHealth playerHealth)
    {
        yield return new WaitForSeconds(delay);
        // Kích hoạt hoạt hình của bẫy
        spikeAnimator.SetTrigger("ActivateSpike");
        // Làm tổn thương người chơi
        playerHealth.TakeDamage(damageAmount);
    }
}
