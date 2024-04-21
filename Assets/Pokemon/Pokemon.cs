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
    


    /**
     * Pokemon damage function. The damage dealt depends on the type of the attacking pokémon relative
     * to the type of the current pokemon, as well as the original power of the attack
     */
    public void TakeDamage(float damage, Type type)
    {
        float damageInflicted;
        switch (type)
        {
            case (Type.Ruby):
                if (data_.type == Type.Sapphire) damageInflicted = 0.5f * damage;
                else if (data_.type == Type.Emerald) damageInflicted = 1.5f * damage;
                else damageInflicted = damage; // (data_.type == Type.Ruby)
                break;
            case (Type.Sapphire):
                if (data_.type == Type.Ruby) damageInflicted = 1.5f * damage;
                else if (data_.type == Type.Emerald) damageInflicted = 0.5f * damage;
                else damageInflicted = damage; // (data_.type == Type.Sapphire)
                break;
            case (Type.Emerald):
                if (data_.type == Type.Sapphire) damageInflicted = 1.5f * damage;
                else if (data_.type == Type.Ruby) damageInflicted = 0.5f * damage;
                else damageInflicted = damage; // (data_.type == Type.Emerald)
                break;
            default:
                damageInflicted = damage;
                break;
        }

        /*
         * We apply the damage according of the defense coefficient of the pokemon
         */
        data_.hp -= (damageInflicted - data_.defense * defenseCoef_);
        
        // KO
        if (data_.hp < 0) data_.ko = true;
    }
    
    /**
     * Reduces the pokemon attack and defense by a percentage for battle duration
     * -> used when the opposed pokemon uses a distraction attack
     */
    public void TakeDistraction()
    {
        /*
         * If the damage coefficient is lesser or equal to 0.5f then we do nothing
         * -> For the condition, we can test only one out of the two value because they
         *    are modified at the same time, by the same percentage
         */
        if (damageCoef_ >= 1.5f) return;
        
        damageCoef_ += 0.1f;
        defenseCoef_ -= 0.1f;
    }

    /**
     * Increased the pokemon attack and defense by a percentage for battle duration
     * -> used when the pokemon uses a focusing attack
     */
    public void TakeFocus()
    {
        /*
         * If the damage coefficient is greater or equal to 1.5f then we do nothing
         * -> For the condition, we can test only one out of the two value because they
         *    are modified at the same time, by the same percentage
         */
        if (damageCoef_ <= 0.5f) return;
        
        damageCoef_ -= 0.1f;
        defenseCoef_ += 0.1f;
    }


    /**
     * Reset all values modified during a fight which are specific to a fight
     * This function is called at the end of a fight
     */
    public void ResetCoeffs()
    {
        damageCoef_ = 1f;
        defenseCoef_ = 1f;
    }
}
