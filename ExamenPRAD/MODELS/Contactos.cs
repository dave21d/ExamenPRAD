using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ExamenPRAD.MODELS
{
    public class Contactos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public string nombres { get; set; }

        [MaxLength(100)]
        public string apellidos { get; set; }
        public double edad { get; set; }

        public string pais { get; set; }

        [MaxLength(300)]
        public string nota { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
