
using UnityEngine;

public class Tiles : MonoBehaviour
{
    Vector3 objPosition;
    float distance = 24;
    void OnMouseDown()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, (Input.mousePosition.x/ distance) +(Input.mousePosition.y/ distance));

        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log($"���콺��ǥ:{Input.mousePosition},ī�޶���ǥ:{objPosition}, ���� ��ǥ:{this.transform.position}");


    }
}
