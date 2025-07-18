using TMPro;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public sealed class EnemyAmountUI : MonoBehaviour
{
    [Inject] private SignalCenter _signalCenter;
    [SerializeField] private TextMeshProUGUI _enemyAmountText;

    private void OnEnable()
    {
        _signalCenter.Subscribe<EnemyAmountChangedSignal>(OnEnemyAmountChanged);
    }

    private void OnDisable()
    {
        _signalCenter.Unsubscribe<EnemyAmountChangedSignal>(OnEnemyAmountChanged);
    }

    private void OnEnemyAmountChanged(EnemyAmountChangedSignal signal)
    {
        _enemyAmountText.text = $"Enemiy Amount: {signal.EnemyCount}";
    }
}