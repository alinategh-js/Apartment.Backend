using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.DataGateways
{
    public interface IPersonTableGateway
    {
        Task<IEnumerable<OwnerResidentDTO>> GetAllPeopleByPageAndTypeAsync(int page, int size, int isOwner);
        Task<IEnumerable<PersonDTO>> GetAllPeople();
        Task<int> GetTotalCountOfPeopleAsync();
        Task<int> InsertPersonAsync(PersonDTO person); 
        Task<PersonDTO> GetPersonByIdAsync(int personId);
        Task<int> UpdatePersonAsync(PersonDTO person);
        Task DeletePersonByIdAsync(int personId); 
    }
}
