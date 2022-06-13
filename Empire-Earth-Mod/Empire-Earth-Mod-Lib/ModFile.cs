﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Empire_Earth_Mod_Lib
{
    [Serializable]
    public class ModFile
    {
        public string RelativeFilePath { get; set; }
        public ModFileType FileType { get; set; }
        public Guid Variant { get; set; }
        public string Md5 { get; set; }

        // Each description must be unique
        public enum ModFileType
        {
            [Description("Data")] Data = 0,
            [Description("Config File")] ConfigFile = 1,
            [Description("Executable")] Executable = 2
        }

        public enum ModFileProduct
        {
            EEC = 0,
            AOC = 1,
            Both = 2
        }

        public ModFile(string relativeFilePath, ModFileType type, Guid uuid, string md5)
        {
            this.FileType = type;
            this.Md5 = md5;
            this.RelativeFilePath = relativeFilePath;
            this.Variant = uuid;
        }

        public static string GetModFileName(ModFileType value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static ModFileType ParseModFileType(string description)
        {
            var type = typeof(ModFileType);
            if (!type.IsEnum)
                throw new ArgumentException();
            var fields = type.GetFields();
            var field = fields
                .SelectMany(f => f.GetCustomAttributes(
                    typeof(DescriptionAttribute), false), (
                    f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((DescriptionAttribute)a.Att)
                    .Description == description);
            return field == null ? default(ModFileType) : (ModFileType)field.Field.GetRawConstantValue();
        }

        public static ModFileProduct GetProduct(string filePath)
        {
            if (filePath.StartsWith("EEC"))
                return ModFileProduct.EEC;
            if (filePath.StartsWith("AOC"))
                return ModFileProduct.AOC;
            if (filePath.StartsWith("all"))
                return ModFileProduct.Both;
            throw new Exception("Unknown product");
        }
        
        public ModFileProduct GetProduct()
        {
            if (RelativeFilePath.StartsWith("EEC"))
                return ModFileProduct.EEC;
            if (RelativeFilePath.StartsWith("AOC"))
                return ModFileProduct.AOC;
            if (RelativeFilePath.StartsWith("all"))
                return ModFileProduct.Both;
            throw new Exception("Unknown product");
        }
        
        public static string ParseModFileProduct(ModFileProduct product)
        {
            switch (product)
            {
                case ModFileProduct.EEC:
                    return "EEC";
                case ModFileProduct.AOC:
                    return "AOC";
                case ModFileProduct.Both:
                    return "all";
                default:
                    throw new Exception("Unknown product");
            }
        }

        public static ModFileType GetDefaultModFileType(string fileExtension)
        {
            var fileExtensions = new Dictionary<ModFileType, List<string>>
            {
                {
                    ModFileType.Executable, new List<string>()
                    {
                        ".exe",
                        ".bat",
                        ".cmd"
                    }
                },
                {
                    ModFileType.ConfigFile, new List<string>()
                    {
                        ".json",
                        ".cfg",
                        ".xml",
                        ".config",
                        ".ini"
                    }
                }
            };

            foreach (var fileType in fileExtensions.Keys)
            {
                if (fileExtensions[fileType].Contains(fileExtension))
                    return fileType;
            }
            return ModFileType.Data;
        }
    }
}