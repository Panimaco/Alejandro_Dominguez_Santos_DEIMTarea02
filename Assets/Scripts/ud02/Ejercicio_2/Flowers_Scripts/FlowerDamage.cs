using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDamage : MonoBehaviour 
{

    //Zona de Variables Globales
    [SerializeField]
    //Daño de la flor
    private int _damageAmount = 1;

    private void OnTriggerEnter(Collider infoTrigger) 
    {
        
        //Si el la flor colisiona con el zombie accede a su script
        ZombieData _zombie = infoTrigger.GetComponent<ZombieData>();

        //Si el zombie existe porque no ha sido eliminado aún
        if (_zombie != null) 
        {

            //Llama al "TakeDamage" del script del zombie
            _zombie.TakeDamage(_damageAmount);

            // Destruye el clon de la flor
            Destroy(gameObject);

        }
    }
}
