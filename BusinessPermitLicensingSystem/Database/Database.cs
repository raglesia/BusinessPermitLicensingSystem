using BusinessPermitLicensingSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem
{
    internal static class Database
    {
        // ===================== CONNECTION ===================== //
        private const string ConnectionString =
            "Server=localhost;Database=Masinloc_BPLS;User Id=sa;Password=Strongpassword1;TrustServerCertificate=True;";

        // ===================== INITIALIZE ===================== //
        public static void Initialize()
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            ExecuteNonQuery(con, @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
                CREATE TABLE Users (
                    Id       INT           IDENTITY(1,1) PRIMARY KEY,
                    FullName NVARCHAR(255) NOT NULL,
                    Username NVARCHAR(255) NOT NULL UNIQUE,
                    Position NVARCHAR(255) NOT NULL DEFAULT '',
                    Password NVARCHAR(512) NOT NULL,
                    Created  DATETIME      NOT NULL DEFAULT GETDATE()
                );");

            ExecuteNonQuery(con, @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Profiling' AND xtype='U')
                CREATE TABLE Profiling (
                    SIN              NVARCHAR(100) PRIMARY KEY,
                    FullName         NVARCHAR(255) NOT NULL,
                    BusinessName     NVARCHAR(255) NOT NULL,
                    BusinessSection  NVARCHAR(255) NOT NULL,
                    StallNumber      NVARCHAR(100) NOT NULL,
                    StallSize        NVARCHAR(100) NOT NULL,
                    MonthlyRental    FLOAT         NOT NULL,
                    PaymentStatus    NVARCHAR(50)  NOT NULL DEFAULT 'Unpaid',
                    StartDate        NVARCHAR(50)           DEFAULT '',
                    Penalty          FLOAT                  DEFAULT 0,
                    AdditionalCharge FLOAT                  DEFAULT 0,
                    IsArchived       INT                    DEFAULT 0,
                    DatePaid         NVARCHAR(50)           DEFAULT '',
                    UNIQUE(FullName, BusinessName, StallNumber)
                );");

            ExecuteNonQuery(con, @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AuditTrail' AND xtype='U')
                CREATE TABLE AuditTrail (
                    Id        INT           IDENTITY(1,1) PRIMARY KEY,
                    Action    NVARCHAR(255) NOT NULL,
                    SIN       NVARCHAR(100),
                    UserId    INT           NOT NULL,
                    Timestamp DATETIME      NOT NULL DEFAULT GETDATE(),
                    Details   NVARCHAR(MAX)
                );");

            ExecuteNonQuery(con, @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PaymentHistory' AND xtype='U')
                CREATE TABLE PaymentHistory (
                    Id         INT           IDENTITY(1,1) PRIMARY KEY,
                    SIN        NVARCHAR(100) NOT NULL,
                    ORNumber   NVARCHAR(100) NOT NULL UNIQUE,
                    AmountPaid FLOAT         NOT NULL,
                    Penalty    FLOAT         NOT NULL DEFAULT 0,
                    DatePaid   DATETIME      NOT NULL DEFAULT GETDATE(),
                    RecordedBy INT           NOT NULL,
                    FOREIGN KEY (SIN) REFERENCES Profiling(SIN)
                );");

            ExecuteNonQuery(con, @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RentalRates' AND xtype='U')
                CREATE TABLE RentalRates (
                    Section    NVARCHAR(255) PRIMARY KEY,
                    RatePerSqm FLOAT         NOT NULL DEFAULT 0,
                    FlatRate   FLOAT         NOT NULL DEFAULT 0,
                    RateType   NVARCHAR(50)  NOT NULL DEFAULT 'PerSqm'
                );");

            ExecuteNonQuery(con, @"
                IF NOT EXISTS (SELECT 1 FROM RentalRates)
                BEGIN
                    INSERT INTO RentalRates (Section, RatePerSqm, FlatRate, RateType) VALUES
                        ('Pharmacy (Below 100k)',    150,    0, 'PerSqm'),
                        ('Pharmacy (100k-250k)',     250,    0, 'PerSqm'),
                        ('Pharmacy (Above 250k)',    350,    0, 'PerSqm'),
                        ('Masinloc Mall Stalls',     150,    0, 'PerSqm'),
                        ('Masinloc Mall Food Court',   0, 1200, 'Flat'),
                        ('Corridor',                  0, 1200, 'Flat'),
                        ('Public Market Stalls',     210,    0, 'PerSqm'),
                        ('Carinderia',               210,    0, 'PerSqm'),
                        ('Fruits and Vegetable',     600,    0, 'PerSqm'),
                        ('Fish',                     600,    0, 'PerSqm'),
                        ('Meat',                     600,    0, 'PerSqm'),
                        ('Burger Area',                0, 1000, 'Flat'),
                        ('Kakanin Area',               0,  300, 'Flat'),
                        ('Pasalubong Center',           0, 5500, 'Flat')
                END");

            string[] migrations =
            {
                "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Profiling') AND name = 'StartDate') ALTER TABLE Profiling ADD StartDate NVARCHAR(50) DEFAULT ''",
                "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Profiling') AND name = 'Penalty') ALTER TABLE Profiling ADD Penalty FLOAT DEFAULT 0",
                "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Profiling') AND name = 'AdditionalCharge') ALTER TABLE Profiling ADD AdditionalCharge FLOAT DEFAULT 0",
                "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'Position') ALTER TABLE Users ADD Position NVARCHAR(255) NOT NULL DEFAULT ''",
                "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Profiling') AND name = 'IsArchived') ALTER TABLE Profiling ADD IsArchived INT DEFAULT 0",
                "IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Profiling') AND name = 'DatePaid') ALTER TABLE Profiling ADD DatePaid NVARCHAR(50) DEFAULT ''"
            };

            foreach (string sql in migrations)
            {
                try { ExecuteNonQuery(con, sql); }
                catch { } // Column already exists — ignore
            }
        }

        private static void ExecuteNonQuery(SqlConnection con, string sql)
        {
            using var cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        // ===================== USERS ===================== //
        public static string GetFullName(long userId)
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(
                "SELECT FullName FROM Users WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteScalar()?.ToString() ?? "Unknown";
        }

        public static string GetPosition(long userId)
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(
                "SELECT Position FROM Users WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteScalar()?.ToString() ?? "";
        }

        public static (bool Success, string? ErrorMessage) CreateAccount(
            string fullname,
            string username,
            string position,
            string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(fullname)) return (false, "Full Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(username)) return (false, "Username cannot be empty.");
            if (string.IsNullOrWhiteSpace(position)) return (false, "Position / Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(plainPassword)) return (false, "Password cannot be empty.");

            username = username.Trim();

            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmdCheck = new SqlCommand(
                    "SELECT 1 FROM Users WHERE Username = @u", con);
                cmdCheck.Parameters.AddWithValue("@u", username);

                if (cmdCheck.ExecuteScalar() != null)
                    return (false, "Username already exists.");

                using var cmd = new SqlCommand(@"
                    INSERT INTO Users (FullName, Username, Position, Password)
                    VALUES (@f, @u, @pos, @p)", con);

                cmd.Parameters.AddWithValue("@f", fullname);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@pos", position);
                cmd.Parameters.AddWithValue("@p", HashPassword(plainPassword));

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                return (false, "Username already exists.");
            }
            catch (Exception ex)
            {
                return (false, $"Database error: {ex.Message}");
            }
        }

        public static (bool IsValid, string? MessageOrUserId) VerifyLogin(
            string username,
            string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(plainPassword))
                return (false, "Missing username or password.");

            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(
                    "SELECT Id, Password FROM Users WHERE Username = @u", con);
                cmd.Parameters.AddWithValue("@u", username.Trim());

                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                    return (false, "Invalid username or password.");

                long userId = reader.GetInt32(0);
                string storedHash = reader.GetString(1);

                return VerifyPassword(plainPassword, storedHash)
                    ? (true, userId.ToString())
                    : (false, "Invalid username or password.");
            }
            catch (Exception ex)
            {
                return (false, $"Login error: {ex.Message}");
            }
        }

        // ===================== PROFILING ===================== //
        public static DataTable GetAllProfiles()
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(@"
                SELECT
                    SIN                                                 AS [SIN],
                    FullName                                            AS [Full Name],
                    BusinessName                                        AS [Business Name],
                    BusinessSection                                     AS [Business Section],
                    StallNumber                                         AS [Stall Number],
                    StallSize                                           AS [Stall Size],
                    MonthlyRental                                       AS [Monthly Rental],
                    PaymentStatus                                       AS [Payment Status],
                    Penalty                                             AS [Penalty],
                    AdditionalCharge                                    AS [Additional Charge],
                    CASE
                        WHEN ISDATE(StartDate) = 1
                        THEN FORMAT(CONVERT(DATE, StartDate), 'MM/dd/yyyy')
                        ELSE ''
                    END                                                 AS [Date of Occupancy]
                FROM Profiling
                WHERE IsArchived = 0
                ORDER BY SIN ASC", con);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        // ===================== MONTHLY REPORT ===================== //
        public static DataTable GetMonthlyReport(
            int fromMonth, int fromYear,
            int toMonth, int toYear)
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            const string query = @"
        SELECT
            SIN                  AS [SIN],
            FullName             AS [Full Name],
            BusinessName         AS [Business Name],
            BusinessSection      AS [Business Section],
            MonthlyRental        AS [Monthly Rental],
            Penalty              AS [Penalty],
            AdditionalCharge     AS [Additional Charge],
            PaymentStatus        AS [Payment Status]
        FROM Profiling
        WHERE IsArchived = 0
        AND (
            (YEAR (StartDate) * 12 + MONTH(StartDate))
            BETWEEN
            (@fromYear * 12 + @fromMonth)
            AND
            (@toYear * 12 + @toMonth)
        )
        ORDER BY PaymentStatus ASC, FullName ASC";

            using var cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fromMonth", fromMonth);
            cmd.Parameters.AddWithValue("@fromYear", fromYear);
            cmd.Parameters.AddWithValue("@toMonth", toMonth);
            cmd.Parameters.AddWithValue("@toYear", toYear);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static bool StallNumberExists(string stallNumber, string excludeSIN = "")
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    SELECT 1 FROM Profiling
                    WHERE StallNumber = @stall
                    AND   IsArchived  = 0
                    AND   SIN        != @excludeSIN", con);

                cmd.Parameters.AddWithValue("@stall", stallNumber.Trim());
                cmd.Parameters.AddWithValue("@excludeSIN", excludeSIN);

                return cmd.ExecuteScalar() != null;
            }
            catch { return false; }
        }

        public static (bool Success, string? ErrorMessage) AddProfiling(
            string sin,
            string fullName,
            string businessName,
            string businessSection,
            string stallNumber,
            string stallSize,
            double monthlyRental,
            string startDate,
            double additionalCharge = 0)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(businessName) ||
                string.IsNullOrWhiteSpace(businessSection) ||
                string.IsNullOrWhiteSpace(stallNumber) ||
                string.IsNullOrWhiteSpace(stallSize))
                return (false, "All fields are required.");

            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    INSERT INTO Profiling
                        (SIN, FullName, BusinessName, BusinessSection,
                         StallNumber, StallSize, MonthlyRental, StartDate, AdditionalCharge)
                    VALUES
                        (@sin, @f, @b, @s, @n, @sz, @r, @startDate, @additional)", con);

                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.Parameters.AddWithValue("@f", fullName.Trim());
                cmd.Parameters.AddWithValue("@b", businessName.Trim());
                cmd.Parameters.AddWithValue("@s", businessSection.Trim());
                cmd.Parameters.AddWithValue("@n", stallNumber.Trim());
                cmd.Parameters.AddWithValue("@sz", stallSize.Trim());
                cmd.Parameters.AddWithValue("@r", monthlyRental);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@additional", additionalCharge);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                return (false, "This profile already exists (duplicate).");
            }
            catch (Exception ex)
            {
                return (false, $"Database error: {ex.Message}");
            }
        }

        public static (bool Success, string? ErrorMessage) UpdateProfiling(
            string sin,
            string fullName,
            string businessName,
            string businessSection,
            string stallNumber,
            string stallSize,
            double monthlyRental,
            string startDate,
            double additionalCharge = 0)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    UPDATE Profiling
                    SET
                        FullName         = @FullName,
                        BusinessName     = @BusinessName,
                        BusinessSection  = @BusinessSection,
                        StallNumber      = @StallNumber,
                        StallSize        = @StallSize,
                        MonthlyRental    = @MonthlyRental,
                        StartDate        = @StartDate,
                        AdditionalCharge = @AdditionalCharge
                    WHERE SIN = @SIN", con);

                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@BusinessName", businessName);
                cmd.Parameters.AddWithValue("@BusinessSection", businessSection);
                cmd.Parameters.AddWithValue("@StallNumber", stallNumber);
                cmd.Parameters.AddWithValue("@StallSize", stallSize);
                cmd.Parameters.AddWithValue("@MonthlyRental", monthlyRental);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@AdditionalCharge", additionalCharge);
                cmd.Parameters.AddWithValue("@SIN", sin);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static (bool Success, string? ErrorMessage) DeleteProfiling(string sin)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(
                    "DELETE FROM Profiling WHERE SIN = @SIN", con);
                cmd.Parameters.AddWithValue("@SIN", sin);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected == 0
                    ? (false, "Record not found.")
                    : (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Database error: {ex.Message}");
            }
        }

        public static string GenerateUniqueBIN(SqlConnection con)
        {
            var rnd = new Random();
            string sin;

            do
            {
                sin = $"SIN-{DateTime.Now:yyyy}-{rnd.Next(1000, 10000)}";

                using var cmdCheck = new SqlCommand(
                    "SELECT 1 FROM Profiling WHERE SIN = @sin", con);
                cmdCheck.Parameters.AddWithValue("@sin", sin);

                if (cmdCheck.ExecuteScalar() == null) break;
            }
            while (true);

            return sin;
        }

        // ===================== RENTAL RATES ===================== //
        public static DataTable GetRentalRates()
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand("SELECT * FROM RentalRates ORDER BY Section", con);
            using var adapter = new SqlDataAdapter(cmd);

            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static (double RatePerSqm, double FlatRate, string RateType) GetRateBySection(
            string section)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    SELECT RatePerSqm, FlatRate, RateType
                    FROM   RentalRates
                    WHERE  Section = @section", con);
                cmd.Parameters.AddWithValue("@section", section);

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return (
                        Convert.ToDouble(reader["RatePerSqm"]),
                        Convert.ToDouble(reader["FlatRate"]),
                        reader["RateType"].ToString()!
                    );
                }

                return (0, 0, "PerSqm");
            }
            catch { return (0, 0, "PerSqm"); }
        }

        public static (bool Success, string? ErrorMessage) UpdateRentalRate(
            string section,
            double ratePerSqm,
            double flatRate,
            string rateType)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    UPDATE RentalRates
                    SET
                        RatePerSqm = @ratePerSqm,
                        FlatRate   = @flatRate,
                        RateType   = @rateType
                    WHERE Section  = @section", con);

                cmd.Parameters.AddWithValue("@ratePerSqm", ratePerSqm);
                cmd.Parameters.AddWithValue("@flatRate", flatRate);
                cmd.Parameters.AddWithValue("@rateType", rateType);
                cmd.Parameters.AddWithValue("@section", section);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static (bool Success, string? ErrorMessage) AddRentalRate(
            string section,
            double ratePerSqm,
            double flatRate,
            string rateType)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    INSERT INTO RentalRates (Section, RatePerSqm, FlatRate, RateType)
                    VALUES (@section, @ratePerSqm, @flatRate, @rateType)", con);

                cmd.Parameters.AddWithValue("@section", section.Trim());
                cmd.Parameters.AddWithValue("@ratePerSqm", ratePerSqm);
                cmd.Parameters.AddWithValue("@flatRate", flatRate);
                cmd.Parameters.AddWithValue("@rateType", rateType);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                return (false, "Section already exists.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // ===================== PAYMENT STATUS ===================== //
        public static (bool Success, string? ErrorMessage) UpdatePaymentStatus(
            string sin,
            string status,
            string orNumber = "",
            double amountPaid = 0,
            double penalty = 0,
            long recordedBy = 0)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    UPDATE Profiling
                    SET PaymentStatus = @status
                    WHERE SIN = @sin", con);

                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.ExecuteNonQuery();

                if (status == "Paid" && !string.IsNullOrWhiteSpace(orNumber))
                    RecordPayment(sin, orNumber, amountPaid, penalty, recordedBy);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Database error: {ex.Message}");
            }
        }

        // ===================== PAYMENT HISTORY ===================== //
        public static (bool Success, string? ErrorMessage) RecordPayment(
            string sin,
            string orNumber,
            double amountPaid,
            double penalty,
            long recordedBy)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    INSERT INTO PaymentHistory
                        (SIN, ORNumber, AmountPaid, Penalty, DatePaid, RecordedBy)
                    VALUES
                        (@sin, @or, @amount, @penalty, @date, @recordedBy)", con);

                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.Parameters.AddWithValue("@or", orNumber);
                cmd.Parameters.AddWithValue("@amount", amountPaid);
                cmd.Parameters.AddWithValue("@penalty", penalty);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@recordedBy", recordedBy);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Database error: {ex.Message}");
            }
        }

        public static DataTable GetPaymentHistory(string sin)
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(@"
                SELECT
                    ph.Id                                    AS [#],
                    ph.ORNumber                              AS [OR Number],
                    ph.AmountPaid                            AS [Amount Paid],
                    ph.Penalty                               AS [Penalty],
                    (ph.AmountPaid + ph.Penalty)             AS [Total Paid],
                    FORMAT(ph.DatePaid, 'MM/dd/yyyy HH:mm') AS [Date Paid],
                    u.FullName                               AS [Recorded By]
                FROM PaymentHistory ph
                LEFT JOIN Users u ON ph.RecordedBy = u.Id
                WHERE ph.SIN = @sin
                ORDER BY ph.DatePaid DESC", con);

            cmd.Parameters.AddWithValue("@sin", sin);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        // ===================== ARCHIVE ===================== //
        public static (bool Success, string? ErrorMessage) ArchiveProfiling(string sin)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    UPDATE Profiling
                    SET IsArchived = 1
                    WHERE SIN = @sin", con);

                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static (bool Success, string? ErrorMessage) RestoreProfiling(string sin)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    UPDATE Profiling
                    SET IsArchived = 0
                    WHERE SIN = @sin", con);

                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static DataTable GetArchivedProfiles()
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(@"
                SELECT
                    SIN                                                 AS [SIN],
                    FullName                                            AS [Full Name],
                    BusinessName                                        AS [Business Name],
                    BusinessSection                                     AS [Business Section],
                    StallNumber                                         AS [Stall Number],
                    StallSize                                           AS [Stall Size],
                    MonthlyRental                                       AS [Monthly Rental],
                    PaymentStatus                                       AS [Payment Status],
                    Penalty                                             AS [Penalty],
                    AdditionalCharge                                    AS [Additional Charge],
                    CASE
                        WHEN ISDATE(StartDate) = 1
                        THEN FORMAT(CONVERT(DATE, StartDate), 'MM/dd/yyyy')
                        ELSE ''
                    END                                                 AS [Date of Occupancy]
                FROM Profiling
                WHERE IsArchived = 1
                ORDER BY SIN DESC", con);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        // ===================== PENALTY ===================== //
        public static double CalculatePenalty(
            double monthlyRental,
            string paymentStatus,
            string startDate)
        {
            if (paymentStatus == "Paid") return 0;
            if (string.IsNullOrWhiteSpace(startDate)) return 0;
            if (!DateTime.TryParse(startDate, out DateTime start)) return 0;

            DateTime today = DateTime.Today;
            DateTime firstDueDate = new DateTime(
                start.Month == 12 ? start.Year + 1 : start.Year,
                start.Month == 12 ? 1 : start.Month + 1,
                20);

            if (today <= firstDueDate) return 0;

            int missedMonths = 0;
            DateTime dueDate = firstDueDate;

            while (dueDate < today)
            {
                if (today > dueDate) missedMonths++;

                dueDate = new DateTime(
                    dueDate.Month == 12 ? dueDate.Year + 1 : dueDate.Year,
                    dueDate.Month == 12 ? 1 : dueDate.Month + 1,
                    20);
            }

            if (missedMonths <= 0) return 0;

            return Math.Round(monthlyRental * 0.25 * missedMonths, 2);
        }

        public static void UpdatePenalty(string sin, double penalty)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    UPDATE Profiling
                    SET Penalty = @penalty
                    WHERE SIN = @sin", con);

                cmd.Parameters.AddWithValue("@penalty", penalty);
                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Penalty update error: {ex.Message}");
            }
        }

        public static (int Updated, int Skipped) ApplyPenaltiesToAll()
        {
            int updated = 0;
            int skipped = 0;

            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    SELECT SIN, MonthlyRental, PaymentStatus, StartDate
                    FROM   Profiling
                    WHERE  PaymentStatus != 'Paid'", con);

                using var reader = cmd.ExecuteReader();
                var records = new List<(string sin, double rental, string status, string startDate)>();

                while (reader.Read())
                {
                    records.Add((
                        reader["SIN"].ToString()!,
                        Convert.ToDouble(reader["MonthlyRental"]),
                        reader["PaymentStatus"].ToString()!,
                        reader["StartDate"].ToString()!
                    ));
                }

                reader.Close();

                foreach (var record in records)
                {
                    double penalty = CalculatePenalty(record.rental, record.status, record.startDate);

                    if (penalty > 0) { UpdatePenalty(record.sin, penalty); updated++; }
                    else skipped++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying penalties: {ex.Message}");
            }

            return (updated, skipped);
        }

        // ===================== AUDIT TRAIL ===================== //
        public static DataTable GetAuditTrail()
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(@"
                SELECT Id, Action, SIN, UserId, Timestamp, Details
                FROM   AuditTrail
                WHERE  Action NOT IN ('Login', 'Logout')
                ORDER BY Timestamp DESC", con);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static DataTable GetUserAuditTrail()
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(@"
                SELECT
                    a.Id,
                    a.Action,
                    u.Username AS [Username],
                    a.Timestamp,
                    a.Details
                FROM AuditTrail a
                LEFT JOIN Users u ON a.UserId = u.Id
                WHERE  a.Action IN ('Login', 'Logout')
                ORDER BY a.Timestamp DESC", con);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static void LogAudit(
            string action,
            string? sin,
            long userId,
            string? details = null)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(@"
                    INSERT INTO AuditTrail (Action, SIN, UserId, Timestamp, Details)
                    VALUES (@action, @sin, @user, @timestamp, @details)", con);

                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@sin", sin ?? "");
                cmd.Parameters.AddWithValue("@user", userId);
                cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@details", details ?? "");

                cmd.ExecuteNonQuery();
            }
            catch { } // Fail silently
        }

        // ===================== BILLING REPORT ===================== //
        public static List<BillingReportModel> GetProfiles()
        {
            var list = new List<BillingReportModel>();

            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand("SELECT * FROM Profiling", con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new BillingReportModel
                {
                    SIN = reader["SIN"].ToString()!,
                    FullName = reader["FullName"].ToString()!,
                    BusinessName = reader["BusinessName"].ToString()!,
                    BusinessSection = reader["BusinessSection"].ToString()!,
                    StallNumber = reader["StallNumber"].ToString()!,
                    StallSize = reader["StallSize"].ToString()!,
                    MonthlyRental = Convert.ToDouble(reader["MonthlyRental"])
                });
            }

            return list;
        }

        // ===================== OR NUMBER ===================== //
        public static bool ORNumberExists(string orNumber)
        {
            try
            {
                using var con = new SqlConnection(ConnectionString);
                con.Open();

                using var cmd = new SqlCommand(
                    "SELECT 1 FROM PaymentHistory WHERE ORNumber = @or", con);
                cmd.Parameters.AddWithValue("@or", orNumber.Trim());

                return cmd.ExecuteScalar() != null;
            }
            catch { return false; }
        }

        // ===================== MONTHLY REPORT ===================== //
        public static DataTable GetMonthlyReport(int month, int year)
        {
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            using var cmd = new SqlCommand(@"
                SELECT
                    SIN              AS [SIN],
                    FullName         AS [Full Name],
                    BusinessName     AS [Business Name],
                    BusinessSection  AS [Business Section],
                    MonthlyRental    AS [Monthly Rental],
                    Penalty          AS [Penalty],
                    AdditionalCharge AS [Additional Charge],
                    PaymentStatus    AS [Payment Status]
                FROM Profiling
                WHERE IsArchived        = 0
                AND   ISDATE(StartDate) = 1
                AND   MONTH(CONVERT(DATE, StartDate)) = @month
                AND   YEAR(CONVERT(DATE, StartDate))  = @year
                ORDER BY PaymentStatus ASC, FullName ASC", con);

            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@year", year);

            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        // ===================== PASSWORD HASHING — DO NOT MODIFY ===================== //
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string enteredPassword, string storedHash)
            => CryptographicEquals(HashPassword(enteredPassword), storedHash);

        private static bool CryptographicEquals(string a, string b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}