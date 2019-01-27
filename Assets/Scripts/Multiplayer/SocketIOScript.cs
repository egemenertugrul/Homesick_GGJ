
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

public class ChatData
{
    public string id;
    public string msg;
};

public class SocketIOScript : MonoBehaviour
{
    public string serverURL = "http://localhost:3000";

    public InputField uiInput = null;
    public Button uiSend = null;
    public Button imSend = null;

    public Text uiChatLog = null;

    protected Socket socket = null;
    protected List<string> chatLog = new List<string>();

    protected float timer = 50f;

    TurretPlayer tp;
    void Destroy()
    {
        DoClose();
    }

    // Use this for initialization
    void Start()
    {
        GameObject turret = GameObject.Find("Turret");
        tp = turret.GetComponent<TurretPlayer>();
        DoOpen();

        //imSend.onClick.AddListener(() =>
        //{
        //    SendImage();
        //});


        //      uiSend.onClick.AddListener(() => {
        //	SendChat(uiInput.text);
        //	uiInput.text = "";
        //	uiInput.ActivateInputField();
        //});
    }

    // Update is called once per frame
    void Update()
    {
        timer--;
        //print(timer);
        if(timer <= 0)
        {
            timer = 150f;
            Reconnect();
        }
        //lock (chatLog) {
        //	if (chatLog.Count > 0) {
        //		string str = uiChatLog.text;
        //		foreach (var s in chatLog) {
        //			str = str + "\n" + s;
        //		}
        //		uiChatLog.text = str;
        //		chatLog.Clear ();
        //	}
        //}
    }

    void Reconnect()
    {
        print("Reconnecting...");
        DoClose();
        DoOpen();
    }

    void DoOpen()
    {
        if (socket == null)
        {
            socket = IO.Socket(serverURL);
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                lock (chatLog)
                {
                    // Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
                    chatLog.Add("Socket.IO connected.");
                }
                print("Socket.IO connected.");
            });
            socket.On("chat", (data) =>
            {
                string str = data.ToString();

                ChatData chat = JsonConvert.DeserializeObject<ChatData>(str);
                string strChatLog = "user#" + chat.id + ": " + chat.msg;

                // Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
                lock (chatLog)
                {
                    chatLog.Add(strChatLog);
                }
            });

            socket.On("fire", (data) =>
            {
                string str = data.ToString();
                ChatData chat = JsonConvert.DeserializeObject<ChatData>(str);
                string[] strFire = chat.msg.Split(',');
                Vector2 coordinates = new Vector2(float.Parse(strFire[0]), 1.0f-float.Parse(strFire[1]));
                //print("Fire received from: " + chat.id);
                //print(coordinates);
                tp.lastFire = coordinates;
                tp.lastFired = false;
            });

            socket.On("ack", (data) =>
            {
                //print("ack received");
                timer = 50f;
            });

                socket.On("disconnect", (data) =>
            {
                string str = data.ToString();

                ChatData chat = JsonConvert.DeserializeObject<ChatData>(str);
                //remove chat.id

                //string strChatLog = "user#" + chat.id + ": " + chat.msg;

                //// Access to Unity UI is not allowed in a background thread, so let's put into a shared variable
                //lock (chatLog)
                //{
                //    chatLog.Add(strChatLog);
                //}
            });
        }
    }

    void DoClose()
    {
        if (socket != null)
        {
            socket.Disconnect();
            socket = null;
        }
    }

    void SendChat(string str)
    {
        if (socket != null)
        {
            socket.Emit("chat", str);
        }
    }

    public void SendImageTo(int clientId, byte[] data)
    {
        if (socket != null)
        {
            //print("Sending image to client id: " + clientId);
            //print(System.Text.Encoding.UTF8.GetString(data));
            socket.Emit("image", data);
        }
    }
}
