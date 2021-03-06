﻿using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        NetworkPlayerCharacter spaceship = gamePlayer.GetComponent<NetworkPlayerCharacter>();

        spaceship.name = lobby.name;
        spaceship.characterIndex = lobby.characterIndex;
    }
}
