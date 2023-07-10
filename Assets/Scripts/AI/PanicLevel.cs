using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicLevel : MonoBehaviour
{

    public Slider PanicLevelPrefab;
    public Vector3 PanicLevelOffset;
    [HideInInspector]public Slider targetPanicLevel;
    private EnemyNavigation _enemyNavigation;

    private void Start()
    {
        _enemyNavigation = GetComponent<EnemyNavigation>();
        targetPanicLevel = Instantiate(PanicLevelPrefab, WorldCanvasController.Instance.transform);
    }
    public void Update()
    {
        targetPanicLevel.transform.position = this.transform.position + PanicLevelOffset;

        targetPanicLevel.value = _enemyNavigation.EndGameTimer / _enemyNavigation.EndGameTime;
    }
}
