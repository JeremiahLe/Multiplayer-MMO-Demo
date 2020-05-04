using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class GameManager : MonoBehaviourPun
{
    public string username;

    public GameObject systemsManager;

    public int maxMessages = 25;

    public GameObject chatPanel, textObject;
    public InputField chatBox;

    public Color playerMessage, info;

    public bool cantMove = false;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    private void Awake()
    {
        systemsManager = GameObject.FindGameObjectWithTag("SystemsManager");
        username = systemsManager.GetComponent<NameTransfer>().playerName;
    }

    private void SetName()
    {
        //username = photonView.Owner.NickName;
    }

    void Update()
    {
        //username = photonView.Owner.NickName;

        if (chatBox.text != "")
        {
            cantMove = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //SendMessageToChat(username + ": " +chatBox.text, Message.MessageType.playerMessage);
                photonView.RPC("SendMessageToChat", RpcTarget.All, username + ": " + chatBox.text, Message.MessageType.playerMessage);
                chatBox.text = "";
                Debug.Log("Sent a message");
                cantMove = false;
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
                chatBox.ActivateInputField();

            cantMove = false;
        }

        if(!chatBox.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessageToChat("Server: This is a test message.", Message.MessageType.info);
                Debug.Log("Space");
            }
        }

    }

    [PunRPC]
    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageTypeColor(messageType);

        messageList.Add(newMessage);
    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;

        switch(messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
        }

        return color;
    }
}

[System.Serializable]
public class Message
{
    public string text;

    public Text textObject;

    public MessageType messageType;

    public enum MessageType
    {
        playerMessage, 
        info
    }
}
