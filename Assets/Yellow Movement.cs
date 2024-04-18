using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YellowMovement : Movement
{
    [SerializeField] private LayerMask yellowLayerMask;
    private Tween _tween;
    private readonly Collider[] _yellowOverlap = new Collider[3];
    public int Depth;
    YellowGroup yellowGroup;
    public override void Move()
    {
        for (var i = 0; i < yellowGroup.yellowMoved.Length; i++)
        {
            if (yellowGroup.yellowMoved[i] is null)
            {
                yellowGroup.yellowMoved[i] = GetComponent<BoxCollider>();
                break;
            }
            if (i == 2) return;
        }

        Physics.OverlapSphereNonAlloc(transform.position, 2, _yellowOverlap, yellowLayerMask);
        if (Depth == 0) Depth = _yellowOverlap[2] is null ? 0 : 1;

        if (transform.localPosition.y < transform.localScale.y - 1 && (_tween is null || !_tween.IsPlaying()))
        {
            var newPosition = transform.localPosition.y + (transform.localScale.y - Depth - 1) * 2 ;
            _tween = transform.DOLocalMoveY(newPosition, 0.75f);
        }

        foreach (var yellow in _yellowOverlap)
        {
            if (yellow is not null && !yellowGroup.yellowMoved.Contains(yellow))
            {              
                yellow.GetComponent<YellowMovement>().Follow(Depth + 1);
            }
        }
    }

    public void Follow(int depth)
    {
        Depth = depth;
        Move();
    }

    public override void MoveBack()
    {
        foreach (var yellow in yellowGroup.yellowMoved)
        {
            if (yellow is not null)
            {
                _tween = yellow.transform.DOLocalMoveY(0, 1.25f);
            }
        }
        for (var i = 0; i < yellowGroup.yellowMoved.Length; i++)
        {
            yellowGroup.yellowMoved[i].GetComponent<YellowMovement>().Depth = 0;
            yellowGroup.yellowMoved[i] = null;
        }
    }
    private void Start()
    {
        yellowGroup = transform.parent.GetComponent<YellowGroup>();
    }
}









