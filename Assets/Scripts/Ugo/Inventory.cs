using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    #region Attributes
    public int coinsCount;
    public TMP_Text coinsCountText;
    public static Inventory instance;
    #endregion

    #region API
    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }

    public void AddCoins (int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }
    #endregion
}
