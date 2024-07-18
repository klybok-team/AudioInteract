// <copyright file="EventHandlers.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Handlers;

using AudioPlayer.Features;
using AudioAPI = AudioPlayer.Features.API;
using ServerEvent = Exiled.Events.Handlers.Server;

/// <summary/>
public class EventHandlers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventHandlers"/> class.
    /// </summary>
    public EventHandlers()
    {
        if (!PluginInstance.Instance!.Config.IsEventsEnabled)
        {
            return;
        }

        ServerEvent.WaitingForPlayers += this.OnWaitingForPlayers_EnableMusic;
        ServerEvent.RoundStarted += this.OnRoundStarted_StopMusic;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="EventHandlers"/> class.
    /// </summary>
    ~EventHandlers()
    {
        if (!PluginInstance.Instance!.Config.IsEventsEnabled)
        {
            return;
        }

        ServerEvent.WaitingForPlayers -= this.OnWaitingForPlayers_EnableMusic;
        ServerEvent.RoundStarted -= this.OnRoundStarted_StopMusic;
    }

    /// <summary>
    /// Gets lobby playing NPCs.
    /// </summary>
    public static List<MusicInstance> LobbyPlayingNPC { get; private set; } = new();

    /// <summary>
    /// Plays music (if exists) on waiting for players (lobby).
    /// </summary>
    public void OnWaitingForPlayers_EnableMusic()
    {
        foreach (AudioFile audioFile in PluginInstance.Instance!.Config.LobbyMusic.Where(x => x.IsEnabled))
        {
            MusicInstance? musicInstance = AudioAPI.CreateNPC(PluginInstance.Instance!.Config.LobbyMusicNPCName);

            if (musicInstance == null)
            {
                continue;
            }

            musicInstance.LoggedType = Features.LoggedType.Info;

            musicInstance.Play(audioFile);

            LobbyPlayingNPC.Add(musicInstance);
        }
    }

    /// <summary>
    /// Stops music if was playing in lobby.
    /// </summary>
    public void OnRoundStarted_StopMusic()
    {
        foreach (MusicInstance musicInstance in LobbyPlayingNPC)
        {
            AudioAPI.DestroyNPC(musicInstance);
        }
    }
}
