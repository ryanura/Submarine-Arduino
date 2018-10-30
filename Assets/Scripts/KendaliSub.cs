using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.Threading;

public class KendaliSub : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM4", 500000);

    public float subB; //belok  
    public float subN; //naik
    public float subM; //mundur
    public float sub; //maju
    public float subT; //tembak
    public GameObject ntorpedo;
    public Transform torpedospawn;

    public float Speed;
    public Text speed;
    
    public Text verticalAngle;
    public float VertAngle;

    public Text horizontalAngle;
    public float HorAngle;

    public Text Amunisi;
    public int AmunitionInt;

    public Text Poin;
    public int Point;

    public float depth;
    public Text dalam;  


    void Start () {
        speed.text = sub.ToString();
        verticalAngle.text = subB.ToString();
        AmunitionInt = 0;
        Amunisi.text = AmunitionInt.ToString();
        Poin.text = Point.ToString();
        selfTransform = transform;
        serial.Open();
        StartCoroutine(ReadDataFromSerialPort());
        dalam.text = depth.ToString();
        
    }

    IEnumerator ReadDataFromSerialPort()
    {
        while (true)
        {
            string[] values = serial.ReadLine().Split(',');
            subB = (float.Parse(values[0]));
            subN = (float.Parse(values[1]));
            sub = (float.Parse(values[2]));
            subM = (float.Parse(values[3]));            
            subT = (float.Parse(values[4]));
            yield return new WaitForSeconds(0.2f);
        }
    }
    void Update()
    {
        Poin.text = Point.ToString();
        Amunisi.text = AmunitionInt.ToString();
        dalam.text = depth.ToString();

        depth = (43.6f - transform.position.y);
        depth = depth * 10;

        if (depth <= 0)
        {
            depth = 0;
        }
        if (depth >= 366)
        {
            depth = depth;
        }

        //maju
        //mentrigger perubahan sprite yang awalnya stop menjadi ke slow/half/full tergantung output dari slide potentio
        Speed = sub * 13 / 30;
        speed.text = Speed.ToString();
        transform.Translate(transform.forward * sub * Time.deltaTime/10);

        //mundur
        //mentrigger perubahan sprite yang awalnya stop menjadi ke slow/half/full tergantung output dari slide potentio
        transform.Translate(-transform.forward * subM * Time.deltaTime/10);

        //belok-----> menggerakkan wp_ss_romeo_rudder.001 searah dengan arah kemudi digerakkan
        VertAngle = subB;
        verticalAngle.text = VertAngle.ToString();
        transform.localEulerAngles = new Vector3(0, subB, 0);

        //naik----> menggerakkan wp_ss_romeo_sternplanes.005, wp_ss_romeo_sternplanes.008, wp_ss_romeo_sternplanes.006, 
        //wp_ss_romeo_sternplanes.007,sesuai gerakan kapal/kemudi kapal
        HorAngle = subN;
        horizontalAngle.text = HorAngle.ToString();
        
        if (transform.position.y >= 43.6f)
        {
            if (subN < 0)
            {
                transform.Translate(transform.up * subN * Time.deltaTime / 10);
            }
        }
        else if(transform.position.y <= 7f)
        {
            if (subN > 0)
            {
                transform.Translate(transform.up * subN * Time.deltaTime / 10);
            }
            
        }
        else
        {
            transform.Translate(transform.up * subN * Time.deltaTime / 10);
            
        }
            

        Amunisi.text = AmunitionInt.ToString();
        //tembak----------> harusnya bisa menembak ketika mendapatkan amunisi torpedo, sehingga nilai awal torpedo=0
        if (subT == 1)
        {
            if (AmunitionInt > 0)
            {
                Fire();
                AmunitionInt--;
            }
        }

        Poin.text = Point.ToString();
    }

    void Fire()
    {
        GameObject torpedo = (GameObject)Instantiate(ntorpedo, torpedospawn.position, torpedospawn.rotation);

        torpedo.GetComponent<Rigidbody>().velocity = torpedo.transform.forward * 35f;

        //transform.Translate(Vector3.forward * torpedo * Time.deltaTime);
    }


    public Transform selfTransform { get; set; }


}