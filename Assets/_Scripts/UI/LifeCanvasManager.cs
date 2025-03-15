using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LifeCanvasManager : MonoBehaviour
{
    private GameObject _lifePanel;
    private GameObject _lifePrefab;
    private List<LifeIcon> _lifeIcons = new();
    
    private Vector2 _lifeDimensions = Vector2.zero;
    private HorizontalLayoutGroup _lifePanelLayout;

    private void Awake()
    {
        _lifePrefab = Resources.Load<GameObject>("UI/Life");
        _lifePanel = transform.Find("Container")?.Find("Panel")?.gameObject;
        
        _lifePanelLayout = _lifePanel?.GetComponent<HorizontalLayoutGroup>();
        
        var lifePrefabRect = _lifePrefab.GetComponent<RectTransform>();
        _lifeDimensions = new Vector2(lifePrefabRect.rect.width, lifePrefabRect.rect.height);
    }

    public void PopulateLifeIcons()
    {
        var rectComponent = _lifePanel?.GetComponent<RectTransform>();
        if (_lifePanel == null || rectComponent == null) return;
        
        rectComponent.sizeDelta = new Vector2((_lifeDimensions.x + _lifePanelLayout.padding.horizontal) * GameManager.GetMaxLives(), _lifeDimensions.y + _lifePanelLayout.padding.vertical);
        
        for (var i = 0; i < GameManager.GetCurrentLives(); i++)
        {
            var life = Instantiate(_lifePrefab, _lifePanel.transform);
            var lifeIcon = life.GetComponent<LifeIcon>();
            _lifeIcons.Add(lifeIcon);
        }
    }

    public IEnumerator UpdateLifeIcons(int currentLives)
    {
        for (var i = currentLives; i < _lifeIcons.Count; i++)
        {
            var icon = _lifeIcons[i];
            if (icon.IsDestroyed())
            {
                _lifeIcons.Remove(icon);
                continue;
            }
            yield return icon.Break();
        }
    }
}
