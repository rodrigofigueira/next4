using System.Collections.Generic;

namespace Api.Models
{
    public class LeadForm
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Empresa { get; set; }
        public string CNPJ { get; set; }
        public string TelefoneContato { get; set; }
        public string Email { get; set; }
        public string PilarNegocio { get; set; }
        public int QuantidadeEquipamentos { get; set; }
        public int VolumeImpressao { get; set; }
        public string Mensagem { get; set; }
        public List<LeadCRM> LeadCRMs { get; set; }
        public List<LeadRD> LeadRDs { get; set; }
    }
}
