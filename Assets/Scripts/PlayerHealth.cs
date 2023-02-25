using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private int health = 8;
    [SerializeField] private TMPro.TMP_Text healthText;
    #endregion

    #region Public Fields

    #endregion

    #region Monobehavior Callbacks
    private void Start()
    {
        healthText.text = "Health: " + health;
    }
    #endregion

    #region Private Methods

    #endregion

    #region Public Methods
    public bool TakeDamage()
    {
        health -= 1;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
    #endregion
}
