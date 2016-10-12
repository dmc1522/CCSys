function toogleShowHideDiv(id) {
    var elem, vis;
    if (document.getElementById) // this is the way the standards work
        elem = document.getElementById(id);
    else if (document.all) // this is the way old msie versions work
        elem = document.all[id];
    else if (document.layers) // this is the way nn4 works
        elem = document.layers[id];
    vis = elem.style;
    var bVisible = !(vis.display == "block" || vis.visibility == "visible");
    setVisibility(id, bVisible);
}

function IsCheckBoxChecked(CheckBoxID) {
    var elem;
    if (document.getElementById) // this is the way the standards work
        elem = document.getElementById(CheckBoxID);
    else if (document.all) // this is the way old msie versions work
        elem = document.all[idCheckBoxID];
    return elem.checked;
}

function ShowHideDivOnChkBox(CheckBoxID, layerID) {
    setVisibility(layerID, IsCheckBoxChecked(CheckBoxID));
}
function ShowHideDivOnChkBox2(CheckBoxID, layerID, layerID2) {

    setVisibility2(layerID, layerID2,IsCheckBoxChecked(CheckBoxID));
}

function IsDivVisible(id) {
    var elem, vis;
    if (document.getElementById) // this is the way the standards work
        elem = document.getElementById(id);
    else if (document.all) // this is the way old msie versions work
        elem = document.all[id];
    else if (document.layers) // this is the way nn4 works
        elem = document.layers[id];
    vis = elem.style;
    var bVisible = !(vis.display == "block" || vis.visibility == "visible");
    return bVisible;
}

function setVisibility(id, showDiv) {
var elem, vis;
if (document.getElementById) // this is the way the standards work
    elem = document.getElementById(id);
else if (document.all) // this is the way old msie versions work
    elem = document.all[id];
else if (document.layers) // this is the way nn4 works
    elem = document.layers[id];
vis = elem.style;
    if (showDiv == true) {
        vis.display = "block";
        vis.visibility = "visible";
    }
    else {
        vis.display = "none";
        vis.visibility = "hidden";
    }
}

function setVisibility2(id,id2, showDiv) {
    var elem, vis, vis2;
    if (document.getElementById) // this is the way the standards work
        elem = document.getElementById(id);
    else if (document.all) // this is the way old msie versions work
        elem = document.all[id];
    else if (document.layers) // this is the way nn4 works
        elem = document.layers[id];
    vis = elem.style;

    if (document.getElementById) // this is the way the standards work
        elem = document.getElementById(id2);
    else if (document.all) // this is the way old msie versions work
        elem = document.all[id2];
    else if (document.layers) // this is the way nn4 works
        elem = document.layers[id2];
    vis2 = elem.style;
    
    if (showDiv == true) {
        vis.display = "block";
        vis.visibility = "visible";
        vis2.display = "none";
        vis2.visibility = "hidden";
    }
    else {
        vis.display = "none";
        vis.visibility = "hidden";
        vis2.display = "block";
        vis2.visibility = "visible";
    }
}


function checkValueInList(listid, valueToCheck, divID) {
    var myListBox = listid;  //document.GetElementById(listid);
    var selVal = myListBox.options[myListBox.selectedIndex].text;
    if (selVal == valueToCheck) 
    {
        setVisibility(divID, true);
    } else {
        setVisibility(divID, false);
    }       
}
function checkValueInListNotasVentas(listid, valueToCheck, divID,divID2) {
    var myListBox = listid;  //document.GetElementById(listid);
    var selVal = myListBox.options[myListBox.selectedIndex].text;
    if (selVal == valueToCheck) 
    {
        setVisibility(divID, true);
        setVisibility(divID2, false);
    } else {
        setVisibility(divID, false);
        setVisibility(divID2, true);
    }       
}


