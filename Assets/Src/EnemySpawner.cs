using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Tilemap spawnTilemap; // ������� � ������� ��� ������
    [SerializeField] private GameObject enemyPrefab; // ������ �����
    [SerializeField] private int enemyCount = 5; // ���������� ������ ��� ������

    private List<Vector3> spawnPositions = new List<Vector3>();

    void Start()
    {
        // ���� ������� ��� ������
        CollectSpawnPositions();

        // ����� ������
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
                Debug.LogWarning("������������ ������� ��� ������ ������.");
                break;
            }

            int randomIndex = Random.Range(0, spawnPositions.Count);
            Vector3 spawnPosition = spawnPositions[randomIndex];
            // �������� ����� � ���������� ��������
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, spawnTilemap.transform);
            spawnPositions.RemoveAt(randomIndex); // ������� �������������� �������, ����� �������� ���������� ������ � ��� �� �����
        }
    }
}