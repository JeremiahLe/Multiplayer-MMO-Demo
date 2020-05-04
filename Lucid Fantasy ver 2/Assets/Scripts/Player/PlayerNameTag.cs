using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private Text nameText;

    private void Start()
    {
        //if (photonView.IsMine) { return; }

        SetName();
    }

    private void SetName() => nameText.text = photonView.Owner.NickName;
}
