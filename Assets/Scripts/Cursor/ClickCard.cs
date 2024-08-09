using System.Collections;
using System.Collections.Generic;
using Mygame.Card;
using UnityEngine;
namespace Mygame.Cursor
{
    public class ClickCard : MonoBehaviour
    {
        public bool isPause;
        void Update()
        {
            if (!isPause)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.tag == "Card")
                        {
                            GameCard card = hit.collider.gameObject.GetComponentInParent<GameCard>();
                            CardManager.Instance.ClickCard(card);
                        }
                    }
                }
            }
        }
    }
}