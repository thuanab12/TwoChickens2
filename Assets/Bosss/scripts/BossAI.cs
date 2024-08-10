using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float chaseDistance = 10f;
    public float aniAttack = 5f;

    private bool facingRight = true;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Xử lý trạng thái dí theo người chơi
        if (distanceToPlayer < chaseDistance && distanceToPlayer > aniAttack)
        {
            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking2", false);
            animator.SetBool("isAttacking1", false);

            // Di chuyển Boss về phía người chơi
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Kiểm tra hướng và lật mặt Boss nếu cần thiết
            FlipTowardsPlayer();
        }
        // Xử lý trạng thái tấn công khi trong phạm vi aniAttack
        else if (distanceToPlayer <= aniAttack)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking2", true);
        }
        else
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking2", false);
            animator.SetBool("isAttacking1", false);
        }
    }

    // Hàm này sẽ được gọi từ Animation Event sau khi Attack2 hoàn thành
    public void OnAttack2Complete()
    {
        animator.SetBool("isAttacking2", false);
        animator.SetBool("isAttacking1", true);
    }

    // Hàm này sẽ được gọi từ Animation Event sau khi Attack1 hoàn thành
    public void OnAttack1Complete()
    {
        animator.SetBool("isAttacking1", false);
    }

    void FlipTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Đảo ngược giá trị scale của trục X để lật mặt
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
