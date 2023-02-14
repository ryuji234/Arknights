using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SilverAshButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject silverash;
    RaycastHit hit, hitLayerMask;
    
    private Transform[,] Tile;
    public static Transform SetTile;
    private Transform enemy;
    private float shortDis;
    Vector3 objPosition;

    Vector3 getContactPoint(Vector3 normal, Vector3 planeDot, Vector3 A, Vector3 B)
    {
        Vector3 nAB = (B - A).normalized;
        return A + nAB * Vector3.Dot(normal, planeDot - A) / Vector3.Dot(normal, nAB);
    }
    void Start()
    {
        Tile = Map.Tile;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        silverash.SetActive(true);
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if(Map.Tile[i, j].tag == "Road")
                {
                    Transform tile = Map.Tile[i, j];
                    MeshRenderer mr = tile.GetComponent<MeshRenderer>();
                    mr.material.color= Color.green;
                }
                

            }
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            silverash.transform.position = hit.point;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(enemy.tag != "Road")
        {
            silverash.SetActive(false);
        }
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (Map.Tile[i, j].tag == "Road")
                {
                    Transform tile = Map.Tile[i, j];
                    MeshRenderer mr = tile.GetComponent<MeshRenderer>();
                    mr.material.color = Color.white;
                }


            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

        int layer = 1 << LayerMask.NameToLayer("Plane");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layer))
        {
            Vector3 normal = hitLayerMask.transform.up;
            Vector3 planeDot = hitLayerMask.point + hitLayerMask.collider.transform.up;
            Vector3 A = Camera.main.transform.position;
            Vector3 B = hitLayerMask.point;

            objPosition = getContactPoint(normal, planeDot, A, B);
        }
    }
    
    void Update()
    {

        shortDis = Mathf.Infinity; // 첫번째를 기준으로 잡아주기 

        enemy = null;

        foreach (Transform found in Tile)
        {
            float Distance = Vector3.Distance(objPosition, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                enemy = found;
                shortDis = Distance;
            }
        }
        if (enemy != null)
        {
            if (enemy.tag == "Road")
            {
                silverash.transform.position = new Vector3(enemy.transform.position.x, 2, enemy.transform.position.z);
                SetTile = enemy;
            }
            else
            {
                silverash.transform.position = objPosition;
            }
        }
    }
}
