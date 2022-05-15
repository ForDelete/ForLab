using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera CameraObj;
    public Slider SliderObj;
    public TextMeshProUGUI ScoreTMP;

    public short Score = 0;
    public float Speed = 1;

    Vector3[] PointList = new Vector3[25];//����� 25 �����
    int PointListLen;//�� �������� �� short

    public byte PointCounter = 0;
    public byte CurrentPoint = 0;
    byte QueueCount = 0;//���-�� ����� � �������

    Vector3 ScaleVector;

    //
    public TrajectoryScript TS;
    //

    private void Start()
    {
        PointListLen = PointList.Length-1;
        ScoreTMP.text = Score.ToString("0");//������� ���� �� ������� � ����� ���.
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //if(!EventSystem.current.IsPointerOverGameObject())
            //{
            //    //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //    if(QueueCount!=PointList.Length-1)
            //    {
            //        //ScoreTMP.text = (++Score).ToString();
            //        AddPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //    }
            //    else
            //    {
            //        Debug.Log("Queue owerflow!"+ PointCounter + " " + CurrentPoint);
            //    }
            //}

            foreach (Touch touch in Input.touches)
            {
                int pointerID = touch.fingerId;
                if (!EventSystem.current.IsPointerOverGameObject(pointerID))
                {
                    if (QueueCount != PointList.Length - 1)
                    {
                        //ScoreTMP.text = (++Score).ToString();
                        AddPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                    else
                    {
                        Debug.Log("Queue owerflow!" + PointCounter + " " + CurrentPoint);
                    }
                    return;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if(PointCounter!=CurrentPoint)//���� 
        {
            if(transform.position != PointList[CurrentPoint])//�� �����?
            {
                transform.position = Vector3.MoveTowards(transform.position, PointList[CurrentPoint], Speed/25);//���
            }
            else//������� �����, ��� ������.
            {
                RemPoint();
            }
        }
    }
    void AddPoint(Vector2 Coordinates)//�������� ������2, �� ����������� ��� ������3
    {
        //Debug.Log("����������["+PointCounter+"] ������:" + Coordinates.ToString());
        PointList[PointCounter++] = Coordinates;
        QueueCount++;
        
        if (PointCounter == 25) PointCounter = 0;//\\//
        //
        TS.AddNewPoint(Coordinates);
        //
    }
    void RemPoint()
    {
        //Debug.Log("����������[" + CurrentPoint + "] ����������:");
        CurrentPoint++;
        QueueCount--;
        if (CurrentPoint == 25) CurrentPoint = 0;//\\//
        //
        TS.RemoveOldPoint();
        //

        ScoreUpdate();
    }

    public void ChangeScale(float Value)
    {
        ScaleVector = new Vector3(Value, Value, 0);
        this.transform.localScale += ScaleVector;
    }

    public void SliderUpdate()
    {
        Speed = SliderObj.value;
    }
    public void ScoreUpdate()
    {
        ScoreTMP.text = (++Score).ToString();
    }
}
