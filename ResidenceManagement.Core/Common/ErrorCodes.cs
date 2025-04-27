using System.Collections.Generic;

namespace ResidenceManagement.Core.Common
{
    /// <summary>
    /// Uygulamanın tamamında kullanılacak standart hata kodları ve mesajlarını içerir.
    /// </summary>
    public static class ErrorCodes
    {
        #region Genel Hatalar (1000-1999)

        // Genel Hata Kodları
        public const int GeneralError = 1000;
        public const int ValidationError = 1001;
        public const int ResourceNotFound = 1002;
        public const int NotAuthorized = 1003;
        public const int Forbidden = 1004;
        public const int DuplicateRecord = 1005;
        public const int InvalidOperation = 1006;
        public const int ConcurrencyError = 1007;
        public const int DatabaseError = 1008;
        public const int ExternalServiceError = 1009;
        public const int InvalidData = 1010;
        public const int BusinessRuleViolation = 1011;

        #endregion

        #region Kimlik Doğrulama Hataları (2000-2999)

        // Kimlik Doğrulama Hata Kodları
        public const int InvalidCredentials = 2000;
        public const int AccountLocked = 2001;
        public const int AccountDisabled = 2002;
        public const int InvalidToken = 2003;
        public const int ExpiredToken = 2004;
        public const int RefreshTokenExpired = 2005;
        public const int InvalidRefreshToken = 2006;
        public const int UserNotFound = 2007;
        public const int PasswordResetRequired = 2008;
        public const int TwoFactorRequired = 2009;
        public const int InvalidTwoFactorCode = 2010;
        public const int TooManyLoginAttempts = 2011;
        public const int TooManyPasswordResetAttempts = 2012;

        #endregion

        #region Kullanıcı Hataları (3000-3999)

        // Kullanıcı Hata Kodları
        public const int UserAlreadyExists = 3000;
        public const int InvalidEmail = 3001;
        public const int InvalidPassword = 3002;
        public const int PasswordTooWeak = 3003;
        public const int OldPasswordIncorrect = 3004;
        public const int RoleNotFound = 3005;
        public const int UserRoleAlreadyAssigned = 3006;
        public const int InvalidUserAction = 3007;
        public const int AdminUserCannotBeDeleted = 3008;
        public const int InvalidUserStatus = 3009;

        #endregion

        #region Firma/Şube Hataları (4000-4999)

        // Firma ve Şube Hata Kodları
        public const int CompanyNotFound = 4000;
        public const int BranchNotFound = 4001;
        public const int CompanyAlreadyExists = 4002;
        public const int BranchAlreadyExists = 4003;
        public const int InvalidCompanyAction = 4004;
        public const int InvalidBranchAction = 4005;
        public const int CompanyHasActiveUsers = 4006;
        public const int CompanyHasActiveBranches = 4007;
        public const int BranchHasActiveUsers = 4008;

        #endregion

        #region Rezidans Hataları (5000-5999)

        // Blok ve Daire Hata Kodları
        public const int BlockNotFound = 5000;
        public const int ApartmentNotFound = 5001;
        public const int BlockAlreadyExists = 5002;
        public const int ApartmentAlreadyExists = 5003;
        public const int BlockHasActiveApartments = 5004;
        public const int ApartmentHasActiveResidents = 5005;
        public const int InvalidBlockAction = 5006;
        public const int InvalidApartmentAction = 5007;
        public const int ApartmentAlreadyOccupied = 5008;

        #endregion

        #region Sakin Hataları (6000-6999)

        // Sakin Hata Kodları
        public const int ResidentNotFound = 6000;
        public const int ResidentAlreadyExists = 6001;
        public const int ResidentHasActiveContracts = 6002;
        public const int InvalidResidentAction = 6003;
        public const int InvalidResidentStatus = 6004;
        public const int ResidentHasOutstandingDues = 6005;

        #endregion

        #region Finansal Hataları (7000-7999)

        // Finansal İşlem Hata Kodları
        public const int DuesNotFound = 7000;
        public const int PaymentNotFound = 7001;
        public const int InvalidPaymentAmount = 7002;
        public const int InvalidPaymentDate = 7003;
        public const int InvalidPaymentStatus = 7004;
        public const int DuplicatePayment = 7005;
        public const int InsufficientBalance = 7006;
        public const int ExpenseNotFound = 7007;
        public const int InvalidExpenseAmount = 7008;
        public const int InvalidExpenseDate = 7009;
        public const int InvalidExpenseCategory = 7010;

