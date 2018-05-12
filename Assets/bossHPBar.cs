using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHPBar : MonoBehaviour {

    public CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }
    // Update is called once per frame
    void Update () {
        if (!Global.isBoss)
        {
            canvasGroup.alpha = 0f; //this makes everything transparent
            canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
        }
        else
        {
            canvasGroup.alpha = 1f; //this makes everything transparent
            canvasGroup.blocksRaycasts = true; //this prevents the UI element to receive input events
        }
    }
}
