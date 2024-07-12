// <copyright file="API.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.API;

using AudioPlayer.API.Features;
using CentralAuth;
using Exiled.API.Features;
using PlayerRoles;
using SCPSLAudioApi.AudioCore;

/// <summary>
/// Main API class to interact with NPC and SCPSLAudioPlayer.
/// </summary>
public static class API
{
    /// <summary>
    /// Allow to get settings of NPC.
    /// </summary>
    public static Dictionary<Npc, NPCSettings> NpcToSettings = new();

    /// <summary>
    /// Creates new NPC and creates settings for him.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="role"></param>
    /// <param name="id"></param>
    /// <param name="userID"></param>
    /// <returns></returns>
    public static bool CreateNPC(string name, RoleTypeId role = RoleTypeId.Overwatch, int id = 0, string userID = PlayerAuthenticationManager.DedicatedId)
    {
        var npc = Npc.Spawn(name, role, id, userID);

        NpcToSettings.Add(npc, new(npc.Id));

        return true;
    }
}
