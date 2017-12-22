﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MuhammadBugTracker.Helpers
{
    public class FileUploadValidator
    {
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            if (file == null)
                return false;

            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                           ImageFormat.Png.Equals(img.RawFormat) ||
                           ImageFormat.Gif.Equals(img.RawFormat) ||
                           ImageFormat.Bmp.Equals(img.RawFormat) ||
                           Path.GetExtension(file.FileName) == "pdf" ||
                           Path.GetExtension(file.FileName) == "doc" ||
                           Path.GetExtension(file.FileName) == "xls" ||
                           Path.GetExtension(file.FileName) == "txt" ||
                           Path.GetExtension(file.FileName) == "docx";
                }
            }
            catch
            {
                return false;
            }
        }
    }
}