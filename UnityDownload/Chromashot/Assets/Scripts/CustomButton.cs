using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Outline outline;

    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip hoverClip;
    [SerializeField] AudioClip clickClip;

    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        outline.enabled = false;
        PlayClip(clickClip);
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
        PlayClip(hoverClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }

    void PlayClip(AudioClip clip)
    {
        if (audioSrc != null)
        {
            audioSrc.clip = clip;
            audioSrc.Play();
        }
    }
}
