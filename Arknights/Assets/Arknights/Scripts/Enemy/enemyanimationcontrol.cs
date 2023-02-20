using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyanimationcontrol : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void hit()
    {
        enemy.HitOperator();
    }
}
