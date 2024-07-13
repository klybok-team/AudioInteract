// <copyright file="MusicInstance.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API.Features;

using System.IO;
using System.Linq;
using Exiled.API.Features;
using SCPSLAudioApi.AudioCore;

/// <summary>
/// Music Instance API for NPC.
/// </summary>
public partial class MusicInstance
{
    private AudioPlayerBase audioPlayerBase = new();

    private LoggedType loggedType = LoggedType.None;

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
    /// Gets or sets logs type <see cref="Features.LoggedType"/>.
    /// </summary>
    public LoggedType LoggedType
    {
        get => this.loggedType;
        set
        {
            this.AudioPlayerBase.LogDebug = false;
            this.AudioPlayerBase.LogInfo = false;

            switch (value)
            {
                case LoggedType.Info:
                    this.AudioPlayerBase.LogInfo = true;
                    break;
                case LoggedType.Debug:
                    this.AudioPlayerBase.LogDebug = true;
                    this.AudioPlayerBase.LogInfo = true;
                    break;
                default:
                    break;
            }

            this.loggedType = value;
        }
    }

    /// <summary>
    /// Gets a value indicating whether remove NPC on finish or not.
    /// </summary>
    [Obsolete("Use Stop() method.")]
    public bool ClearOnFinish => this.AudioPlayerBase.ClearOnFinish;

    /// <summary>
    /// Immediately starts playing music.
    /// </summary>
    /// <param name="audioFile">Plays the <see cref="AudioFile"/>.</param>
    public void Play(AudioFile audioFile)
    {
        this.AudioPlayerBase.BroadcastChannel = audioFile.VoiceChannel;
        this.AudioPlayerBase.Volume = audioFile.Volume;
        this.AudioPlayerBase.Loop = audioFile.IsLooped;

        this.AudioPlayerBase.Enqueue(audioFile.FilePath, 0);

        if (this.AudioPlayerBase.CurrentPlay == null)
        {
            this.AudioPlayerBase.Play(0);
        }
    }

    /// <summary>
    /// Immediately starts playing music.
    /// </summary>
    /// <param name="path">Path to file.</param>
    public void Play(string path)
    {
        this.AudioPlayerBase.Enqueue(path, 0);

        if (this.AudioPlayerBase.CurrentPlay == null)
        {
            this.AudioPlayerBase.Play(0);
        }
    }

    /// <summary>
    /// Enqueue music.
    /// </summary>
    /// <param name="audioFile">Enqueue the <see cref="AudioFile"/>.</param>
    public void Enqueue(AudioFile audioFile)
    {
        this.AudioPlayerBase.BroadcastChannel = audioFile.VoiceChannel;
        this.AudioPlayerBase.Volume = audioFile.Volume;
        this.AudioPlayerBase.Loop = audioFile.IsLooped;

        this.AudioPlayerBase.Enqueue(audioFile.FilePath, -1);

        // If music not playing, starts to play.
        if (this.AudioPlayerBase.CurrentPlay == null)
        {
            this.AudioPlayerBase.Play(0);
        }
    }

    /// <summary>
    /// Enqueue music.
    /// </summary>
    /// <param name="path">Path to file.</param>
    /// <returns>Is music started or not.</returns>
    public bool Enqueue(string path)
    {
        this.AudioPlayerBase.Enqueue(path, -1);

        // If music not playing, starts to play.
        if (this.AudioPlayerBase.CurrentPlay == null)
        {
            this.AudioPlayerBase.Play(0);
        }

        return true;
    }

    /// <summary>
    /// Start playing music if current play is null & there is tracks in queue.
    /// </summary>
    public void Play()
    {
        if (this.PlayingTrack == null && this.TrackQueue.Count > 0)
        {
            this.AudioPlayerBase.Play(0);
        }
    }

    /// <summary>
    /// Skip current playing track.
    /// </summary>
    public void Skip() => this.AudioPlayerBase.Stoptrack(false);

    /// <summary>
    /// Stop playing music. Remove all music in queue.
    /// </summary>
    /// <returns>Is music stopped or not.</returns>
    public bool Stop()
    {
        return true;
    }
}