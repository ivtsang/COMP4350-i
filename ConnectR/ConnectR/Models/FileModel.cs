using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConnectR.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        [Required]
        [StringLength(100)]
        public string ContentType { get; set; }
        [Required]
        [MaxLength]
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }

        public virtual Profile Profile { get; set; }
    }
}