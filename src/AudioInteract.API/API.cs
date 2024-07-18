// <copyright file="API.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioPlayer.Features;

using System.Reflection;
using CentralAuth;
using Exiled.API.Features;
using HarmonyLib;
using NVorbis.Contracts;
using PlayerRoles;
using SCPSLAudioApi;

/// <summary>
/// Main API class.
/// </summary>
public static class API
{
    /// <summary>
    /// Gets harmony ID.
    /// </summary>
    public const string HarmonyID = "Klybok.Team - AudioPlayer.API";

    /// <summary>
    /// Harmony main class.
    /// </summary>
    private static Harmony? harmony;

    static API()
    {
        Startup.SetupDependencies();
    }

    /// <summary>
    /// Gets settings of NPC.
    /// </summary>
    public static List<MusicInstance> MusicInstance { get; private set; } = new();

    private static List<Assembly> UsingAssemblys { get; set; } = new();

    /// <summary>
    /// Register patches to handle some extra-features of API.
    /// </summary>
    public static void RegisterPatches()
    {
        Assembly callingAssembly = Assembly.GetCallingAssembly();

        if (callingAssembly == null)
        {
            return;
        }

        UsingAssemblys.Add(callingAssembly);

        harmony = new(HarmonyID);
        harmony.PatchAll();
    }

    /// <summary>
    /// Unregister patches.
    /// </summary>
    public static void UnregisterPatches()
    {
        Assembly callingAssembly = Assembly.GetCallingAssembly();

        if (callingAssembly == null)
        {
            return;
        }

        UsingAssemblys.Remove(callingAssembly);

        if (UsingAssemblys.Count > 0)
        {
            return;
        }

        harmony?.UnpatchAll();
        harmony = null;
    }

    /// <summary>
    /// Get file tags.
    /// </summary>
    /// <param name="filePath">Path to file.</param>
    /// <returns><see cref="ITagData"/>.</returns>
    public static ITagData GetFileTags(string filePath)
    {
        return new NVorbis.VorbisReader(filePath).Tags;
    }

    /// <summary>
    /// Get file tags.
    /// </summary>
    /// <param name="audioFile">Audio File.</param>
    /// <returns><see cref="ITagData"/>.</returns>
    public static ITagData GetFileTags(AudioFile audioFile)
    {
        return new NVorbis.VorbisReader(audioFile.FilePath).Tags;
    }

    /// <summary>
    /// Creates new NPC and create settings for him.
    /// </summary>
    /// <param name="name">Name setted to NPC.</param>
    /// <param name="role">Role setted to NPC.</param>
    /// <param name="id">ID setted to NPC. 0 - select new ID.</param>
    /// <param name="userID">UserID setted to NPC. DO NOT CHANGE THIS IF YOU NOT WANT TO BREAK VSR. Default value hides NPC from list.</param>
    /// <returns>Returns Music Instance.</returns>
    public static MusicInstance CreateNPC(string name, RoleTypeId role = RoleTypeId.None, int id = 0, string userID = PlayerAuthenticationManager.DedicatedId)
    {
        Npc npc = Npc.Spawn(name, role, id, userID);

        try
        {
            if (userID == PlayerAuthenticationManager.DedicatedId)
            {
                npc.ReferenceHub.authManager.SyncedUserId = userID;
                try
                {
                    npc.ReferenceHub.authManager.InstanceMode = ClientInstanceMode.DedicatedServer;
                }
                catch (Exception e)
                {
                    Log.Debug($"Ignore: {e}");
                }
            }
            else
            {
                npc.ReferenceHub.authManager.UserId = userID == string.Empty ? $"Dummy@localhost" : userID;
            }
        }
        catch (Exception e)
        {
            Log.Debug($"Ignore: {e}");
        }

        MusicInstance.Add(new(npc));

        return MusicInstance.FirstOrDefault(x => x.Npc == npc);
    }

    /// <summary>
    /// Create settings for NPC.
    /// </summary>
    /// <param name="npc">NPC to set settings.</param>
    /// <returns>Indicates settings is successfuly created or not.</returns>
    public static MusicInstance? AddSetingsToNPC(Npc npc)
    {
        if (npc == null)
        {
            return null;
        }

        if (MusicInstance.FirstOrDefault(x => x.Npc == npc) == null)
        {
            return null;
        }

        MusicInstance.Add(new(npc));

        return MusicInstance.FirstOrDefault(x => x.Npc == npc);
    }

    /// <summary>
    /// Destroy NPC.
    /// </summary>
    /// <param name="npc">NPC to destroy.</param>
    /// <returns>Indicates NPC is successfuly destroyed or not.</returns>
    public static bool DestroyNPC(Npc npc)
    {
        if (npc == null)
        {
            return false;
        }

        MusicInstance musicInstance = MusicInstance.FirstOrDefault(x => x.Npc == npc);
        if (musicInstance != null)
        {
            musicInstance.AudioPlayerBase?.OnDestroy();

            MusicInstance.Remove(musicInstance);
        }

        try
        {
            npc.Destroy();

            return true;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex);

            return false;
        }
    }

    /// <summary>
    /// Destroy NPC.
    /// </summary>
    /// <param name="musicInstance">Music Instance.</param>
    /// <returns>Indicates NPC is successfuly destroyed or not.</returns>
    public static bool DestroyNPC(MusicInstance musicInstance)
    {
        if (musicInstance.Npc == null)
        {
            return false;
        }

        musicInstance.AudioPlayerBase?.OnDestroy();

        MusicInstance.Remove(musicInstance);

        try
        {
            musicInstance?.Npc.Destroy();

            return true;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex);

            return false;
        }
    }
}
