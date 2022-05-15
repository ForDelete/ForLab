using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TrashScr : MonoBehaviour
{
    public Camera CameraObj;
    public Slider SliderObj;

    public TextMeshProUGUI ScoreTMP;
    public int Score = 0;
    public float Speed = 1;

    private Vector3 MousePos;
    private Vector3 CurrentTarget;
    private Vector3 ScaleVector;

    Vector3[] PointList;
    int PointCounter = 0;
    bool Queue = false;
    bool GoToBox;

    void AddPoint(Vector3 Coordinates)
    {
        print("Координаты пришли:" + PointCounter + " " + Coordinates.ToString());
        PointList = new Vector3[PointCounter + 1];//Я не вдуплил почему +1...
        PointList[PointCounter] = Coordinates;
        PointCounter++;
        if (Queue == false) Queue = true;//Я не знаю, что сильнее влияет, проверка или замена.
    }
    void RemPoint()
    {
        CurrentTarget = PointList[PointCounter - 1];
        PointCounter--;
        print("Координаты удалены:" + PointCounter + " Текущие координаты:" + CurrentTarget.ToString());
        if (PointCounter == 0) Queue = false;
    }

    private void Start()
    {
        ScoreTMP.text = Score.ToString("0");
        //MousePos = this.transform.position;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //AddPoint(Input.mousePosition);
            //MousePos = CameraObj.WorldToScreenPoint(Input.mousePosition);
            MousePos = Input.mousePosition;
            //MousePos = CameraObj.WorldToScreenPoint(CameraObj.transform.position);//Получили координаты.
            print(transform.position + " " + MousePos);
            Score++; ScoreTMP.text = Score.ToString();//Хуйня

            GoToBox = true;
        }

        if (Input.GetKeyDown(KeyCode.W)) ChangeScale(0.1f);
        if (Input.GetKeyDown(KeyCode.S)) ChangeScale(-0.1f);
        if ((Input.GetKeyDown(KeyCode.D)) && (Queue == true)) RemPoint();
    }

    private void FixedUpdate()
    {
        if (GoToBox)
        {
            transform.position = (MousePos) * Speed * Time.deltaTime;
            //if ((MousePos - transform.position).sqrMagnitude < 0.01f) GoToBox = false;
        }
        //this.transform.position += MousePos.normalized * Speed * Time.deltaTime;
        //this.transform.position -= (this.transform.position-MousePos.normalized*Speed*Time.deltaTime);
        //transform.position -= (MousePos).normalized * Speed * Time.deltaTime;
        //if ((this.transform.position - transform.position).sqrMagnitude < 0.01f)
        //{
        //    if(Queue==true)RemPoint();
        //}
        //if ((PlayerObj.transform.position - transform.position).sqrMagnitude < 0.01f) Destroy(gameObject);
    }

    public void SliderUpdate()
    {
        Speed = SliderObj.value;
    }

    public void ChangeScale(float Value)
    {
        if (this.transform.localScale.x > 0.1)//Баги. Переделай
        {
            ScaleVector = new Vector3(Value, Value, 0);
            this.transform.localScale += ScaleVector;
        }
    }
}
