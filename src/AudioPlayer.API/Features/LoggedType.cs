// <copyright file="LoggedType.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

/// <summary>
/// Display track info.
/// </summary>
public enum LoggedType
{
    /// <summary>
    /// Do not show any logs in console.
    /// </summary>
    None,

    /// <summary>
    /// Display info when: track starts to play, track completed, track loading.
    /// </summary>
    Info,

    /// <summary>
    /// Display ALL information of current playing track. Use ONLY for debugging. Displays info for: allowedSamples, samplesPerSecond, StreamBuffer.Count, PlaybackBuffer, PlaybackBuffer.WriteHead.
    /// </summary>
    Debug,
}