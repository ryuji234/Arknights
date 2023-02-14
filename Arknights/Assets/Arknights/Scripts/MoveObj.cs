using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    RaycastHit hit, hitLayerMask;
    GameObject objectHitPosition;
    float yHeight;
    private Transform[,] Tile;
    private Transform enemy;
    private float shortDis;
    Vector3 objPosition;
    Vector3 getContactPoint(Vector3 normal, Vector3 planeDot, Vector3 A, Vector3 B)
    {
        Vector3 nAB = (B - A).normalized;

        return A + nAB * Vector3.Dot(normal, planeDot - A) / Vector3.Dot(normal, nAB);
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            objectHitPosition = new GameObject("Empty");
            objectHitPosition.transform.position = hit.point;
            this.transform.SetParent(objectHitPosition.transform);
        }
    }

    void OnMouseUp()
    {
        this.transform.parent = null;
        Destroy(objectHitPosition);
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

        int layer = 1 << LayerMask.NameToLayer("Plane");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layer))
        {
            Vector3 normal = hitLayerMask.transform.up;
            Vector3 planeDot = hitLayerMask.point + hitLayerMask.collider.transform.up * yHeight;
            Vector3 A = Camera.main.transform.position;
            Vector3 B = hitLayerMask.point;

            this.transform.rotation
              = Quaternion.LookRotation(hitLayerMask.collider.transform.forward);
            objPosition = getContactPoint(normal, planeDot, A, B);
        }
    }

    void Start()
    {
        yHeight = this.transform.localScale.y;
        Tile = Map.Tile;
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

                transform.position = new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z);
            }
        }
    }
}
