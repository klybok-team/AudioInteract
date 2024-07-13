// <copyright file="MusicInstance.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

using System.Linq;
using Exiled.API.Features;
using SCPSLAudioApi.AudioCore;

/// <summary>
/// Music Instance API for NPC.
/// </summary>
public partial class MusicInstance
{
    private readonly AudioPlayerBase audioPlayerBase = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="MusicInstance"/> class.
    /// </summary>
    public MusicInstance()
    {
        this.audioPlayerBase = new();
    }

    /// <summary>
    /// Gets or sets IDs who recive playing music.
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
    /// Gets or sets players who recive playing music.
    /// </summary>
    [Obsolete("Use plays for with IDs (int). Ignore this if you don't care.")]
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

        set => this.AudioPlayerBase.BroadcastTo = value.Select(x => x.Id).ToList();
    }

    /// <summary>
    /// Gets <see cref="AudioPlayerBase"/> main class.
    /// </summary>
    public AudioPlayerBase AudioPlayerBase => this.audioPlayerBase;

    /// <summary>
    /// Gets track queue. Doesn't show current playing track.
    /// </summary>
    public List<string> TrackQueue => this.AudioPlayerBase.AudioToPlay;

    /// <summary>
    /// Gets current playing track.
    /// </summary>
    public string PlayingTrack => this.AudioPlayerBase.CurrentPlay;

    /// <summary>
    /// Gets or sets a value indicating whether current track are looped or not.
    /// </summary>
    public bool IsLoop
    {
        get => this.AudioPlayerBase.Loop;
        set => this.AudioPlayerBase.Loop = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether playing tracks are mixed or not.
    /// </summary>
    public bool IsShuffle
    {
        get => this.AudioPlayerBase.Shuffle;
        set => this.AudioPlayerBase.Shuffle = value;
    }

    /// <summary>
    /// Gets a value indicating whether track is finished or not.
    /// </summary>
    public bool IsFinished => this.AudioPlayerBase.IsFinished;

    /// <summary>
    /// Gets a value indicating whether track is finished or not.
    /// </summary>
    public bool IsFinished => this.AudioPlayerBase.LogInfo;

    /// <summary>
    /// Start playing music.
    /// </summary>
    /// <param name="audioFile">Plays <see cref="AudioFile"/>.</param>
    /// <returns>Is music started or not.</returns>
    public bool Play(AudioFile audioFile)
    {
        return true;
    }

    /// <summary>
    /// Stop playing music. Remove all music in queue.
    /// </summary>
    /// <returns>Is music stopped or not.</returns>
    public bool Stop()
    {
        return true;
    }
}