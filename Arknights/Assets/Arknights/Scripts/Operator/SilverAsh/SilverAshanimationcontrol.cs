using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverAshanimationcontrol : MonoBehaviour
{
    public SilverAsh silverash;
    // Start is called before the first frame update
    private void Hit()
    {
        silverash.attackenemy();
    }
    private void Die()
    {
        
        silverash.Die();
    }
}
