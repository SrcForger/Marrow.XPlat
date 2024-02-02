<img src="assets/ProjectBanner.png" height="150" alt="Marrow.XPlat project banner" />

# Marrow XPlat

It is a cross platform (desktop and mobile) application framework for .NET which aims to close the gap between differently evolved systems by providing reliable real APIs for everyone.

## Status

| Group | Interface | Members |
| --- | --- | --- |
| ApplicationModel | IAppInfo | BuildString, Name, PackageName, VersionString |
| ApplicationModel | IBrowser | OpenAsync(uri) |
| ApplicationModel | IEmail | IsComposeSupported, ComposeAsync(message) |
| DataTransfer | IClipboard | GetTextAsync(), SetTextAsync(string) |
| Devices | IDeviceInfo | Manufacturer, Model, Name, VersionString |
| Media | IMediaPicker | PickPhotoAsync(opts), PickVideoAsync(opts) |
| Media | IScreenshot | CaptureAsync(), IsCaptureSupported |
| Media | IScreenshotResult | CopyToAsync(Stream,int), Height, Width, OpenReadAsync(int) |
| Storage | IFilePicker | PickAsync(opts), PickMultipleAsync(opts) |
| Storage | IFileResult | FileName, FullPath, OpenReadAsync() |
| Storage | IFileSystem | CacheDirectory, AppDataDirectory |
| Storage | IPreferences | Clear, Set(key, value), Get(key, default), ContainsKey(key), Remove(key) |
| Storage | ISecureStorage | GetAsync(string), Remove(string), RemoveAll(), SetAsync(string,string) |

## Build

```bash
dotnet workload install android
dotnet build
dotnet run
```

## License

Marrow.XPlat is made available under the terms and conditions of the [AGPL license](LICENSE).
