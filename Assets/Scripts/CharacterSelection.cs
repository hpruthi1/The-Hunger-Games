using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    #region Serialized .
    [SerializeField]
    private CharacterTypes _characterType;
    public CharacterTypes CharacterType { get { return _characterType; } }

    #endregion

    #region Private .

    private CharacterCustomization _characterCustomization;
    #endregion
    
    public void OnClickButton()
    {
        _characterCustomization.SelectCharacter(this);
    }
}
