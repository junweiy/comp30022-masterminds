using Command;
using Manager;
using UnityEngine;

public class InputHandler {

    private Game g;

    public InputHandler(Game g)
    {
        this.g = g;
    }

    public void UpdateInput()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // can be different, to be modified later
                Vector3 dest = new Vector3((int)hit.point.x, (int)hit.point.y, (int)hit.point.z);
                ICommand act = new MoveCommand(g.CurrentFrame, PlayerManager.LocalCharacterID, dest);
                CommandSender.AddCommand(act);
            }
        }


    }

}
