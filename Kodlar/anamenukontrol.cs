using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anamenukontrol : MonoBehaviour
{
    public Text enyuksekskortext;
    public Text puantext;
    void Start()
    {
        int enyuksekskor = PlayerPrefs.GetInt("enyuksekpuankayit");
        enyuksekskortext.text = "En Yuksek Puan = " + enyuksekskor;
        int puangenel = PlayerPrefs.GetInt("puankayit");
        puantext.text = "Puaniniz = " + puangenel;
    }

    
    void Update()
    {
        
    }
    public void oyunaGit()
    {
        SceneManager.LoadScene("level1");
    }
    public void oyundanCik()
    {
        Application.Quit();
    }
}