function checkValueInListNotasVentas2(listid, divIDDiesel, divIDCaja,divIDBoletas,divIDBancos, divIDCheque) {
    var myListBox = listid;  //document.GetElementById(listid);
    var selVal = myListBox.options[myListBox.selectedIndex].text;





    if (selVal == 'TARJETA DIESEL') {
        setVisibility(divIDDiesel, true);
        setVisibility(divIDCaja, false);
        setVisibility(divIDBoletas, false);
        setVisibility(divIDBancos, false);
        setVisibility(divIDCheque, false);
        //setVisibility(divID5, false);
    } else if(selVal == 'EFECTIVO'){
        setVisibility(divIDDiesel, false);
        setVisibility(divIDCaja, true);
        setVisibility(divIDBoletas, false);
        setVisibility(divIDBancos, false);
        setVisibility(divIDCheque, false);
    }else if(selVal == 'BOLETA'){
        setVisibility(divIDDiesel, false);
        setVisibility(divIDCaja, false);
        setVisibility(divIDBoletas, true);
        setVisibility(divIDBancos, false);
        setVisibility(divIDCheque, false);
    } else if (selVal == 'TRANSFERENCIA' || selVal == 'DEPOSITO') {
        setVisibility(divIDDiesel, false);
        setVisibility(divIDCaja, false);
        setVisibility(divIDBoletas, false);
        setVisibility(divIDBancos, true);
        setVisibility(divIDCheque, false);
    }
    else if (selVal == 'CHEQUE') {
        setVisibility(divIDDiesel, false);
        setVisibility(divIDCaja, false);
        setVisibility(divIDBoletas, false);
        setVisibility(divIDBancos, false);
        setVisibility(divIDCheque, true);
    }
    
    
}


function getCtrlByID(ID) {
    var elem;
    if (document.getElementById) // this is the way the standards work
        elem = document.getElementById(ID);
    else if (document.all) // this is the way old msie versions work
        elem = document.all[ID];
    return elem;
}

function sumTextBoxes(txtop1, txtop2, resultTxt) {
    var op1 = getCtrlByID(txtop1);
    var op2 = getCtrlByID(txtop2);
    var res = getCtrlByID(resultTxt);

    res.value = parseFloat(op1.value) + parseFloat(op2.value);
}

function getFloat(valor) {
    var valor2 = valor.replace(",", "");
    var valor3 = valor.replace("$", "");
    if (!parseFloat(valor3)) {
        return 0;
    }
    else {
        return parseFloat(valor);
    }
}

function subTextBoxes(txtop1, txtop2, resultTxt) {
    var op1 = getCtrlByID(txtop1);
    var op2 = getCtrlByID(txtop2);
    var res = getCtrlByID(resultTxt);

    res.value = getFloat(op1.value) - getFloat(op2.value);
    if (res.value == "NaN") {
        res.value = 0;
    }
}

function mulTextBoxes(txtop1, txtop2, resultTxt) {
    var op1 = getCtrlByID(txtop1);
    var op2 = getCtrlByID(txtop2);
    var res = getCtrlByID(resultTxt);

    res.value = getFloat(op1.value) * getFloat(op2.value);
    if (res.value == "NaN") {
        res.value = 0;
    }
}

function mulTextBoxesNotNeg(txtop1, txtop2, resultTxt) {
    var op1 = getCtrlByID(txtop1);
    var op2 = getCtrlByID(txtop2);
    var res = getCtrlByID(resultTxt);

    res.value = getFloat(op1.value) * getFloat(op2.value);
    if (res.value == "NaN") {
        res.value = 0;
    }
    else {
        if (res.value < 0) {
            res.value = 0;
        }
    }
    var fRes = 0.0;
    fRes = getFloat(op1.value) * getFloat(op2.value);
    res.value = fRes.toFixed(2);
}
function RestaYDivide(txt1, txt2,txt3,txtResult) {
    var op1 = getCtrlByID(txt1);
    var op2 = getCtrlByID(txt2);
    var op3 = getCtrlByID(txt3);
    var res = getCtrlByID(txtResult)

    var fRes = 0.0;

    fRes = (getFloat(op1.value) - getFloat(op2.value)) / op3.value;
    if (fRes =="NaN") {
        fRes = "0.000"
       
   }
       res.value = fRes.toFixed(3);
    
    
   
}

function CopyTextBoxValue(txt1, txt2) {
    var op1 = getCtrlByID(txt1);
    var op2 = getCtrlByID(txt2);
    op2.value = op1.value;
}

