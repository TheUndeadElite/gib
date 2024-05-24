using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleHandScript : MonoBehaviour
{
   


    public void EnableAttackColliderInSnoken()
    {
        GetComponentInParent<DamageTaker>().EnableDamageCollider();
    }

    public void DiablettackColliderInSnoken()
    {
        GetComponentInParent<DamageTaker>().DisableDamageCollider();
    }

}
