using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
  [SerializeField] private float speedBoost;
  [SerializeField] private float powerUpTime;
  [SerializeField] private float speedNorm;
  [SerializeField] private GameObject artToDisable = null;

  private Collider collider;

    private void Awake()
    {
            collider = GetComponent<Collider>();
    }

  void OnTriggerEnter(Collider other)
  {
    PlayerMovement PlayerMovement = other.gameObject.GetComponent<PlayerMovement>();

    if(PlayerMovement != null)
    {
        
        StartCoroutine(SpeedPowerUp(PlayerMovement));
    }
  }

  public IEnumerator SpeedPowerUp(PlayerMovement PlayerMovement)
  {
        //speed = speedBoost;
        collider.enabled = false;
        artToDisable.SetActive(false);

        ActivatePowerUp(PlayerMovement);

        yield return new WaitForSeconds(powerUpTime);
        //speed = speedNorm;

        DeactivatePowerUp(PlayerMovement);

        Destroy(gameObject);
  }

  private void ActivatePowerUp(PlayerMovement PlayerMovement)
  {
    PlayerMovement.SetMoveSpeed(speedBoost);
  }
  private void DeactivatePowerUp(PlayerMovement PlayerMovement)
  {
    PlayerMovement.SetMoveSpeed(-speedBoost);
  }

 
}