        #endregion

        #region Hizmet Talep Hataları (8000-8999)

        // Hizmet Talep Hata Kodları
        public const int ServiceRequestNotFound = 8000;
        public const int InvalidServiceRequestStatus = 8001;
        public const int ServiceCategoryNotFound = 8002;
        public const int ServiceRequestAlreadyCompleted = 8003;
        public const int ServiceRequestAlreadyCancelled = 8004;
        public const int ServiceRequestCannotBeModified = 8005;
        public const int AssignedUserNotFound = 8006;
        public const int InvalidServicePriority = 8007;

        #endregion

        #region Hata Mesajları Dictionary

        // Hata kodu -> Hata mesajı eşleşmesi
        private static readonly Dictionary<int, string> ErrorMessages = new Dictionary<int, string>
        {
            // Genel Hatalar
            { GeneralError, "Bir hata oluştu." },
            { ValidationError, "Validasyon hatası oluştu." },
            { ResourceNotFound, "İstenen kaynak bulunamadı." },
            { NotAuthorized, "Bu işlem için yetkiniz bulunmamaktadır." },
            { Forbidden, "Bu işlemi yapmak için gerekli izne sahip değilsiniz." },
            { DuplicateRecord, "Bu kayıt zaten mevcut." },
            { InvalidOperation, "Geçersiz işlem." },
            { ConcurrencyError, "Bu kayıt başka bir kullanıcı tarafından değiştirilmiş." },
            { DatabaseError, "Veritabanı hatası oluştu." },
            { ExternalServiceError, "Dış servis hatası oluştu." },
            { InvalidData, "Geçersiz veri." },
            { BusinessRuleViolation, "İş kuralı ihlali." },

            // Kimlik Doğrulama Hataları
            { InvalidCredentials, "Kullanıcı adı veya şifre hatalı." },
            { AccountLocked, "Hesabınız kilitlendi. Lütfen yönetici ile iletişime geçin." },
            { AccountDisabled, "Hesabınız devre dışı bırakıldı." },
            { InvalidToken, "Geçersiz token." },
            { ExpiredToken, "Token süresi doldu." },
            { RefreshTokenExpired, "Yenileme tokenının süresi doldu, lütfen tekrar giriş yapın." },
            { InvalidRefreshToken, "Geçersiz yenileme tokeni." },
            { UserNotFound, "Kullanıcı bulunamadı." },
            { PasswordResetRequired, "Şifrenizi sıfırlamanız gerekiyor." },
            { TwoFactorRequired, "İki faktörlü kimlik doğrulama gerekli." },
            { InvalidTwoFactorCode, "Geçersiz iki faktörlü doğrulama kodu." },
            { TooManyLoginAttempts, "Çok fazla başarısız giriş denemesi. Lütfen daha sonra tekrar deneyin." },
            { TooManyPasswordResetAttempts, "Çok fazla şifre sıfırlama denemesi. Lütfen daha sonra tekrar deneyin." },

            // Kullanıcı Hataları
            { UserAlreadyExists, "Bu kullanıcı adı veya e-posta adresi zaten kullanılmaktadır." },
            { InvalidEmail, "Geçersiz e-posta adresi." },
            { InvalidPassword, "Geçersiz şifre." },
            { PasswordTooWeak, "Şifre çok zayıf. Daha güçlü bir şifre seçin." },
            { OldPasswordIncorrect, "Eski şifre yanlış." },
            { RoleNotFound, "Rol bulunamadı." },
            { UserRoleAlreadyAssigned, "Bu rol kullanıcıya zaten atanmış." },
            { InvalidUserAction, "Kullanıcı için geçersiz işlem." },
            { AdminUserCannotBeDeleted, "Admin kullanıcısı silinemez." },
            { InvalidUserStatus, "Geçersiz kullanıcı durumu." },

            // Firma/Şube Hataları
            { CompanyNotFound, "Firma bulunamadı." },
            { BranchNotFound, "Şube bulunamadı." },
            { CompanyAlreadyExists, "Bu firma zaten mevcut." },
            { BranchAlreadyExists, "Bu şube zaten mevcut." },
            { InvalidCompanyAction, "Firma için geçersiz işlem." },
            { InvalidBranchAction, "Şube için geçersiz işlem." },
            { CompanyHasActiveUsers, "Firmanın aktif kullanıcıları var, silinemez." },
            { CompanyHasActiveBranches, "Firmanın aktif şubeleri var, silinemez." },
            { BranchHasActiveUsers, "Şubenin aktif kullanıcıları var, silinemez." },

            // Rezidans Hataları
            { BlockNotFound, "Blok bulunamadı." },
            { ApartmentNotFound, "Daire bulunamadı." },
            { BlockAlreadyExists, "Bu blok zaten mevcut." },
            { ApartmentAlreadyExists, "Bu daire zaten mevcut." },
            { BlockHasActiveApartments, "Blokun aktif daireleri var, silinemez." },
            { ApartmentHasActiveResidents, "Dairenin aktif sakinleri var, silinemez." },
            { InvalidBlockAction, "Blok için geçersiz işlem." },
            { InvalidApartmentAction, "Daire için geçersiz işlem." },
            { ApartmentAlreadyOccupied, "Bu daire zaten dolu." },

            // Sakin Hataları
            { ResidentNotFound, "Sakin bulunamadı." },
            { ResidentAlreadyExists, "Bu sakin zaten mevcut." },
            { ResidentHasActiveContracts, "Sakinin aktif sözleşmeleri var, silinemez." },
            { InvalidResidentAction, "Sakin için geçersiz işlem." },
            { InvalidResidentStatus, "Geçersiz sakin durumu." },
            { ResidentHasOutstandingDues, "Sakinin ödenmemiş aidatları var." },

            // Finansal Hatalar
            { DuesNotFound, "Aidat bulunamadı." },
            { PaymentNotFound, "Ödeme bulunamadı." },
            { InvalidPaymentAmount, "Geçersiz ödeme tutarı." },
            { InvalidPaymentDate, "Geçersiz ödeme tarihi." },
            { InvalidPaymentStatus, "Geçersiz ödeme durumu." },
            { DuplicatePayment, "Bu ödeme zaten yapılmış." },
            { InsufficientBalance, "Yetersiz bakiye." },
            { ExpenseNotFound, "Gider bulunamadı." },
            { InvalidExpenseAmount, "Geçersiz gider tutarı." },
            { InvalidExpenseDate, "Geçersiz gider tarihi." },
            { InvalidExpenseCategory, "Geçersiz gider kategorisi." },

            // Hizmet Talep Hataları
            { ServiceRequestNotFound, "Hizmet talebi bulunamadı." },
            { InvalidServiceRequestStatus, "Geçersiz hizmet talebi durumu." },
            { ServiceCategoryNotFound, "Hizmet kategorisi bulunamadı." },
            { ServiceRequestAlreadyCompleted, "Hizmet talebi zaten tamamlanmış." },
            { ServiceRequestAlreadyCancelled, "Hizmet talebi zaten iptal edilmiş." },
            { ServiceRequestCannotBeModified, "Hizmet talebi artık değiştirilemez." },
            { AssignedUserNotFound, "Atanan kullanıcı bulunamadı." },
            { InvalidServicePriority, "Geçersiz hizmet önceliği." }
        };

        #endregion

        /// <summary>
        /// Verilen hata kodu için hata mesajını getirir.
        /// </summary>
        /// <param name="errorCode">Hata kodu</param>
        /// <returns>Hata mesajı, eğer bulunamazsa genel hata mesajı döner</returns>
        public static string GetErrorMessage(int errorCode)
        {
            if (ErrorMessages.TryGetValue(errorCode, out string message))
            {
                return message;
            }

            return ErrorMessages[GeneralError];
        }

        /// <summary>
        /// Özel bir hata mesajı ile birlikte hata kodu döndürür.
        /// </summary>
        /// <param name="errorCode">Hata kodu</param>
        /// <param name="customMessage">Özel hata mesajı</param>
        /// <returns>Özel hata mesajı</returns>
        public static string GetErrorMessage(int errorCode, string customMessage)
        {
            if (string.IsNullOrWhiteSpace(customMessage))
            {
                return GetErrorMessage(errorCode);
            }

            return customMessage;
        }
    }
} 