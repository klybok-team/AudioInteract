// <copyright file="AudioFile.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

using Exiled.API.Features;
using VoiceChat;

/// <summary>
/// Creates audio file to easy interact with API.
/// </summary>
public class AudioFile
{
    /// <summary>
    /// Gets or sets path to audio file. Default leads to EXILED root directory (EXILED/Audio/track.ogg).
    /// </summary>
    public string FilePath { get; set; } = Path.Combine(Paths.Exiled, "Audio", "track.ogg");

    /// <summary>
    /// Gets or sets a value indicating whether loop track or not. Default is false.
    /// </summary>
    public bool IsLooped { get; set; } = false;

    /// <summary>
    /// Gets or sets value that indicates volume of track. Default is 75.
    /// </summary>
    public int Volume { get; set; } = 75;

    /// <summary>
    /// Gets or sets <see cref="VoiceChatChannel"/> of bot. Default is Intercom.
    /// </summary>
    public VoiceChatChannel VoiceChannel { get; set; } = VoiceChatChannel.Intercom;

    /// <summary>
    /// Gets or sets ID of bot. If ID is negative - will create new bot with random ID.
    /// </summary>
    public int BotId { get; set; } = -1;

    /// <summary>
    /// Start playing music.
    /// </summary>
    /// <returns>Is music started or not.</returns>
    public bool Play()
    {
        return true;
    }

    /// <summary>
    /// Stop playing music.
    /// </summary>
    /// <returns>Is music started or not.</returns>
    public bool Stop()
    {
        return true;
    }
}
