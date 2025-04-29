using TMPro;
using UnityEngine;

public class EnemyStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI statsText;
    private EnemyStats stats;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        UpdateStatsText();
    }

    public void UpdateStatsText()
    {
        if (stats != null && statsText != null)
        {
            statsText.text =
                $"HP: {stats.hp:F2}\n" +
                $"DMG: {stats.damage:F2}\n" +
                $"Rate: {stats.attackRate:F2}\n" +
                $"Range: {stats.range:F2}\n" +
                $"Speed: {stats.speed:F2}\n" +
                $"Diff: {stats.difficultyScore:F2}\n" +
                $"Bal: {stats.balanceScore:F2}\n" +
                $"Total: {stats.totalScore:F2}";
        }
    }
}
