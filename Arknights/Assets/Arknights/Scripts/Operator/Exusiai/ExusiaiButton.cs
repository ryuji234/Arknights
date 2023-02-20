using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExusiaiButton : OperatorsButton, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public static Transform tileposition; 
    
    public override void Start()
    {
        base.Start();
        Cost = 12;
        tile = "Wall";
        PosY = 3;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {

        base.OnPointerUp(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void Update()
    {
        base.Update();

        tileposition = setTile;
    }
}
