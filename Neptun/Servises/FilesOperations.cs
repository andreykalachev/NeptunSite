using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Neptun.Servises
{
    public static class FilesOperations
    {

        public static string SaveImg(HttpPostedFileBase photo)
        {
            string newFilePath = null;
            if (photo != null && photo.ContentLength > 0)
            {
                var directory = @"~/App_Data/DownloadedPictures/";

                var fileExtention = Path.GetExtension(photo.FileName);

                if (!IsFilePicture(fileExtention))
                {
                    throw new FileLoadException();
                }

                newFilePath = SaveFile(photo, directory);
            }

            return newFilePath;
        }

        public static string SavePdf(HttpPostedFileBase file)
        {
            string newFilePath = null;
            if (file != null && file.ContentLength > 0)
            {
                var directory = @"~/App_Data/DownloadedPDF/";

                var fileExtention = Path.GetExtension(file.FileName);

                if (!IsFilePdf(fileExtention))
                {
                    throw new FileLoadException();
                }

                newFilePath = SaveFile(file, directory);
            }

            return newFilePath;
        }

        public static void DeleteFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var fileToDelete = fileName;
                var saveDirectory = HttpContext.Current.Server.MapPath(fileToDelete);
                if (File.Exists(saveDirectory)) File.Delete(saveDirectory);
            }
        }

        public static bool IsFilePicture(string fileExtention)
        {
            var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            return supportedTypes.Contains(fileExtention);
        }

        public static bool IsFilePdf(string fileExtention)
        {
            var supportedTypes = new[] { ".pdf" };

            return supportedTypes.Contains(fileExtention);
        }

        public static string SaveFile(HttpPostedFileBase file, string localDirectory)
        {
            var fileName = Path.GetFileName(file.FileName);
            var newFileName = ChangeFileName(fileName);
            var saveDirectory = HttpContext.Current.Server.MapPath(localDirectory);
            var savePath = Path.Combine(saveDirectory, newFileName);
            file.SaveAs(savePath);
            return Path.Combine(localDirectory.Substring(1), newFileName);
        }

        public static string ChangeFileName(string fileName)
        {
            var dotIndex = fileName.LastIndexOf('.');
            return fileName.Insert(dotIndex, DateTime.Now.ToString("yyyyMMddHHmmsstt"));
        }
    }
}