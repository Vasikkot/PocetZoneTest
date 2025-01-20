using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Tilemap spawnTilemap; // Тайлмап с тайлами для спавна
    [SerializeField] private GameObject enemyPrefab; // Префаб врага
    [SerializeField] private int enemyCount = 5; // Количество врагов для спавна

    private List<Vector3> spawnPositions = new List<Vector3>();

    void Start()
    {
        // Сбор позиций для спавна
        CollectSpawnPositions();

        // Спавн врагов
        SpawnEnemies();
    }

    void CollectSpawnPositions()
    {
        BoundsInt bounds = spawnTilemap.cellBounds;
        TileBase[] allTiles = spawnTilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3Int localPlace = new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0);
                    Vector3 worldPosition = spawnTilemap.CellToWorld(localPlace) + spawnTilemap.tileAnchor;
                    spawnPositions.Add(worldPosition);
                }
            }
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            if (spawnPositions.Count == 0)
            {
                Debug.LogWarning("Недостаточно позиций для спавна врагов.");
                break;
            }

            int randomIndex = Random.Range(0, spawnPositions.Count);
            Vector3 spawnPosition = spawnPositions[randomIndex];
            // Создание врага с установкой родителя
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, spawnTilemap.transform);
            spawnPositions.RemoveAt(randomIndex); // Удаляем использованную позицию, чтобы избежать повторного спавна в том же месте
        }
    }
}