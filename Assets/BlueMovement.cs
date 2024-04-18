using DG.Tweening;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMovement : Movement
{
    BoxCollider boxCollider;
    private Tween _tween;
    public override void MoveBack()
    {
        if (-transform.localPosition.y < transform.localScale.y - 1 && (_tween is null || !_tween.IsPlaying()))
        {
            var newPosition = transform.localPosition.y - 2;
            _tween = transform.DOLocalMoveY(newPosition, 0.5f);
            boxCollider.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.TryGetComponent<FirstPersonController>(out var firstPersonController))
        {
            firstPersonController.BlueBounce();
        }
        Thrown();
    }
    public void Thrown()
    {
        _tween = transform.DOLocalMoveY(2, 0.5f);
        boxCollider.enabled = false;
    }
    public override void Move()
    {

    }
    private void Awake()
    {
        boxCollider = transform.GetComponent<BoxCollider>();
    }
}
