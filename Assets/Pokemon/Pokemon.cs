using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pokemon : MonoBehaviour
{
    /*
     * Coefficients apply in a fight
     */
    private float damageCoef_ = 1f; 
    private float defenseCoef_ = 1f;

    [SerializeField] private PokemonSO data_;
    
    
    
    #region Getter & Setter

    public string GetName()
    {
        return data_.name;
    }

    public void SetName(string newName)
    {
        data_.name = newName;
    }
    
    public string GetDescription()
    {
        return data_.description;
    }
    
    public float GetHp()
    {
        return data_.hp;
    }
    
    public void SetHp(float newHp)
    {
        data_.hp = newHp;
    }

    public float GetDamage()
    {
        return data_.damage * damageCoef_;
    }
    
    public float GetSpeed()
    {
        return data_.speed;
    }

    public Type GetType()
    {
        return data_.type;
    }

    public bool IsKO()
    {
        return data_.ko;
    }

    public void Revive()
    {
        data_.ko = false;
    }
    
    #endregion
}
