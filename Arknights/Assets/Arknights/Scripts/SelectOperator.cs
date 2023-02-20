using UnityEngine;
using UnityEngine.UI;

public class SelectOperator : MonoBehaviour
{
    RaycastHit hit;
    
    Quaternion mainposition;
    static SelectOperator instance = null;
    public static Transform selectedTarget;

    public Transform Cam; //Object you want to rotate
    public Transform myTransform; //Object you want to rotate
    public GameObject UI;
    public Image skill;
    
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
        selectedTarget = null;
    }

    void selectTarget(Transform obj)
    {
        if (obj == null) return;

        UI.SetActive(true);
        selectedTarget = obj;
        
        skill.sprite = selectedTarget.GetComponent<Operators>().skill;

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
                if(obj.GetComponent<Operators>().firstSetting == false)
                {
                    selectTarget(obj);
                }
                
            }
            else /* Block을 선택하지 않은 경우 */
            {
                int layer2 = 1 << LayerMask.NameToLayer("UI");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer2))
                {
                    Debug.Log("여기 오는건가");

                }
                else
                {
                    clearTarget();
                }
                
            }
        }
        if (selectedTarget != null)
        {
            Cam.position = new Vector3(selectedTarget.position.x + 10, 20, selectedTarget.position.z);
        }
        else
        {
            Cam.position = new Vector3(27, 20, 20);
            UI.SetActive(false);
        }
    }
    public void Despwan()
    {
        selectedTarget.GetComponent<Operators>().Despwan();
        clearTarget();
    }
}
