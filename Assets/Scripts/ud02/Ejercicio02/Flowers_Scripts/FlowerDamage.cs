using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDamage : MonoBehaviour 
{
    [SerializeField]
    private int _damageAmount = 1; //Daño de la flor

    private void OnTriggerEnter(Collider other) {
        //Si el la flor colisiona con el zombie
        ZombieData zombie = other.GetComponent<ZombieData>();

        if (zombie != null) {
            //Llama al "TakeDamage" del script del zombie
            zombie.TakeDamage(_damageAmount);

            // Destruye la flor
            Destroy(gameObject);
        }
    }
}
