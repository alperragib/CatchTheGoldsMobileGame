using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource audioButtonPrressed,music1;
    public GameObject panelButton,panelMarket,panelSettings;
    public Button satin_al_button;
    public Text satin_al_text,price_text,money_text,button_efekt, button_muzik,button_gozle_kontrol,hScoreText;
    public Image image_player;
    public Slider slider;
    public Sprite[] sprites;
    public int[] price;
    int index = 0;

    void Start()
    {

        int selected_player = PlayerPrefs.GetInt("SelectedPlayer", 0);
        image_player.sprite = sprites[selected_player];
        price_text.text = price[selected_player].ToString();
        index = selected_player;
        satin_al_text.text = "Etkin";
        satin_al_button.interactable = false;

        int[] buyed_players = PlayerPrefsX.GetIntArray("BuyedPlayers");
        if (buyed_players.Length < 1)
        {
            int[] buyed_players_n=new int[1];
            buyed_players_n[0] = 0;
            PlayerPrefsX.SetIntArray("BuyedPlayers", buyed_players_n);
        }

        int a = PlayerPrefs.GetInt("EfekSes", 1);
        if (a == 1)
        {
            button_efekt.text = "Efekt sesini kapat";
        }
        else
        {
            button_efekt.text = "Efekt sesini aç";
        }

        int b = PlayerPrefs.GetInt("MuzikSes", 1);
        if (b == 1)
        {
            button_muzik.text = "Müzik sesini kapat";
            music1.Play();
        }
        else
        {
            button_muzik.text = "Müzik sesini aç";
            music1.Stop();
        }

        int c = PlayerPrefs.GetInt("GozleKontrol", 0);
        if (c == 1)
        {
            button_gozle_kontrol.text = "Gözle kontrolü kapat";
        }
        else
        {
            button_gozle_kontrol.text = "Gözle kontrolü aç";
        }

        hScoreText.text = PlayerPrefs.GetInt("HightScore", 0).ToString();

        slider.value = PlayerPrefs.GetInt("Zorluk", 2);


    }

    void Update()
    {
        money_text.text = PlayerPrefs.GetInt("Money", 0).ToString();

        if (!panelButton.activeSelf && (panelMarket.activeSelf || panelSettings.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
                {
                    audioButtonPrressed.Play();
                }
                panelButton.SetActive(true);
                panelSettings.SetActive(false);
                panelMarket.SetActive(false);
            }
        }
    }

    public void ButtonPlay() {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }
        SceneManager.LoadScene(1);
        ScoreText.scoreValue = 0;
        ScoreText.old_scoreValue = 0;
        ControlCollider.health = 5;
    }
    public void ButtonMarket() {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }
        if (panelButton.activeSelf) {
            panelButton.SetActive(false);
            panelSettings.SetActive(false);
            panelMarket.SetActive(true);
        }
    }
    public void ButtonAyarlar()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }
        if (panelButton.activeSelf)
        {
            panelButton.SetActive(false);
            panelMarket.SetActive(false);
            panelSettings.SetActive(true);
        }
    }
    public void ButtonMarketGeriDon()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }
        if (panelMarket.activeSelf)
        { 
            panelSettings.SetActive(false);
            panelMarket.SetActive(false);
            panelButton.SetActive(true);
        }
    }
    public void ButtonMarketSatinAl()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }

        if (satin_al_text.text == "Etkinleştir") {
            PlayerPrefs.SetInt("SelectedPlayer",index);
            satin_al_text.text = "Etkin";
            satin_al_button.interactable = false;
        } else if (satin_al_text.text == "Satın al") { 
        int money = PlayerPrefs.GetInt("Money", 0);
        if (money >= price[index])
        {
            money -= price[index];
            satin_al_text.text = "Etkin";
            satin_al_button.interactable = false;
            PlayerPrefs.SetInt("Money", money);
            int[] buyed_players = PlayerPrefsX.GetIntArray("BuyedPlayers");
            int[] dizi = new int[buyed_players.Length+1];
            for (int i=0;i<buyed_players.Length;i++) {
                    dizi[i] = buyed_players[i];
            }
            dizi[buyed_players.Length] = index;
            PlayerPrefsX.SetIntArray("BuyedPlayers",dizi);
            PlayerPrefs.SetInt("SelectedPlayer", index);
            }
        }
        
    }
    public void ButtonMarketArrowLeft()
    {
        if (index>0) {
            if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
            {
                audioButtonPrressed.Play();
            }
            index--;
            image_player.sprite = sprites[index];
            price_text.text = price[index].ToString();
            
            int player = PlayerPrefs.GetInt("SelectedPlayer", 0);
            if (player == index)
            {
                satin_al_text.text = "Etkin";
                satin_al_button.interactable = false;
            }
            else
            {
                bool varmi = false;
                int[] buyed_players = PlayerPrefsX.GetIntArray("BuyedPlayers");

                for (int i=0;i<buyed_players.Length;i++) {
                    if (buyed_players[i]==index) {
                        varmi = true;
                        break;
                    }
                }
                if (varmi) {
                    satin_al_text.text = "Etkinleştir";
                    satin_al_button.interactable = true;
                }
                else { 
                satin_al_text.text = "Satın al";
                int money = PlayerPrefs.GetInt("Money", 0);
                if (money >= price[index])
                {
                    satin_al_button.interactable = true;
                }
                else
                {
                    satin_al_button.interactable = false;
                }
                }
                
            }
        }
        

    }
    public void ButtonMarketArrowRight()
    {
        if (index < sprites.Length-1)
        {
            if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
            {
                audioButtonPrressed.Play();
            }
            index++;
            image_player.sprite = sprites[index];
            price_text.text = price[index].ToString();

            int player = PlayerPrefs.GetInt("SelectedPlayer", 0);
            if (player == index)
            {
                satin_al_text.text = "Etkin";
                satin_al_button.interactable = false;
            }
            else
            {
                bool varmi = false;
                int []buyed_players = PlayerPrefsX.GetIntArray("BuyedPlayers");
                for (int i = 0; i < buyed_players.Length; i++)
                {
                    if (buyed_players[i] == index)
                    {
                        varmi = true;
                        break;
                    }
                }
                if (varmi)
                {
                    satin_al_text.text = "Etkinleştir";
                    satin_al_button.interactable = true;
                }
                else
                {
                    satin_al_text.text = "Satın al";
                    int money = PlayerPrefs.GetInt("Money", 0);
                    if (money >= price[index])
                    {
                        satin_al_button.interactable = true;
                    }
                    else
                    {
                        satin_al_button.interactable = false;
                    }
                }

            }
        }
    }
    public void ButtonAyarlarGeriDon()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }
        if (panelSettings.activeSelf)
        {
            panelSettings.SetActive(false);
            panelMarket.SetActive(false);
            panelButton.SetActive(true);
        }
    }
    public void ButtonAyarlarEfekt()
    {
        
        int a = PlayerPrefs.GetInt("EfekSes", 1);
        if (a == 1)
        {
            PlayerPrefs.SetInt("EfekSes", 0);
            button_efekt.text = "Efekt sesini aç";
        }
        else {
            PlayerPrefs.SetInt("EfekSes", 1);
            button_efekt.text = "Efekt sesini kapat";
            audioButtonPrressed.Play();
        }
    }
    public void ButtonAyarlarMusic()
    {
        
        if (PlayerPrefs.GetInt("EfekSes", 1)==1) {
            audioButtonPrressed.Play();
        }
        int b = PlayerPrefs.GetInt("MuzikSes", 1);
        if (b == 1)
        {
            PlayerPrefs.SetInt("MuzikSes", 0);
            button_muzik.text = "Müzik sesini aç";
            music1.Stop();
        }
        else
        {
            PlayerPrefs.SetInt("MuzikSes", 1);
            button_muzik.text = "Müzik sesini kapat";
            music1.Play();
        }

    }

    public void ButtonAyarlarGozleKontrol()
    {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }
        int c = PlayerPrefs.GetInt("GozleKontrol", 0);
        if (c == 1)
        {
            PlayerPrefs.SetInt("GozleKontrol", 0);
            button_gozle_kontrol.text = "Gözle kontrolü aç";
        }
        else
        {
            PlayerPrefs.SetInt("GozleKontrol", 1);
            button_gozle_kontrol.text = "Gözle kontrolü kapat";
        }

    }
    public void SliderDifficulty() {
        if (PlayerPrefs.GetInt("EfekSes", 1) == 1)
        {
            audioButtonPrressed.Play();
        }
        PlayerPrefs.SetInt("Zorluk", (int) slider.value);
    }
}
