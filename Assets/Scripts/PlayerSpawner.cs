using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSpawner : NetworkBehaviour
{
    #region Types .
    [System.Serializable]

    private class CharacterPrefab
    {
        [SerializeField]
        private GameObject _prefab;
        public GameObject Prefab { get { return _prefab; } }

        [SerializeField]
        private CharacterTypes _characterType;

        public CharacterTypes CharacterType { get { return _characterType; } }
    }

    #endregion

    #region Serialized .

    [SerializeField]
    private List<CharacterPrefab> _characterPrefabs = new List<CharacterPrefab>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TrySpawn(CharacterTypes characterType)
    {
        //CmdTrySpawn((int)characterType);
    }

    [Command]

    private void CmdTrySpawn(int characterType)
    {
       // CharacterTypes characterEnum = (CharacterTypes)characterType;
        //int index = -_characterPrefabs.FindIndex(x => x.CharacterType == characterEnum);

        //GameObject result = Instantiate(_characterPrefabs[index].Prefab,gameObject.transform.position,Quaternion.identity);
        //NetworkServer.Spawn(result, base.netIdentity.connectionToClient);
    }
}
