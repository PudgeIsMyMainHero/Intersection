using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedMovement : Movement
{
    private Tween _tween;
 
    public override void MoveBack()
    {
        if (-transform.localPosition.y < transform.localScale.y - 1 && (_tween is null || !_tween.IsPlaying()))
        {
            var newPosition = transform.localPosition.y-2;
            _tween = transform.DOLocalMoveY(newPosition, 0.5f);
  
        }
    }

    public override void Move()
    {
        if (transform.localPosition.y < transform.localScale.y - 1 && (_tween is null || !_tween.IsPlaying())) 
        {
           var newPosition = transform.localPosition.y+2;
           _tween = transform.DOLocalMoveY(newPosition, 0.5f);
           

        }
    }
}
