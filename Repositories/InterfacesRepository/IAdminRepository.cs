using MyGymProject.Server.Models;
using MyGymProject.Server.DTOs;
using MyGymProject.Server.DTOs.Admin;

namespace MyGymProject.Server.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> CreateAdminAsync(Admin dto);

        Task<Admin?> GetAdminByLoginAsync(string login);

        Task<IEnumerable<Admin?>> GetAllAdminsAsync();

        Task<Admin?> GetAdminByIdAsync(int id);

        Task<bool> UpdateAdminAsync(Admin updatedAdmin);

        Task<bool> DeleteAdminAsync(int id);
    }
}
