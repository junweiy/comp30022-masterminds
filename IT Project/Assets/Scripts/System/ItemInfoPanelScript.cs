using UnityEngine;
using System.Collections;

public class ItemInfoPanelScript : MonoBehaviour {

    private Item item;
    private Character character;

    public void initialise(Item item, Character character)
    {
        this.item = item;
        this.character = character;
    }

    public void buyButton()
    {
        if (item.itemType != ItemTypeEnum.Spell)
        {
            // upgrade item
            if (character.items.Contains(item))
            {
                this.item.removeEffect(this.character);
                this.item.levelUp();
                this.item.applyEffect(this.character);
            }
            // buy new item
            else
            {
                this.item.applyEffect(this.character);
                character.addItem(item);
            }
        }
        else
        {
            //TODO: how to upgrade spell
            //if (character.spells.Contains(item))
            //{

            //}
        }
        Destroy(GameObject.Find("InfoPanel(Clone)"));
    }

    public void closeButton()
    {
        Destroy(GameObject.Find("InfoPanel(Clone)"));
    }
}
