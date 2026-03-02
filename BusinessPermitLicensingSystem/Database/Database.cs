using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Data;
using System.Data.SQLite;
using System.Runtime.InteropServices.Marshalling;
using static BusinessPermitLicensingSystem.Helpers.InputValidator;

namespace BusinessPermitLicensingSystem
{
    internal static class Database
    {
        private const string ConnectionString = "Data Source=database.db;Version=3;";

        public static void Initialize()
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            // USERS TABLE //
            const string createUsers = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT    NOT NULL,
                    Username TEXT    NOT NULL UNIQUE,
                    Password TEXT    NOT NULL, 
                    Created  TEXT    NOT NULL DEFAULT (datetime('now'))
                );";

            using var cmd = new SQLiteCommand(createUsers, con);
            cmd.ExecuteNonQuery();


            // PROFILING TABLE //
            const string createProfiling = @"
                CREATE TABLE IF NOT EXISTS Profiling (
                    SIN TEXT PRIMARY KEY,
                    FullName TEXT NOT NULL,
                    BusinessName TEXT NOT NULL,
                    BusinessSection TEXT NOT NULL,
                    StallNumber TEXT NOT NULL,
                    StallSize TEXT NOT NULL,
                    MonthlyRental REAL NOT NULL,
                    UNIQUE(FullName, BusinessName, StallNumber)
                );";

            using var cmd2 = new SQLiteCommand(createProfiling, con);
            cmd2.ExecuteNonQuery();


            // AUDIT TRAIL TABLE //
            const string createAuditTrail = @"
                CREATE TABLE IF NOT EXISTS AuditTrail (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Action TEXT NOT NULL,
                    SIN TEXT,
                    UserId INTEGER NOT NULL,
                    Timestamp TEXT NOT NULL DEFAULT (datetime('now')),
                    Details TEXT
                );";

