using System.Collections.Generic;
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
    public GameObject operName;
    public GameObject operAttackValue;
    public GameObject operDefenceValue;
    public GameObject operResistValue;
    public GameObject operAbletoStopValue;
    public Image skill;
    public Image skillGuage;
    public Image operatorillust;
    private Vector2 screenPoint;
    List<GameObject> oper;
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
    void clearTarget()
    {
        if (selectedTarget == null) return;
        selectedTarget = null;
    }

    void selectTarget(Transform obj)
    {
        if (obj == null)
        {
            return;
        }
        Debug.Log("오퍼레이터");
        
        selectedTarget = obj;
        if(selectedTarget.GetComponent<Operators>().firstSetting == false)
        {
            UI.SetActive(true);
            skill.sprite = selectedTarget.GetComponent<Operators>().skill;
            operatorillust.sprite = selectedTarget.GetComponent<Operators>().illust;
            skillGuage.fillAmount = selectedTarget.GetComponent<Operators>().skillGuage;
            GF.SetTextMeshPro(operName, selectedTarget.GetComponent<Operators>().operatorname);
            GF.SetTextMeshPro(operAttackValue, selectedTarget.GetComponent<Operators>().attack.ToString());
            GF.SetTextMeshPro(operDefenceValue, selectedTarget.GetComponent<Operators>().defence.ToString());
            GF.SetTextMeshPro(operResistValue, selectedTarget.GetComponent<Operators>().resist.ToString());
            GF.SetTextMeshPro(operAbletoStopValue, selectedTarget.GetComponent<Operators>().ableToStop.ToString());
        }
        else
        {
            if(selectedTarget.GetComponent<Operators>().Isready == true)
            {
                selectedTarget.GetComponent<Operators>().TilePosition();
                selectedTarget.GetComponent<Operators>().onClick = true;
            }
            
        }
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
            else /* Operator를 선택하지 않은 경우 */
            {

                int layer2 = 1 << LayerMask.NameToLayer("UI");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer2))
                {

                }
                else
                {
                    clearTarget();
                    Cam.position = new Vector3(27, 20, 20);
                    UI.SetActive(false);
                }

            }
            if (selectedTarget != null)
            {
                if(UI.activeSelf)
                {
                    Cam.position = new Vector3(selectedTarget.position.x + 10, 20, selectedTarget.position.z);
                    skillGuage.fillAmount = selectedTarget.GetComponent<Operators>().skillGuage;
                }
                
            }
            else
            {
                Cam.position = new Vector3(27, 20, 20);
                UI.SetActive(false);
                oper = new List<GameObject>(GameObject.FindGameObjectsWithTag("Operators"));
                foreach (GameObject obj in oper)
                {
                    if(obj.GetComponent<Operators>().firstSetting == true)
                    {
                        if (obj.GetComponent<Operators>().Isready == true)
                        {
                            obj.GetComponent<Operators>().Isready = false;
                            obj.SetActive(false);
                            if (UIManager.onDoubleSpeed)
                            {
                                Time.timeScale = 2;
                            }
                            else
                            {
                                Time.timeScale = 1;
                            }

                        }
                        
                    }
                    
                }

            }

        }
    }
    public void Despwan()
    {
        selectedTarget.GetComponent<Operators>().Despwan();
        clearTarget();
        Cam.position = new Vector3(27, 20, 20);
        UI.SetActive(false);
    }
}
