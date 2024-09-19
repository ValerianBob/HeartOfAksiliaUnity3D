using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitPointLine : MonoBehaviour
{
    public PlayerController playerController;

    private float _maxHP = 100;

    private float _hp;

    [SerializeField]
    private UnityEvent Die;

    [SerializeField]
    private UnityEvent<float> HpChanged;

    [SerializeField]
    private UnityEvent<float> HpChangedPercent;

    public float HP
    {
        get => _hp;
        set 
        {
            _hp = value;
            HpChanged?.Invoke(_hp);
            HpChangedPercent?.Invoke(_hp / _maxHP);

            if (_hp <= 0)
            {
                Die?.Invoke();
            }

        }
    }

    private void Update()
    {
        HP = playerController.hitPoints;
    }
}