function url(path, ventanatitle) {
    url(path, ventanatitle, 200, 200, 1, 1);
}
function url(path, ventanatitle, top, left, width, height) {
    var options = 'top=' + top.toString();
    options = options + ',left=' + left.toString();
    options = options + ',width=' + width.toString();
    options = options + ',height=' + height.toString() + ',maximize=no,status=no,resizable=no,scrollbars=yes';
    hidden = window.open(path, 'GARIBAY', options);
}

function SelectOnChange(combo1, combo2) {
    var ctrl1 = getCtrlByID(combo1);
    var ctrl2 = getCtrlByID(combo2);

    if (ctrl1 && ctrl2) {
        ctrl2.selectedIndex = ctrl1.selectedIndex;
    }
}


function showHideDivTipoDeCambio(divID, cuentaUnoID, cuentaDosID) {
    var ctrl1 = getCtrlByID(cuentaUnoID);
    var ctrl2 = getCtrlByID(cuentaDosID);
    var div = getCtrlByID(divID);

    var sTipo1 = ctrl1.options[ctrl1.selectedIndex].text;
    var sTipo2 = ctrl2.options[ctrl2.selectedIndex].text;

    if ((sTipo1.indexOf('PESOS') > -1 && (sTipo2.indexOf('DOLARES') > -1)) || ((sTipo1.indexOf('DOLARES') > -1 && (sTipo2.indexOf('PESOS') > -1)))) {
        setVisibility(divID, true);
    }
    else {
        setVisibility(divID, false);
    }
            
}

function showDivOnListContains(listid, valueToCheck, divID) {
    var myListBox = listid;  //document.GetElementById(listid);
    var selVal = myListBox.options[myListBox.selectedIndex].text;
    if (selVal.indexOf(valueToCheck) > -1 ) {
        setVisibility(divID, true);
    } else {
        setVisibility(divID, false);
    }

}
function showDivOnListContainsExc(listid, valueToCheck, divID) {
    var myListBox = listid;  //document.GetElementById(listid);
    var selVal = myListBox.options[myListBox.selectedIndex].text;
    if (selVal.indexOf(valueToCheck) > -1) {
        setVisibility(divID, false);
    } else {
        setVisibility(divID, true);
    }

}

function showDivOnListNOTContains(listid, valueToCheck, divID) {
    var myListBox = listid;  //document.GetElementById(listid);
    var selVal = myListBox.options[myListBox.selectedIndex].text;
    if (selVal.indexOf(valueToCheck) <= -1) {
        setVisibility(divID, true);
    } else {
        setVisibility(divID, false);
    }

}

function EnableDisableAbonosCargos(conceptosID, cmbAbonoCargo) {
    var ctrlConceptos = getCtrlByID(conceptosID);
    var ctrlAbonosCargos = getCtrlByID(cmbAbonoCargo);
    var sSelected = ctrlConceptos.options[ctrlConceptos.selectedIndex].text;
    var iIndex = -1;
    ctrlAbonosCargos.selectedIndex = 0;
    ctrlAbonosCargos.disabled = false;
    switch (sSelected) {
    //abonos
        case 'ABONO BANCO':
        case 'DEPOSITO BANCARIO':
            ctrlAbonosCargos.selectedIndex = 1;
            ctrlAbonosCargos.disabled = true;
            break;
    	
        case 'CARGO BANCO':
        case 'RETIRO DE EFECTIVO':
        case 'CHEQUE':
            ctrlAbonosCargos.selectedIndex = 0;
            ctrlAbonosCargos.disabled = true;
            break;
        //cualquier cargo/abono
    
        case 'OTRO':
        case 'TRANSFERENCIA INTERBANCARIA':
        default:
    }
}

function DisableButton(btnID) {
    var btn = getCtrlByID(btnID);
    if (navigator.appName != "Microsoft Internet Explorer") {
        //btn.disabled = "disabled";
    }
    else {
        //btn.disabled = true;
    }
    if (btn.value == "Procesando...") {
        alert("Deja de dar doble click sobre el boton!!!\nEl sistema esta procesando la peticion...");
        return false;
    }
    btn.value = "Procesando...";
    return true;
}

