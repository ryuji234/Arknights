using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject colliders;
    public GameObject Waypoints;
    public Image HPbar;
    public int HP;
    
    public Waypoint waypoint;
    public Transform target;
    public int wavepointIndex = 0;
    public int maxHP = 3;
    public float HPguage;
    public bool Isrun;
    public bool contact;
    public bool Set;
    public Animator anime;

    //Operators operators;
    public List<Operators> operators = new List<Operators>();

    
    public virtual void Start()
    {
        Set = true;
        anime = GetComponentInChildren<Animator>();
    }

    public virtual void Update()
    {
        if(Set)
        {
            HP = maxHP;
            waypoint = Waypoints.GetComponent<Waypoint>();
            target = waypoint.points[0];
            wavepointIndex = 0;
            Set = false;
            Isrun = true;
        }
        HPguage = (float)HP / (float)maxHP;
        HPbar.fillAmount = HPguage;
        
        if (Isrun)
        {
            anime.SetBool("IsAttack", false);
            anime.SetBool("IsWalk", true);
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }
        }
        else
        {
            anime.SetBool("IsWalk", false);
        }
        // 오퍼레이터와 접촉, 오퍼레이터한테 저지당함
        if (operators.Count > 0)
        {
            if (operators[0].HP <= 0 || operators[0].isActiveAndEnabled == false)
            {
                Debug.Log("오퍼레이터 사망 전진");
                contact = false;
                Isrun = true;
                anime.SetBool("IsAttack", false);
                operators.Remove(operators[0]);
            }
            else
            {
                if (operators[0].ableToStop > operators[0].Stop)
                {
                    
                    colliders.SetActive(true);
                    if(contact == false)
                    {
                        contact = true;
                        Debug.Log("오퍼레이터와 접촉 공격중");
                        operators[0].Stop++;
                        operators[0].StopObjects.Add(gameObject);
                        Debug.Log($"{operators[0].StopObjects.Count}");
                    }
                    Isrun = false;
                    colliders.SetActive(false);
                    anime.SetBool("IsAttack", true);

                }
                else
                {

                }
            }
        }
        if (HP <= 0)
        {
            UIManager.passedEnemyNumber++;
            if (contact)
            {
                operators[0].StopObjects.Remove(gameObject);
                operators[0].Stop--;
                contact = false;
            }
            operators.Clear();
            Set = true;
            
            gameObject.SetActive(false);
        }

    }

    public virtual void GetNextWaypoint()
    {
        if (wavepointIndex >= waypoint.points.Length - 1)
        {
            
            Set = true;
            UIManager.passedEnemyNumber++;
            gameObject.SetActive(false);
            return;
        }
        wavepointIndex++;
        target = waypoint.points[wavepointIndex];
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Operators")
        {
            operators.Add(other.GetComponent<Operators>());
        }
        
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Operators")
        {
            operators.Remove(other.GetComponent<Operators>());
        }
    }

    public virtual void HitOperator()
    {
        if(operators.Count > 0)
        {
            operators[0].HP--;
        }
        
    }
}
