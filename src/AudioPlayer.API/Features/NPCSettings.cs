// <copyright file="NPCSettings.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

using Exiled.API.Features;
using SCPSLAudioApi.AudioCore;

/// <summary>
/// NPC settings.
/// </summary>
public class NPCSettings
{
    private readonly AudioPlayerBase audioPlayerBase = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCSettings"/> class.
    /// </summary>
    public NPCSettings()
    {
        this.audioPlayerBase = new();
    }

    /// <summary>
    /// Gets or sets currently playing music for IDs.
    /// </summary>
    public List<int> PlaysFor
    {
        get => this.AudioPlayerBase.BroadcastTo;
        set
        {
            this.AudioPlayerBase.BroadcastTo = value;
        }
    }

    /// <summary>
    /// Gets or sets currently playing music for IDs.
    /// </summary>
    public List<Player> PlaysForPlayers
    {
        get
        {
            List<Player> played = new();

            this.AudioPlayerBase.BroadcastTo.ForEach(x =>
            {
                if (Player.TryGet(x, out Player pl))
                {
                    played.Add(pl);
                }
            });

            return played;
        }

        set
        {
            this.AudioPlayerBase.BroadcastTo = value;
        }
    }

    /// <summary>
    /// Gets <see cref="AudioPlayerBase"/> main class.
    /// </summary>
    public AudioPlayerBase AudioPlayerBase => this.audioPlayerBase;
}