//Query existencias page, to return the current existencias.
function QueryExistencias(ctrlID, cmbCiclo, cmbProd, cmbBod) {
    var url = 'frmQueryExistencias.aspx?cicloID=';
              
    var ctrl = getCtrlByID(cmbCiclo);
    
    url = url + ctrl.options[ctrl.selectedIndex].value;
    ctrl = getCtrlByID(cmbProd);
    url = url + '&prodID=';
    url = url + ctrl.options[ctrl.selectedIndex].value;
    ctrl = getCtrlByID(cmbBod);
    url = url + '&bodID=';
    url = url + ctrl.options[ctrl.selectedIndex].value;
    var control = getCtrlByID(ctrlID);
    control.value = ' ';
    new Ajax.Request(url,
    { method: 'get',
        onComplete: function ( response ) {
            var control = getCtrlByID(ctrlID);
            control.value=response.responseText;
            
        }
    });
}

function QueryPrecios(cmbProd,txtPrecio,chkCredito){
    var url = 'frmQueryPrecio.aspx?prodID=';
    var ctrl = getCtrlByID(cmbProd);
    url = url + ctrl.options[ctrl.selectedIndex].value;
    var checkbox=getCtrlByID(chkCredito);
    if(checkbox.checked){
    url = url + '&precioID=2';
    }else{
    url = url + '&precioID=1';
    }
    
    
    
    new Ajax.Request(url,
    { method: 'get',
        onComplete: function ( response ) {
            var control = getCtrlByID(txtPrecio);
            control.value=response.responseText;
            
        }
    });
}



function loadClienteData(ctrlClienteID, dom, city, state, rfc, phone, col, cp) {
    var url = 'frmQueryClienteData.aspx?clienteID=';
    var ctrl = getCtrlByID(ctrlClienteID);
    url = url + ctrl.options[ctrl.selectedIndex].value;
    var control = getCtrlByID(dom);
    control.value = '';
    var control = getCtrlByID(city);
    control.value = '';
    var control = getCtrlByID(state);
    control.value = '';
    var control = getCtrlByID(rfc);
    control.value = '';
    var control = getCtrlByID(phone);
    control.value = '';
    var control = getCtrlByID(col);
    control.value = '';
    var control = getCtrlByID(cp);
    control.value = '';
    new Ajax.Request(url,
    { method: 'get',
        onComplete: function(response) {

            var control = getCtrlByID(dom);
            if (response.responseXML.documentElement.getElementsByTagName('address')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('address')[0].firstChild.data;
            }
            
            var control = getCtrlByID(city);
            if (response.responseXML.documentElement.getElementsByTagName('city')[0].firstChild != null) {
                control.value = response.responseXML.documentElement.getElementsByTagName('city')[0].firstChild.data;
            }

            var control = getCtrlByID(state);
            if (response.responseXML.documentElement.getElementsByTagName('state')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('state')[0].firstChild.data;
            }
            var control = getCtrlByID(rfc);
            if (response.responseXML.documentElement.getElementsByTagName('rfc')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('rfc')[0].firstChild.data;
            }

            var control = getCtrlByID(phone);
            if (response.responseXML.documentElement.getElementsByTagName('phone')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('phone')[0].firstChild.data;
            }

            var control = getCtrlByID(col);
            if (response.responseXML.documentElement.getElementsByTagName('col')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('col')[0].firstChild.data;
            }

            var control = getCtrlByID(cp);
            if (response.responseXML.documentElement.getElementsByTagName('cp')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('cp')[0].firstChild.data;
            }
            
        }
    })
}

   
function popupCenteredFACBOL(url,width, height) {
    var left = (screen.width - width) / 2;
    var top = (screen.height - height) / 2;
    var params = 'width=' + width + ', height=' + height;
    params += ', top=' + top + ', left=' + left;
    params += ', directories=no';
    params += ', location=no';
    params += ', menubar=no';
    params += ', resizable=yes';
    params += ', scrollbars=yes';
    params += ', status=no';
    params += ', toolbar=no';
    newwin = window.open(url, 'BOLETASFAC', params);
    if (window.focus) { newwin.focus() }
    return false;
}

function iftextboxNotNullChangeDDl(textBoxID, ddlID, cmbAbonoCargo, texto) {
    var ctrlText,ddl, textemp;
    ctrlText = getCtrlByID(textBoxID);
    ddl= getCtrlByID(ddlID);
   
    if (getFloat(ctrlText.value) > 0) {
     var i;
     for(i=0; i<ddl.length; i++){
       if(ddl.options[i].text == texto){
           ddl.options[i].selected = true;
           var ctrlAbonosCargos = getCtrlByID(cmbAbonoCargo);
           ctrlAbonosCargos.selectedIndex = 0;
           ctrlAbonosCargos.disabled = true;
       }
       else {
           ddl.options[i].selected = false;
       }
     }
    }
}



