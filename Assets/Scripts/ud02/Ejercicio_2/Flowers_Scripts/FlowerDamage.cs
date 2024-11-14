using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDamage : MonoBehaviour 
{

    //Zona de Variables Globales
    [SerializeField]
    private int _damageAmount = 1; //Daño de la flor

    private void OnTriggerEnter(Collider clon) 
    {
        
        //Si el la flor colisiona con el zombie
        ZombieData _zombie = clon.GetComponent<ZombieData>();

        //Si el zombie existe porque no ha sido eliminado aún
        if (_zombie != null) {
            //Llama al "TakeDamage" del script del zombie
            _zombie.TakeDamage(_damageAmount);

            // Destruye la flor
            Destroy(gameObject);

        }
    }
}
