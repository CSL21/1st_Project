using UnityEngine;

public class LobbyButtonManager : MonoBehaviour
{
    public GameObject shopObject;
    public GameObject Inventory;


    public void OpenInv()
    {
        if (Inventory != null)

            Inventory.SetActive(true);

    }

    public void CloseInv()
    {
        if (Inventory != null)

            Inventory.SetActive(false);
    }


    public void OpenShop()
    {
        if (shopObject != null)

            shopObject.SetActive(true);

    }

    public void CloseShop()
    {
        if (shopObject != null)
            
            shopObject.SetActive(false);
    }
}