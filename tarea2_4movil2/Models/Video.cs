using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace tarea2_4movil2.Models
{
    public class Video
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Uri { get; set; }

        public string Descripcion { get; set; }
    }
}