function validarExistencia(txtCantidad, txtExistencia,txtPrecio,txtImporte) {
    var ctrlCantidad, ctrlExistencia, lbl;
    ctrlCantidad = getCtrlByID(txtCantidad);
    ctrlExistencia= getCtrlByID(txtExistencia);

   var valor=ctrlExistencia.value.replace(/,/g,'');
   
   if (getFloat(ctrlCantidad.value) <= parseFloat(valor)) 
     {
   
        if (ctrlCantidad.value < 0) {
            ctrlCantidad.value = 0;
           
        }
        
    }
    else
    {
    ctrlCantidad.value=0;
     
   }
   
    mulTextBoxesNotNeg(txtCantidad, txtPrecio,txtImporte);
}

function validarExistencia2(txtCantidad, txtExistencia) {
    var ctrlCantidad, ctrlExistencia, lbl;
    ctrlCantidad = getCtrlByID(txtCantidad);
    ctrlExistencia= getCtrlByID(txtExistencia);

   var valor=ctrlExistencia.value.replace(/,/g,'');
   
   if (getFloat(ctrlCantidad.value) <= parseFloat(valor)) 
     {
   
        if (ctrlCantidad.value < 0) {
            ctrlCantidad.value = 0;
           
        }
        
    }
    else
    {
    ctrlCantidad.value=0;
     
   }
   
   
}

function loadProducerData(ddlProductor, destino, domicilio, poblacion, estado, tel, cel, ife,chofer,transportista) {
    var url = 'frmQueryProductorData.aspx?ProductorID=';
    var ctrl = getCtrlByID(ddlProductor);
    url = url + ctrl.options[ctrl.selectedIndex].value;
    var control = getCtrlByID(destino);
    control.value = '';
    var control = getCtrlByID(domicilio);
    control.value = '';
    var control = getCtrlByID(poblacion);
    control.value = '';
    var control = getCtrlByID(estado);
    control.value = '';
    var control = getCtrlByID(tel);
    control.value = '';
    var control = getCtrlByID(cel);
    control.value = '';
    var control = getCtrlByID(ife);
    control.value = '';
    new Ajax.Request(url,
    { method: 'get',
        onComplete: function(response) {

            var control = getCtrlByID(destino);
            if (response.responseXML.documentElement.getElementsByTagName('destino')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('destino')[0].firstChild.data;
            }
            
            var control = getCtrlByID(domicilio);
            if (response.responseXML.documentElement.getElementsByTagName('domicilio')[0].firstChild != null) {
                control.value = response.responseXML.documentElement.getElementsByTagName('domicilio')[0].firstChild.data;
            }

            var control = getCtrlByID(poblacion);
            if (response.responseXML.documentElement.getElementsByTagName('poblacion')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('poblacion')[0].firstChild.data;
            }
            var control = getCtrlByID(estado);
            if (response.responseXML.documentElement.getElementsByTagName('estado')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('estado')[0].firstChild.data;
            }

            var control = getCtrlByID(tel);
            if (response.responseXML.documentElement.getElementsByTagName('telefono')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('telefono')[0].firstChild.data;
            }

            var control = getCtrlByID(cel);
            if (response.responseXML.documentElement.getElementsByTagName('celular')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('celular')[0].firstChild.data;
            }

            var control = getCtrlByID(ife);
            if (response.responseXML.documentElement.getElementsByTagName('IFE')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('IFE')[0].firstChild.data;
            }
            var control= getCtrlByID(chofer);
            if (response.responseXML.documentElement.getElementsByTagName('NOMBRE')[0].firstChild) {
                control.value = response.responseXML.documentElement.getElementsByTagName('NOMBRE')[0].firstChild.data;
                var control= getCtrlByID(transportista);
                control.value= response.responseXML.documentElement.getElementsByTagName('NOMBRE')[0].firstChild.data;


           }
            
            
        }
    })
}

