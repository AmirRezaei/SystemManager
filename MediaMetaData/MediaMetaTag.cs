using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Util;
using PluginInterface;

namespace MediaMetaData
{
    public class MediaMetaTagPlugin : Plugin
    {
        public MediaMetaTagPlugin(string path)
        {
            base.Name = "Media Meta Tag Plugin";
            base.Author = "Amir Rezaei";
            base.Description = "Reads attributes from images and videos.";
            base.Version = "1.0";
            Entity = new MediaMetaTag(path);
        }
    }
    public class MediaMetaTag : Entity
    {
        private enum TagNames
        {
            Name,
            Ext,
            Size,
            Date,
            Attr
        }
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
            try
            {
                FileInfo[] files = new DirectoryInfo(Path).GetFiles("*.*", SearchOption.AllDirectories);
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
                                entity.Values.Add("Date Taken", "Missing");
                            }
                            else
                            {
                                DateTime dateTime = directory.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                                entity.Values.Add("Date Taken", dateTime.ToString(CultureInfo.InvariantCulture));
                            }
                            entity.Values.Add("Type", fileType.ToString());

                        }
                        else if ((fileType == FileType.Mp4 || fileType == FileType.QuickTime) && fileType != FileType.Unknown)
                        {
                            var directory = directories.OfType<MetadataExtractor.Formats.QuickTime.QuickTimeMovieHeaderDirectory>().FirstOrDefault();

                            if (directory == null)
                            {
                                entity.Values.Add("Date Taken", "Missing");
                            }
                            else
                            {
                                DateTime dateTime = directory.GetDateTime(MetadataExtractor.Formats.QuickTime.QuickTimeMovieHeaderDirectory.TagCreated);
                                entity.Values.Add("Date Taken", dateTime.ToString(CultureInfo.InvariantCulture));
                            }
                            entity.Values.Add("Type", fileType.ToString());
                        }
                        else
                        {
                            entity.Values.Add("Date Taken", "N/A");
                            entity.Values.Add("Type", "Unknown");
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

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }


            return entities;
        }

        public override IEnumerable<Entity> Seek(string path)
        {
            IsDirectory = true;
            IsRoot = true;
            Path = path;
            Parent = path;

            Tags.Add(new PluginInterface.Tag(nameof(TagNames.Name), typeof(string), 65));
            Tags.Add(new PluginInterface.Tag(nameof(TagNames.Ext), typeof(string), 10));
            Tags.Add(new PluginInterface.Tag(nameof(TagNames.Size), typeof(long), 10));
            Tags.Add(new PluginInterface.Tag(nameof(TagNames.Date), typeof(DateTime), 15));
            Tags.Add(new PluginInterface.Tag(nameof(TagNames.Attr), typeof(string), 10));

            Entities = GetEntities();
            return Entities;
        }


        public MediaMetaTag(string path) : base(path)
        {
            Path = path;
            Parent = path;
        }
    }
}
