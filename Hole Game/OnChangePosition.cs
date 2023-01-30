using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D hole2DColider;
    public PolygonCollider2D ground2DColider;
    public MeshCollider GeneratedMeshColider;
    public Collider GroundColider;
    public float initialScale = 0.5f;
    Mesh GeneratedMesh;
    public TextMeshProUGUI money_text;
    public GameObject BuySellPanel,FullText;
    public MoneyAndSize money_and_size;
    public TextMeshProUGUI required_money_Capacity_Text, required_money_Speed_Text, required_money_Radius_Text;
    public void Move(BaseEventData myEvent)
    {
        if(((PointerEventData)myEvent).pointerCurrentRaycast.isValid)
        {
            transform.position = ((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition;
        }
    }


    

    private void Start()
    {
        
        GameObject[] AllGOs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var go in AllGOs)
        {
            if (go.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(go.GetComponent<Collider>(), GeneratedMeshColider, true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Upgrade"))
        {
            money_text.text = money_and_size.Money.ToString();
            BuySellPanel.SetActive(true);
            required_money_Capacity_Text.text = money_and_size.required_money_Capacity.ToString();
            required_money_Speed_Text.text = money_and_size.required_money_Speed.ToString();
            required_money_Radius_Text.text = money_and_size.required_money_Radius.ToString();
        }

        else if (other.CompareTag("Market"))
        {
            money_text.text = money_and_size.Money.ToString();
            money_and_size.Capacity = 0;
        }
        else
        {
            Physics.IgnoreCollision(other,GroundColider, true);
            Physics.IgnoreCollision(other, GeneratedMeshColider, false); 
   
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, GroundColider, false);
        Physics.IgnoreCollision(other, GeneratedMeshColider, true);
        BuySellPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        
        if (money_and_size.Capacity <= money_and_size.MaxCapacity)
        {
            FullText.SetActive(false);
            if(transform.hasChanged)
            {
                transform.hasChanged = false;
                hole2DColider.transform.position = new Vector2(transform.position.x, transform.position.z);
                hole2DColider.transform.localScale = transform.localScale * initialScale;
                MakeHole2D();
                Make3DMeshColider();
            }
        }
        else
        {
            FullText.SetActive(true);
        }
       
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DColider.GetPath(0);

        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DColider.transform.TransformPoint(PointPositions[i]);
        }

        ground2DColider.pathCount = 2;
        ground2DColider.SetPath(1, PointPositions);
    }

    private void Make3DMeshColider()
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2DColider.CreateMesh(true, true);
        GeneratedMeshColider.sharedMesh = GeneratedMesh;
    }

    
}
