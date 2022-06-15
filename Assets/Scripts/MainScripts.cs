using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class MainScripts : MonoBehaviour
{
    
    public GameObject coin, bomb,astroid,heart,pause_panel, heart1, gameover_panel, canAl_panel;
    public Text score_text;
    public static float timer_coin = 1.9f;
    public static float timer_bomb = 2.3f;
    float timer_heart = 30;
    public static float timer_astroid = 1.3f;
    private Vector2 screenBounds;
    public AudioSource audioPressButton1,music2;
    public GameObject[] players;
    private int b;
    

    void Start()
    {
        int selected_player = PlayerPrefs.GetInt("SelectedPlayer", 0);

        for (int i=0;i<players.Length;i++) 
        {
            if (selected_player==i) {
                players[i].SetActive(true);
                break;
            }
        }

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        b = PlayerPrefs.GetInt("MuzikSes", 1);
        if (b == 1)
        {
            music2.Play();
        }
        else
        {
            music2.Stop();
        }
        int dif = PlayerPrefs.GetInt("Zorluk", 2);

        switch (dif)
        {
            case 1:
                timer_coin = 2.2f;
                timer_bomb = 2.9f;
                timer_astroid = 1.8f;
                break;
            case 2:
                timer_coin = 1.9f;
                timer_bomb = 2.3f;
                timer_astroid = 1.3f;
                break;
            case 3:
                timer_coin = 1.7f;
                timer_bomb = 1.9f;
                timer_astroid = 0.9f;
                break;
        }

        StartCoroutine(coinWave());
        StartCoroutine(bombWave());
        StartCoroutine(heartWave());
        StartCoroutine(asteroidWave());

        

    }
    private void Update()
    {
        if (!pause_panel.activeSelf && Time.timeScale == 1) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ButtonPlayPause();
            }
        }

        if (b==1) {
            if (Time.timeScale == 0)
            {
                if (music2.isPlaying)
                {
                    music2.Pause();
                }
            }
            else {
                if (!music2.isPlaying)
                {
                    music2.Play();
                }
            }
        }
        

    }
    private void spawnCoin() {
        GameObject a = Instantiate(coin) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x / 1.5f, screenBounds.x / 1.5f), coin.transform.position.y);
    }
    IEnumerator coinWave() {
        while (true) {
            yield return new WaitForSeconds(timer_coin);
            spawnCoin();
        }
    }
    private void spawnBomb()
    {
        GameObject b = Instantiate(bomb) as GameObject;
        b.transform.position = new Vector2(Random.Range(-screenBounds.x / 1.5f, screenBounds.x / 1.5f), bomb.transform.position.y);
    }
    IEnumerator bombWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer_bomb);
            spawnBomb();
        }
    }
    private void spawnHeart()
    {
        GameObject b = Instantiate(heart) as GameObject;
        b.transform.position = new Vector2(Random.Range(-screenBounds.x / 1.5f, screenBounds.x / 1.5f), heart.transform.position.y);
    }
    IEnumerator heartWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer_heart);
            if (ControlCollider.health < 4)
            {
                spawnHeart();
            }
        }
    }
    private void spawnAsteroid()
    {
        GameObject b = Instantiate(astroid) as GameObject;
        b.transform.position = new Vector2(Random.Range(-screenBounds.x / 1.5f, screenBounds.x / 1.5f), astroid.transform.position.y);
    }
    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timer_astroid);
            spawnAsteroid();
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!pause_panel.activeSelf && Time.timeScale==1)
            {
                ButtonPlayPause();
            }
        }
    }
    public void ButtonPlayPause() {


        if (Time.timeScale == 1)
        {
            if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
            {
                audioPressButton1.Play();
            }
            Time.timeScale = 0;
            if (!pause_panel.activeSelf)
            {
                pause_panel.SetActive(true);
            }
        }
        
    }
    public void ButtonDevam()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioPressButton1.Play();
        }
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        if (pause_panel.activeSelf)
        {
            pause_panel.SetActive(false);
        }

    }
    public void ButtonAnaMenu()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioPressButton1.Play();
        }
        ScoreText.scoreValue = 0;
        ScoreText.old_scoreValue = 0;
        ControlCollider.health = 5;
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(0);
    }
    public void ButtonRestart()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioPressButton1.Play();
        }
        ScoreText.scoreValue = 0;
        ScoreText.old_scoreValue = 0;
        ControlCollider.health = 5;
        SceneManager.LoadScene(1);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }
    public void ButtonCanAlAltinla()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioPressButton1.Play();
        }
        int money1 = PlayerPrefs.GetInt("Money", 0);
        if (money1 >= 250)
        {
            money1 -= 250;
            PlayerPrefs.SetInt("Money", money1);
            ControlCollider.health++;
            heart1.SetActive(true);
            if (canAl_panel.activeSelf)
            {
                canAl_panel.SetActive(false);
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Bomb");
            foreach (GameObject enemy in enemies)
                GameObject.Destroy(enemy);
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject coin in coins)
                GameObject.Destroy(coin);

            if (!pause_panel.activeSelf)
            {
                pause_panel.SetActive(true);
            }
        }
    }
    public void ButtonCanAlVideoIzle()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioPressButton1.Play();
        }
        if (ControlCollider.rewardedAd.IsLoaded())
        {
            ControlCollider.rewardedAd.Show();
        }
    }
    public void ButtonOyunuBitir()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioPressButton1.Play();
        }
        if (canAl_panel.activeSelf)
        {
            canAl_panel.SetActive(false);
            int money = PlayerPrefs.GetInt("Money", 0);
            PlayerPrefs.SetInt("Money", (ScoreText.scoreValue + money));
            int hightScore = PlayerPrefs.GetInt("HightScore", 0);
            if (ScoreText.scoreValue > hightScore)
            {
                PlayerPrefs.SetInt("HightScore", ScoreText.scoreValue);
            }
            if (!gameover_panel.activeSelf)
            {
                if (ControlCollider.interstitial.IsLoaded())
                {
                    ControlCollider.interstitial.Show();
                }
                score_text.text = ScoreText.scoreValue.ToString();
                gameover_panel.SetActive(true);
            }
        }
    }

}
