using Krivodeling.UI.Effects;
using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishEffect;
    [SerializeField] private  GameObject _finishScreen;
    [SerializeField] private UIBlur _uIBlur;

    bool _isReached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isReached == true)
        {
            return;
        }
        if(other.TryGetComponent(out PlayerBodySegment bodySegment))
        {
            _finishEffect.Play();
            _isReached = true;
            StartCoroutine(WaitStartFinishDisplay());            
        }
    }

    private IEnumerator WaitStartFinishDisplay()
    {
        float activateDelay = 1.5f;
        float blurSpeed = 0.5f;
        yield return new WaitForSeconds(activateDelay);
        _finishScreen.SetActive(true);
        _uIBlur.BeginBlur(blurSpeed);
    }
}
