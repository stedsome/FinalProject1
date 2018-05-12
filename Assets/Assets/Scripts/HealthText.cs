using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HealthText : MonoBehaviour {

    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = Global.remainingHealth + " / " + Global.health;
    }
}
