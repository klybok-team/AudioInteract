// <copyright file="BotTemplate.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.API.Features;

using Exiled.API.Features;
using PlayerRoles;

/// <summary>
/// Bot template for dynamicly changable values like nickname, badge and other staff.
/// </summary>
public class BotTemplate
{
    /// <summary>
    /// Gets or sets nickname for bot.
    /// </summary>
    public string Nickname { get; set; } = "Bot";

    /// <summary>
    /// Gets or sets badge for bot (you can only set in constructor).
    /// </summary>
    public Badge Badge { get; set; } = new("Best bot", "white");

    /// <summary>
    /// Gets or sets bot role <see cref="RoleTypeId"/>.
    /// </summary>
    public RoleTypeId RoleType { get; set; } = RoleTypeId.Overwatch;
}
