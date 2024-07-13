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
    /// Initializes a new instance of the <see cref="AudioFile"/> class.
    /// </summary>
    /// <param name="filePath">Path to played file.</param>
    /// <param name="isLooped">Is track looped or not.</param>
    /// <param name="volume">Volume of track.</param>
    /// <param name="voicechannel">Voice channel of track.</param>
    /// <param name="botid">Bot ID.</param>
    public AudioFile(string filePath, bool isLooped = false, int volume = 75, VoiceChatChannel voicechannel = VoiceChatChannel.Intercom, int botid = -1)
    {
        this.FilePath = filePath;
        this.IsLooped = isLooped;
        this.Volume = volume;
        this.VoiceChannel = voicechannel;
        this.BotId = botid;
    }

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
    /// Gets or sets ID of bot. If ID is negative, API will create new bot with random ID.
    /// </summary>
    public int BotId { get; set; } = -1;
}
