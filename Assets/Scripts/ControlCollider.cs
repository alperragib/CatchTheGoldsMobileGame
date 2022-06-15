using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCollider : MonoBehaviour
{
    public static int health = 5;
    public GameObject heart1, heart2, heart3, heart4, heart5,gameover_panel, canAl_panel, pause_panel;
    public Button video_izle, altinla_al;
    public Text score_text;
    public AudioSource audioCoin,audioGameError1, audioGameError2, audioHealth;
    private int dif;
    public static InterstitialAd interstitial;
    public static RewardedAd rewardedAd;
    private bool canAl = false;
    private void Start()
    {
        dif = PlayerPrefs.GetInt("Zorluk", 2);

        MobileAds.Initialize(initStatus => { });
        interstitial = new InterstitialAd("ca-app-pub-3332967002509193/2905867078");
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);

        rewardedAd = new RewardedAd("ca-app-pub-3332967002509193/7386277371");
        rewardedAd.OnUserEarnedReward += onRewarded;
        rewardedAd.OnAdClosed += onAdClosed;
        AdRequest request1 = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request1);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bomb") {
            Destroy(col.gameObject);
            health--;

            switch (health)
            {
                case 4:
                    heart5.SetActive(false);
                    break;
                case 3:
                    heart4.SetActive(false);
                    break;
                case 2:
                    heart3.SetActive(false);
                    break;
                case 1:
                    heart2.SetActive(false);
                    break;
                case 0:
                    heart1.SetActive(false);
                    if (Time.timeScale == 1)
                    {
                        Time.timeScale = 0;
                    }
                    int money = PlayerPrefs.GetInt("Money", 0);

                    if (ScoreText.scoreValue > 25 && !canAl && (money >= 250 || rewardedAd.IsLoaded()))
                    {
                        if (money >= 250)
                        {
                            altinla_al.interactable = true;
                        }
                        else {
                            altinla_al.interactable = false;
                        }
                        if (rewardedAd.IsLoaded())
                        {
                            video_izle.interactable = true;
                        }
                        else
                        {
                            video_izle.interactable = false;
                        }

                        if (!canAl_panel.activeSelf)
                        {
                            canAl_panel.SetActive(true);
                            canAl = true;
                            
                        }

                    }
                    else {
                        

                        PlayerPrefs.SetInt("Money", (ScoreText.scoreValue + money));
                        int hightScore = PlayerPrefs.GetInt("HightScore", 0);
                        if (ScoreText.scoreValue > hightScore)
                        {
                            PlayerPrefs.SetInt("HightScore", ScoreText.scoreValue);
                        }
                        if (!gameover_panel.activeSelf)
                        {
                            if (interstitial.IsLoaded())
                            {
                                interstitial.Show();
                            }
                            score_text.text = ScoreText.scoreValue.ToString();
                            gameover_panel.SetActive(true);
                        }
                    }

                    
                    

                    break;
            }
            if (col.gameObject.name == "Bomb(Clone)")
            {
                if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
                {
                    audioGameError1.Play();
                }
                
            }
            else {
                if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
                {
                    audioGameError2.Play();
                }
            }
                
        }
            
        if (col.gameObject.tag == "Coin") {
            Destroy(col.gameObject);
            switch (dif)
            {
                case 1:
                    ScoreText.scoreValue += 1;
                    break;
                case 2:
                    ScoreText.scoreValue += 3;
                    break;
                case 3:
                    ScoreText.scoreValue += 5;
                    break;
            }
            
            if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
            {
                audioCoin.Play();
            }
        }
        if (col.gameObject.tag == "Heart")
        {
            Destroy(col.gameObject);
            health++;
            switch (health)
            {
                case 5:
                    heart5.SetActive(true);
                    break;
                case 4:
                    heart4.SetActive(true);
                    break;
                case 3:
                    heart3.SetActive(true);
                    break;
                case 2:
                    heart2.SetActive(true);
                    break;
                case 1:
                    heart1.SetActive(true);
                    break;
            }
            if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
            {
                audioHealth.Play();
            }
        }

    }

    public void onAdClosed(object sender, EventArgs args)
    {
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
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                }
                score_text.text = ScoreText.scoreValue.ToString();
                gameover_panel.SetActive(true);
            }
        }
    }
    public void onRewarded(object sender, Reward args)
    {
        health++;
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
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
        }
   
}
