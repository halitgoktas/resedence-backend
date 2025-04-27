using System;
using System.Collections.Generic;

namespace ResidenceManagement.Core.Models.Services
{
    // Servis talebinde görev alan teknisyen bilgilerini tutan sınıf
    public class ServiceTechnician
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Bağlı olduğu servis talebi
        public int ServiceRequestId { get; set; }
        
        // Teknisyen bilgileri
        public int TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianPhone { get; set; }
        public string TechnicianEmail { get; set; }
        public string TechnicianTitle { get; set; }
        public string TechnicianDepartment { get; set; }
        public string TechnicianSpecialty { get; set; }
        
        // İş atama bilgileri
        public DateTime AssignmentDate { get; set; }
        public string AssignedBy { get; set; }
        public string AssignmentNotes { get; set; }
        
        // İş planlama bilgileri
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        
        // Çalışma bilgileri
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int? ActualDurationMinutes { get; set; }
        
        // Maliyet bilgileri
        public decimal HourlyRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal TotalCost { get; set; }
        public string CurrencyCode { get; set; }
        
        // İş durumu
        public TechnicianStatus Status { get; set; }
        
        // Çalışma günlüğü
        public List<WorkLogEntry> WorkLogs { get; set; }
        
        // Notlar
        public string Notes { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // Teknisyen durumu için enum
        public enum TechnicianStatus
        {
            Assigned = 1,      // Atandı
            Planned = 2,       // Planlandı
            InProgress = 3,    // Devam ediyor
            OnHold = 4,        // Beklemede
            Completed = 5,     // Tamamlandı
            Reassigned = 6,    // Yeniden atandı
            Cancelled = 7      // İptal edildi
        }
        
        // Çalışma günlüğü için iç sınıf
        public class WorkLogEntry
        {
            public int Id { get; set; }
            public DateTime LogDate { get; set; }
            public string ActionType { get; set; }  // StatusChange, Note, WorkStarted, WorkPaused, WorkResumed, WorkCompleted
            public string Description { get; set; }
            public string PerformedBy { get; set; }
            public int? DurationMinutes { get; set; }
            public string Location { get; set; }
            public string ImageUrl { get; set; }
        }
        
        // Yapıcı metot
        public ServiceTechnician()
        {
            AssignmentDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Status = TechnicianStatus.Assigned;
            CurrencyCode = "TRY";
            HourlyRate = 0;
            OvertimeRate = 0;
            TotalCost = 0;
            WorkLogs = new List<WorkLogEntry>();
        }
        
        // Durumu güncelle ve güncelleme kaydı ekle
        public void UpdateStatus(TechnicianStatus newStatus, string updatedBy, string notes = null)
        {
            var previousStatus = Status;
            Status = newStatus;
            UpdatedBy = updatedBy;
            UpdatedDate = DateTime.Now;
            
            // Çalışma günlüğüne durum değişikliğini ekle
            WorkLogs.Add(new WorkLogEntry
            {
                LogDate = DateTime.Now,
                ActionType = "StatusChange",
                Description = $"Durum değiştirildi: {previousStatus} -> {newStatus}" + (string.IsNullOrEmpty(notes) ? "" : $" ({notes})"),
                PerformedBy = updatedBy
            });
            
            // Notlar alanını güncelle
            if (!string.IsNullOrEmpty(notes))
            {
                if (string.IsNullOrEmpty(Notes))
                {
                    Notes = notes;
                }
                else
                {
                    Notes += Environment.NewLine + notes;
                }
            }
            
            // Duruma göre otomatik tarih güncellemeleri
            switch (newStatus)
            {
                case TechnicianStatus.InProgress:
                    if (!ActualStartDate.HasValue)
                    {
                        ActualStartDate = DateTime.Now;
                        AddWorkLog("WorkStarted", "Çalışma başlatıldı", updatedBy);
                    }
                    break;
                    
                case TechnicianStatus.Completed:
                    if (!ActualEndDate.HasValue)
                    {
                        ActualEndDate = DateTime.Now;
                        AddWorkLog("WorkCompleted", "Çalışma tamamlandı", updatedBy);
                        // Gerçek süreyi hesapla
                        CalculateActualDuration();
                        // Toplam maliyeti hesapla
                        CalculateTotalCost();
                    }
                    break;
            }
        }
        
        // Çalışma günlüğüne kayıt ekle
        public void AddWorkLog(string actionType, string description, string performedBy, int? durationMinutes = null, string location = null, string imageUrl = null)
        {
            WorkLogs.Add(new WorkLogEntry
            {
                LogDate = DateTime.Now,
                ActionType = actionType,
                Description = description,
                PerformedBy = performedBy,
                DurationMinutes = durationMinutes,
                Location = location,
                ImageUrl = imageUrl
            });
            
            UpdatedBy = performedBy;
            UpdatedDate = DateTime.Now;
        }
        
        // Çalışmayı başlat
        public void StartWork(string performedBy, string location = null, string notes = null)
        {
            if (Status != TechnicianStatus.InProgress)
            {
                UpdateStatus(TechnicianStatus.InProgress, performedBy, notes);
            }
            
            ActualStartDate = DateTime.Now;
            AddWorkLog("WorkStarted", "Çalışma başlatıldı", performedBy, null, location);
        }
        
        // Çalışmayı duraklat
        public void PauseWork(string performedBy, string reason, string location = null)
        {
            if (Status == TechnicianStatus.InProgress)
            {
                UpdateStatus(TechnicianStatus.OnHold, performedBy, reason);
                AddWorkLog("WorkPaused", $"Çalışma duraklatıldı: {reason}", performedBy, null, location);
            }
        }
        
        // Çalışmayı devam ettir
        public void ResumeWork(string performedBy, string location = null)
        {
            if (Status == TechnicianStatus.OnHold)
            {
                UpdateStatus(TechnicianStatus.InProgress, performedBy);
                AddWorkLog("WorkResumed", "Çalışma devam ettirildi", performedBy, null, location);
            }
        }
        
        // Çalışmayı tamamla
        public void CompleteWork(string performedBy, string notes = null, string location = null)
        {
            ActualEndDate = DateTime.Now;
            UpdateStatus(TechnicianStatus.Completed, performedBy, notes);
            AddWorkLog("WorkCompleted", "Çalışma tamamlandı", performedBy, null, location);
            
            // Gerçek süreyi hesapla
            CalculateActualDuration();
            
            // Toplam maliyeti hesapla
            CalculateTotalCost();
        }
        
        // Gerçek çalışma süresini hesapla
        private void CalculateActualDuration()
        {
            if (ActualStartDate.HasValue && ActualEndDate.HasValue)
            {
                TimeSpan duration = ActualEndDate.Value - ActualStartDate.Value;
                ActualDurationMinutes = (int)duration.TotalMinutes;
            }
        }
        
        // Toplam maliyeti hesapla
        public void CalculateTotalCost()
        {
            if (ActualDurationMinutes.HasValue)
            {
                decimal hours = (decimal)ActualDurationMinutes.Value / 60;
                decimal standardHours = Math.Min(hours, 8);  // Günlük 8 saat standart mesai
                decimal overtimeHours = Math.Max(0, hours - 8);  // 8 saatin üzeri fazla mesai
                
                decimal standardCost = standardHours * HourlyRate;
                decimal overtimeCost = overtimeHours * OvertimeRate;
                
                TotalCost = Math.Round(standardCost + overtimeCost, 2);
            }
            else
            {
                TotalCost = 0;
            }
        }
        
        // Saatlik ücret oranını güncelle ve maliyeti yeniden hesapla
        public void UpdateHourlyRate(decimal hourlyRate, decimal? overtimeRate = null)
        {
            HourlyRate = hourlyRate;
            
            if (overtimeRate.HasValue)
            {
                OvertimeRate = overtimeRate.Value;
            }
            else
            {
                OvertimeRate = hourlyRate * 1.5m;  // Varsayılan olarak normal ücretin 1.5 katı
            }
            
            CalculateTotalCost();
        }
    }
} 