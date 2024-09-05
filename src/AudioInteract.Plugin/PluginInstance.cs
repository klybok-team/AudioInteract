// <copyright file="PluginInstance.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

using AudioInteract.Features;

namespace AudioInteract.Plugin;

// НА 5 ЛАЙКОВ ДОБАВЛЯЮ НОВЫЕ ИВЕНТЫ ДЛЯ АЙПИ
// СТАВЬТЕ ЛАЙК И МЕРЖИТЕ СВОЙ ПУЛЛ РЕКВЕСТ - 1 лайк

/// <inheritdoc/>
public class PluginInstance : Exiled.API.Features.Plugin<Configs.Plugin>
{
    /// <summary/>
    public const string HarmonyID = $"AudioInteract - Klybok Team";

    /// <summary>
    /// Gets plugin instance.
    /// </summary>
    public static PluginInstance? Instance { get; private set; }

    /// <inheritdoc/>
    public override string Name => "AudioInteract";

    /// <inheritdoc/>
    public override string Author => "Klybok Team";

    /// <inheritdoc/>
    public override Version Version => new(1, 4, 8, 8);

    /// <summary/>
    public Handlers.EventHandlers? EventHandlers { get; set; }

    /// <inheritdoc/>
    public override void OnEnabled()
    {
        Instance = this;

        MusicAPI.RegisterPatches();

        this.EventHandlers = new();

        base.OnEnabled();
    }

    /// <inheritdoc/>
    public override void OnDisabled()
    {
        Instance = null;

        MusicAPI.UnregisterPatches();

        this.EventHandlers = null;

        base.OnDisabled();
    }
}
