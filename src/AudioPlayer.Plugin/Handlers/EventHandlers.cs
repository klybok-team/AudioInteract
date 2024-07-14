// <copyright file="EventHandlers.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin.Handlers;

using Exiled.API.Features;
using AudioAPI = AudioPlayer.Features.API;
using ServerEvent = Exiled.Events.Handlers.Server;
using PlayerEvent = Exiled.Events.Handlers.Player;
using Exiled.Events.EventArgs.Player;
using SCPSLAudioApi.AudioCore;

/// <summary/>
public class EventHandlers
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventHandlers"/> class.
    /// </summary>
    public EventHandlers()
    {
        PlayerEvent.Verified += this.OnVerifed_AudioPlayerBase_Get;

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
        PlayerEvent.Verified -= this.OnVerifed_AudioPlayerBase_Get;

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
        foreach (var audioFile in PluginInstance.Instance!.Config.LobbyMusic.Where(x => x.IsEnabled))
        {
            Log.Info(audioFile.FilePath);

            var musicInstance = AudioAPI.CreateNPC(PluginInstance.Instance!.Config.LobbyMusicNPCName);

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
        foreach (var npc in LobbyPlayingNPC)
        {
            AudioAPI.DestroyNPC(npc);
        }
    }

    /// <summary>
    /// Add component to player, when he is joined to server.
    /// </summary>
    /// <param name="ev">Event Args.</param>
    public void OnVerifed_AudioPlayerBase_Get(VerifiedEventArgs ev)
    {
        AudioPlayerBase.Get(ev.Player.ReferenceHub);
    }
}
