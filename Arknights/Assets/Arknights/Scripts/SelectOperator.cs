using UnityEngine;

public class SelectOperator : MonoBehaviour
{
    RaycastHit hit;
    Transform selectedTarget;
    
    Quaternion mainposition;
    static SelectOperator instance = null;

    public Transform Cam; //Object you want to rotate
    public Transform myTransform; //Object you want to rotate
    public GameObject UI;

    private Vector2 screenPoint;

    public static SelectOperator Instance
    {
        get
        {
            if (null == instance) instance = FindObjectOfType<SelectOperator>();
            return instance;
        }
    }

    void Awake()
    {
        if (null == instance) instance = this;
    }

    void Start()
    {
        mainposition = this.transform.rotation;

    }


    void clearTarget()
    {
        if (selectedTarget == null) return;
        UI.SetActive(false);
        selectedTarget = null;
        Debug.Log("오퍼레이터 해제");
        Debug.Log(selectedTarget);
    }

    void selectTarget(Transform obj)
    {
        if (obj == null) return;

        UI.SetActive(true);
        selectedTarget = obj;

        Debug.Log("오퍼레이터 선택");
        Debug.Log(selectedTarget.transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            int layer = 1 << LayerMask.NameToLayer("Operators");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                Transform obj = hit.transform;
                selectTarget(obj);
            }
            else /* Block을 선택하지 않은 경우 */
            {
                clearTarget();
            }
        }
        if (selectedTarget != null)
        {
            Cam.position = new Vector3(selectedTarget.position.x + 10, 20, selectedTarget.position.z);
        }
        else
        {
            Cam.position = new Vector3(25, 20, 20);
        }
    }
}
