using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "CaptureModifier", menuName = "Inventory/CaptureModifier")]
    public class PokemonStatCaptureModifierSO : PokemonStatModifierSO
    {
        public override bool AffectPokemon(PokemonSO pokemonSo, float value, Type type)
        {
            /*
             * If the pokeball didn't has a value, we define the rate of catching the pokemon
             * -> the rate change according to the type of the ball and the pokemon
             */
            if (value == 0)
            {
                float rate = 1 - pokemonSo.hp / pokemonSo.hpMax;
            
                if (type == pokemonSo.type) // The ball as the same type as the pokemon
                {
                    return CatchPokemon(rate * 1.5f);
                }
                else if (type == Type.Simple) // The ball doesn't have any type
                {
                    return CatchPokemon(rate);
                }
                else
                {
                    return CatchPokemon(rate * 0.5f);
                }
            }
            /*
             * If the pokeball has a value, we try to catch the pokemon with the ball rate
             */
            else
            {
                return CatchPokemon(value);
            }
        }
        
        /**
         * Return true if the pokemon is catched
         */
        private bool CatchPokemon(float value)
        {
            if (value >= 1f) return true;
            
            // TODO : attraper un pokemon
            return false;
        }
    }
}
