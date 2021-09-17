using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{

    private bool continueText = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            continueText = true;
            GetComponent<UnityEngine.UI.Text>().enabled = false;
            
        }
        if (continueText == false){
            if ((int)Time.time % 2 != 0)
            {
                GetComponent<UnityEngine.UI.Text>().enabled = true;
            }
            else
            {
                GetComponent<UnityEngine.UI.Text>().enabled = false;
            }
        }
    }
}
