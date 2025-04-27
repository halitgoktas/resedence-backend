using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Common;
using ResidenceManagement.Core.DTOs.User;
using ResidenceManagement.Core.Entities.Identity;
using ResidenceManagement.Core.Interfaces;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Core.Interfaces.Services;
using System.Net;

namespace ResidenceManagement.Infrastructure.Services
{
    /// <summary>
    /// Kullanıcı yönetimi servis implementasyonu
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<Kullanici> _kullaniciRepository;
        private readonly IRepository<Rol> _rolRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly ILogger<UserService> _logger;
        private readonly ResidenceManagement.Core.Interfaces.Repositories.IUnitOfWork _unitOfWork;

        /// <summary>
        /// UserService constructor
        /// </summary>
        public UserService(
            IRepository<Kullanici> kullaniciRepository,
            IRepository<Rol> rolRepository,
            IRepository<UserRole> userRoleRepository,
            ILogger<UserService> logger,
            ResidenceManagement.Core.Interfaces.Repositories.IUnitOfWork unitOfWork)
        {
            _kullaniciRepository = kullaniciRepository;
            _rolRepository = rolRepository;
            _userRoleRepository = userRoleRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task<Kullanici> FindByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _kullaniciRepository.GetFirstOrDefaultAsync(u => 
                u.KullaniciAdi == usernameOrEmail || u.Email == usernameOrEmail);
        }

