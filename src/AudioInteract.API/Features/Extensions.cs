// <copyright file="Extensions.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.Features;

using Exiled.API.Features;

/// <summary>
/// A collection of API methods.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Gets path to audio file root directory. Leads to EXILED root directory (EXILED/Audio).
    /// </summary>
    public static string FolderPath
    {
        get
        {
            var settedPath = Path.Combine(Paths.Exiled, "Audio");

            Directory.CreateDirectory(settedPath);

            return settedPath;
        }
    }

    /// <summary>
    /// Checks the path for playable track.
    /// </summary>
    /// <param name="audioFile">AudioFile to check.</param>
    /// <returns>Is file exists and ready to be played.</returns>
    public static bool CheckTrack(AudioFile audioFile)
    {
        if (!File.Exists(audioFile.FilePath))
        {
            return false;
        }

        if (Path.GetExtension(audioFile.FilePath) != ".ogg")
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks the path for playable track.
    /// </summary>
    /// <param name="filePath">Path to file.</param>
    /// <returns>Is file exists and ready to be played.</returns>
    public static bool CheckTrack(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }

        if (Path.GetExtension(filePath) != ".ogg")
        {
            return false;
        }

        return true;
    }
}