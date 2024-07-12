// <copyright file="Plugin.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Configs;

using System.ComponentModel;
using Exiled.API.Interfaces;

/// <summary/>
public sealed class Plugin : IConfig
{
    /// <summary/>
    [Description("Indicates plugin is enabled or not")]
    public bool IsEnabled { get; set; } = true;

    /// <summary/>
    [Description("Indicates debug mode is enabled or not")]
    public bool Debug { get; set; } = true;
}