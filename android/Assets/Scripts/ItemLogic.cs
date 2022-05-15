using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemLogic : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] ItemGenerator IG;

    public GameObject EOGPanel;

    int FoodCollect = 0;
    float NewScale = 0.25f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.tag == "Enemy")
        {
            this.gameObject.transform.position = new Vector3(0, 0, 0);
            PC.CurrentPoint = PC.PointCounter;

            PC.TS.RemoveAllPoints();
            PC.ScoreUpdate();

        }
        else
        {
            
            switch (collision.gameObject.name)
            {
                case "BoxGreen(Clone)":
                    {
                        if (FoodCollect != 0) PC.ScoreUpdate();
                        else PC.Score--;
                        Debug.Log("Green Catch!"); break;
                    }
                //case "BoxGray": break;
                case "BoxRed(Clone)": PC.ChangeScale(-NewScale); break;
                case "BoxBlue(Clone)": PC.ChangeScale(NewScale); break;
                default: break;

            }
            FoodCollect++;
            Destroy(collision.gameObject);

            if (FoodCollect == IG.FoodAmount)
            {
                EOGPanel.SetActive(true);
                EOGPanel.GetComponentInChildren<TextMeshProUGUI>().text = ("Игра завершена за "+(PC.Score+1) + " шагов.");
            }
        }
    }
}
