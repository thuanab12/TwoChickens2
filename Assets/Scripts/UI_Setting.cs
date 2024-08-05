using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_Seting : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public void newgame()
    {
        SceneManager.LoadScene("Game"); // load scene: truyền vào tên màn
    }

    public void pause()
    {
        Time.timeScale = 0f; // Freeze game time
        GameIsPaused = true;
    }

    public void resume()
    {
        Time.timeScale = 1f; // Resume game time
        GameIsPaused = false;
    }

    public void exitGame()
    {
        Application.Quit(); // thoát game
    }


    public GameObject panelrank;
    public GameObject huongdan;
    public void hthuongdan()
    {
        huongdan.SetActive(true);
        setingpanel.SetActive(false);
    }
    public void anhuongdan()
    {
        huongdan.SetActive(false);
    }
    public void htrank()
    {
        panelrank.SetActive(true);

        pause();
    }

    public void anrank()
    {
        panelrank.SetActive(false);
        resume();
    }

    public GameObject setingpanel;

    
    public void htseting()
    {
        setingpanel.SetActive(true);
        pause();

    }

    public void anseting()
    {
        setingpanel.SetActive(false);
        resume();
    }


    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f; // Resume game time
        GameIsPaused = false;
    }
    public void scene0()
    {
        
        SceneManager.LoadScene(0);


    }
    public void LoadGame()
    {
        SceneManager.LoadScene(2);
       
    }

    public void BG()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Ensure time scale is reset before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
        GameIsPaused = false;
    }


    // Thêm biến Slider và AudioSource hoặc AudioMixer
    public Slider volumeSlider;
    public AudioSource audioSource; // Sử dụng nếu bạn không dùng AudioMixer

    void Start()
    {
        // Đảm bảo rằng slider đã được gán và thêm sự kiện vào slider
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.75f); // Lấy giá trị lưu trữ trước đó nếu có
        }

        // Đăng ký sự kiện khi scene được load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị hủy
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Phương thức được gọi khi scene được load
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        // Kiểm tra và phát âm thanh
        PlaySceneAudio(scene.name);
    }

    // Phương thức phát âm thanh tương ứng với scene
    void PlaySceneAudio(string sceneName)
    {
        // Ví dụ: phát âm thanh khi scene "Game" được load
        if (sceneName == "Game")
        {
            // Phát âm thanh từ audioSource
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume); // Lưu giá trị âm lượng
        PlayerPrefs.Save();

        if (audioSource != null)
        {
            audioSource.volume = volume;
        }


    }
}