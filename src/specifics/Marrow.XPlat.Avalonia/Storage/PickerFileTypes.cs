using Avalonia.Platform.Storage;

namespace Marrow.XPlat.Avalonia.Storage
{
    public static class PickerFileTypes
    {
        public static FilePickerFileType AllImages { get; } = new("All images")
        {
            Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.gif", "*.bmp" },
            AppleUniformTypeIdentifiers = new[] { "public.image" },
            MimeTypes = new[] { "image/*" }
        };

        public static FilePickerFileType AllVideos { get; } = new("All videos")
        {
            Patterns = new[]
            {
                "*.mp4", "*.mov", "*.avi", "*.wmv", "*.m4v", "*.mpg",
                "*.mpeg", "*.mp2", "*.mkv", "*.flv", "*.givf", "*.qt"
            },
            AppleUniformTypeIdentifiers = new[] { "public.video" },
            MimeTypes = new[] { "video/*" }
        };
    }
}