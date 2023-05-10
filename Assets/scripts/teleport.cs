using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform player, source, destination, normalEntry, normalExit;
    public GameObject playerObj;

    private void OnTriggerEnter() {
        playerObj.SetActive(false);
        player.position = destination.position;
        var baseRotation = Quaternion.LookRotation(normalExit.forward).eulerAngles.y;
        var relativeRotation = playerObj.GetComponent<playerLook>().XYRotation.y - Quaternion.LookRotation(-1* normalEntry.forward).eulerAngles.y;
        playerObj.GetComponent<playerLook>().XYRotation.y = baseRotation + relativeRotation;
        playerObj.GetComponent<playerLook>().FixAngles();
        playerObj.SetActive(true);
    }
}
