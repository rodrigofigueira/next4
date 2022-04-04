using Api.Models.DTO.RD;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface IRDService
    {
        public Task<bool> Post(RDPost rdPost);
    }
}
