using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PackagePickupManager : MonoBehaviour
{
    [SerializeField]
    private GameObject packageCarry;
    [SerializeField]
    private GameObject packagePrefab;
    [SerializeField]
    private Transform spawnArea;
    private float spawnAreaPadding = 2f;
    private bool hasPackage = false;
    private string[] illegalSpawnAreaTags = { "Obstacle", "DropArea" };
    private List<Collider2D> illegalSpawnAreas = new List<Collider2D>();
    private void Start()
    {
        foreach (string tag in illegalSpawnAreaTags)
        {
            IEnumerable<Collider2D> obstacles = GameObject.FindGameObjectsWithTag(tag).ToList().Select(x => x.GetComponent<Collider2D>());
            illegalSpawnAreas.AddRange(obstacles);
        }
        SpawnPackage();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Package":
                HandlePackage(other.gameObject);
                break;
            case "DropArea":
                HandleDropArea();
                break;
        }
    }
    private void HandlePackage(GameObject package)
    {
        hasPackage = true;
        packageCarry.SetActive(true);
        packageCarry.transform.position = transform.position;
        Destroy(package);
    }
    private void HandleDropArea()
    {
        hasPackage = false;
        packageCarry.SetActive(false);
        SpawnPackage();
    }
    private void SpawnPackage()
    {
        float spawnXAreaHalf = spawnArea.localScale.x / 2;
        float spawnYAreaHalf = spawnArea.localScale.y / 2;

        Vector2 spawnPosition = GenerateSpawnPos(spawnXAreaHalf, spawnYAreaHalf);

        while (!ValidSpawnPosition(spawnPosition))
        {
            spawnPosition = GenerateSpawnPos(spawnXAreaHalf, spawnYAreaHalf);
        }

        Instantiate(packagePrefab, spawnPosition, Quaternion.identity);
    }
    private Vector2 GenerateSpawnPos(float spawnXAreaHalf, float spawnYAreaHalf)
    {
        float spawnX = Random.Range(-spawnXAreaHalf + spawnAreaPadding, spawnXAreaHalf - spawnAreaPadding);
        float spawnY = Random.Range(-spawnYAreaHalf + spawnAreaPadding, spawnYAreaHalf - spawnAreaPadding);
        return new Vector2(spawnX, spawnY);
    }
    private bool ValidSpawnPosition(Vector2 pos)
    {
        foreach (Collider2D coll in illegalSpawnAreas)
        {
            if (coll.bounds.Contains(pos))
            {
                return false;
            }
        }
        return true;
    }
}
