using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

public static class ExceptionFormatting
{
    /// <summary>
    /// Mensaje útil para el usuario: recorre AggregateException y DbUpdateException hasta el error de servidor (p. ej. MySQL).
    /// </summary>
    public static string GetDiagnosticMessage(Exception ex)
    {
        if (ex is AggregateException agg)
        {
            var flat = agg.Flatten().InnerExceptions.ToList();
            if (flat.Count == 0)
                return agg.Message;
            if (flat.Count == 1)
                return GetDiagnosticMessage(flat[0]);
            return string.Join("; ", flat.Select(GetDiagnosticMessage));
        }

        if (ex is DbUpdateException db && db.InnerException != null)
            return GetDiagnosticMessage(db.InnerException);

        var deepest = ex;
        while (deepest.InnerException != null)
            deepest = deepest.InnerException;
        return deepest.Message;
    }
}
