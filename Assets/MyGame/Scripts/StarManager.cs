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
    private Text starText;
    private int starLevel = 0;
    private PackageManager packageManager;
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
        starText.text = $"Stars: {starLevel}, Packages Delivered: {packageManager.GetDeliveredPackagesCount()}";
    }
    private void IncreaseDifficulty(int level)
    {
        switch (level)
        {
            case 1:
            case 2:
                SpawnEnemy(1);
                break;
            case 3:
                SpawnEnemy(2, true);
                break;
            case 4:
                SpawnEnemy(3);
                break;
            case 5:
                SpawnEnemy(3, true);
                break;
        }
    }
    private void SpawnEnemy(int count, bool aggressive = false)
    {
        for (int i = 0; i < count; i++)
        {
            packageManager.SpawnObject(aggressive ? aggressiveEnemyCarPrefab : enemyCarPrefab);
        }
    }
}
