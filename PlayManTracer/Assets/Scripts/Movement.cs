using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Численные блоки
    public float speed = 5f;                 //Скорость перемещения
    public float speedRotate = 150f;         //Скорость вращения
    public float speedRotateAir = 250f;      //Скорость вращения в воздухе
    public float speedRotateFront = 300f;    //Скорость вращения сальтушек
    //public float jump = 50f;

    public float RaycastDis = 0.65f;

    
    //Объектные блоки
    public Transform transformRaycast;

    public GameObject box;
    public TextMesh score;

    //Приватные блоки
    int scorecount = 0;
    float scoreplus = 0;
    float startSpeedRotate;
    float jumpPower = 0.5f;

    void Start() {
        startSpeedRotate = speedRotate;
    }

    void Update() {
        score.text = ("Score: " + scorecount + "\nPlus:" + scoreplus + "\nJump power: " + jumpPower);
        transform.position = box.transform.position;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speedRotate;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(0, 0, z);

        if(!Physics.Raycast(transformRaycast.position, -transform.up, RaycastDis)) {
            jumpPower = 0.5f;
            speedRotate = speedRotateFront;
            box.GetComponent<Transform>().Rotate(0, x, 0);
            if (x > 0.1 || x < -0.1) {
                scoreplus += Time.deltaTime * 2; // СЧЕТ
            }
        } else {
            transform.rotation = box.GetComponent<Transform>().rotation;
            transform.Translate(x, 0, 0);
            speedRotate = startSpeedRotate;
        }

        if(Physics.Raycast(transformRaycast.position, -transform.up, RaycastDis)) {
            scorecount += Mathf.RoundToInt(scoreplus);
            scoreplus = 0;    
        }
        //Debug.Log();
                                      // ВРАЩЕНИЕ \\

        if (!Physics.Raycast(transformRaycast.position, -transform.up, RaycastDis)) {
            if (Input.GetKey(KeyCode.Z)) {
                box.GetComponent<Transform>().Rotate(speedRotateFront * Time.deltaTime, 0, 0);
                scoreplus += Time.deltaTime * 3; // СЧЕТ
            }
            if (Input.GetKey(KeyCode.X)) {
                box.GetComponent<Transform>().Rotate(-speedRotateFront * Time.deltaTime, 0, 0);
                scoreplus += Time.deltaTime * 3; // СЧЕТ
            }
            if (Input.GetKey(KeyCode.C)) {
                GetComponent<Transform>().Rotate(0, 0, speedRotateFront * Time.deltaTime);
                scoreplus += Time.deltaTime * 3; // СЧЕТ
            }
            if (Input.GetKey(KeyCode.V)) {
                GetComponent<Transform>().Rotate(0, 0, -speedRotateFront * Time.deltaTime);
                scoreplus += Time.deltaTime * 3; // СЧЕТ
            }
        }
                                       // РЕСПАВН \\
        if (Input.GetKey(KeyCode.R)) {
            //transform.position = new Vector3(0, 0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 1);
            //box.GetComponent<Transform>().position = new Vector3(0, 0, 0);
            box.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 1);
            scoreplus = 0;
        }
        

        if (Input.GetKey("space")) {    //
            //Debug.Log("space");       //
            jumpPower += 0.035f;        //Расчетная сила прыжка
            if (jumpPower > 2.5f)       //
                jumpPower = 2.5f;       //

        }
        if (Input.GetKeyUp("space")) {
            if (Physics.Raycast(transformRaycast.position, -transform.up, RaycastDis)) {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpPower * 20, 0), ForceMode.Impulse); // Импульс(Прыжок)
            }
        }
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, jump * 20, 0), ForceMode.Impulse);

        Debug.DrawRay(transformRaycast.position, -transform.up * RaycastDis, Color.red);
    }
}
