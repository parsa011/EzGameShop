using System;
using System.ComponentModel;

namespace EzGame.Domain.Enums
{
    public class FileManagerType
    {
        public enum FileType
        {
            GameImage = 1,
            PostImage = 2
        }

        public static string ParseType(FileType type)
        {
            if (!Enum.IsDefined(typeof(FileType), type))
            {
                throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(FileType));
            }

            return type switch
            {
                FileType.GameImage => "GameImages",
                FileType.PostImage => "PostImages",
                _ => throw new ArgumentException("please enter a valid type")
            };
        }
    }
}