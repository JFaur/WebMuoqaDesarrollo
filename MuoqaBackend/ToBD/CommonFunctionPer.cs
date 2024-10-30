using System.Data;
using System.Reflection;

namespace MuoqaBackend.ToBD
{
    public class CommonFunctionPer
    {
        protected DataTable ConvertToData<T>(List<T> data)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Obtener todas las propiedades del tipo T
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //Crear columnas en el DataTable basadas en las propiedades
            foreach (PropertyInfo property in properties)
            {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }
            //Añadir filas al DataTable
            foreach(T item in data) 
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyInfo property in properties) 
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
