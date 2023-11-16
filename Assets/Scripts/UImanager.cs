using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    AudioSource aud;
    public AudioClip knopka;
    public bool sound;
    public Image img;
    public Sprite soundOn;
    public Sprite soundOff;
    public GameObject panel;
    public GameObject ButtonPause;
    public GameObject ButtonClick;
    public GameObject ButtonUpgradeW;
    public GameObject ButtonUpgradeM;
    public GameObject ButtonMenu;
    public GameObject ButtonRestart;
    public Text T_Completed;
    public GameObject T_Hint;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        sound = true;
        T_Completed.text = "";
        ButtonPause.SetActive(true);
        ButtonClick.SetActive(true);
        ButtonUpgradeW.SetActive(true);
        ButtonUpgradeM.SetActive(true);
        ButtonMenu.SetActive(false);
        ButtonRestart.SetActive(false);
        Time.timeScale = 1f;
        if (AudioListener.volume == 0f)
        {
            img.sprite = soundOff;
            sound = false;
        }
    }

    public void Win()
    {
        Time.timeScale = 0f;
        ButtonPause.SetActive(false);
        ButtonClick.SetActive(false);
        ButtonUpgradeW.SetActive(false);
        ButtonUpgradeM.SetActive(false);
        T_Completed.text = "Победа, все зомби были уничтожены";
        ButtonMenu.SetActive(true);
        ButtonRestart.SetActive(true);
    }

    public void Loose()
    {
        Time.timeScale = 0f;
        ButtonPause.SetActive(false);
        ButtonClick.SetActive(false);
        ButtonUpgradeW.SetActive(false);
        ButtonUpgradeM.SetActive(false);
        T_Completed.text = "Поражение, все люди превратились в зомби";
        ButtonMenu.SetActive(true);
        ButtonRestart.SetActive(true);
    }

    public void Pause()
    {
        ButtonClick.SetActive(false);
        ButtonUpgradeW.SetActive(false);
        Time.timeScale = 0f;
        GetComponent<Click>().save();
        panel.SetActive(true);
    }

    public void resume()
    {
        ButtonClick.SetActive(true);
        ButtonUpgradeW.SetActive(true);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void exit()
    {
        SceneManager.LoadScene("menu");
    }

    public void newGame()
    {
        GetComponent<Click>().ResetData();
        SceneManager.LoadScene("game");
    }

    public void continueGame()
    {
        SceneManager.LoadScene("game");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void Sound()
    {
        if (!sound)
        {
            img.sprite = soundOn;
            AudioListener.volume = 1f;
            sound = true;
        }
        else
        {
            img.sprite = soundOff;
            AudioListener.volume = 0f;
            sound = false;
        }
    }

    public void Hint()
    {
        T_Hint.SetActive(true);
    }

}