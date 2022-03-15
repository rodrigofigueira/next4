using System;

namespace Api.Models
{
    public class LeadCRM
    {
        public int Id { get; set; }
        public LeadForm LeadForm { get; set; }
        public string AccountIdCrm { get; set; }
        public string StatusLead { get; set; }
        public string EncLead { get; set; }
        public string FilialAtendimento { get; set; }
        public string GC { get; set; }
        public string EmpresaCRM { get; set; }
        public string ChaveCRM { get; set; }
        public string Vertical { get; set; }
        public DateTime DtRecLeadGc { get; set; }
        public string FeedbackAtend { get; set; }
        public int Funcionarios { get; set; }
        public int Filiais { get; set; }
        public string Faturamento { get; set; }
        public string StatusContaCRM { get; set; }
        public string RazaoStatus { get; set; }
        public string ModalidadeTucunare { get; set; }
        public string ProbabilidadeFechamento { get; set; }
        public DateTime DtEstimativaFechamento { get; set; }
        public string ReceitaMensalEstimada { get; set; }
        public string ReceitaTucunare { get; set; }
        public int PrazoContratoEstimado { get; set; }
        public string TotalContrato { get; set; }
        public string PilarOpp { get; set; }
        public int NumeroProjetoTucunare { get; set; }
        public int NumeroPropostaTucunare { get; set; }
        public int ListaTucunare { get; set; }
        public DateTime DtFechamentoNegocio { get; set; }
        public DateTime DtPerdaNegocio { get; set; }
        public DateTime DtCriacaoOpp { get; set; }
        public string EstagioProcesso { get; set; }
        public string DescricaoProjeto { get; set; }
        public string GerenteConta { get; set; }
        public string UnidadeNegocios { get; set; }
        public string Gcom { get; set; }
        public string Oportunidade { get; set; }
        public string TipoConta { get; set; }

    }
}
