using System;

namespace Isomanager.Models
{
    public class Document
    {
        public string Id { get; set; } // Cambiado de DocumentId a Id
        public string Version { get; set; }
        public string Status { get; set; }
        public string ResponsiblePerson { get; set; }
        public DateTime LastModified { get; set; }
    }
}