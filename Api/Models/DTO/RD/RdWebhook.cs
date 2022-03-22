using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.DTO.RD
{
    public class RdWebhook
    {
        public List<Lead> leads { get; set; }
    }

    public class Content
    {
        public string identificador { get; set; }
        public string traffic_source { get; set; }
        public string nome { get; set; }
        public string Sobrenome { get; set; }
        public string empresa { get; set; }
        public string CNPJ { get; set; }
        public string telefone { get; set; }

        [JsonProperty("Qual o pilar do seu negócio que você deseja falar?")]
        public string QualOPilarDoSeuNegocioQueVoceDesejaFalar { get; set; }

        [JsonProperty("Quantidade de pilares")]
        public string QuantidadeDePilares { get; set; }
        public string Volume { get; set; }
        public string Mensagem { get; set; }
        public List<string> SejaclienteModel_aceito_ { get; set; }
        public string form_url { get; set; }
        public string page_title { get; set; }
        public string email_lead { get; set; }
        public object UF { get; set; }
    }

    public class ConversionOrigin
    {
        public string source { get; set; }
        public string medium { get; set; }
        public string value { get; set; }
        public string campaign { get; set; }
        public string channel { get; set; }
    }

    public class FirstConversion
    {
        public Content content { get; set; }
        public DateTime created_at { get; set; }
        public string cumulative_sum { get; set; }
        public string source { get; set; }
        public ConversionOrigin conversion_origin { get; set; }
    }

    public class LastConversion
    {
        public Content content { get; set; }
        public DateTime created_at { get; set; }
        public string cumulative_sum { get; set; }
        public string source { get; set; }
        public ConversionOrigin conversion_origin { get; set; }
    }

    public class CustomFields
    {
        public string CNPJ { get; set; }
        public string Sobrenome { get; set; }
        public string Mensagem { get; set; }

        [JsonProperty("Quantidade de pilares")]
        public string QuantidadeDePilares { get; set; }
        public string Volume { get; set; }

        [JsonProperty("Qual o pilar do seu negócio que você deseja falar?")]
        public string QualOPilarDoSeuNegocioQueVoceDesejaFalar { get; set; }
    }

    public class Lead
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public object job_title { get; set; }
        public object bio { get; set; }
        public string public_url { get; set; }
        public DateTime created_at { get; set; }
        public string opportunity { get; set; }
        public string number_conversions { get; set; }
        public object user { get; set; }
        public FirstConversion first_conversion { get; set; }
        public LastConversion last_conversion { get; set; }
        public CustomFields custom_fields { get; set; }
        public object website { get; set; }
        public string personal_phone { get; set; }
        public object mobile_phone { get; set; }
        public object city { get; set; }
        public object state { get; set; }
        public object tags { get; set; }
        public string lead_stage { get; set; }
        public object last_marked_opportunity_date { get; set; }
        public string uuid { get; set; }
        public string fit_score { get; set; }
        public int interest { get; set; }
    }

}
