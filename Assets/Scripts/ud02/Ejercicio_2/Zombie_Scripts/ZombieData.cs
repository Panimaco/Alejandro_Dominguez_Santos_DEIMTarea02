using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieData : MonoBehaviour {
    [SerializeField]
    private int _maxLife = 2,
                _currentLife;

    [SerializeField]
    private static int _zombieCount = 0;

    private void Start() 
    {
        
        //Empezamos con la vida máxima
        _currentLife = _maxLife;

    }

    //Void público al que llama el script de las flores
    public void TakeDamage(int damage) 
    {
        
        //Le quitamos vida en proporcion al daño de las flores
        _currentLife -= damage;

        //Si la vida baja de 0
        if (_currentLife <= 0) {
            
            //Muere
            Die();

        }
    }

    private void Die() 
    {

        //Destruye el zombie
        Destroy(gameObject);

        //Aumenta el numero de zombies muertos
        _zombieCount++;

        //Reduce la cantidad de zombies en el contador para que sigan generando
        ZombieSpawner.ZombieCount--;

        //Enseña la cantidad de zombies que llevas matado
        Debug.Log("Zombie eliminado. Llevas " + _zombieCount + " zombies.");

    }
}
