using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exusiaianimationcontrol : MonoBehaviour
{
    public Exusiai exusiai;
    // Start is called before the first frame update
    private void Hit()
    {
        exusiai.attackenemy();
    }
    private void Die()
    {

        exusiai.Die();
    }
}
