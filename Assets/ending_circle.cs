using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending_circle : MonoBehaviour
{
    public GameObject endingScreen, endingCircle;

    void Start() {
        endingScreen.SetActive(false);
    }

    private void OnTriggerEnter() {
        endingScreen.SetActive(true);
        endingCircle.SetActive(false);
    }
}
