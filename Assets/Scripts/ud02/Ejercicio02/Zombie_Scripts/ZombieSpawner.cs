using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject _zombiePrefab;   // Prefab del zombie a instanciar
    [SerializeField]
    private float _spawnTime = 3.0f,    // Intervalo de tiempo entre cada aparición de zombie
                  _timer;
    [SerializeField]
    public static int ZombieCount = 1;

    private void Update() {
        _timer += Time.deltaTime;

        // Si el tiempo de espera ha pasado, genera un nuevo zombie
        if (_timer >= _spawnTime && ZombieCount <= 5) {
            _timer = 0.0f;
            SpawnZombie();
            ZombieCount++;
        }
    }

    private void SpawnZombie() {
        // Rango de posición en el eje X para la aparición de zombies
        float rangoMax = 6.0f;
        float rangoMin = -6.0f;

        // Posición de aparición con un valor pseudoaleatorio en el eje X
        float randomX = rangoMin + Mathf.Abs(Mathf.Sin(Time.time * 3.0f)) * (rangoMax - rangoMin);
        Vector3 spawnPosition = new Vector3(randomX, 0.0f, 1.0f);

        // Instancia el prefab del zombie en la posición generada
        Instantiate(_zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