            using var cmdAudit = new SQLiteCommand(createAuditTrail, con);
            cmdAudit.ExecuteNonQuery();
        }

        // PROFILE AUDIT TRAIL ONLY (exclude login/logout)
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

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        // USERS AUDIT TRAIL (LOGIN/LOGOUT) //
        public static DataTable GetUserAuditTrail()
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            const string query = @"
                SELECT 
                    a.Id,
                    a.Action,
                    u.Username AS UserName,
                    a.Timestamp,
                    a.Details
                FROM AuditTrail a
                LEFT JOIN Users u ON a.UserId = u.Id
                WHERE a.Action IN ('Login', 'Logout')
                ORDER BY a.Timestamp DESC";

            using var cmd = new SQLiteCommand(query, con);
            using var adapter = new SQLiteDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        public static string GetFullName(long userId)
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            using var cmd = new SQLiteCommand(
                "SELECT FullName FROM Users WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@id", userId);

            var result = cmd.ExecuteScalar();
            return result?.ToString() ?? "Unknown";
        }

        // ACCOUNT CREATION //
        public static (bool Success, string? ErrorMessage) CreateAccount(
            string fullname,
            string username,
            string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                return (false, "Full Name cannot be empty");
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Username cannot be empty");
            if (string.IsNullOrWhiteSpace(plainPassword))
                return (false, "Password cannot be empty");
            if (plainPassword.Length < 8)
                return (false, "Password must be at least 8 characters");

            username = username.Trim();

            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                using var cmdCheck = new SQLiteCommand(
                    "SELECT 1 FROM Users WHERE Username = @u", con);
                cmdCheck.Parameters.AddWithValue("@u", username);

                if (cmdCheck.ExecuteScalar() != null)
                    return (false, "Username already exists");

                string hashedPassword = HashPassword(plainPassword);

                const string insert = @"
                    INSERT INTO Users (FullName, Username, Password)
                    VALUES (@f, @u, @p)";

                using var cmd = new SQLiteCommand(insert, con);
                cmd.Parameters.AddWithValue("@f", fullname);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", hashedPassword);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
            {
                return (false, "Username already exists");
            }
            catch (Exception ex)
            {
                return (false, $"Database error: {ex.Message}");
            }
        }

        // VERIFY LOGIN //
        public static (bool IsValid, string? MessageOrUserId) VerifyLogin(
            string username,
            string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(plainPassword))
                return (false, "Missing username or password");

            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                const string query = @"
                    SELECT Id, Password
                    FROM Users
                    WHERE Username = @u";

                using var cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@u", username.Trim());

                using var reader = cmd.ExecuteReader();
                if (!reader.Read())
                    return (false, "Invalid username or password");

                string storedHash = reader.GetString(1);
                long userId = reader.GetInt64(0);

                if (VerifyPassword(plainPassword, storedHash))
                    return (true, userId.ToString());

                return (false, "Invalid username or password");
            }
            catch (Exception ex)
            {
                return (false, $"Login error: {ex.Message}");
            }
        }

        // ADD RECORD (SAVE) //
        public static (bool Success, string? ErrorMessage) AddProfiling(
            string sin,
            string fullName,
            string businessName,
            string businessSection,
            string stallNumber,
            string stallSize,
            double monthlyRental)
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
                        (SIN, FullName, BusinessName, BusinessSection, StallNumber, StallSize, MonthlyRental)
                    VALUES
                        (@sin, @f, @b, @s, @n, @sz, @r)";

                using var cmd = new SQLiteCommand(insert, con);
                cmd.Parameters.AddWithValue("@sin", sin);
                cmd.Parameters.AddWithValue("@f", fullName.Trim());
                cmd.Parameters.AddWithValue("@b", businessName.Trim());
                cmd.Parameters.AddWithValue("@s", businessSection.Trim());
                cmd.Parameters.AddWithValue("@n", stallNumber.Trim());
                cmd.Parameters.AddWithValue("@sz", stallSize.Trim());
                cmd.Parameters.AddWithValue("@r", monthlyRental);

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

        // EDIT RECORD //
        public static (bool Success, string? ErrorMessage) UpdateProfiling(
            string sin,
            string fullName,
            string businessName,
            string businessSection,
            string stallNumber,
            string stallSize,
            double monthlyRental)
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                const string query = @"
                    UPDATE Profiling
                    SET
                        FullName = @FullName,
                        BusinessName = @BusinessName,
                        BusinessSection = @BusinessSection,
                        StallNumber = @StallNumber,
                        StallSize = @StallSize,
                        MonthlyRental = @MonthlyRental
                    WHERE SIN = @SIN";

                using var cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@BusinessName", businessName);
                cmd.Parameters.AddWithValue("@BusinessSection", businessSection);
                cmd.Parameters.AddWithValue("@StallNumber", stallNumber);
                cmd.Parameters.AddWithValue("@StallSize", stallSize);
                cmd.Parameters.AddWithValue("@MonthlyRental", monthlyRental);
                cmd.Parameters.AddWithValue("@SIN", sin);

                cmd.ExecuteNonQuery();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // DELETE RECORD //
        public static (bool Success, string? ErrorMessage) DeleteProfiling(string sin)
        {
            try
            {
                using var con = new SQLiteConnection(ConnectionString);
                con.Open();

                const string query = "DELETE FROM Profiling WHERE SIN = @SIN";

                using var cmd = new SQLiteCommand(query, con);
                cmd.Parameters.AddWithValue("@SIN", sin);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                    return (false, "Record not found.");

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Database error: {ex.Message}");
            }
        }

        public static DataTable GetAllProfiles()
        {
            using var con = new SQLiteConnection(ConnectionString);
            con.Open();

            const string query = @"
                SELECT
                    SIN AS 'SIN',
                    FullName AS 'Full Name',
                    BusinessName AS 'Business Name',
                    BusinessSection AS 'Business Section',
                    StallNumber AS 'Stall Number',
                    StallSize AS 'Stall Size',
                    MonthlyRental AS 'Monthly Rental'
                FROM Profiling
                ORDER BY ROWID DESC";

            using var cmd = new SQLiteCommand(query, con);
            using var adapter = new SQLiteDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        // SIN GENERATION (UNIQUE) //
        public static string GenerateUniqueBIN(SQLiteConnection con)
        {
            string sin;
            var rnd = new Random();

            do
            {
                sin = $"SIN-{DateTime.Now:yyyy}-{rnd.Next(1000, 10000)}";

                using var cmdCheck = new SQLiteCommand(
                    "SELECT 1 FROM Profiling WHERE SIN = @sin", con);
                cmdCheck.Parameters.AddWithValue("@sin", sin);
                var exists = cmdCheck.ExecuteScalar() != null;

                if (!exists) break;

            } while (true);

            return sin;
        }

        // AUDIT LOGGING //
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

                const string insert = @"
                    INSERT INTO AuditTrail (Action, SIN, UserId, Details)
                    VALUES (@action, @sin, @user, @details)";

                using var cmd = new SQLiteCommand(insert, con);
                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@sin", sin ?? DBNull.Value.ToString());
                cmd.Parameters.AddWithValue("@user", userId);
                cmd.Parameters.AddWithValue("@details", details ?? "");
                cmd.ExecuteNonQuery();
            }
            catch
            {
                // Fail silently or handle error in enterprise systems
            }
        }

        // =============================================(PASSWORD HASHING) DO NOT TOUCH ================================================================= //

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hashOfEntered = HashPassword(enteredPassword);
            return CryptographicEquals(hashOfEntered, storedHash);
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