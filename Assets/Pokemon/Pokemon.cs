using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    /*
     * Coefficients apply in a fight
     */
    private float damageCoef_; 
    private float defenseCoef_;

    [SerializeField] private PokemonSO data_;
    
    
    #region Getter & Setter

    public float GetHp()
    {
        return data_.hp;
    }

    public float GetDamage()
    {
        return data_.damage;
    }

    public float GetDefense()
    {
        return data_.defense;
    }

    public float GetSpeed()
    {
        return data_.speed;
    }

    public Type GetType()
    {
        return data_.type;
    }

    #endregion


    public void TakeDamage(float damage, Type type)
    {
        // TODO
    }

    public void TakeDistraction()
    {
        damageCoef_ -= 0.1f;
        defenseCoef_ -= 0.1f;
    }

    public void TakeFocus()
    {
        damageCoef_ += 0.1f;
        defenseCoef_ += 0.1f;
    }

    public bool IsKO()
    {
        return data_.hp < 0f;
    }
}
