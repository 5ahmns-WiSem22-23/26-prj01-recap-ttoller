using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyCarPrefab;
    [SerializeField]
    private GameObject aggressiveEnemyCarPrefab;
    [SerializeField]
    private float starGainSteepness = 1.2f;
    [SerializeField]
    private int initialMaxEnemies = 10;
    [SerializeField]
    private Text packagesText;
    [SerializeField]
    private Sprite filledStarImage;
    [SerializeField]
    private Image[] starImages;
    private int starLevel = 0;
    private PackageManager packageManager;
    public int currentCars = 0;
    public int totalKills = 0;
    public int maxEnemies = 10;
    private void Start()
    {
        packageManager = GameObject.FindWithTag("Player").GetComponent<PackageManager>();
        UpdateText();
    }
    public void UpdateStarLevel()
    {
        int calculatedStar = Mathf.Min(Mathf.FloorToInt(Mathf.Pow(starGainSteepness, packageManager.GetDeliveredPackagesCount()) - 1), 5);
        if (calculatedStar > starLevel) IncreaseDifficulty(calculatedStar);
        starLevel = calculatedStar;

        UpdateText();
    }
    private void UpdateText()
    {
        packagesText.text = $"Packages Delivered: {packageManager.GetDeliveredPackagesCount()}";
    }
    private void IncreaseDifficulty(int level)
    {
        switch (level)
        {
            case 1:
                SpawnEnemy(1);
                break;
            case 2:
                StartCoroutine(SpawnEnemyCoroutine(1, 15, false));
                break;
            case 3:
                StartCoroutine(SpawnEnemyCoroutine(3, 20, false));
                break;
            case 4:
                StartCoroutine(SpawnEnemyCoroutine(3, 10, true));
                break;
            case 5:
                StartCoroutine(SpawnEnemyCoroutine(3, 2, true));
                break;
        }
        starImages[level - 1].sprite = filledStarImage;
    }
    private void SpawnEnemy(int count, bool aggressive = false)
    {
        for (int i = 0; i < count; i++)
        {
            currentCars++;
            packageManager.SpawnObject(aggressive ? aggressiveEnemyCarPrefab : enemyCarPrefab);
        }
    }
    public int GetStarLevel()
    {
        return starLevel;
    }
    public void EnemyCarDestroyed()
    {
        currentCars--;
        totalKills++;
        maxEnemies = Mathf.Min(Mathf.FloorToInt(Mathf.Pow(starGainSteepness, totalKills) - 1) + initialMaxEnemies, 100);
    }
    IEnumerator SpawnEnemyCoroutine(int waveSize, int waveDelay, bool aggressive = false)
    {
        while (true)
        {
            if (currentCars < maxEnemies) SpawnEnemy(waveSize, aggressive);
            yield return new WaitForSeconds(waveDelay);
        }
    }
}
