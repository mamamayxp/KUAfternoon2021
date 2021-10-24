using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
        public Light light1;
    private void OnMouseDown()
    {
        if (light1.enabled == true)
        {
            light1.enabled = false;
        }
        else
        {
            light1.enabled = true;
        }
    }

}



