using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Operators : MonoBehaviour
{
    public Sprite skill;
    public List<GameObject> StopObjects;
    public List<GameObject> FoundObjects;
    public int maxHP;
    public int HP;
    public int attack;
    public int defence;
    public int resist;
    public int respawn;
    public int spawnCost;
    public int ableToStop;
    public int Stop;
    public int Skillpoint;
    public float attackspeed;
    public bool firstSetting = true;
    public virtual void  Die()
    {
        StopObjects.Clear();
        FoundObjects.Clear();
        gameObject.SetActive(false);
    }
    public virtual void Despwan()
    {
        StopObjects.Clear();
        FoundObjects.Clear();
        gameObject.SetActive(false);
    }
    public virtual void Defend()
    {

    }
    
}
