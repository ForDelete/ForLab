using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryScript : MonoBehaviour
{
    private LineRenderer LR;
    Vector3[] points = new Vector3[25];
    public int LastPoint = 1;
    void Start()
    {
        LR = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        points[0] = this.transform.position;
        ShowLine();
    }
    public void ShowLine()//Vector3 Start, Vector3 End, int Queue)
    {
        //points[LastPoint++] = Start;
        //points[LastPoint++] = End;
        //Debug.Log("Отображено:" + Queue + " LP=" + LastPoint);
        LR.SetPositions(points[0..LastPoint]);

        //if (LastPoint == 25) LastPoint = 1;
    }

    public void AddNewPoint(Vector3 PointPos)
    {
        points[LastPoint++] = PointPos;
        LR.positionCount = LastPoint;
        ShowLine();
    }

    public void RemoveOldPoint()
    {
    //    Debug.Log("Обновление массива");

        for(int i=1;i<=23;i++)
        {
            points[i] = points[i + 1];
        }
        LastPoint--;
        LR.positionCount = LastPoint;
    }
    public void RemoveAllPoints()
    {
        for (int i = 1; i <= 23; i++)
        {
            points[i] = this.transform.position;
        }
        LastPoint = 1;
        LR.positionCount = 1;
    }
}
