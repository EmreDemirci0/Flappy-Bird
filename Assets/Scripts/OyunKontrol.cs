using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour
{
    public GameObject gokyuzuBir;
    public GameObject gokyuzuİki;
    public float Arkaplan_Hız = 2f;

    Rigidbody2D fizikBir;
    Rigidbody2D fizikİki;

    float uzunluk = 0;

    public GameObject engel;
    public int kacAdetEngel = 5;
    GameObject[] engeller;
    float degisimZaman = 0;
    int sayac = 0;
    public float BoruAralık = 1f;
    AudioSource sourcwe;

    bool oyunBittiMi = false;

    void Start()
    {
        sourcwe = GetComponent<AudioSource>();
        fizikBir = gokyuzuBir.GetComponent<Rigidbody2D>();
        fizikİki = gokyuzuİki.GetComponent<Rigidbody2D>();

        fizikBir.velocity = new Vector2(-Arkaplan_Hız+0.001f, 0);
        fizikİki.velocity = new Vector2(-Arkaplan_Hız, 0);

        uzunluk = gokyuzuBir.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kacAdetEngel];
        for (int i = 0; i < engeller.Length; i++)
        {
            /***??**/
            engeller[i] = Instantiate(engel, new Vector2(-20, -20), Quaternion.identity);
            Rigidbody2D fizikEngel = engeller[i].AddComponent<Rigidbody2D>();
            fizikEngel.gravityScale = 0;
            fizikEngel.velocity = new Vector2(-Arkaplan_Hız, 0);
        }

    }
    void Update()
    {
        if (oyunBittiMi == false)
        {
            if (gokyuzuBir.transform.position.x <= -uzunluk)
            {
                gokyuzuBir.transform.position += new Vector3(uzunluk * 2, 0);
            }
            else if (gokyuzuİki.transform.position.x <= -uzunluk)
            {
                gokyuzuİki.transform.position += new Vector3(uzunluk * 2, 0);
            }
            /**********************************************************************/
            degisimZaman += Time.deltaTime;
            if (degisimZaman > BoruAralık)
            {
                degisimZaman = 0;
                //if (sayac < engeller.Length)
                //{
                float Y_eksenim = Random.Range(-3f, -1.05f);
                engeller[sayac].transform.position = new Vector3(9.3f, Y_eksenim);
                sayac++;
                //}
                //else
                //  sayac = 0;
                if (sayac >= engeller.Length)
                {
                    sayac = 0;
                }
            }
        }
        else
        {
            sourcwe.Stop();
        }
    }
    public void oyunbitti()
    {
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            fizikBir.velocity = Vector2.zero;
            fizikİki.velocity = Vector2.zero;

        }
        oyunBittiMi = true;
    }

}





