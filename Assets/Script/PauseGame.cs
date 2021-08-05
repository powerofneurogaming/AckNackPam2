using UnityEngine;
using System.Collections;
 
 public class PauseGame : MonoBehaviour
{

    bool Pause = false;

    void Update()
    {

        if (Pause == false)
        {
            Time.timeScale = 1;
        }

        else
        {
            Time.timeScale = 0;
        }


        if (Input.GetKey(KeyCode.P))
        {
            if (Pause == true)
            {
                Pause = false;
            }

            else
            {
                Pause = true;
            }
        }


    }


}