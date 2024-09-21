// <copyright file="Extensions.cs" company="Klybok Team">
// Copyright (c) Klybok Team. All rights reserved.
// </copyright>

namespace AudioInteract.Features;

using Exiled.API.Features;
using NVorbis;
using NVorbis.Contracts;

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
            string settedPath = Path.Combine(Paths.Exiled, "Audio");

            Directory.CreateDirectory(settedPath);

            return settedPath;
        }
    }

    /// <summary>
    /// Check is this file is even url or VALID and PLAYABLE file.
    /// </summary>
    /// <param name="urlOrPath">File path or url.</param>
    /// <returns>Is this valid URL or path.</returns>
    public static bool IsValidFileOrValidUrl(this string urlOrPath)
    {
        if (Uri.TryCreate(urlOrPath, UriKind.Absolute, out Uri _))
        {
            return true;
        }

        if (File.Exists(urlOrPath) && Path.GetExtension(urlOrPath) == ".ogg")
        {
            return true;
        }

        return false;
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

    /// <summary>
    /// Get file tags.
    /// </summary>
    /// <param name="filePath">Path to file.</param>
    /// <returns><see cref="ITagData"/>.</returns>
    public static ITagData GetFileTags(this string filePath)
    {
        return new VorbisReader(filePath).Tags;
    }

    /// <summary>
    /// Get file tags.
    /// </summary>
    /// <param name="audioFile">Audio File.</param>
    /// <returns><see cref="ITagData"/>.</returns>
    public static ITagData GetFileTags(this AudioFile audioFile)
    {
        return new VorbisReader(audioFile.FilePath).Tags;
    }
}