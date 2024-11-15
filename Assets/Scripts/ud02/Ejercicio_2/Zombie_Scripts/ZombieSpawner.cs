using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour 
{

    //Zona de Variables Globales
    [SerializeField]
    private GameObject _zombiePrefab;   //Prefab del zombie a clonar

    [SerializeField]
    private float _spawnTime = 3.0f,    //Intervalo de tiempo entre cada aparición de zombie
                  _timer,               //Temporizador
                  _spawnRadius = 10.0f, //Radio de Spawneo de Zombies
                  _angle = 0.0f;        //Ángulo de Spawneo

    [SerializeField]
    private Transform _playerTransform; //Componente Transform del Jugador

    [SerializeField]
    public static int ZombieCount = 1;  //Cantidad "estática" de Zombies eliminados

    private void Update() 
    {

        //Busca un GameObject con la etiqueta de "Player"
        GameObject _playerObject = GameObject.FindWithTag("Player");

        //Convierte el "_playerTransform" en el "transform" del "Player"
        _playerTransform = _playerObject.transform;

        //Temporizador
        _timer += Time.deltaTime;

        // Si el tiempo de espera ha pasado, genera un nuevo zombie
        if (_timer >= _spawnTime && ZombieCount <= 5)
        {
            _timer = 0.0f;
            SpawnZombie();
            ZombieCount++;

        }
    }

    private void SpawnZombie() 
    {

        //Incrementa el ángulo para que la posición varíe en cada spawn
        _angle += 45.0f;

        //Si el ángulo de aparición supera los 360
        if (_angle >= 360.0f) 
        {

            //Resetea el ángulo
            _angle -= 360.0f;
        
        }
        
        float _angleInRadians = _angle * Mathf.Deg2Rad,                 //Convierte el ángulo a radiantes
              _offsetX = Mathf.Cos(_angleInRadians) * _spawnRadius,     //Posición del "círculo" en x
              _offsetZ = Mathf.Sin(_angleInRadians) * _spawnRadius;     //Posición del "círculo" en z

        //Establece la posición de spawn
        Vector3 _spawnPosition = new Vector3(

            _playerTransform.position.x + _offsetX,
            _playerTransform.position.y,
            _playerTransform.position.z + _offsetZ
            
            );

        //Si el personaje está saltando no le importa al spawner de zombies
        if (_playerTransform.position.y > 0) 
        {

            _spawnPosition.y = 0;
        
        }

        // Instancia el prefab del zombie en la posición generada
        Instantiate(_zombiePrefab, _spawnPosition, Quaternion.identity);

    }
}
