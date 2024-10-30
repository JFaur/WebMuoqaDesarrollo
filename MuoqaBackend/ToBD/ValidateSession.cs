using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuoqaBD;
using MuoqaIdentidades;
using Serilog;

namespace MuoqaBackend.ToBD
{
    public class ValidateSession
    {
        private readonly MuoqaBDConf _conn;
        public ValidateSession(MuoqaBDConf conn)
        {
            _conn = conn ?? throw new ArgumentNullException(nameof(conn));
        }

        public async Task<bool> UploadNewUser(Account data)
        {
            try
            {
                Account load = new Account//Esto se puede reducir pasando data nomas
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    UserPassword = data.UserPassword,
                    Email = data.Email,
                    ActivatedUser = "No",
                };
                _conn.RegisteredUsers.Add(load);
                int rows = await _conn.SaveChangesAsync();
                if(rows >0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Log.Error($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<string> CheckUserAndMail(string user, string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(await _conn.RegisteredUsers.Where(u => u.UserName == user).Select(u => u.UserName).FirstOrDefaultAsync()))
                    return "user";
                if (!string.IsNullOrEmpty(await _conn.RegisteredUsers.Where(u => u.UserName == user).Select(u => u.UserName).FirstOrDefaultAsync()))
                    return "email";

                return "ok";

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Log.Error($"Error: {ex.Message}");
                return "false";
            }
        }

        public async Task<bool> CheckUser(string usr)
        {
            try
            {
                string? user = await _conn.RegisteredUsers
                    .Where(u => u.UserName == usr)
                    .Select(u => u.UserName)
                    .FirstOrDefaultAsync();

                if (user != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Log.Error($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ComparePwd(string usr, string pwd)
        {
            try
            {
                string? originalPwd = await _conn.RegisteredUsers
                    .Where(u => u.UserName == usr)
                    .Select(u => u.UserPassword)
                    .FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(originalPwd))
                {
                    if (VerfyPassword(originalPwd, pwd))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex} B");
                Log.Error($"Error: {ex.Message} B");
                return false;
            }
        }

        public bool VerfyPassword(string pwd1, string pwd2) 
        {
            return BCrypt.Net.BCrypt.Verify(pwd1, pwd2);
        }
    }
}
