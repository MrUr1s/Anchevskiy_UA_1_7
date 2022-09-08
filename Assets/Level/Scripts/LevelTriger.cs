using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTriger : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameManager_sc.instance.UpdateLevel();   
        }
    }

}
