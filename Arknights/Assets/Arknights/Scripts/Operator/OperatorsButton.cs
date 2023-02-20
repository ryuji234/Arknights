using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OperatorsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject Operator;
    public static Transform setTile;

    public string tile;
    public int PosY;
    public int Cost;
    public Transform[,] Tile;
    public Transform enemy;
    public float shortDis;

    public RaycastHit hit, hitLayerMask;
    public GameObject Oper;
    public Vector3 objPosition;

    Vector3 getContactPoint(Vector3 normal, Vector3 planeDot, Vector3 A, Vector3 B)
    {
        Vector3 nAB = (B - A).normalized;
        return A + nAB * Vector3.Dot(normal, planeDot - A) / Vector3.Dot(normal, nAB);
    }
    public virtual void Start()
    {
        Tile = Map.Tile;
        Oper = Instantiate(Operator, null);
        Oper.SetActive(false);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 0.2f;
        Oper.SetActive(true);
        Oper.transform.position = new Vector3(0, -1, 0);
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (Map.Tile[i, j].tag == tile)
                {
                    Transform tile = Map.Tile[i, j];
                    MeshRenderer mr = tile.GetComponent<MeshRenderer>();
                    mr.material.color = Color.green;
                }


            }
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Oper.transform.position = hit.point;
        }
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {

        if (enemy.tag != tile)
        {
            Time.timeScale = 1;
            Oper.SetActive(false);
        }
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (Map.Tile[i, j].tag == tile)
                {
                    Transform tile = Map.Tile[i, j];
                    MeshRenderer mr = tile.GetComponent<MeshRenderer>();
                    mr.material.color = Color.white;
                }


            }
        }
    }
    public virtual void OnDrag(PointerEventData eventData)
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

    public virtual void Update()
    {
        if (Oper != null)
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
                if (enemy.tag == tile)
                {
                    Oper.transform.position = new Vector3(enemy.transform.position.x, PosY, enemy.transform.position.z);
                    setTile = enemy;
                }
                else
                {
                    Oper.transform.position = objPosition;
                }
            }
        }

    }
}
