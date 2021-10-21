using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystemPlayer playerH = other.GetComponent<HealthSystemPlayer>();
            playerH.GetDamage(playerH.currentLife);

            HudController.GetHudController().m_GameOver.SetBool("GameOver", true);

            //GameOver.SetBool("GameOver", true);
            // playerH.ResetStates();
            //other.transform.position = TeleportController.GetTeleportController().SpawnToLastTeleport();
           
        }
    }
}
