// <copyright file="RAPlayerListShowNPC.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.API.Patches;

using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Exiled.API.Features;
using Exiled.API.Features.Pools;
using HarmonyLib;
using RemoteAdmin.Communication;
using static HarmonyLib.AccessTools;

/// <summary>
/// Show NPC in remote admin panel.
/// </summary>
[HarmonyPatch(typeof(RaPlayerList), nameof(RaPlayerList.ReceiveData), [typeof(CommandSender), typeof(string)])]
public static class RAPlayerListShowNPC
{
    /// <summary/>
    /// <param name="instructions">.</param>
    /// <param name="generator">..</param>
    /// <returns>fuck docs, please fix summary for this method.</returns>
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Pool.Get(instructions);

        // please add index
        /* 0x0003169C 2D0A IL_0084: brtrue.s IL_0090 Transfers control to a target instruction (short form) if value is true, not null, or non-zero. */

        newInstructions.InsertRange(
            84,
            new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_S, 7),
                new CodeInstruction(OpCodes.Call, Method(typeof(RAPlayerListShowNPC), nameof(AddNPC))),
            });

        for (int z = 0; z < newInstructions.Count; z++)
        {
            yield return newInstructions[z];
        }

        ListPool<CodeInstruction>.Pool.Return(newInstructions);
    }

    /// <summary>
    /// Show NPC in RA List.
    /// </summary>
    /// <param name="stringBuilder">String Builder.</param>
    public static void AddNPC(StringBuilder stringBuilder)
    {
        try
        {
            foreach (Features.MusicInstance musicInstance in AudioInteract.Features.API.MusicInstance)
            {
                if (musicInstance.Npc.ReferenceHub.Mode != CentralAuth.ClientInstanceMode.DedicatedServer)
                {
                    // если какой-либо другой мод - видно в RA и списке.
                    continue;
                }

                stringBuilder.AppendLine($"({musicInstance.Npc.Id}) {musicInstance.Npc.ReferenceHub.nicknameSync.CombinedName.Replace("\n", string.Empty).Replace("RA_", string.Empty)}");
            }
        }
        catch (System.Exception ex)
        {
            Log.Error(ex);
        }
    }
}