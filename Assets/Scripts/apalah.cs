using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class apalah : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GameObject Tuas;
    public GameObject ntorpedo;
    public Transform torpedospawn;
    public bool kendaliMode;
    public Text modeKendali;
    public float accelSpeed;
    public float accelVerSpeed;
    public float curSpeed;
    public float curVerSpeed;
    public float maxSpeed;
    public float maxVerSpeed;
    public float clockwise;
    public float counterClockwise;
    public float upRotation;
    public float downRotation;
    public int tembak;
    public int amunisi;
    public Text Amunisi;
    public int scoreInt;
    public Text Score;
    public float Depth;
    public Text Kedalaman;
    public Text Speed;
    public float StartHealth = 100;
    public float nyawa;
    public Image Health;
    public bool moveBackward;
    public bool isIdle;
    public bool isCouroutineStarted = false;
    public Canvas GameOver;
    public Canvas MainGame;
    public Text GameOverScore;
    static float t = 0f;

    private Transform selfTransform;
    [SerializeField]
    float subB, subN, sub, subM, subT;
    KendaliSubMultiThread kendaliSubMultiThread;
    LevelManager levelmanager;
    bool Thread = true;


    // Use this for initialization
    void Start()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;

        accelSpeed = 5f;
        accelVerSpeed = 1f;
        maxSpeed = 15f;
        maxVerSpeed = 5f;
        clockwise = 10f;
        counterClockwise = -10f;
        isIdle = true;

        selfTransform = transform;
        amunisi = 0;

        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        levelmanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManager>();
        kendaliSubMultiThread = GetComponent<KendaliSubMultiThread>();

        GameOver.enabled = false;

        kendaliMode = true;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mines")
        {
            //Reset orientation
            ResetOritentation();
            //TakeDamage
            TakeDamage(50);

            Destroy(other.gameObject);
        }
    }

    void ResetOritentation()
    {
        Quaternion targetRotation = Quaternion.identity;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30f * Time.deltaTime);
        //transform.rotation = Quaternion.identity;
    }

    public void TakeDamage(float amount)
    {
        nyawa -= amount;
        Health.fillAmount = nyawa / StartHealth;
        if (nyawa <= 0)
        {
            nyawa = 0;
            GameOvers();
        }
    }

    public void GameOvers()
    {
        kendaliSubMultiThread.CloseConnection();
        GameOver.enabled = true;
        GameOverScore.text = scoreInt.ToString();

        Time.timeScale = 0;
    }

    void ToggleKendaliMode()
    {
        if (kendaliMode != null)
        {
            kendaliMode = !kendaliMode;
        }
    }

    void StartAnotherThread()
    {
        if (Thread == true)
        {
            kendaliSubMultiThread.StartThreading();
            Thread = false;
        }
    }

    void StopAnotherThread()
    {
        if (Thread == false)
        {
            kendaliSubMultiThread.StopThreadLoop();
            Thread = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleKendaliMode();
        }

        if (kendaliMode)
        {
            modeKendali.text = "Kendali Keyboard";
            KendaliBiasa();
            StopAnotherThread();
        }
        else
        {
            modeKendali.text = "Kendali Arduino";
            KendaliArduino();
            StartAnotherThread();
        }

        Depth = (43.6f - transform.position.y);
        Depth = Depth * 10;

        if (Depth <= 0) Depth = 0;
        if (Depth >= 366) Depth = Depth;
        
        Amunisi.text = amunisi.ToString();
        Score.text = scoreInt.ToString();
        Kedalaman.text = Depth.ToString();
        Speed.text = Mathf.Abs (curSpeed).ToString();
    }

    void KendaliBiasa()
    {
        Vector3 curRotTuas1 = Tuas.transform.rotation.eulerAngles;
        Quaternion TuasRot = Quaternion.Euler(curRotTuas1);

        if (Input.GetKey(KeyCode.W))
        {
            isIdle = false;

            curSpeed += accelSpeed * Time.deltaTime;
            if (curSpeed >= maxSpeed) curSpeed = maxSpeed;

            transform.position += transform.forward * Time.deltaTime * curSpeed;

            moveBackward = false;

            //Quaternion NewTuasRot = Quaternion.Euler(0, 0, curSpeed / maxSpeed * 82f);
            
            float targetRot;
            Quaternion NewTuasRot;
            if (curSpeed <= 0)
            {
                targetRot = 360 + (Mathf.Abs(curSpeed) / Mathf.Abs(maxSpeed) * -75f);
                NewTuasRot = Quaternion.Euler(0, 0, targetRot);
            } else
            {
                NewTuasRot = Quaternion.Euler(0, 0, curSpeed / maxSpeed * 82f);
            }

            if (!isCouroutineStarted && TuasRot != NewTuasRot)
            {
                StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f * curSpeed / maxSpeed));
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isIdle = false;

            curSpeed -= accelSpeed * Time.deltaTime;
            if (curSpeed <= -maxSpeed) curSpeed = -maxSpeed;

            transform.position += transform.forward * Time.deltaTime * curSpeed;

            moveBackward = true;
            
            float targetRot;
            Quaternion NewTuasRot;
            if (curSpeed <= 0)
            {
                targetRot = 360 + (Mathf.Abs(curSpeed) / Mathf.Abs(maxSpeed) * -75f);
                NewTuasRot = Quaternion.Euler(0, 0, targetRot);
            } else
            {
                NewTuasRot = Quaternion.Euler(0, 0, curSpeed / maxSpeed * 82f);
            }

            if (!isCouroutineStarted && TuasRot != NewTuasRot)
            {
                StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
            }
        }
        else
        {
            isIdle = true;

           // curSpeed -= accelSpeed * Time.deltaTime;
            //if (curSpeed <= 0) curSpeed = 0;
            if (moveBackward == false && curSpeed >= 0 ) 
            {
                curSpeed -= accelSpeed * Time.deltaTime;
                transform.position += transform.forward * Time.deltaTime * curSpeed;
            }
            else if (moveBackward == true  && curSpeed <= 0)
            {
                curSpeed += accelSpeed * Time.deltaTime;
                transform.position += transform.forward * Time.deltaTime * curSpeed;
            }

            Quaternion NewTuasRot = Quaternion.Euler(0, 0, 0);

            if (!isCouroutineStarted && TuasRot != NewTuasRot)
            {
                StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, Time.deltaTime * counterClockwise, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, Time.deltaTime * clockwise, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, Time.deltaTime * clockwise, 0);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, Time.deltaTime * counterClockwise, 0);
        }

        if (transform.position.y <= 43.6f && transform.position.y >= 7f)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                curVerSpeed += accelVerSpeed * Time.deltaTime;
                if (curVerSpeed >= maxVerSpeed) curVerSpeed = maxVerSpeed;

                transform.position += Vector3.up * Time.deltaTime * curVerSpeed;
                
                upRotation += Time.deltaTime * -5f;
                upRotation = Mathf.Clamp (upRotation, -15f, 15f);
                transform.localEulerAngles = new Vector3(upRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
                t = 0.0f;

                if ( transform.position.y > 43.6f)  transform.position = new Vector3(transform.position.x,  43.6f, transform.position.z);
                if ( transform.position.y <= 7f)  transform.position = new Vector3(transform.position.x, 7f, transform.position.z);
                
            }else if (Input.GetKey(KeyCode.DownArrow) ){
                
                curVerSpeed -= accelVerSpeed * Time.deltaTime;
                if (curVerSpeed <= -maxVerSpeed) curVerSpeed = -maxVerSpeed;

                transform.position += Vector3.down * Time.deltaTime * Mathf.Abs(curVerSpeed);

                upRotation += Time.deltaTime * 5f;
                upRotation = Mathf.Clamp (upRotation, -15f, 15f);
                transform.localEulerAngles = new Vector3(upRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
                t = 0.0f;

                if ( transform.position.y < 7f)  transform.position = new Vector3(transform.position.x, 7f, transform.position.z);
                if ( transform.position.y >= 43.6f)  transform.position = new Vector3(transform.position.x,  43.6f, transform.position.z);
      
            }else 
            {

                curVerSpeed = Mathf.Lerp (curVerSpeed, 0f, 1);
                
                if (transform.localEulerAngles.x != 0.0f)
                {
                    t += Time.deltaTime * 0.02f;
                    
                    upRotation = Mathf.Lerp (upRotation, 0f, t);
                    transform.localEulerAngles = new Vector3(upRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
                    if (t >= 1f) t = 0f;
                    Debug.Log (t);
                }else {
                    t = 0f;
                }        
           }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FireTorpedo());
        }
    }

    void KendaliArduino()
    {
        subB = kendaliSubMultiThread.subB;
        subN = kendaliSubMultiThread.subN;
        sub = kendaliSubMultiThread.sub;
        subM = kendaliSubMultiThread.subM;
        subT = kendaliSubMultiThread.subT;

        //float subClamp = Mathf.Clamp(31, 0f, 1f)x;
        float subNormalized = sub / 31f;
        subNormalized = Mathf.Round(subNormalized * 100f) / 100f;
        float subMNormalized = subM / 30f;
        subNormalized = Mathf.Round(subNormalized * 100f) / 100f;

        float maxSpeedClamp = maxSpeed * subNormalized;
        float maxSpeedMClamp = maxSpeed * subMNormalized;

        Vector3 curRotTuas = Tuas.transform.rotation.eulerAngles;
        Quaternion TuasRot = Quaternion.Euler(curRotTuas);

        //both
        if (sub > 0 && subM > 0)
        {
            if (sub == subM)
            {
                curSpeed = 0;

                Quaternion NewTuasRot = Quaternion.Euler(0, 0, 0);
                if (!isCouroutineStarted && TuasRot != NewTuasRot)
                {
                    StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
                }
            }
            else if (sub > subM)
            {
                float difmaxspeedclamp = maxSpeed * (subNormalized - subMNormalized);
                float difcurspeedclamp = subNormalized - subMNormalized;

                curSpeed += accelSpeed * Time.deltaTime;
                if (curSpeed >= difmaxspeedclamp) curSpeed = difmaxspeedclamp;

                transform.position += transform.forward * Time.deltaTime * curSpeed;

                Quaternion NewTuasRot = Quaternion.Euler(0, 0, difcurspeedclamp * 82f);
                if (!isCouroutineStarted && TuasRot != NewTuasRot)
                {
                    StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
                }

                moveBackward = false;
            }
            else if (sub < subM)
            {
                float difmaxspeedclamp1 = maxSpeed * (subMNormalized - subNormalized);
                float difcurspeedclamp = subMNormalized - subNormalized;

                curSpeed += accelSpeed * Time.deltaTime;
                if (curSpeed >= difmaxspeedclamp1) curSpeed = difmaxspeedclamp1;

                transform.position -= transform.forward * Time.deltaTime * curSpeed;
                float targetRot = 360 + (subMNormalized * -75f);

                Quaternion NewTuasRot = Quaternion.Euler(0, 0, targetRot);
                if (!isCouroutineStarted && TuasRot != NewTuasRot)
                {
                    StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
                }

                moveBackward = true;
            }
        }
        //maju tok
        else if (sub > 0)
        {
            curSpeed += accelSpeed * Time.deltaTime;
            if (curSpeed >= maxSpeedClamp) curSpeed = maxSpeedClamp;

            transform.position += transform.forward * Time.deltaTime * curSpeed;

            Quaternion NewTuasRot = Quaternion.Euler(0, 0, subNormalized * 82f);

            if (!isCouroutineStarted && TuasRot != NewTuasRot)
            {
                StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
            }

            moveBackward = false;

        }
        else if (subM > 0)
        {
            curSpeed += accelSpeed * Time.deltaTime;
            if (curSpeed >= maxSpeedMClamp) curSpeed = maxSpeedMClamp;

            transform.position -= transform.forward * Time.deltaTime * curSpeed;

            float targetRot = 360 + (subMNormalized * -75f);
            Quaternion NewTuasRot = Quaternion.Euler(0, 0, targetRot);

            if (!isCouroutineStarted && TuasRot != NewTuasRot)
            {
                StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
            }

            moveBackward = true;
        }
        else
        {
            if (moveBackward == false && curSpeed >= 0 ) 
            {
                curSpeed -= accelSpeed * Time.deltaTime;
                transform.position += transform.forward * Time.deltaTime * curSpeed;
            }
            else if (moveBackward == true  && curSpeed <= 0)
            {
                curSpeed += accelSpeed * Time.deltaTime;
                transform.position += transform.forward * Time.deltaTime * curSpeed;
            }
            
            Quaternion NewTuasRot = Quaternion.Euler(0, 0, 0);
            if (!isCouroutineStarted && TuasRot != NewTuasRot)
            {
                StartCoroutine(RotateTuas(TuasRot, NewTuasRot, 1f));
            }
        }

        //LEFT OR RIGHT
        if (subB > 5)
        {
            transform.Rotate(0, Time.deltaTime * clockwise, 0);
        }
        else if (subB < -5)
        {
            transform.Rotate(0, Time.deltaTime * counterClockwise, 0);
        }

        //UP OR DOWN
        if (subN > 5 && transform.position.y <= 43.6f)
        {
            curVerSpeed += accelVerSpeed * Time.deltaTime;
            if (curVerSpeed >= maxVerSpeed) curVerSpeed = maxVerSpeed;

            transform.position += Vector3.up * Time.deltaTime * curVerSpeed;
            t = 0.0f;
        }
        else if (subN < -5 && transform.position.y >= 7f)
        {
            curVerSpeed -= accelVerSpeed * Time.deltaTime;
            if (curVerSpeed <= -maxVerSpeed) curVerSpeed = -maxVerSpeed;

            transform.position += Vector3.down * Time.deltaTime * Mathf.Abs(curVerSpeed);
            t = 0.0f;
        }
        else 
        {
            curVerSpeed = Mathf.Lerp (curVerSpeed, 0f, 1);
            
            if (transform.localEulerAngles.x != 0.0f)
            {
                t += Time.deltaTime * 0.02f;
                
                upRotation = Mathf.Lerp (upRotation, 0f, t);
                transform.localEulerAngles = new Vector3(upRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
                if (t >= 1f) t = 0f;
                Debug.Log (t);
            }else {
                t = 0f;
            }        
        }

        if (subT == 1)
        {
            StartCoroutine(FireTorpedo());
        }
    }

    IEnumerator RotateTuas(Quaternion _start, Quaternion _targetRot, float _duration)
    {
        Vector3 curRotTuas = Tuas.transform.localRotation.eulerAngles;
        curRotTuas.z = Mathf.Clamp(curRotTuas.z, -75f, 82f);

        isCouroutineStarted = true;

        float startTime = Time.time;
        float endTime = startTime + _duration;
        //float elapsed = 0f;
        Tuas.transform.rotation = _start;

        yield return null;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / _duration;
            Tuas.transform.rotation = Quaternion.Slerp(_start, _targetRot, progress);

            yield return null;
        }

        Tuas.transform.rotation = _targetRot;
        isCouroutineStarted = false;
    }

    IEnumerator FireTorpedo()
    {
        if (amunisi > 0)
        {
            GameObject torpedo = (GameObject)Instantiate(ntorpedo, torpedospawn.position, torpedospawn.rotation);

            torpedo.GetComponent<Rigidbody>().velocity = torpedo.transform.forward * 20f;

            amunisi--;

            yield return new WaitForSeconds(2);
        }
        else
        {
            yield break;
        }
    }
}