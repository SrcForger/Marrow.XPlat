<img src="assets/ProjectBanner.png" height="150" alt="Marrow.XPlat project banner" />

# Marrow XPlat

It is a cross platform (desktop and mobile) application framework for .NET which aims to close the gap between differently evolved systems by providing reliable real APIs for everyone.

## Structure

| *NuGet* | Package | Notes |
| --- | --- | --- |
| [![NuGet](https://img.shields.io/nuget/v/Marrow.XPlat.API.svg)](https://www.nuget.org/packages/Marrow.XPlat.API/) | Marrow.XPlat.API | The core interface abstraction |
| [![NuGet](https://img.shields.io/nuget/v/Marrow.XPlat.Desktop.svg)](https://www.nuget.org/packages/Marrow.XPlat.Desktop/) | Marrow.XPlat.Desktop | Platform module for desktop |
| [![NuGet](https://img.shields.io/nuget/v/Marrow.XPlat.Mobile.svg)](https://www.nuget.org/packages/Marrow.XPlat.Mobile/) | Marrow.XPlat.Mobile | Platform module for mobile |
| [![NuGet](https://img.shields.io/nuget/v/Marrow.XPlat.Avalonia.svg)](https://www.nuget.org/packages/Marrow.XPlat.Avalonia/) | Marrow.XPlat.Avalonia | Use Avalonia for desktop |
| [![NuGet](https://img.shields.io/nuget/v/Marrow.XPlat.Utils.svg)](https://www.nuget.org/packages/Marrow.XPlat.Utils/) | Marrow.XPlat.Utils | Some tools and extensions |

## Status

| Group | Interface | Members |
| --- | --- | --- |
| ApplicationModel | IAppInfo | BuildString, Name, PackageName, VersionString |
| ApplicationModel | IBrowser | OpenAsync(uri) |
| ApplicationModel | IEmail | IsComposeSupported, ComposeAsync(message) |
| DataTransfer | IClipboard | GetTextAsync(), SetTextAsync(string) |
| DataTransfer | IShare | RequestAsync(ShareTextReq / ShareFileReq / ShareMultipleFilesReq) |
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
