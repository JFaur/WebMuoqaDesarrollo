using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuoqaBD;
using MuoqaIdentidades;
using Serilog;
using System.Data;

namespace MuoqaBackend.ToBD
{
    public class UpdatePrice : CommonFunctionPer
    {
        private readonly MuoqaBDConf _conn;
        public UpdatePrice(MuoqaBDConf conn)
        {
            _conn = conn ?? throw new ArgumentNullException(nameof(conn));
        }

        public DataTable GetPrices()
        {
            try
            {
                List<ServicesPrices> list = _conn.ServicesPrices.ToList();
                DataTable data = ConvertToData(list);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Log.Error($"Error: {ex.Message}");
                return new DataTable();
            }
        }
    }
}
