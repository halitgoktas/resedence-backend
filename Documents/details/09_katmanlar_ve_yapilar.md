# Residence Management Projesi - Katmanlar ve Yapılar

## 1. Katmanlar (Layers)

### Core Katmanı
- **Entities**: Veritabanı modelleri
  - `Resident.cs`
  - `Apartment.cs`
  - `Payment.cs`
  - `User.cs`
  - `Role.cs`

- **DTOs**: Veri transfer nesneleri
  - `ResidentDto.cs`
  - `ApartmentDto.cs`
  - `PaymentDto.cs`
  - `UserDto.cs`

- **Interfaces**: Servis ve repository arayüzleri
  - `IResidentService.cs`
  - `IApartmentService.cs`
  - `IPaymentService.cs`
  - `IUserService.cs`

- **Models**: İş mantığı modelleri
  - `ResidentModel.cs`
  - `ApartmentModel.cs`
  - `PaymentModel.cs`
  - `UserModel.cs`

### Infrastructure Katmanı
- **Data**: Veritabanı işlemleri
  - `ResidentRepository.cs`
  - `ApartmentRepository.cs`
  - `PaymentRepository.cs`
  - `UserRepository.cs`

- **Services**: Servis implementasyonları
  - `ResidentService.cs`
  - `ApartmentService.cs`
  - `PaymentService.cs`
  - `UserService.cs`

### API Katmanı
- **Controllers**: API endpoint'leri
  - `ResidentController.cs`
  - `ApartmentController.cs`
  - `PaymentController.cs`
  - `UserController.cs`

## 2. İsimlendirme Kuralları

### Sınıf İsimlendirme
- PascalCase kullanımı
- Açıklayıcı ve anlamlı isimler
- Tekil isimler
- Örnek: `ResidentService`, `PaymentRepository`

### Metod İsimlendirme
- PascalCase kullanımı
- Fiil ile başlama
- Açıklayıcı isimler
- Örnek: `GetResidentById`, `CreatePayment`

### Değişken İsimlendirme
- camelCase kullanımı
- Açıklayıcı isimler
- Örnek: `residentId`, `paymentAmount`

### Interface İsimlendirme
- "I" prefix'i ile başlama
- PascalCase kullanımı
- Örnek: `IResidentService`, `IPaymentRepository`

## 3. Komponentler (Components)

### Common Komponentler
- **Button**
  - `PrimaryButton.tsx`
  - `SecondaryButton.tsx`
  - `IconButton.tsx`
  - `LoadingButton.tsx`

- **Input**
  - `TextInput.tsx`
  - `NumberInput.tsx`
  - `SelectInput.tsx`
  - `DateInput.tsx`

- **Table**
  - `DataTable.tsx`
  - `SortableTable.tsx`
  - `FilterableTable.tsx`
  - `PaginationTable.tsx`

- **Modal**
  - `ConfirmationModal.tsx`
  - `FormModal.tsx`
  - `InfoModal.tsx`
  - `ErrorModal.tsx`

### Layout Komponentler
- **Header**
  - `MainHeader.tsx`
  - `UserHeader.tsx`
  - `NotificationHeader.tsx`

- **Sidebar**
  - `MainSidebar.tsx`
  - `UserSidebar.tsx`
  - `AdminSidebar.tsx`

- **Footer**
  - `MainFooter.tsx`
  - `UserFooter.tsx`

### Feature Komponentler
- **Resident**
  - `ResidentList.tsx`
  - `ResidentForm.tsx`
  - `ResidentDetails.tsx`
  - `ResidentCard.tsx`

- **Payment**
  - `PaymentList.tsx`
  - `PaymentForm.tsx`
  - `PaymentDetails.tsx`
  - `PaymentHistory.tsx`

## 4. Sayfalar (Pages)

### Auth Sayfaları
- **Login**
  - `LoginPage.tsx`
  - `LoginForm.tsx`
  - `ForgotPassword.tsx`

- **Register**
  - `RegisterPage.tsx`
  - `RegisterForm.tsx`
  - `VerificationPage.tsx`

### Dashboard Sayfaları
- **Overview**
  - `DashboardPage.tsx`
  - `StatisticsCard.tsx`
  - `ActivityList.tsx`

- **Analytics**
  - `AnalyticsPage.tsx`
  - `ChartComponent.tsx`
  - `ReportList.tsx`

### Resident Sayfaları
- **List**
  - `ResidentListPage.tsx`
  - `ResidentFilter.tsx`
  - `ResidentTable.tsx`

- **Details**
  - `ResidentDetailsPage.tsx`
  - `ResidentInfo.tsx`
  - `ResidentHistory.tsx`

## 5. Servisler (Services)

### API Servisleri
- **Auth Service**
  - `login()`
  - `register()`
  - `logout()`
  - `refreshToken()`

- **Resident Service**
  - `getResidents()`
  - `getResidentById()`
  - `createResident()`
  - `updateResident()`
  - `deleteResident()`

- **Payment Service**
  - `getPayments()`
  - `getPaymentById()`
  - `createPayment()`
  - `updatePayment()`
  - `deletePayment()`

### Utility Servisleri
- **Notification Service**
  - `showSuccess()`
  - `showError()`
  - `showWarning()`
  - `showInfo()`

- **Storage Service**
  - `setItem()`
  - `getItem()`
  - `removeItem()`
  - `clear()`

## 6. Yardımcı Fonksiyonlar (Helpers)

### Format Fonksiyonları
- **Date Format**
  - `formatDate()`
  - `formatDateTime()`
  - `formatTime()`
  - `parseDate()`

- **Number Format**
  - `formatCurrency()`
  - `formatNumber()`
  - `formatPercentage()`
  - `parseNumber()`

### Validation Fonksiyonları
- **Input Validation**
  - `validateEmail()`
  - `validatePhone()`
  - `validatePassword()`
  - `validateRequired()`

- **Form Validation**
  - `validateForm()`
  - `getFormErrors()`
  - `isFormValid()`
  - `resetForm()`

### Utility Fonksiyonları
- **Array Helpers**
  - `groupBy()`
  - `sortBy()`
  - `filterBy()`
  - `mapTo()`

- **Object Helpers**
  - `deepClone()`
  - `mergeObjects()`
  - `pickProperties()`
  - `omitProperties()`

### Security Fonksiyonları
- **Auth Helpers**
  - `isAuthenticated()`
  - `hasPermission()`
  - `getUserRole()`
  - `getUserPermissions()`

- **Token Helpers**
  - `getToken()`
  - `setToken()`
  - `removeToken()`
  - `isTokenValid()` 