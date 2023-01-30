using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyAndSize : MonoBehaviour
{
    public  int Money;
    public int MaxCapacity,Capacity;
    public GameObject Hole;
    public int Speed;
    // Start is called before the first frame update
    public OnChangePosition hole_code;
   
    private void Start()
    {
        MaxCapacity = 10;
        required_money_Capacity = 50;
        required_money_Radius = 50;
        required_money_Speed = 50;
    }

    public int required_money_Capacity;
    public int required_money_Speed;
    public int required_money_Radius;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.CompareTag("small"))
        {
            Capacity += 1;
            Money += 10;
        }
        if (other.CompareTag("mid"))
        {
            Capacity += 2;
            Money += 20;
        }
        if (other.CompareTag("large"))
        {
            Capacity += 3;
            Money += 30;
        }
        
    }

    public void AddSpeed()
    {
        
        if (Money >= required_money_Speed)
        {
            Money -= required_money_Speed;
            required_money_Speed += 50;
 
        }
    }
    public void AddRadius()
    {
        if (Money >= required_money_Radius)
        {
            Money -= required_money_Radius;
           required_money_Radius += 25;
           Hole.transform.localScale = new Vector3(Hole.transform.localScale.x, Hole.transform.localScale.y,
               Hole.transform.localScale.z) * 1.5f;
        }
    }
    public void AddCapacity()
    {
        if (Money >= required_money_Capacity)
        {
            Money -= required_money_Capacity;
            required_money_Capacity += 40;
            MaxCapacity += 10;
        }
    }
}
