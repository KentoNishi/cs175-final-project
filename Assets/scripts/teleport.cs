using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerObj;
    
    void Start() {
        Debug.Log("debug log");
    }

    private void OnTriggerEnter() {
        playerObj.SetActive(false);
        player.position = destination.position;
        playerObj.SetActive(true);
    }
}
