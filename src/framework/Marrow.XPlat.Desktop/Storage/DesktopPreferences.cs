﻿using System;
using System.IO;
using System.Text;
using Tomlyn;
using Tomlyn.Model;

namespace Marrow.XPlat.Storage
{
    public class DesktopPreferences : IPreferences
    {
        private readonly IFileSystem _fs;
        private readonly string _name;
        private readonly string _ext;
        private readonly Lazy<TomlTable> _model;

        public DesktopPreferences(IFileSystem fs, string name = "preferences", string ext = "toml")
        {
            _fs = fs;
            _name = name;
            _ext = ext;
            _model = new Lazy<TomlTable>(ReadToml);
        }

        private string GetFileName()
        {
            var path = Path.Combine(_fs.AppDataDirectory, $"{_name}.{_ext}");
            return path;
        }

        private TomlTable ReadToml()
        {
            var text = string.Empty;
            var fileName = GetFileName();
            if (File.Exists(fileName))
                text = File.ReadAllText(fileName, Encoding.UTF8);
            var model = Toml.ToModel(text);
            return model;
        }

        private void WriteToml(Action<TomlTable> action)
        {
            var model = _model.Value;
            action(model);
            var text = Toml.FromModel(model);
            var fileName = GetFileName();
            File.WriteAllText(fileName, text, Encoding.UTF8);
        }

        public void Clear()
            => WriteToml(model => model.Clear());

        public bool ContainsKey(string key)
        {
            var model = _model.Value;
            var res = model.ContainsKey(key);
            return res;
        }

        public T Get<T>(string key, T defaultValue)
        {
            var model = _model.Value;
            if (model.TryGetValue(key, out var result))
            {
                var stored = (T)result;
                return stored;
            }
            return defaultValue;
        }

        public void Remove(string key)
            => WriteToml(model => model.Remove(key));

        public void Set<T>(string key, T value)
            => WriteToml(model =>
            {
                var stored = value;
                model.Add(key, stored!);
            });
    }
}