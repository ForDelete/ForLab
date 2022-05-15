using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] Camera CameraObj;
    public RectTransform SpawnArea;

    Vector3 SpawnPoint;
    bool CanToSpawn=false;
    //List <Vector3> NoSpawnZones;
    Vector3[] NoSpawnZones;
    float distance;
    int x=1;

    Vector3 LD, RU;//Границы
    int GrayC=5, BlueC=3, RedC=3, SpikeC=5;


    public GameObject BoxGreen;
    public GameObject BoxGray;
    public GameObject BoxBlue;
    public GameObject BoxRed;
    public GameObject Spike;
    public int FoodAmount;
    void Start()
    {
        FoodAmount = GrayC + BlueC + RedC + 1;//+1 за зелёный
        LD = Camera.main.ScreenToWorldPoint(SpawnArea.TransformPoint(SpawnArea.rect.x, SpawnArea.rect.y, 0));
        RU = Camera.main.ScreenToWorldPoint(SpawnArea.TransformPoint(SpawnArea.rect.size.x / 2, SpawnArea.rect.height / 2, 0));

        NoSpawnZones = new Vector3[FoodAmount+SpikeC+1];//Кол-во еды, шипов и позиция игрока
        NoSpawnZones[0] = this.transform.position;

        //NoSpawnZones = new List<Vector3>();
        //NoSpawnZones.Add(this.transform.position);

        SpawnItem(BoxGreen);
        for (int i = 0; i < GrayC; i++) SpawnItem(BoxGray);
        for (int i = 0; i < BlueC; i++) SpawnItem(BoxBlue);
        for (int i = 0; i < RedC; i++) SpawnItem(BoxRed);
        for (int i = 0; i < SpikeC; i++) SpawnItem(Spike);
    }

    public void SpawnItem(GameObject Item)
    {
        while(!CanToSpawn)
        {
            CanToSpawn = true;
            SpawnPoint = new Vector3(Random.Range(LD.x, RU.x), Random.Range(LD.y, RU.y), 0);
            for(int i=0;i<NoSpawnZones.Length;i++)
            {
                distance = Vector3.Distance(SpawnPoint, NoSpawnZones[i]);
                if (distance<1)
                {
                    CanToSpawn = false;
                    break;
                }
            }
            if(CanToSpawn)
            {
                NoSpawnZones[x++] = SpawnPoint;
                Instantiate(Item, SpawnPoint, Quaternion.identity);
            }
        }
        CanToSpawn = false;

        //Vector3 HZ = SpawnArea.TransformPoint(SpawnArea.rect.x, SpawnArea.rect.y, 0);
        //Vector3 KZ = SpawnArea.TransformPoint(SpawnArea.rect.size.x/2, SpawnArea.rect.height/2, 0);
        //Vector3 DZ = Camera.main.ScreenToWorldPoint(HZ);
        //Vector3 PZ = Camera.main.ScreenToWorldPoint(KZ);

        //Instantiate(BoxGreen, HZ, Quaternion.identity);
        //Instantiate(BoxGreen, KZ, Quaternion.identity);
        //Instantiate(BoxGreen, DZ, Quaternion.identity);
        //Instantiate(BoxGreen, PZ, Quaternion.identity);
    }
}
