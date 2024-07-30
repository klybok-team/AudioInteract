// <copyright file="Track.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.Features.Events;

using AudioInteract.API.Events.EventArgs;
using Exiled.Events.Features;

/// <summary>
/// Cassie related events.
/// </summary>
public static class Track
{
#pragma warning disable SA1623 // Property summary documentation should match accessors

    /// <summary>
    /// Invoked after track are finished.
    /// </summary>
    public static Event<TrackFinishedEventArgs> TrackFinished { get; set; } = new();

    /// <summary>
    /// Invoked after track are selected.
    /// </summary>
    public static Event<TrackSelectedEventArgs> TrackSelected { get; set; } = new();

    /// <summary>
    /// Invoked after track are loaded.
    /// </summary>
    public static Event<TrackLoadedEventArgs> TrackLoaded { get; set; } = new();

    /// <summary>
    /// Called after track are finished.
    /// </summary>
    /// <param name="ev">The <see cref="TrackFinishedEventArgs" /> instance.</param>
    public static void OnTrackFinished(TrackFinishedEventArgs ev) => TrackFinished.InvokeSafely(ev);

    /// <summary>
    /// Called after track are loaded.
    /// </summary>
    /// <param name="ev">The <see cref="TrackFinishedEventArgs" /> instance.</param>
    public static void OnTrackLoaded(TrackLoadedEventArgs ev) => TrackLoaded.InvokeSafely(ev);

    /// <summary>
    /// Called after track are selected.
    /// </summary>
    /// <param name="ev">The <see cref="TrackFinishedEventArgs" /> instance.</param>
    public static void OnTrackSelected(TrackSelectedEventArgs ev) => TrackSelected.InvokeSafely(ev);
}