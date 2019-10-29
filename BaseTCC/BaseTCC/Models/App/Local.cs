using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.App
{
    public class Local
    {
        public string Nome { get; private set; }
        public float Lat { get; private set; }
        public float Long { get; private set; }

        public Local(string nome, float latitude, float logitude)
        {
            this.Nome = nome;
            this.Lat = latitude;
            this.Long = logitude;

        }

        public class LocalAdpter : Local
        {
            public float DistanciaReferencia { get; set; }

            public HashSet<LocalAdpter> DistanciaLocais { get; set; }

            [JsonConstructor]
            public LocalAdpter(float DistanciaReferencia, HashSet<LocalAdpter> DistanciaLocais, string Nome, float Lat, float Long) : base(Nome, Lat, Long)
            {
                this.DistanciaReferencia = DistanciaReferencia;
                this.DistanciaLocais = DistanciaLocais;
            }

            public LocalAdpter(string nome, float latitude, float logitude, float distanciaReferencia) : base(nome, latitude, logitude)
            {
                this.DistanciaReferencia = distanciaReferencia;
                this.DistanciaLocais = new HashSet<LocalAdpter>();
            }

            public LocalAdpter(Local local) : base(local.Nome, local.Lat, local.Long)
            {

            }

            public override string ToString()
            {
                if (DistanciaReferencia == 0) return base.Nome;

                return this.DistanciaReferencia + " - " + base.Nome;
            }

        }

        public override bool Equals(object obj)
        {
            return obj is Local local &&
                   Lat == local.Lat &&
                   Long == local.Long;
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public override int GetHashCode()
        {
            var hashCode = -1450610079;
            hashCode = hashCode * -1521134295 + Lat.GetHashCode();
            hashCode = hashCode * -1521134295 + Long.GetHashCode();
            return hashCode;
        }
    }
}
