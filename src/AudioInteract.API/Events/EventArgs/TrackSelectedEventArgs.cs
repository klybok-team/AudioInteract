// -----------------------------------------------------------------------
// <copyright file="TrackSelectedEventArgs.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AudioInteract.API.Events.EventArgs;

using AudioInteract.Features;
using SCPSLAudioApi.AudioCore;

/// <summary>
/// Contains all information after track are finished.
/// </summary>
public class TrackSelectedEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrackSelectedEventArgs"/> class.
    /// </summary>
    /// <param name="playerBase">Base AudioPlayer instance.</param>
    /// <param name="track">Path to track what be played.</param>
    /// <param name="directPlay">Play directly or not.</param>
    /// <param name="queuePos">Next queue played track.</param>
    public TrackSelectedEventArgs(AudioPlayerBase playerBase, string track, bool directPlay, int queuePos)
    {
        this.MusicInstance = MusicAPI.MusicInstances.FirstOrDefault(x => x.AudioPlayerBase == playerBase);
        this.Track = track;
        this.DirectPlay = directPlay;
        this.QueuePos = queuePos;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackSelectedEventArgs"/> class.
    /// </summary>
    /// <param name="musicInstance">Music Instance.</param>
    /// <param name="track">Path to track what be played.</param>
    /// <param name="directPlay">Track are forced to play or not. (queue == -1).</param>
    /// <param name="queuePos">Next queue played track.</param>
    public TrackSelectedEventArgs(MusicInstance musicInstance, string track, bool directPlay, int queuePos)
    {
        this.MusicInstance = musicInstance;
        this.Track = track;
        this.DirectPlay = directPlay;
        this.QueuePos = queuePos;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackSelectedEventArgs"/> class.
    /// </summary>
    /// <param name="musicInstance">Music Instance.</param>
    /// <param name="track">Audio File.</param>
    /// <param name="directPlay">Track are forced to play or not. (queue == -1).</param>
    /// <param name="queuePos">Next queue played track.</param>
    public TrackSelectedEventArgs(MusicInstance musicInstance, AudioFile track, bool directPlay, int queuePos)
    {
        this.MusicInstance = musicInstance;
        this.Track = track.FilePath;
        this.DirectPlay = directPlay;
        this.QueuePos = queuePos;
    }

    /// <summary>
    /// Gets music Instance.
    /// </summary>
    public MusicInstance? MusicInstance { get; }

    /// <summary>
    /// Gets path to played track.
    /// </summary>
    public string Track { get; }

    /// <summary>
    /// Gets a value indicating whether track are forced to play or not. (queue == -1).
    /// </summary>
    public bool DirectPlay { get; }

    /// <summary>
    /// Gets next queue position.
    /// </summary>
    public int QueuePos { get; }
}