namespace AudioPlayer.API.Features;

using Exiled.API.Features;
using VoiceChat;

public class AudioFile
{
    /// <summary>
    /// Gets or sets path to audio file. Default leads to EXILED root directory (EXILED/Audio/track.ogg).
    /// </summary>
    public string FilePath { get; set; } = Path.Combine(Paths.Exiled, "Audio", "track.ogg");

    /// <summary>
    /// Gets or sets a value indicating whether loop track or not. Default is false.
    /// </summary>
    public bool IsLooped { get; set; } = false;

    /// <summary>
    /// Gets or sets value that indicates volume of track. Default is 75.
    /// </summary>
    public int Volume { get; set; } = 75;

    /// <summary>
    /// Gets or sets <see cref="VoiceChatChannel"/> of bot. Default is Intercom.
    /// </summary>
    public VoiceChatChannel VoiceChannel { get; set; } = VoiceChatChannel.Intercom;

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioFile"/> class.
    /// </summary>
    /// <param name="filePath">Path to audio file.</param>
    public AudioFile(string filePath)
    {
        this.FilePath = filePath;
    }
}
