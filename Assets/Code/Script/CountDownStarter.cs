using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownStarter : MonoBehaviour
{
    public UIHandler uihandler;
    void Start()
    {
        uihandler = GameObject.Find("UICanvas 1").GetComponent<UIHandler>();
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(2f);
        print("dingdong");
<<<<<<< HEAD
        //uihandler.CountDownStart();
=======
        uihandler.CountDownStart();
>>>>>>> d9106e2... UI and Audio work, my apologies for forgetting do it in my branch
    }

}
