using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kontrol : MonoBehaviour
{
    public Sprite[] kuşSprite;
    SpriteRenderer spriteRenderer;
    bool ileriKontrol = true;
    public float hiz_Tersorantılı;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;
    Rigidbody2D fizik;
    public int ZıplamaHız;

    int puan = 0;
    public Text PuanText;

    bool oyunbitti = false;

    OyunKontrol oyunKontrol;

    AudioSource[] sesler;
    public int enYuksekPuan = 0;
    public int score = 0;
    
  
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<OyunKontrol>();
       sesler = GetComponents<AudioSource>();
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit");
        score = PlayerPrefs.GetInt("puanKayit");
        Debug.Log("En Yuksek puan"+ enYuksekPuan);
    }

    // Update is called once per frame
    void Update()
    {    animasyonn();
        if ((Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Space)) && oyunbitti==false)
        {
            fizik.velocity = new Vector2(0,0);//Yerçekimi dolayısıyla artan hızı 0lar
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,ZıplamaHız));
            sesler[0].Play();
            
        }
        if (fizik.velocity.y>0)//Yani zıplarken
        {
            transform.eulerAngles = new Vector3(0,0,25);
        }
        else if (fizik.velocity.y < 0)//Yani düşerken
        {
            transform.eulerAngles = new Vector3(0, 0,-25);
        }
        

    }
   void animasyonn()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > hiz_Tersorantılı)
        {
            kusAnimasyonZaman = 0;
            if (ileriKontrol == true)
            {
                spriteRenderer.sprite = kuşSprite[kusSayac];// 0 1 2
                kusSayac++;
                if (kusSayac == kuşSprite.Length)
                {
                    kusSayac--; //2
                    ileriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = kuşSprite[kusSayac];//1 0 
                if (kusSayac == 0)
                {
                    //kusSayac++;
                    ileriKontrol = true;
                    //Debug.Log("Sayaç=" + kusSayac);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag==("puan"))
        {
            puan++;
            PuanText.text = puan.ToString();
            sesler[1].Play();

        }
        if (col.gameObject.tag == "engel") 
        {   

            oyunbitti = true;
            oyunKontrol.oyunbitti();
            sesler[2].Play();
            GetComponent<CapsuleCollider2D>().enabled =false;
            if (puan>enYuksekPuan)
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("enYuksekPuanKayit",enYuksekPuan);

            }
            Invoke("anaMenuyeDon",2);
            //2 sn sonra anaMenuyeDon metodunu çağır

   //         SceneManager.LoadScene("AnaMenu");


        }
    }
    void anaMenuyeDon()
    {
        PlayerPrefs.SetInt("puanKayit",puan);
        SceneManager.LoadScene("AnaMenu");
    }
}




