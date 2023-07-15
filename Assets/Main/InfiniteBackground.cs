using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private float tweenDuration;
    //https://www.youtube.com/watch?v=rDs0fUnDVUU
    private void Awake()
    {

        var image = GetComponent<Image>();
        var rectTransform = GetComponent<RectTransform>();
        var sprite = image.sprite;

        //var posTween = DOTween.To(
        //    () => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x,
        //     new Vector2(
        //         -sprite.texture.width * .05f,
        //         -sprite.texture.height * .05f),
        //     tweenDuration);

        //posTween.SetEase(Ease.Linear);
        //posTween.SetLoops(-1, LoopType.Restart);

        var sizeTween = DOTween.To(
            () => rectTransform.sizeDelta, x => rectTransform.sizeDelta = x,
            new Vector2(
                sprite.texture.width, 
                sprite.texture.height),
            tweenDuration);
        sizeTween.SetEase(Ease.Linear);
        sizeTween.SetLoops(-1, LoopType.Restart);
    }
}
