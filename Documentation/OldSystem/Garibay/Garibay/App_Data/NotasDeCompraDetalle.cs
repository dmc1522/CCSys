using System.Data;
using System;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
public partial class NotasDeCompraDetalle
{
    private string _Producto = string.Empty;
    private string _Bodega = string.Empty;
    public string Bodega
    {
        get { return _Bodega; }
        set { _Bodega = value; }
    }
    public string Producto
    {
        get { return _Producto; }
        set { _Producto = value; }
    }

    public void MapFrom(DataRow dr)
    {
        NDCdetalleID = dr["NDCdetalleID"] != DBNull.Value ? Convert.ToInt32(dr["NDCdetalleID"]) : NDCdetalleID = null;
        NotadecompraID = dr["notadecompraID"] != DBNull.Value ? Convert.ToInt32(dr["notadecompraID"]) : NotadecompraID = null;
        ProductoID = dr["productoID"] != DBNull.Value ? Convert.ToInt32(dr["productoID"]) : ProductoID = null;
        BodegaID = dr["bodegaID"] != DBNull.Value ? Convert.ToInt32(dr["bodegaID"]) : BodegaID = null;
        Cantidad = dr["cantidad"] != DBNull.Value ? Convert.ToDouble(dr["cantidad"]) : Cantidad = null;
        Preciodecompra = dr["preciodecompra"] != DBNull.Value ? Convert.ToDecimal(dr["preciodecompra"]) : Preciodecompra = null;
        Sacos = dr["sacos"] != DBNull.Value ? Convert.ToDouble(dr["sacos"]) : Sacos = null;
        _Producto = dr["Producto"] != DBNull.Value ? dr["Producto"].ToString() : string.Empty;
        _Bodega = dr["bodega"] != DBNull.Value ? dr["bodega"].ToString() : string.Empty;
    }

    [DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
    public static NotasDeCompraDetalle[] Search(System.Int32? notadecompraID)
    {
        DataSet ds;
        Database db;
        string sqlCommand;
        DbCommand dbCommand;


        db = DatabaseFactory.CreateDatabase();
        sqlCommand = "[dbo].gspNotasDeCompraDetalle_SEARCH";
        dbCommand = db.GetStoredProcCommand(sqlCommand, null,
            notadecompraID, null, null, null, null, null, null);

        ds = db.ExecuteDataSet(dbCommand);
        ds.Tables[0].TableName = TABLE_NAME;
        return NotasDeCompraDetalle.MapFrom(ds);
    }
}

