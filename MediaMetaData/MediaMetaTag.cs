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
            base.Headers.Add(new Header("Name", typeof(string), 65));
            base.Headers.Add(new Header("Ext", typeof(string), 10));
            base.Headers.Add(new Header("Size", typeof(long), 10));
            base.Headers.Add(new Header("Date", typeof(DateTime), 15));
            base.Headers.Add(new Header("Attr", typeof(string), 10));
            Entity = new MediaMetaTag(path);
        }
    }
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
                                entity.Attributes.Add("Date Taken", "Missing");
                            }
                            else
                            {
                                DateTime dateTime = directory.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                                entity.Attributes.Add("Date Taken", dateTime.ToString(CultureInfo.InvariantCulture));
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
                            entity.Attributes.Add("Type", "Unknown");
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

            Entities = GetEntities();
            return  Entities;
        }


        public MediaMetaTag(string path) : base(path)
        {
            Path = path;
            Parent = path;
        }
    }
}
