using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Util;
using NLog;
using PluginInterface;

namespace MediaMetaTagPlugin
{
    public class MediaMetaTag : Entity
    {
        static FileType GetFileType(string file)
        {
            try
            {
                using (var stream = File.OpenRead(file)) 
                    return FileTypeDetector.DetectFileType(stream);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
        }
        public override IEnumerable<Entity> GetEntities()
        {
            var entities = new List<Entity>();
            FileInfo[] files = new DirectoryInfo(Path).GetFiles("*.*",  SearchOption.AllDirectories);

            foreach (var file in files)
            {
                try
                {
                    Entity entity = new MediaMetaTag(file.FullName)
                    {
                        Name = file.Name
                    };
                    IReadOnlyList<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(file.FullName);
                    FileType fileType = GetFileType(file.FullName);

                    if (fileType != FileType.Mp4 && fileType != FileType.QuickTime && fileType != FileType.Unknown)
                    {
                        var directory = directories.OfType<MetadataExtractor.Formats.Exif.ExifSubIfdDirectory>().FirstOrDefault();
                        if (directory == null)
                        {
                            entity.Attributes.Add("Date Taken", "Missing" );
                        }
                        else
                        {
                            DateTime dateTime = directory.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                            entity.Attributes.Add("Date Taken" , dateTime.ToString(CultureInfo.InvariantCulture));
                        }
                        entity.Attributes.Add("Type", fileType.ToString());

                    }
                    else if ((fileType == FileType.Mp4 || fileType == FileType.QuickTime) && fileType != FileType.Unknown)
                    {
                        var directory = directories.OfType<MetadataExtractor.Formats.QuickTime.QuickTimeMovieHeaderDirectory>().FirstOrDefault();

                        if (directory == null)
                        {
                            entity.Attributes.Add("Date Taken", "Missing");
                        }
                        else
                        {
                            DateTime dateTime = directory.GetDateTime(MetadataExtractor.Formats.QuickTime.QuickTimeMovieHeaderDirectory.TagCreated);
                            entity.Attributes.Add("Date Taken", dateTime.ToString(CultureInfo.InvariantCulture));
                        }
                        entity.Attributes.Add("Type", fileType.ToString());
                    }
                    else
                    {
                        entity.Attributes.Add("Date Taken", "N/A");
                        entity.Attributes.Add("Type", "Unknown" );
                    }

                    entities.Add(entity);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Logger.Error(ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }
            return entities;
        }

        public MediaMetaTag(string path) : base(path)
        {
            Path = path;
            Parent = this;
        }

        public override Entity Parent { get; }
       
    }
}
