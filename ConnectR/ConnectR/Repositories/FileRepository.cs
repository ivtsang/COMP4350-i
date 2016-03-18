using ConnectR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectR.Repositories
{
    public class FileRepository
    {
        private Entities db = new Entities();

        public FileModel GetFileById(int id)
        {
            var file = db.Files.Find(id);
            return ConvertToModel(file);
        }

        public FileModel ConvertToModel(File file)
        {
            FileModel fileM = new FileModel()
            {
                FileName = file.FileName,
                Content = file.Content,
                ContentType = file.ContentType,
                FileType = (FileType)file.FileType,
            };

            return fileM;
        }
    }
}