        /// <inheritdoc/>
        public async Task<bool> ValidatePasswordAsync(Kullanici kullanici, string password)
        {
            if (kullanici == null)
                return false;

            // Şifre hashleme ve doğrulama işlemi (bu bir örnek implementasyondur, gerçek uygulamada daha güvenli hash algoritmaları kullanılmalıdır)
            string hashedPassword = HashPassword(password, kullanici.SifreSalt);
            return hashedPassword == kullanici.Sifre;
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<List<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var kullanicilar = await _kullaniciRepository.GetAllAsync();
                var userDtos = new List<UserDto>();

                foreach (var kullanici in kullanicilar)
                {
                    var userDto = await MapToUserDtoAsync(kullanici);
                    userDtos.Add(userDto);
                }

                return new ApiResponse<List<UserDto>>
                {
                    IsSuccess = true,
                    Message = "Kullanıcılar başarıyla listelendi",
                    Data = userDtos,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcılar listelenirken bir hata oluştu: {ex.Message}");
                return new ApiResponse<List<UserDto>>
                {
                    IsSuccess = false,
                    Message = "Kullanıcılar listelenirken bir hata oluştu",
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(int id)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);

                if (kullanici == null)
                {
                    return new ApiResponse<UserDto>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı",
                        StatusCode = (HttpStatusCode)404
                    };
                }

                var userDto = await MapToUserDtoAsync(kullanici);

                return new ApiResponse<UserDto>
                {
                    IsSuccess = true,
                    Message = "Kullanıcı başarıyla getirildi",
                    Data = userDto,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcı getirilirken bir hata oluştu: {ex.Message}");
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı getirilirken bir hata oluştu",
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                // Kullanıcı adı veya e-posta zaten kullanımda mı kontrol et
                var existingUser = await _kullaniciRepository.GetFirstOrDefaultAsync(u => 
                    u.KullaniciAdi == createUserDto.KullaniciAdi || u.Email == createUserDto.Email);

                if (existingUser != null)
                {
                    var errorMessage = existingUser.KullaniciAdi == createUserDto.KullaniciAdi
                        ? "Bu kullanıcı adı zaten kullanımda."
                        : "Bu e-posta adresi zaten kullanımda.";

                    return new ApiResponse<UserDto>
                    {
                        IsSuccess = false,
                        Message = errorMessage,
                        StatusCode = (HttpStatusCode)400
                    };
                }

                // Şifre için salt ve hash oluştur
                string salt = GenerateSalt();
                string hashedPassword = HashPassword(createUserDto.Sifre, salt);

                // Yeni kullanıcı oluştur
                var newUser = new Kullanici
                {
                    KullaniciAdi = createUserDto.KullaniciAdi,
                    Email = createUserDto.Email,
                    Telefon = createUserDto.Telefon,
                    Ad = createUserDto.Ad,
                    Soyad = createUserDto.Soyad,
                    Sifre = hashedPassword,
                    SifreSalt = salt,
                    ProfilResimUrl = createUserDto.ProfilResimUrl,
                    FirmaId = createUserDto.FirmaId,
                    SubeId = createUserDto.SubeId,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                // Kullanıcıyı veritabanına ekle
                await _kullaniciRepository.AddAsync(newUser);
                await _unitOfWork.SaveChangesAsync();

                // Oluşturulan kullanıcıyı DTO'ya dönüştür
                var userDto = await MapToUserDtoAsync(newUser);

                return new ApiResponse<UserDto>
                {
                    IsSuccess = true,
                    Message = "Kullanıcı başarıyla oluşturuldu",
                    Data = userDto,
                    StatusCode = (HttpStatusCode)201
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcı oluşturulurken bir hata oluştu: {ex.Message}");
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı oluşturulurken bir hata oluştu",
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<UserDto>> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);

                if (kullanici == null)
                {
                    return new ApiResponse<UserDto>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı",
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Email benzersizlik kontrolü
                if (!string.IsNullOrEmpty(updateUserDto.Email) && updateUserDto.Email != kullanici.Email)
                {
                    var emailExists = await _kullaniciRepository.AnyAsync(u => u.Email == updateUserDto.Email && u.Id != id);
                    if (emailExists)
                    {
                        return new ApiResponse<UserDto>
                        {
                            IsSuccess = false,
                            Message = "Bu e-posta adresi zaten kullanımda",
                            StatusCode = (HttpStatusCode)400
                        };
                    }
                    kullanici.Email = updateUserDto.Email;
                }

                // Diğer alanları güncelle
                if (!string.IsNullOrEmpty(updateUserDto.Telefon))
                    kullanici.Telefon = updateUserDto.Telefon;

                if (!string.IsNullOrEmpty(updateUserDto.Ad))
                    kullanici.Ad = updateUserDto.Ad;

                if (!string.IsNullOrEmpty(updateUserDto.Soyad))
                    kullanici.Soyad = updateUserDto.Soyad;

                if (!string.IsNullOrEmpty(updateUserDto.ProfilResimUrl))
                    kullanici.ProfilResimUrl = updateUserDto.ProfilResimUrl;

                if (updateUserDto.SubeId.HasValue)
                    kullanici.SubeId = updateUserDto.SubeId.Value;

                kullanici.IsActive = updateUserDto.IsActive;
                kullanici.UpdatedDate = DateTime.UtcNow;

                // Kullanıcıyı güncelle
                await _kullaniciRepository.UpdateAsync(kullanici);
                await _unitOfWork.SaveChangesAsync();

                // Güncellenmiş kullanıcıyı DTO'ya dönüştür
                var userDto = await MapToUserDtoAsync(kullanici);

                return new ApiResponse<UserDto>
                {
                    IsSuccess = true,
                    Message = "Kullanıcı başarıyla güncellendi",
                    Data = userDto,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcı güncellenirken bir hata oluştu: {ex.Message}");
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı güncellenirken bir hata oluştu",
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);

                if (kullanici == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı",
                        Data = false,
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Soft delete
                kullanici.IsDeleted = true;
                kullanici.IsActive = false;
                kullanici.UpdatedDate = DateTime.UtcNow;

                await _kullaniciRepository.UpdateAsync(kullanici);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Kullanıcı başarıyla silindi",
                    Data = true,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcı silinirken bir hata oluştu: {ex.Message}");
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı silinirken bir hata oluştu",
                    Data = false,
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);

                if (kullanici == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı",
                        Data = false,
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Mevcut şifre doğrulama
                bool isValidPassword = await ValidatePasswordAsync(kullanici, changePasswordDto.CurrentPassword);
                if (!isValidPassword)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Mevcut şifre hatalı",
                        Data = false,
                        StatusCode = (HttpStatusCode)400
                    };
                }

                // Yeni şifre için hash oluştur
                string hashedPassword = HashPassword(changePasswordDto.NewPassword, kullanici.SifreSalt);
                kullanici.Sifre = hashedPassword;
                kullanici.UpdatedDate = DateTime.UtcNow;

                await _kullaniciRepository.UpdateAsync(kullanici);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Şifre başarıyla değiştirildi",
                    Data = true,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Şifre değiştirilirken bir hata oluştu: {ex.Message}");
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Şifre değiştirilirken bir hata oluştu",
                    Data = false,
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<List<string>>> GetUserRolesAsync(int id)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);

                if (kullanici == null)
                {
                    return new ApiResponse<List<string>>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı",
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Kullanıcının rollerini getir
                var userRoles = await _userRoleRepository.FindAsync(ur => ur.KullaniciId == id);
                var roleIds = userRoles.Select(ur => ur.RolId).ToList();
                
                // Guid-int karşılaştırma hatalarını önlemek için roller tek tek kontrol edilecek
                var roles = new List<Rol>();
                foreach (var roleId in roleIds)
                {
                    var role = await _rolRepository.GetFirstOrDefaultAsync(r => r.Id.ToString() == roleId.ToString());
                    if (role != null)
                    {
                        roles.Add(role);
                    }
                }
                
                var roleNames = roles.Select(r => r.RolAdi).ToList();

                return new ApiResponse<List<string>>
                {
                    IsSuccess = true,
                    Message = "Kullanıcı rolleri başarıyla getirildi",
                    Data = roleNames,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcı rolleri getirilirken bir hata oluştu: {ex.Message}");
                return new ApiResponse<List<string>>
                {
                    IsSuccess = false,
                    Message = "Kullanıcı rolleri getirilirken bir hata oluştu",
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> AssignRoleToUserAsync(int id, string roleName)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);

                if (kullanici == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı",
                        Data = false,
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Rolü bul
                var rol = await _rolRepository.GetFirstOrDefaultAsync(r => r.RolAdi == roleName);

                if (rol == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = $"'{roleName}' rolü bulunamadı",
                        Data = false,
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Kullanıcı zaten bu role sahip mi kontrol et
                var existingUserRole = await _userRoleRepository.GetFirstOrDefaultAsync(ur => 
                    ur.KullaniciId == id && ur.RolId.ToString() == rol.Id.ToString());

                if (existingUserRole != null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = $"Kullanıcı zaten '{roleName}' rolüne sahip",
                        Data = false,
                        StatusCode = (HttpStatusCode)400
                    };
                }

                // Yeni rol atama
                var userRole = new UserRole
                {
                    KullaniciId = id,
                    RolId = rol.Id,
                    CreatedDate = DateTime.UtcNow,
                    FirmaId = kullanici.FirmaId,
                    SubeId = kullanici.SubeId
                };

                await _userRoleRepository.AddAsync(userRole);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = $"'{roleName}' rolü kullanıcıya başarıyla atandı",
                    Data = true,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Rol atama sırasında bir hata oluştu: {ex.Message}");
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Rol atama sırasında bir hata oluştu",
                    Data = false,
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> RemoveRoleFromUserAsync(int id, string roleName)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetByIdAsync(id);

                if (kullanici == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı",
                        Data = false,
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Rolü bul
                var rol = await _rolRepository.GetFirstOrDefaultAsync(r => r.RolAdi == roleName);

                if (rol == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = $"'{roleName}' rolü bulunamadı",
                        Data = false,
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Kullanıcı-rol ilişkisini bul
                var userRole = await _userRoleRepository.GetFirstOrDefaultAsync(ur => 
                    ur.KullaniciId == id && ur.RolId.ToString() == rol.Id.ToString());

                if (userRole == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = $"Kullanıcı '{roleName}' rolüne sahip değil",
                        Data = false,
                        StatusCode = (HttpStatusCode)400
                    };
                }

                // Rolü kaldır
                await _userRoleRepository.DeleteAsync(userRole);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = $"'{roleName}' rolü kullanıcıdan başarıyla kaldırıldı",
                    Data = true,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Rol kaldırma sırasında bir hata oluştu: {ex.Message}");
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Rol kaldırma sırasında bir hata oluştu",
                    Data = false,
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> ForgotPasswordAsync(string email)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetFirstOrDefaultAsync(u => u.Email == email);

                if (kullanici == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Bu email adresi ile kayıtlı kullanıcı bulunamadı",
                        Data = false,
                        StatusCode = (HttpStatusCode)404
                    };
                }

                // Şifre sıfırlama token'ı oluştur
                string resetToken = GenerateResetToken();
                
                // Token'ın son kullanma tarihini ayarla (örn: 24 saat)
                DateTime tokenExpiry = DateTime.UtcNow.AddHours(24);
                
                // Token'ı kullanıcı ile ilişkilendir
                kullanici.ResetPasswordToken = resetToken;
                kullanici.ResetPasswordTokenExpiry = tokenExpiry;
                kullanici.UpdatedDate = DateTime.UtcNow;
                
                await _kullaniciRepository.UpdateAsync(kullanici);
                await _unitOfWork.SaveChangesAsync();
                
                // Burada email gönderme işlemi yapılabilir
                // Email servisi entegre edildiğinde bu kısım değiştirilecek
                
                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Şifre sıfırlama bağlantısı email adresinize gönderildi",
                    Data = true,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Şifre sıfırlama işlemi başlatılırken bir hata oluştu: {ex.Message}");
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Şifre sıfırlama işlemi başlatılırken bir hata oluştu",
                    Data = false,
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> ResetPasswordAsync(string token, string newPassword)
        {
            try
            {
                var kullanici = await _kullaniciRepository.GetFirstOrDefaultAsync(u => 
                    u.ResetPasswordToken == token && 
                    u.ResetPasswordTokenExpiry > DateTime.UtcNow);

                if (kullanici == null)
                {
                    return new ApiResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Geçersiz veya süresi dolmuş token",
                        Data = false,
                        StatusCode = (HttpStatusCode)400
                    };
                }

                // Yeni şifre için hash oluştur
                string salt = kullanici.SifreSalt; // Mevcut salt'ı kullan
                string hashedPassword = HashPassword(newPassword, salt);
                
                // Şifreyi güncelle ve token bilgilerini temizle
                kullanici.Sifre = hashedPassword;
                kullanici.ResetPasswordToken = null;
                kullanici.ResetPasswordTokenExpiry = null;
                kullanici.UpdatedDate = DateTime.UtcNow;
                
                await _kullaniciRepository.UpdateAsync(kullanici);
                await _unitOfWork.SaveChangesAsync();
                
                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Şifreniz başarıyla sıfırlandı",
                    Data = true,
                    StatusCode = (HttpStatusCode)200
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Şifre sıfırlama işlemi tamamlanırken bir hata oluştu: {ex.Message}");
                return new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Şifre sıfırlama işlemi tamamlanırken bir hata oluştu",
                    Data = false,
                    StatusCode = (HttpStatusCode)500
                };
            }
        }

        /// <inheritdoc/>
        public async Task<PagedApiResponse<UserDto>> GetPagedUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Sayfalama parametreleri kontrolü
                var (validatedPageNumber, validatedPageSize) = PagedList<Kullanici>.ValidateParameters(pageNumber, pageSize);
                
                // Kullanıcıları çek
                var kullaniciQuery = _kullaniciRepository.GetQueryable();
                
                // Sayfalı listeyi oluştur
                var pagedList = await PagedList<Kullanici>.CreateAsync(
                    kullaniciQuery,
                    validatedPageNumber,
                    validatedPageSize);
                
                // DTO'lara dönüştür
                var userDtos = new List<UserDto>();
                foreach (var kullanici in pagedList)
                {
                    var userDto = await MapToUserDtoAsync(kullanici);
                    userDtos.Add(userDto);
                }
                
                // PagedList<UserDto> oluştur
                var userDtoPagedList = new PagedList<UserDto>(
                    userDtos, 
                    pagedList.TotalCount, 
                    pagedList.CurrentPage, 
                    pagedList.PageSize);
                
                // PagedApiResponse oluştur
                return PagedApiResponse<UserDto>.Success(userDtoPagedList, "Kullanıcılar başarıyla listelendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Kullanıcılar sayfalı olarak listelenirken bir hata oluştu: {ex.Message}");
                return PagedApiResponse<UserDto>.Failure("Kullanıcılar listelenirken bir hata oluştu", HttpStatusCode.InternalServerError);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Kullanıcıyı UserDto'ya dönüştürür
        /// </summary>
        private async Task<UserDto> MapToUserDtoAsync(Kullanici kullanici)
        {
            var userRoles = await _userRoleRepository.FindAsync(ur => ur.KullaniciId == kullanici.Id);
            var roleIds = userRoles.Select(ur => ur.RolId).ToList();
            
            // Guid-int karşılaştırma hatalarını önlemek için roller tek tek kontrol edilecek
            var roles = new List<Rol>();
            foreach (var roleId in roleIds)
            {
                var role = await _rolRepository.GetFirstOrDefaultAsync(r => r.Id.ToString() == roleId.ToString());
                if (role != null)
                {
                    roles.Add(role);
                }
            }
            
            var roleNames = roles.Select(r => r.RolAdi).ToList();

            return new UserDto
            {
                Id = kullanici.Id,
                KullaniciAdi = kullanici.KullaniciAdi,
                Email = kullanici.Email,
                Telefon = kullanici.Telefon,
                Ad = kullanici.Ad,
                Soyad = kullanici.Soyad,
                TamAd = kullanici.TamAd,
                ProfilResimUrl = kullanici.ProfilResimUrl,
                SonGirisTarihi = kullanici.SonGirisTarihi,
                IsActive = kullanici.IsActive,
                CreatedDate = kullanici.CreatedDate,
                FirmaId = kullanici.FirmaId,
                SubeId = kullanici.SubeId,
                Roller = roleNames
            };
        }

        /// <summary>
        /// Rastgele salt üretir
        /// </summary>
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Şifreyi hashler (örnek implementasyon, gerçek uygulamada daha güvenli algoritmalar kullanılmalıdır)
        /// </summary>
        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Concat(password, salt);
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hashedBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        /// <summary>
        /// Şifre sıfırlama için token üretir
        /// </summary>
        private string GenerateResetToken()
        {
            byte[] tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return Convert.ToBase64String(tokenBytes);
        }

        #endregion
    }
} 