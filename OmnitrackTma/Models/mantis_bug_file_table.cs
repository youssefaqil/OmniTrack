//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OmnitrackTma.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mantis_bug_file_table
    {
        public long id { get; set; }
        public long bug_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string diskfile { get; set; }
        public string filename { get; set; }
        public string folder { get; set; }
        public int filesize { get; set; }
        public string file_type { get; set; }
        public System.DateTime date_added { get; set; }
        public byte[] content { get; set; }
    }
}