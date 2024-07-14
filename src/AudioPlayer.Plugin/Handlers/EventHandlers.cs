// <copyright file="EventHandlers.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Handlers;

using Exiled.API.Features;
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
    public static List<Npc> LobbyPlayingNPC { get; private set; } = new();

    /// <summary>
    /// Plays music (if exists) on waiting for players (lobby).
    /// </summary>
    public void OnWaitingForPlayers_EnableMusic()
    {
        foreach (Features.AudioFile? audioFile in PluginInstance.Instance!.Config.LobbyMusic.Where(x => x.IsEnabled))
        {
            Log.Info(audioFile.FilePath);

            Features.MusicInstance? musicInstance = AudioAPI.CreateNPC(PluginInstance.Instance!.Config.LobbyMusicNPCName);

            if (musicInstance == null)
            {
                continue;
            }

            musicInstance.LoggedType = Features.LoggedType.Info;

            musicInstance.Play(audioFile);
        }
    }

    /// <summary>
    /// Stops music if was playing in lobby.
    /// </summary>
    public void OnRoundStarted_StopMusic()
    {
        foreach (Npc npc in LobbyPlayingNPC)
        {
            AudioAPI.DestroyNPC(npc);
        }
    }
}
