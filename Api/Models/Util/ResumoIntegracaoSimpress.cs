using System.Collections.Generic;

namespace Api.Models.Util
{
    public class ResumoIntegracaoSimpress
    {
        public List<string> UUIDIntegradas { get; set; }
        public List<string> UUIDNaoIntegradas { get; set; }
        
        public ResumoIntegracaoSimpress()
        {
            UUIDIntegradas = new List<string>();
            UUIDNaoIntegradas = new List<string>();
        }
    }
}
