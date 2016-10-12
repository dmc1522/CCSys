using Garibay;
public partial class Solicitudes
{
    public Solicitudes()
    {
        _experiencia = _plazo;
        _statusID = 0;
        _hectAseguradas =_valordegarantias =_monto = _superficieFinanciada =_superficieasembrar = 
            _recursosPropios = _costoTotalSeguro = _otrosPasivosMonto =  _ingNetosAnualOtrosCultivos = 
            _ingNetosAnualGanaderia = _ingNetosComercioServicios = 
            _otrosActivos = _totalActivos = _garantiaLiquida = _montoSoporteGarantia = 0.0;
	    _descParcelas = _descripciondegarantias  = _testigo1 = _testigo2 =_aval1 =_aval2 = _aval1Dom = 
            _aval2Dom = _ubicacionGarantia = _otrosPasivosAQuienLeDebe = _conceptoSoporteGarantia ="";
	    _storeTS = _updateTS =_fecha = Utils.Now;

        _casaHabitacion =_rastra = _arado = _cultivadora = _subsuelo = _tractor = _sembradora = _camioneta = 0;
	
	    _domicilioDelDeposito =_firmaAutorizada1 = _firmaAutorizada2 = _firmaAutorizada3 = _firmaAutorizada4 =
        _firmaAutorizada5 = _ejido = "";
    }
}

