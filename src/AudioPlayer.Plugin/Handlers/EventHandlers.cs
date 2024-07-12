// <copyright file="EventHandlers.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Handlers;

using ServerEvent = Exiled.Events.Handlers.Server;

/// <summary/>
public class EventHandlers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventHandlers"/> class.
    /// </summary>
    public EventHandlers()
    {
        ServerEvent.WaitingForPlayers += this.OnWaitingForPlayers_EnableMusic;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="EventHandlers"/> class.
    /// </summary>
    ~EventHandlers()
    {
        ServerEvent.WaitingForPlayers -= this.OnWaitingForPlayers_EnableMusic;
    }

    /// <summary>
    /// Plays music (if exists) on waiting for players (lobby).
    /// </summary>
    public void OnWaitingForPlayers_EnableMusic()
    {
        return;
    }
}
