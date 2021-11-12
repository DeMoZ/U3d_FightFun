using System;
using UnityEngine;

public class MessageHub : MonoBehaviour
{
    public static Action<Bot, Bot> BotGotHit;
}