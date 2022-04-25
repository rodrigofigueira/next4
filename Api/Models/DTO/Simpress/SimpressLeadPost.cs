namespace Api.Models.DTO.Simpress
{
    public class SimpressLeadPost
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string companyname { get; set; }
        public string stg_cnpj { get; set; }
        public string telephone1 { get; set; }
        public string emailaddress1 { get; set; }
        public int sim_pilar_negocio_interesse { get; set; }
        public int sim_qtd_impressora_multifuncionais { get; set; }
        public int sim_volume_impressao { get; set; }
        public string qualificationcomments { get; set; }
    }
}
