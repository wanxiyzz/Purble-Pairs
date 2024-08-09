using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame.Card;
namespace Mygame.Cursor
{
    public class CheckCard : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        GameCard card;
        public bool isPause;
        void Update()
        {
            if (!isPause)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Card")
                    {
                        card?.IsSelected(false);
                        card = hit.collider.gameObject.GetComponentInParent<GameCard>();
                        card.IsSelected(true);
                    }
                    else
                    {
                        card.IsSelected(false);
                    }
                }
                else
                {
                    card?.IsSelected(false);
                    card = null;
                }
            }
        }
    }
}