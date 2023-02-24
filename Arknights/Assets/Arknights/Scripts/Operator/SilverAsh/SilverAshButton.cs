using UnityEngine;
using UnityEngine.EventSystems;

public class SilverAshButton : OperatorsButton, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public static Transform tileposition;
    
    public override void Start()
    {

        base.Start();
        timeValue = 60;
        Cost = 18;
        tile = "Road";
        PosY = 2;
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
