// <copyright file="NPCSettings.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

using SCPSLAudioApi.AudioCore;

/// <summary>
/// NPC settings.
/// </summary>
public class NPCSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NPCSettings"/> class.
    /// </summary>
    /// <param name="id">ID to set to bot.</param>
    public NPCSettings(int id = 0)
    {
        this.AudioPlayerBase = new();
        this.ID = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NPCSettings"/> class.
    /// </summary>
    public NPCSettings()
    {
        this.AudioPlayerBase = new();
    }

    /// <summary>
    /// Gets or sets <see cref="AudioPlayerBase"/> main class.
    /// </summary>
    public AudioPlayerBase? AudioPlayerBase { get; set; }

    /// <summary>
    /// Gets ID in player list of bot.
    /// </summary>
    public int ID { get; } = -1;
}
