using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RFinancieros_Facturas.Infrastructure.Audit.Models;

namespace RFinancieros_Facturas.Infrastructure.Audit.Repositories
{
    public class SqlAuditRepository : IAuditRepository
    {
        private readonly string _connectionString;

        public SqlAuditRepository()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["dbFacturasFinancieros"]
                .ConnectionString;
        }

        public long Save(AuditEntry entry)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        long auditId = InsertAuditHeader(
                            connection,
                            transaction,
                            entry);

                        InsertAuditDetails(
                            connection,
                            transaction,
                            auditId,
                            entry.Cambios);

                        transaction.Commit();

                        return auditId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public long InsertAudit(AuditEntry entry)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        long auditId = InsertAuditHeader(
                            connection,
                            transaction,
                            entry);

                        InsertAuditDetails(
                            connection,
                            transaction,
                            auditId,
                            entry.Cambios);

                        transaction.Commit();

                        return auditId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void InsertChanges(long auditId, IEnumerable<AuditChange> changes)
        {
            // La persistencia de los detalles ya se realiza
            // dentro de InsertAudit() utilizando una sola transacción.
            // Este método queda por compatibilidad con la interfaz.
        }

        #region Métodos privados

        private long InsertAuditHeader(
            SqlConnection connection,
            SqlTransaction transaction,
            AuditEntry entry)
        {
            using (SqlCommand cmd = new SqlCommand("ins_AuditLog", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fecha", entry.Fecha);
                cmd.Parameters.AddWithValue("@Operacion", entry.Operacion.ToString());
                cmd.Parameters.AddWithValue("@Tabla", entry.Tabla ?? string.Empty);
                cmd.Parameters.AddWithValue("@LlavePrimaria", entry.LlavePrimaria ?? string.Empty);
                cmd.Parameters.AddWithValue("@Usuario", entry.Usuario ?? string.Empty);
                cmd.Parameters.AddWithValue("@Host", (object)entry.Host ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Aplicacion", (object)entry.Aplicacion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StoredProcedure", (object)entry.StoredProcedure ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SessionId", (object)entry.SessionId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CantidadRegistros", entry.CantidadRegistros);
                cmd.Parameters.AddWithValue("@DuracionMs", (object)entry.DuracionMs ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Observaciones", (object)entry.Observaciones ?? DBNull.Value);

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        private void InsertAuditDetails(
            SqlConnection connection,
            SqlTransaction transaction,
            long auditId,
            IEnumerable<AuditChange> changes)
        {
            if (changes == null)
                return;

            foreach (AuditChange change in changes)
            {
                using (SqlCommand cmd = new SqlCommand("ins_AuditLogDetalle", connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AuditId", auditId);
                    cmd.Parameters.AddWithValue("@Campo", change.Column);
                    cmd.Parameters.AddWithValue("@ValorAnterior",
                        (object)change.OldValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ValorNuevo",
                        (object)change.NewValue ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}