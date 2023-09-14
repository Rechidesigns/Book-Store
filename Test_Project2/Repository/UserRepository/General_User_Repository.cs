using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Test_Project2.Data_Contest;
using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public class General_User_Repository : IGeneral_User_Repository
    {
        private readonly Book_Shop_DataContext book_Context;
        private readonly IWebHostEnvironment environment;

        public General_User_Repository(Book_Shop_DataContext book_Context, IWebHostEnvironment environment)
        {
            this.book_Context = book_Context;
            this.environment = environment;
        }


        public async Task<General_User> AuthenticateAsync(string email, string password)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(
                x => x.Email == email);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            if (!VerifyPassword(password, user.PasswordHash!, user.PasswordSalt!))
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.

            var userRoles = await book_Context.User_Roles!.Where(x => x.GeneralUsersId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await book_Context.Roles!.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name!);
                    }
                }
            }
            // user.Password = null;
            return user!;
        }

        private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }


        public async Task<General_User> AddAsync(General_User users, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            users.Id = Guid.NewGuid();
            users.PasswordHash = passwordHash;
            users.PasswordSalt = passwordSalt;
            await book_Context.AddAsync(users);
            await book_Context.SaveChangesAsync();
            return users;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<General_User?> DeleteAsync(Guid id)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            var users = await book_Context.General_User!.FirstOrDefaultAsync(x => x.Id == id);
            if (users == null)
            {
                return null;
            }
            else
            {
                // Delete Region
                book_Context.General_User!.Remove(users);
                await book_Context.SaveChangesAsync();
                return users;
            }

        }

        public async Task<IEnumerable<General_User>> GetAllAsync()
        {
            return await book_Context.General_User!.ToListAsync();
        }

        public async Task<General_User> GetAsync(Guid id)
        {

#pragma warning disable CS8603 // Possible null reference return.
            return await book_Context.General_User!.FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<General_User?> UpdateAsync(Guid id, General_User user)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            var existinguser = await book_Context.General_User!.FirstOrDefaultAsync(x => x.Id == id);

            if (existinguser == null)
            {
                return null;
            }
            else
            {
                existinguser.SurName = user.SurName;
                existinguser.FirstName = user.FirstName;
                await book_Context.SaveChangesAsync();
                return existinguser;
            }
        }

        public async Task<bool> UserExistAsync(string email)
        {
            var user = await book_Context.General_User!.AnyAsync(x => x.Email == email);
            if (user)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return true;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UserPhoneNoExistAsync(string phoneNo)
        {
            var user = await book_Context.General_User!.AnyAsync(x => x.PhoneNo == phoneNo);
            if (user)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return true;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UserNameExistAsync(string userName)
        {
            var user = await book_Context.General_User!.AnyAsync(x => x.SurName == userName);
            if (user)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return true;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                return false;
            }
        }


        public async Task<General_User> VerifyAsync(string token)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            user.EmailConfirmed = true;
            user.VerifiedAt = DateTime.Now;
            await book_Context.SaveChangesAsync();
            return user;
        }
        public async Task<General_User> PhoneNoVerifyAsync(string otp)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(u => u.PhoneVerificationToken == otp);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            user.PhoneNoConfirmed = true;
            user.PhoneVerifiedAt = DateTime.Now;
            await book_Context.SaveChangesAsync();
            return user;
        }

        public async Task<General_User> ForgetPasswordAsync(string email)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddMinutes(10);
            await book_Context.SaveChangesAsync();
            return user;
        }
        public async Task<General_User> RequestNewEmailTokenAsync(string email)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }

            user.VerificationToken = CreateRandomToken();
            await book_Context.SaveChangesAsync();
            return user;
        }


        public async Task<General_User> TwoFactorEnabledAsync(Guid id)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            user.TwoFactorEnabled = true;
            await book_Context.SaveChangesAsync();
            return user;
        }

        public async Task<General_User> CheckUserAsync(Guid id)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            return user;
        }
        private static string CreateRandomToken()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new();
            for (int i = 0; i < 5; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos)!.ToString()!)) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }

        public async Task<General_User> PasswordResetAsync(string token, string password)
        {
            var user = await book_Context.General_User!.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await book_Context.SaveChangesAsync();
            return user;
        }
        public async Task<General_User> UploadPicsAsync(Guid id, General_User imagePath)
        {

            var existinguser = await book_Context.General_User!.FirstOrDefaultAsync(x => x.Id == id);

            if (existinguser == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            else
            {
                existinguser.ImagePath = imagePath.ImagePath;
                await book_Context.SaveChangesAsync();
                return existinguser;
            }
        }

    }
}
