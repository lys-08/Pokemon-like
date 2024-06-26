using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage_;
        [SerializeField] private UIHealthBar uiHealthBar;
        [SerializeField] private TextMeshProUGUI title_;
        [SerializeField] private TextMeshProUGUI description_;


        private void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            /*
             * We disable the image so the quantity is also disabled
             */
            this.itemImage_.gameObject.SetActive(false);
            this.uiHealthBar.gameObject.SetActive(false);
            this.title_.text = "";
            this.description_.text = "";
        }

        /**
         * Methods that print the description of the clicked item
         */
        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            this.itemImage_.gameObject.SetActive(true);
            this.uiHealthBar.gameObject.SetActive(false);
            this.itemImage_.sprite = sprite;
            this.title_.text = itemName;
            this.description_.text = itemDescription;
        }
        
        /**
         * Methods that print the description of the clicked item
         */
        public void SetPokemonDescription(PokemonSO pokemon)
        {
            this.itemImage_.gameObject.SetActive(true);
            this.uiHealthBar.gameObject.SetActive(true);
            this.uiHealthBar.SetPokemon(pokemon);
            this.itemImage_.sprite = pokemon.image;
            this.title_.text = pokemon.Name;
            string itemDescription = pokemon.description + "\n\n"
                                     + "<b>Type</b> : " + pokemon.type + "\n"
                                     + "<b>Damage</b> : " + pokemon.damage + "\n"
                                     + "<b>Defense</b> : " + pokemon.defense + "\n"
                                     + "<b>Speed</b> : " + pokemon.speed + "\n";
            this.description_.text = itemDescription;
        }
    }

}