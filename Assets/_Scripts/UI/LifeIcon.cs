using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeIcon : MonoBehaviour
{
    private Image _full;
    private Image _half1;
    private Image _half2;

    private const float Gravity = -9.81f / 10;
    private const float BreakDuration = 0.5f;
    
    private float _yVelocity;
    private bool _isBreaking;
    private float _breakTime;
    
    private void Start()
    {
        _full = transform.Find("Full").GetComponent<Image>();
        _half1 = transform.Find("Half_1").GetComponent<Image>();
        _half2 = transform.Find("Half_2").GetComponent<Image>();

        var mainRect = GetComponent<RectTransform>();
        var temp = new List<Image> { _full, _half1, _half2 };
        foreach (var img in temp)
        {
            var rect = img.GetComponent<RectTransform>();
            rect.sizeDelta = mainRect.sizeDelta;
        }
        
        _full.gameObject.SetActive(true);
    }
    
    public IEnumerator Break()
    {
        if (_isBreaking) yield break;
        _isBreaking = true;
        
        if (_full)
        {
            _full.gameObject.SetActive(false);
        }

        if (!_half1 || !_half2) yield break;
        
        _half1.gameObject.SetActive(true);
        _half2.gameObject.SetActive(true);
        Canvas.ForceUpdateCanvases();
        yield return new WaitForSeconds(0.5f);

        while (_breakTime < BreakDuration)
        {

            _breakTime += Time.deltaTime;
            _yVelocity += Time.deltaTime * Gravity;
            var xDis = 20 * Time.deltaTime;
            var zRot = 60 * Time.deltaTime;
            _half1.transform.localPosition += new Vector3(-xDis, _yVelocity, 0);
            _half1.transform.rotation = Quaternion.Euler(0, 0, _half1.transform.localEulerAngles.z + zRot);
            _half2.transform.localPosition += new Vector3(xDis, _yVelocity, 0);
            _half2.transform.rotation = Quaternion.Euler(0, 0, _half2.transform.localEulerAngles.z - zRot);

            yield return new WaitForEndOfFrame();
        }
        
        var rect1 = _half1.GetComponent<RectTransform>();
        var rect2 = _half2.GetComponent<RectTransform>();
        while (rect1.rect.width > 0)
        {
            rect1.sizeDelta = new Vector2(rect1.rect.width-1, rect1.rect.height-1);    
            rect2.sizeDelta = new Vector2(rect1.rect.width-1, rect1.rect.height-1);  
            yield return new WaitForEndOfFrame();
        }
        
        Destroy(gameObject);
    }
}
