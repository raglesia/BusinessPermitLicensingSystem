using BusinessPermitLicensingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BusinessPermitLicensingSystem
{
    internal static class Database
    {
        // ===================== CONNECTION ===================== //
        private const string ConnectionString = "Data Source=database.db;Version=3;";

        // ===================== INITIALIZE ===================== //
        public static void Initialize()
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            // Users table
            ExecuteNonQuery(con, @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT    NOT NULL,
                    Username TEXT    NOT NULL UNIQUE,
                    Position TEXT    NOT NULL DEFAULT '',
                    Password TEXT    NOT NULL,
                    Created  TEXT    NOT NULL DEFAULT (datetime('now'))
                );");

            // Profiling table
            ExecuteNonQuery(con, @"
                CREATE TABLE IF NOT EXISTS Profiling (
                    SIN             TEXT PRIMARY KEY,
                    FullName        TEXT NOT NULL,
                    BusinessName    TEXT NOT NULL,
                    BusinessSection TEXT NOT NULL,
                    StallNumber     TEXT NOT NULL,
                    StallSize       TEXT NOT NULL,
                    MonthlyRental   REAL NOT NULL,
                    PaymentStatus   TEXT NOT NULL DEFAULT 'Unpaid',
                    StartDate       TEXT          DEFAULT '',
                    Penalty         REAL          DEFAULT 0,
                    UNIQUE(FullName, BusinessName, StallNumber)
                );");

            // Audit trail table
            ExecuteNonQuery(con, @"
                CREATE TABLE IF NOT EXISTS AuditTrail (
                    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                    Action    TEXT    NOT NULL,
                    SIN       TEXT,
                    UserId    INTEGER NOT NULL,
                    Timestamp TEXT    NOT NULL DEFAULT (datetime('now')),
                    Details   TEXT
                );");

            // Payment history table
            ExecuteNonQuery(con, @"
                CREATE TABLE IF NOT EXISTS PaymentHistory (
                    Id         INTEGER PRIMARY KEY AUTOINCREMENT,
                    SIN        TEXT    NOT NULL,
                    ORNumber   TEXT    NOT NULL,
                    AmountPaid REAL    NOT NULL,
                    Penalty    REAL    NOT NULL DEFAULT 0,
                    DatePaid   TEXT    NOT NULL DEFAULT (datetime('now')),
                    RecordedBy INTEGER NOT NULL,
                    FOREIGN KEY (SIN) REFERENCES Profiling(SIN)
                );");

            // Settings table
            ExecuteNonQuery(con, @"
                CREATE TABLE IF NOT EXISTS Settings (
                    Key   TEXT PRIMARY KEY,
                    Value TEXT NOT NULL
                );");

            // Migrate existing databases — add columns if missing
            var alterCommands = new[]
            {
                "ALTER TABLE Profiling ADD COLUMN StartDate TEXT DEFAULT ''",
                "ALTER TABLE Profiling ADD COLUMN Penalty REAL DEFAULT 0",
                "ALTER TABLE Users ADD COLUMN Position TEXT NOT NULL DEFAULT ''",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_unique_ornumber ON PaymentHistory(ORNumber)"
            };

            foreach (var sql in alterCommands)
            {
                try
                {
                    using var alterCmd = new SQLiteCommand(sql, con);
                    alterCmd.ExecuteNonQuery();
                }
                catch { } // Column already exists — ignore
            }
        }

        // Helper to reduce repetition in Initialize()
        private static void ExecuteNonQuery(SQLiteConnection con, string sql)
        {
            using var cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        // ===================== USERS ===================== //
        public static string GetFullName(long userId)
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            using var cmd = new SQLiteCommand(
                "SELECT FullName FROM Users WHERE Id = @id", con);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteScalar()?.ToString() ?? "Unknown";
        }

        public static string GetPosition(long userId)
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            using var cmd = new SQLiteCommand(
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
            if (string.IsNullOrWhiteSpace(fullname))
                return (false, "Full Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Username cannot be empty.");
            if (string.IsNullOrWhiteSpace(position))
                return (false, "Position / Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(plainPassword))
                return (false, "Password cannot be empty.");

            username = username.Trim();

            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmdCheck = new SQLiteCommand(
                    "SELECT 1 FROM Users WHERE Username = @u", con);
                cmdCheck.Parameters.AddWithValue("@u", username);

                if (cmdCheck.ExecuteScalar() != null)
                    return (false, "Username already exists.");

                const string insert = @"
                    INSERT INTO Users (FullName, Username, Position, Password)
                    VALUES (@f, @u, @pos, @p)";

                using var cmd = new SQLiteCommand(insert, con);
                cmd.Parameters.AddWithValue("@f", fullname);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@pos", position);
                cmd.Parameters.AddWithValue("@p", HashPassword(plainPassword));

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
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
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(
                    "SELECT Id, Password FROM Users WHERE Username = @u", con);
                cmd.Parameters.AddWithValue("@u", username.Trim());

                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                    return (false, "Invalid username or password.");

                long userId = reader.GetInt64(0);
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
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            const string query = @"
                SELECT
                    SIN                                AS [SIN],
                    FullName                           AS [Full Name],
                    BusinessName                       AS [Business Name],
                    BusinessSection                    AS [Business Section],
                    StallNumber                        AS [Stall Number],
                    StallSize                          AS [Stall Size],
                    MonthlyRental                      AS [Monthly Rental],
                    PaymentStatus                      AS [Payment Status],
                    Penalty                            AS [Penalty],
                    strftime('%m/%d/%Y', StartDate)    AS [Date of Occupancy]
                FROM Profiling
                ORDER BY ROWID DESC";

            using var cmd = new SQLiteCommand(query, con);
            using var adapter = new SQLiteDataAdapter(cmd);

            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static (bool Success, string? ErrorMessage) AddProfiling(
            string sin,
            string fullName,
            string businessName,
            string businessSection,
            string stallNumber,
            string stallSize,
            double monthlyRental,
            string startDate)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(businessName) ||
                string.IsNullOrWhiteSpace(businessSection) ||
                string.IsNullOrWhiteSpace(stallNumber) ||
                string.IsNullOrWhiteSpace(stallSize))
                return (false, "All fields are required.");

            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                const string insert = @"
                    INSERT INTO Profiling
                        (SIN, FullName, BusinessName, BusinessSection,
                         StallNumber, StallSize, MonthlyRental, StartDate)
                    VALUES
                        (@sin, @f, @b, @s, @n, @sz, @r, @startDate)";

                using var cmd = new SQLiteCommand(insert, con);
                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.Parameters.AddWithValue("@f", fullName.Trim());
                cmd.Parameters.AddWithValue("@b", businessName.Trim());
                cmd.Parameters.AddWithValue("@s", businessSection.Trim());
                cmd.Parameters.AddWithValue("@n", stallNumber.Trim());
                cmd.Parameters.AddWithValue("@sz", stallSize.Trim());
                cmd.Parameters.AddWithValue("@r", monthlyRental);
                cmd.Parameters.AddWithValue("@startDate", startDate);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
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
            string startDate)
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                const string query = @"
                    UPDATE Profiling
                    SET
                        FullName        = @FullName,
                        BusinessName    = @BusinessName,
                        BusinessSection = @BusinessSection,
                        StallNumber     = @StallNumber,
                        StallSize       = @StallSize,
                        MonthlyRental   = @MonthlyRental,
                        StartDate       = @StartDate
                    WHERE SIN = @SIN";

                using var cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@BusinessName", businessName);
                cmd.Parameters.AddWithValue("@BusinessSection", businessSection);
                cmd.Parameters.AddWithValue("@StallNumber", stallNumber);
                cmd.Parameters.AddWithValue("@StallSize", stallSize);
                cmd.Parameters.AddWithValue("@MonthlyRental", monthlyRental);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
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
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(
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

        public static string GenerateUniqueBIN(SQLiteConnection con)
        {
            var rnd = new Random();
            string sin;

            do
            {
                sin = $"SIN-{DateTime.Now:yyyy}-{rnd.Next(1000, 10000)}";

                using var cmdCheck = new SQLiteCommand(
                    "SELECT 1 FROM Profiling WHERE SIN = @sin", con);
                cmdCheck.Parameters.AddWithValue("@sin", sin);

                if (cmdCheck.ExecuteScalar() == null) break;

            } while (true);

            return sin;
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
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(@"
                    UPDATE Profiling
                    SET PaymentStatus = @status
                    WHERE SIN = @sin", con);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.ExecuteNonQuery();

                // Auto-record payment when marked as Paid
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
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                const string insert = @"
                    INSERT INTO PaymentHistory
                        (SIN, ORNumber, AmountPaid, Penalty, DatePaid, RecordedBy)
                    VALUES
                        (@sin, @or, @amount, @penalty, @date, @recordedBy)";

                using var cmd = new SQLiteCommand(insert, con);
                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.Parameters.AddWithValue("@or", orNumber);
                cmd.Parameters.AddWithValue("@amount", amountPaid);
                cmd.Parameters.AddWithValue("@penalty", penalty);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            const string query = @"
                SELECT
                    ph.Id                                       AS [#],
                    ph.ORNumber                                 AS [OR Number],
                    ph.AmountPaid                               AS [Amount Paid],
                    ph.Penalty                                  AS [Penalty],
                    (ph.AmountPaid + ph.Penalty)                AS [Total Paid],
                    strftime('%m/%d/%Y %H:%M', ph.DatePaid)     AS [Date Paid],
                    u.FullName                                  AS [Recorded By]
                FROM PaymentHistory ph
                LEFT JOIN Users u ON ph.RecordedBy = u.Id
                WHERE ph.SIN = @sin
                ORDER BY ph.DatePaid DESC";

            using var cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@sin", sin);
            using var adapter = new SQLiteDataAdapter(cmd);

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

            // First due date is the 20th of month AFTER start date
            DateTime firstDueDate = new DateTime(
                start.Month == 12 ? start.Year + 1 : start.Year,
                start.Month == 12 ? 1 : start.Month + 1,
                20);

            // No penalty if first due date hasn't passed yet
            if (today <= firstDueDate) return 0;

            // ✅ Count how many due dates have passed and are unpaid
            int missedMonths = 0;
            DateTime dueDate = firstDueDate;

            while (dueDate < today)
            {
                // Only count if the due date has fully passed
                if (today > dueDate)
                    missedMonths++;

                // Move to next month's due date
                dueDate = new DateTime(
                    dueDate.Month == 12 ? dueDate.Year + 1 : dueDate.Year,
                    dueDate.Month == 12 ? 1 : dueDate.Month + 1,
                    20);
            }

            if (missedMonths <= 0) return 0;

            // ✅ Penalty = 25% × monthly rental × number of missed months
            double penalty = monthlyRental * 0.25 * missedMonths;
            return Math.Round(penalty, 2);
        }

        public static void UpdatePenalty(string sin, double penalty)
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(@"
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
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(@"
                    SELECT SIN, MonthlyRental, PaymentStatus, StartDate
                    FROM Profiling
                    WHERE PaymentStatus != 'Paid'", con);

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
                    double penalty = CalculatePenalty(
                        record.rental, record.status, record.startDate);

                    if (penalty > 0)
                    {
                        UpdatePenalty(record.sin, penalty);
                        updated++;
                    }
                    else
                        skipped++;
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
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            const string query = @"
                SELECT
                    Id,
                    Action,
                    SIN,
                    UserId,
                    Timestamp,
                    Details
                FROM AuditTrail
                WHERE Action NOT IN ('Login', 'Logout')
                ORDER BY Timestamp DESC";

            using var cmd = new SQLiteCommand(query, con);
            using var adapter = new SQLiteDataAdapter(cmd);

            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static DataTable GetUserAuditTrail()
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            const string query = @"
                SELECT
                    a.Id,
                    a.Action,
                    u.Username  AS [Username],
                    a.Timestamp,
                    a.Details
                FROM AuditTrail a
                LEFT JOIN Users u ON a.UserId = u.Id
                WHERE a.Action IN ('Login', 'Logout')
                ORDER BY a.Timestamp DESC";

            using var cmd = new SQLiteCommand(query, con);
            using var adapter = new SQLiteDataAdapter(cmd);

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
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(@"
                    INSERT INTO AuditTrail (Action, SIN, UserId, Details)
                    VALUES (@action, @sin, @user, @details)", con);

                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@sin", sin ?? "");
                cmd.Parameters.AddWithValue("@user", userId);
                cmd.Parameters.AddWithValue("@details", details ?? "");

                cmd.ExecuteNonQuery();
            }
            catch { } // Fail silently
        }

        // ===================== SETTINGS ===================== //
        public static string GetSetting(string key)
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(
                    "SELECT Value FROM Settings WHERE Key = @key", con);
                cmd.Parameters.AddWithValue("@key", key);

                return cmd.ExecuteScalar()?.ToString() ?? "";
            }
            catch { return ""; }
        }

        public static void SaveSetting(string key, string value)
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(@"
                    INSERT INTO Settings (Key, Value)
                    VALUES (@key, @value)
                    ON CONFLICT(Key) DO UPDATE SET Value = @value", con);

                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@value", value);

                cmd.ExecuteNonQuery();
            }
            catch { }
        }

        public static (int Reset, string? ErrorMessage) ResetMonthlyPaymentStatus()
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(@"
                    UPDATE Profiling
                    SET
                        PaymentStatus = 'Unpaid',
                        Penalty       = 0
                    WHERE PaymentStatus = 'Paid'", con);

                int rowsAffected = cmd.ExecuteNonQuery();
                return (rowsAffected, null);
            }
            catch (Exception ex)
            {
                return (0, $"Database error: {ex.Message}");
            }
        }

        // ===================== BILLING REPORT ===================== //
        public static List<BillingReportModel> GetProfiles()
        {
            var list = new List<BillingReportModel>();

            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            using var cmd = new SQLiteCommand("SELECT * FROM Profiling", con);
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

        // CHECK IF OR NUMBER ALREADY EXISTS //
        public static bool ORNumberExists(string orNumber)
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmd = new SQLiteCommand(
                    "SELECT 1 FROM PaymentHistory WHERE ORNumber = @or", con);
                cmd.Parameters.AddWithValue("@or", orNumber.Trim());

                return cmd.ExecuteScalar() != null;
            }
            catch { return false; }
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
        {
            return CryptographicEquals(HashPassword(enteredPassword), storedHash);
        }

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