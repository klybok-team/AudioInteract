// <copyright file="PluginInstance.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Plugin;

using AudioPlayerAPI = AudioPlayer.Features.API;

/// <inheritdoc/>
public class PluginInstance : Exiled.API.Features.Plugin<Configs.Plugin>
{
    /// <summary/>
    public const string HarmonyID = $"AudioPlayer - Klybok Team";

    /// <inheritdoc/>
    public override string Name => "AudioPlayer";

    /// <inheritdoc/>
    public override string Author => "Klybok Team";

    /// <inheritdoc/>
    public override Version Version => new(1, 0, 0);

    /// <inheritdoc/>
    public override void OnEnabled()
    {
        AudioPlayerAPI.RegisterPatches();

        base.OnEnabled();
    }

    /// <inheritdoc/>
    public override void OnDisabled()
    {
        AudioPlayerAPI.UnregisterPatches();

        base.OnDisabled();
    }
}
