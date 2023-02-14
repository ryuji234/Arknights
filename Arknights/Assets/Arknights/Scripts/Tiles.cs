
using UnityEngine;

public class Tiles : MonoBehaviour
{
    Vector3 objPosition;
    float distance = 24;
    void OnMouseDown()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, (Input.mousePosition.x/ distance) +(Input.mousePosition.y/ distance));

        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log($"마우스좌표:{Input.mousePosition},카메라좌표:{objPosition}, 실제 좌표:{this.transform.position}");


    }
}
