using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunkontrol : MonoBehaviour
{
    public GameObject gokyuzu1; //unity üzerinden erişebilmek için public tanımladık.
    public GameObject gokyuzu2;
    public float arkaplanHiz = -1.5f;

    Rigidbody2D fizikBir;  //2sine de ayrı ayrı rigidbody verdik.
    Rigidbody2D fizikIki;
    Rigidbody2D fizikengel;

    float uzunluk = 0;

    public GameObject engel; // engeli unityde sürükle bırak yapıcaz.
    public int kacAdetengel = 5;
    GameObject[] engeller; //engeller dizisi tanımlandı.

    float degisimZaman = 0;
    int sayac = 0;
    bool oyunsonaerdi = true;

    void Start()
    {
        fizikBir = gokyuzu1.GetComponent<Rigidbody2D>(); // rigidbodylerine eriştik.
        fizikIki = gokyuzu2.GetComponent<Rigidbody2D>();

        fizikBir.velocity = new Vector2(arkaplanHiz, 0); // ekranın hareket etmesini sağladık.
        fizikIki.velocity = new Vector2(arkaplanHiz, 0);

        uzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x; // boxcollider'in uzunluğuna eriştik.

        engeller = new GameObject[kacAdetengel]; // engeller dizisine kacadetengel varsa onu atadık.

        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-20, -20),Quaternion.identity);
            fizikengel = engeller[i].GetComponent<Rigidbody2D>();
            fizikengel.velocity = new Vector2(arkaplanHiz, 0);
        }
    }


    void Update()
    {
        if (oyunsonaerdi)
        {
            if (gokyuzu1.transform.position.x <= -uzunluk)// burada gokyuzunun uzunluğu boxcolliderin uzunluğundan kücükse uzunluğu 2ile çarpıp başa ekler.
            {
                gokyuzu1.transform.position += new Vector3(uzunluk * 2, 0, 0);
            }
            if (gokyuzu2.transform.position.x <= -uzunluk)
            {
                gokyuzu2.transform.position += new Vector3(uzunluk * 2, 0, 0);
            }

            // --------------------------------------------------------------------------------------

            degisimZaman += Time.deltaTime;
            if (degisimZaman > 2f)
            {
                degisimZaman = 0;
                float Yeksenim = Random.Range(-6f, -2.2f);
                engeller[sayac].transform.position = new Vector3(13, Yeksenim);
                sayac++;
                if (sayac >= engeller.Length)
                {
                    sayac = 0;
                }
            }
        }
        
    }
        public void oyunbitti()
        {
            for (int i = 0; i < engeller.Length; i++)
            {
                engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                fizikBir.velocity = Vector2.zero;
                fizikIki.velocity = Vector2.zero;
            }
        oyunsonaerdi = false;
    }

    }


