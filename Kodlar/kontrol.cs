using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kontrol : MonoBehaviour
{
    public Sprite[] kusSprite; // dizi tanımladık birden fazla resim var.
    SpriteRenderer spriteRenderer; //component almak için tanımlandı rigidbody hatırla.
    bool ileriGeriKontrol = true; //kuş kanat çırpışı kontrol
    int kusSayac = 0; //fotoğraflar için tanımladığımız diziyi kontrol ediyoruz.
    float kusAnimasyonZaman = 0; //kanat çırpış hızı ayarlamak için tanımladık.
    Rigidbody2D fizik; //rigidbody tanımladık. hareket için.
    int puan = 0;
    public Text puantext;
    bool oyunbitti = true;
    AudioSource []sesler;
    int enyuksekPuan = 0;
    

    oyunkontrol oyunkontrol;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //rigidbodyde aldığımız gibi componente ulaştık.
        fizik = GetComponent<Rigidbody2D>();
        oyunkontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<oyunkontrol>();
        sesler = GetComponents<AudioSource>();
        enyuksekPuan = PlayerPrefs.GetInt("enyuksekpuankayit");
        

        Debug.Log(enyuksekPuan);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && oyunbitti) // mouse sol tıklandığında
        {
            fizik.velocity=(new Vector2(0, 0)); // hizi veya yerçekimini sıfır yaptık.
            fizik.AddForce(new Vector2(0,200)); //sonra kuvvet uyguladık.
            sesler[0].Play();
            
        }
        if (fizik.velocity.y > 0) // hızımız 0'dan büyükse 
        {
            transform.eulerAngles=(new Vector3(0, 0, 45)); // kuşun kafasını yukarı kaldır
        }
        else // hızımız 0'dan küçükse 
        {
            transform.eulerAngles = (new Vector3(0, 0, -45)); //kuşun kafasını aşağı eğ
        }
        Animasyon(); // animasyon fonk. çağrıldı.
    }
    void Animasyon()
    {
        kusAnimasyonZaman += Time.deltaTime; // kanat çırpış hızına normal 0.2 0.3lü zaman eklendi.
        if (kusAnimasyonZaman > 0.1f) // burayı bu şekilde yaptık ki 0.1den büyük olunca içeri girip 0 yapıp tekrar time deltatime donsün hızlansın diye.
            
        {
            kusAnimasyonZaman = 0; 
            if (ileriGeriKontrol)
            {
                spriteRenderer.sprite = kusSprite[kusSayac]; //kus sprite dizisine ulaştık.
                kusSayac++; 
                if (kusSayac == kusSprite.Length) //kuş sayaç sprite dizisi uzunluğuna ulaşınca 1 eksilttik ve kanatları durdurduk.
                {
                    kusSayac--;
                    ileriGeriKontrol = false;
                }
            }
            else
            {
                kusSayac--; //buraya gelince sayaç en son dizinin uzunluğunda olduğundan 1 eksilttik ve tekrar kanat çırpmaya başlattık.
                spriteRenderer.sprite = kusSprite[kusSayac];
                if (kusSayac == 0) // kus sayac 0 olduğunda tekrar sayacı çalıştırdık ve kanatlar çalışmaya devam etti.
                {
                    kusSayac++;
                    ileriGeriKontrol = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag=="puan")
        {
            puan++;
            sesler[2].Play();
            puantext.text = "Puan: " + puan + "";
        }
        if (coll.gameObject.tag == "engel")
        {
            oyunbitti = false;
            sesler[1].Play();
            sesler[3].Play();
            oyunkontrol.oyunbitti();

            GetComponent<CircleCollider2D>().enabled = false;

            if (puan > enyuksekPuan)
            {
                enyuksekPuan = puan;
                PlayerPrefs.SetInt("enyuksekpuankayit", enyuksekPuan);
            }
            Invoke("anaMenuyeDon", 2);
        }
        

    }
    public void anaMenuyeDon()
    {
        PlayerPrefs.SetInt("puankayit", puan);
        SceneManager.LoadScene("AnaMenu");
    }
}
