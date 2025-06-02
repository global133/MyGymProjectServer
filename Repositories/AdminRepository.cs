using MyGymProject.Server.Data;
using MyGymProject.Server.Models;
using MyGymProject.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyGymProject.Server.DTOs.Admin;
using System.Reflection.Metadata.Ecma335;

namespace MyGymProject.Server.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataBaseConnection _context;

        public AdminRepository(DataBaseConnection context)
        {
            this._context = context;
        }

        public async Task<Admin?> GetAdminByLoginAsync(string login)
        {
            return await this._context.Admins.FirstOrDefaultAsync(a => a.Login == login);
        }

        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            await this._context.Admins.AddAsync(admin);
            await this._context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin?> GetAdminByIdAsync(int id)
        {
            return await this._context.Admins.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Admin?>> GetAllAdminsAsync()
        {
            return await this._context.Admins.ToListAsync();
        }

        public async Task<bool> UpdateAdminAsync(Admin updatedAdmin)
        {
            var existingAdmin = await _context.Admins.FindAsync(updatedAdmin.Id);
            if (existingAdmin == null)
            {
                return false; 
            }

            existingAdmin.FullName = updatedAdmin.FullName;
            existingAdmin.DateOfBirth = updatedAdmin.DateOfBirth;
            existingAdmin.Phone = updatedAdmin.Phone;
            existingAdmin.Email = updatedAdmin.Email;
            existingAdmin.Gender = updatedAdmin.Gender;
            existingAdmin.Status = updatedAdmin.Status;
            existingAdmin.Login = updatedAdmin.Login;
            existingAdmin.Password = updatedAdmin.Password;

            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            var existing = await this._context.Admins.FindAsync(id);

            if (existing == null)
                return false;

            this._context.Admins.Remove(existing);
            await this._context.SaveChangesAsync();
            return true;
        }
    }
}
