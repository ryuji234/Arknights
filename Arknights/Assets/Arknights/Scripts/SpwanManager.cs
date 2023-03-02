using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

public class SpwanManager : MonoBehaviour
{

    public GameObject Waypoint1;
    public GameObject Waypoint2;
    public GameObject Waypoint3;
    
    EnemyPooling Enemypool;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        Enemypool = FindAnyObjectByType<EnemyPooling>();
        Enemypool.CreateMultiplePoolObjects();
        StartCoroutine(StartWave1());
        StartCoroutine(StartWave2());
        StartCoroutine(StartWave3());
    }
    

    IEnumerator StartWave1()
    {
        const int CURRENTWAVE = 1;
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 3; i++)
        {
            WayPoint1("Hound", CURRENTWAVE);
            yield return new WaitForSeconds(10.0f);
        }
        
    }
    IEnumerator StartWave2()
    {
        const int CURRENTWAVE = 2;
        yield return new WaitForSeconds(9.0f);
        for (int i = 0; i < 4; i++)
        {
            WayPoint2("Soldier", CURRENTWAVE);
            yield return new WaitForSeconds(6.0f);
        }

    }
    IEnumerator StartWave3()
    {
       
        yield return new WaitForSeconds(30.0f);
        for (int i = 0; i < 4; i++)
        {
            WayPoint3("DualBlade");
            yield return new WaitForSeconds(6.0f);
        }

    }

    void WayPoint1(string name,int currentwave)
    {
        
        GameObject soldier = GetSoldier(Waypoint1, new Vector3(12, 1, 40),name);
  
    }
    void WayPoint2(string name, int currentwave)
    {
        GameObject soldier = GetSoldier(Waypoint2, new Vector3(24, 1, 28), name);
        
    }
    void WayPoint3(string name)
    {
        GameObject soldier = GetSoldier(Waypoint3, new Vector3(0, 1, 24), name);
        
    }

    GameObject GetSoldier(GameObject waypoint,Vector3 position,string name)
    {
        GameObject soldier = Enemypool.GetPooledObject(name);
        soldier.SetActive(true);
        soldier.GetComponent<Enemy>().Waypoints = waypoint;
        soldier.transform.position = position;
          
        soldier.GetComponent<Enemy>().HP = soldier.GetComponent<Enemy>().maxHP;

        return soldier;
    }

    GameObject GetSoldier(GameObject waypoint, Vector3 position, string name, int currentwave)
    {
        GameObject soldier = Enemypool.GetPooledObject(name);
        soldier.SetActive(true);
        soldier.GetComponent<Enemy>().Waypoints = waypoint;
        soldier.transform.position = position;
        soldier.GetComponent<Enemy>().HP = soldier.GetComponent<Enemy>().maxHP;

        return soldier;
    }
}
