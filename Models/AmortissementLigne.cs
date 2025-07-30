﻿namespace SimpleFrameworkApp.Models
{
    public class AmortissementLigne
    {
        public int Periode { get; set; }
        public decimal SoldeDebut { get; set; }
        public decimal Mensualite { get; set; }
        public decimal Interet { get; set; }
        public decimal CapitalRembourse { get; set; }
        public decimal SoldeFin { get; set; }
    }
}