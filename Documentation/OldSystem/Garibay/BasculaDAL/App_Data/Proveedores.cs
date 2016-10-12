using System.Data;
public partial class Proveedores
{
    public DataSet MapTo()
    {
        DataSet ds = new DataSet();
        this.MapTo(ds);
        return ds;
    }
}

