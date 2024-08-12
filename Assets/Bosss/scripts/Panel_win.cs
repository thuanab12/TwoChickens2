using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_win : MonoBehaviour
{
    public GameObject panel; // Tham chiếu tới panel UI
    private bool hasTriggered = false; // Biến để kiểm tra xem panel đã hiện chưa
    public static bool gameipause = false;



    private void Start()
    {
        panel.SetActive(false); // Đảm bảo panel bắt đầu ở chế độ inactive
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hasTriggered)
        {
            panel.SetActive(true); // Hiển thị panel
            hasTriggered = true; // Đặt biến thành true để không hiện panel lần nữa
            Time.timeScale = 0;
            gameipause = true;

        }

    }
 
}




