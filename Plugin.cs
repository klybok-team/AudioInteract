// <copyright file="Plugin.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer;

using Exiled.API.Features;
using HarmonyLib;

/// <inheritdoc/>
public class Plugin : Exiled.API.Features.Plugin<Configs.Plugin>
{
    /// <summary/>
    public const string HarmonyID = $"AudioPlayer - Klybok Team";

    /// <summary/>
    public static Harmony? Harmony { get; private set; }

    /// <inheritdoc/>
    public override string Name => "AudioPlayer";

    /// <inheritdoc/>
    public override string Author => "Klybok Team";

    /// <inheritdoc/>
    public override Version Version => new(1, 0, 0);

    /// <inheritdoc/>
    public override void OnEnabled()
    {
        Harmony = new Harmony(HarmonyID);
        Harmony.PatchAll();

        base.OnEnabled();
    }

    /// <inheritdoc/>
    public override void OnDisabled()
    {
        Harmony?.UnpatchAll(HarmonyID);
        Harmony = null;

        base.OnDisabled();
    }
}
